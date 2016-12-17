using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BluetoothAuscultation.Forms;
using BluetoothAuscultation.Devices;
using BluetoothAuscultation.Commucation;
using System.IO;
using Newtonsoft.Json;
using BluetoothAuscultation.Models;
using BluetoothAuscultation.Utilities;
using ProtocalData.Utilities;
using ProtocalData.Protocol;
using System.Data.SQLite;
using BluetoothAuscultation.SQLite;
using ProtocalData.Protocol.Derive;
using System.Diagnostics;
using System.Text;

namespace BluetoothAuscultation
{
    static class Program
    {
         
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Directory.Exists(@"Enclosure\Temp"))
            {
                foreach (var file in Directory.GetFiles(@"Enclosure\Temp"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {

                    }
                    
                }
            }
            bool isCreate;
            var mutex = new System.Threading.Mutex(true, "BluetoothAuscultation.exe",out isCreate);
//#if !DEBUG
            if (!isCreate)
            {
                MessageBox.Show("本程序已经在运行了，请不要重复运行！");
                return;
            }
//#endif

            Setting.isConnected = false;
            // #if !DEBUG
            try
            {
                var init = Mediator.remoteService.GetData(0);
                Setting.isConnected = true;
            }
            catch
            {

            }
//#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.ThreadException += Mediator.Application_ThreadException;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            var appLicensePath = Path.Combine(Application.StartupPath, "appLicense.txt");
            AuthorizationInfo authorizationInfo = null;
          
            if (File.Exists(appLicensePath))
            {
                var json = File.ReadAllText(appLicensePath);
                authorizationInfo = GetAuthorizationInfo(json);

                if (null == authorizationInfo)
                {
                    var mac = CommonUtil.GetMacAddressByNetworkInformation();
                    var regFrom = new FormReg(mac);
                    if (DialogResult.OK != regFrom.ShowDialog())
                    {
                        return;
                    }
                    MessageBox.Show("注册成功，请重启程序!");
                    return;
                    authorizationInfo = GetAuthorizationInfo(regFrom.License);
                    if (authorizationInfo == null)
                    {
                        MessageBox.Show("再次验证,服务器验证异常:"+regFrom.License);
                        return;
                    }
                }
                else
                {
                    if (Setting.isConnected)
                    {
                        var code = Mediator.remoteService.AccountCredentials(authorizationInfo.MachineCode, authorizationInfo.AuthorizationNum);
                        var RegistCode = Newtonsoft.Json.JsonConvert.DeserializeObject<RegistCode>(code);
                        var path = Path.Combine(Application.StartupPath, "applicense.txt");
                        System.IO.File.WriteAllText(path, RegistCode.License);
                        if (!RegistCode.isLegal)
                        {
                            return;
                        }
                       
                        authorizationInfo = GetAuthorizationInfo(RegistCode.License);
                        if (authorizationInfo == null)
                        {
                            MessageBox.Show("刷新验证异常" +Setting.GetAuthorizationError( RegistCode.License));
                            return;
                        }
                    }
                    if (authorizationInfo == null)
                    {
                        MessageBox.Show("本地验证异常:" + json);
                        return;
                    }
                }
            }
            else
            {
                var mac = CommonUtil.GetMacAddressByNetworkInformation();
                var regFrom = new FormReg(mac);
                if (DialogResult.OK != regFrom.ShowDialog())
                {
                    return;
                }
                MessageBox.Show("注册成功，请重启程序!");
                return;
                authorizationInfo = GetAuthorizationInfo(regFrom.License);
                if (authorizationInfo == null)
                {
                    MessageBox.Show("首次验证,服务器验证异常");
                    return;
                }
            }
            Setting.authorizationInfo = authorizationInfo;
            //var formLogin = new FormLogin();
            //formLogin.StartPosition = FormStartPosition.CenterScreen;
            //formLogin.SN = authorizationInfo.AuthorizationNum;
            //if(DialogResult.OK!=formLogin.ShowDialog())
            //{
            //    return;
            //}
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            var formMain = new FormMain();
            formMain.WindowState = FormWindowState.Maximized;
            Mediator.InitMediator(formMain);
             Application.Run(formMain);
             //Application.Run(new FormEx());
            mutex.Close();
            Mediator.remoteService.Close();
            try
            {   //程序退出时，关闭连接的蓝牙听诊器
                foreach (var stethoscope in StethoscopeManager.StethoscopeList)
                {
                    if (stethoscope != null && stethoscope.IsConnected)
                    {
                        stethoscope.Disconnect();
                    }
                }
            }
            catch { }
            try
            {  //防止用户更改时间
                if (authorizationInfo != null)
                {
                    authorizationInfo.LastUseTime =
                    authorizationInfo.LastUseTime.AddSeconds(formMain.Times) >= DateTime.Now ?
                    authorizationInfo.LastUseTime.AddSeconds(formMain.Times) : DateTime.Now;
                    WriteAuth(appLicensePath, authorizationInfo);

                }
            }
            catch { }
          
           
            
        }

      
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
          
        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var title = "在" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "时刻\r\n捕捉到线程异常\r\n  ";
            WriteError(title + e.Exception.ToString());
        }
        static void WriteError(string errMsg)
        {
            try
            {
                var dir = Path.Combine(Application.StartupPath, "Errors" + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\r\n");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var filePath = Path.Combine(dir, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                File.AppendAllText(filePath, errMsg, Encoding.UTF8);
            }

            catch { }
           
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var title = "在"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"时刻\r\n捕捉到程序域范围内异常\r\n  ";
            WriteError(title+e.ExceptionObject.ToString());

            //MessageBox.Show();
        }
        static AuthorizationInfo GetAuthorizationInfo(string Json)
        {
                var authorizationInfo = JsonConvert.DeserializeObject<AuthorizationInfo>(Json);
                if (authorizationInfo == null) return null;// throw new Exception("指定的文本不是可识别格式");
                var mac = CommonUtil.GetMacAddressByNetworkInformation();
                if (authorizationInfo.MachineCode != mac) return null;// throw new Exception(string.Format("Mac地址不匹配,源：{0},本机:{1}", authorizationInfo.MachineCode,mac));
                var content = authorizationInfo.MachineCode + authorizationInfo.AuthorizationNum + authorizationInfo.AuthStartTime.ToString("yyyyMMddhhmmss") + authorizationInfo.LastUseTime.ToString("yyyyMMddhhmmss") + authorizationInfo.AuthDays + "wanjian";
                var md5 = CommonUtil.GetMD5(content);
                if (authorizationInfo.HashCode != md5) return null;//throw new Exception(string.Format("哈希码,源：{0},本机:{1}", authorizationInfo.HashCode, md5));
                if (authorizationInfo.LastUseTime > DateTime.Now) return null;// throw new Exception("最后使用时间大于当前时间");
                if (authorizationInfo.AuthStartTime > DateTime.Now) return null;// throw new Exception("激活时间大于当前时间");
                var span=DateTime.Now - authorizationInfo.AuthStartTime;
                if (span.Days * 24 + span.Hours > authorizationInfo.AuthDays * 24) return null;//throw new Exception("注册码已过期");
                return authorizationInfo;
        }
        static void WriteAuth(string filePath, AuthorizationInfo authorizationInfo)
        {
            var content = authorizationInfo.MachineCode + authorizationInfo.AuthorizationNum + authorizationInfo.AuthStartTime.ToString("yyyyMMddhhmmss") + authorizationInfo.LastUseTime.ToString("yyyyMMddhhmmss") + authorizationInfo.AuthDays + "wanjian";
            var md5 = CommonUtil.GetMD5(content);
            authorizationInfo.HashCode = md5;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(authorizationInfo);
            System.IO.File.WriteAllText(filePath, json);
        }
       

         

       
    }
}

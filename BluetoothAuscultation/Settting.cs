using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BluetoothAuscultation.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using BluetoothAuscultation.Utilities;
using Newtonsoft.Json;
using System.Xml;
using System.Configuration;

namespace BluetoothAuscultation
{
    public static class Setting
    {
        public static string[] FilePaths
        {
            get
            {
                return Directory.GetFiles(@"Image\Part");
            }
        }
        public static string serverUrl
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["serverUrl"];
            }
        }
        public static string localData
        {
            get
            {
               // return System.Configuration.ConfigurationManager.AppSettings["localData"];
                return Application.StartupPath;
            }
        }
        public static string localSqliteConnectstring
        {
            get
            {
                //return System.Configuration.ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
                return string.Format(@"Data Source={0}\mydb.db;Version=3;", localData);
            }
        }
        public static string virtualMac
        {
            get
            {
                 return System.Configuration.ConfigurationManager.AppSettings["virtualMac"];
            }
        }
       
        public static Color themeColor
        {
            get
            {

                string colorStr = System.Configuration.ConfigurationManager.AppSettings["themeColor"];
                var argb = colorStr.Split(',');
                if (argb.Length != 3) return Color.White;
                int[] arr = new int[3];
                for (int i = 0; i < argb.Length; i++)
                {
                    if (!int.TryParse(argb[i], out arr[i]))
                        return Color.White;
                }
                return Color.FromArgb(arr[0], arr[1], arr[2]);
            }
            set
            {
                string color = string.Join(",", new string[] { value.R + "", value.G + "", value.B + "" });
                Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //根据Key读取<add>元素的Value
                string name = config.AppSettings.Settings["themeColor"].Value;
                //写入<add>元素的Value
                config.AppSettings.Settings["themeColor"].Value = color;
                ////增加<add>元素
                //config.AppSettings.Settings.Add("url", "http://www.xieyc.com");
                ////删除<add>元素
                //config.AppSettings.Settings.Remove("name");
                //一定要记得保存，写不带参数的config.Save()也可以
                config.Save(ConfigurationSaveMode.Modified);
                //刷新，否则程序读取的还是之前的值（可能已装入内存）
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
        }
        private static byte GetAvg(byte b, byte g, byte r)
        {
            return (byte)((r + g + b) / 3);
        }
        public static Bitmap GetDarkImage(Image headImage)
        {
            Bitmap b = new Bitmap(headImage);
            Bitmap bmp = b.Clone(new Rectangle(0, 0, headImage.Width, headImage.Height), PixelFormat.Format24bppRgb);
            b.Dispose();
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            byte[] byColorInfo = new byte[bmp.Height * bmpData.Stride];
            Marshal.Copy(bmpData.Scan0, byColorInfo, 0, byColorInfo.Length);
            for (int x = 0, xLen = bmp.Width; x < xLen; x++)
            {
                for (int y = 0, yLen = bmp.Height; y < yLen; y++)
                {
                    byColorInfo[y * bmpData.Stride + x * 3] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 1] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 2] =
                        GetAvg(
                        byColorInfo[y * bmpData.Stride + x * 3],
                        byColorInfo[y * bmpData.Stride + x * 3 + 1],
                        byColorInfo[y * bmpData.Stride + x * 3 + 2]);
                }
            }
            Marshal.Copy(byColorInfo, 0, bmpData.Scan0, byColorInfo.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
        public static AuthorizationInfo authorizationInfo { get; set; }
     
      
        public static StetInfo GetStetInfoByStetName(string StetName)
        {
            string sql = "select * from StetInfo where StetName={0} and MAC={1}";
            var dt = Mediator.sqliteHelper.ExecuteDatatable(sql, StetName, Setting.authorizationInfo.MachineCode);
            StetInfo stetInfo = new StetInfo() { StetName=StetName
            ,SN=Setting.authorizationInfo.AuthorizationNum,
            MAC=Setting.authorizationInfo.MachineCode,
            PCName=CommonUtil.GetMachineName(),
            StetType=2
            };
            if (dt.Rows.Count > 0)
            {
                stetInfo.StetChineseName = dt.Rows[0]["StetChineseName"].ToString();
                stetInfo.Owner = dt.Rows[0]["Owner"].ToString();
                stetInfo.FuncDescript = dt.Rows[0]["FuncDescript"].ToString();
                stetInfo.ReMark = dt.Rows[0]["ReMark"].ToString();
                stetInfo.RecordTime = DateTime.Parse(dt.Rows[0]["RecordTime"].ToString());
                stetInfo.StetType = int.Parse(dt.Rows[0]["StetType"].ToString());
            }
            return stetInfo;
        }

        public static Image RootImage = new Bitmap("Image\\pc.png");
        public static Image OnlineImage = new Bitmap("Image\\tzq.png");
        public static Image OfflineImage = GetDarkImage(OnlineImage);

        public static Image ImageEdit = new Bitmap("Image\\edit.png");
        public static Image ImageShare = new Bitmap("Image\\share.png");
        public static Image ImagePlay = new Bitmap("Image\\play.png");
        public static Image ImageDelete = new Bitmap("Image\\delete.png");
        //public static Image ImageUpload = new Bitmap("Image\\upload.png");
        public static Image ImageDownload = new Bitmap("Image\\download.png");
        public static Image ImageDetail = new Bitmap("Image\\xq.png");
        public static Image ImageRecord = new Bitmap("Image\\ly.png");
        public static Image ImageJCBG= new Bitmap("Image\\jcbg.png");

        public static Image ImageTZPZ = new Bitmap("Image\\TZPZ.png");
        public static Image ImageTZLY = new Bitmap("Image\\TZLY.png");
        public static Image ImageTZJX = new Bitmap("Image\\TZJX.png");
        public static Image ImageYDTZ = new Bitmap("Image\\YDTZ.png");
        public static Image ImageYCJX = new Bitmap("Image\\YCJX.png");
       // public static Image ImageFJ = new Bitmap("Image\\fj.png");


        static bool _isConnected = false;
        public static bool isConnected
        {
            get
            {
                if (!_isConnected)
                {
                           Mediator.ShowMsg("与服务器重连中...");
                           Mediator.SuperSocket.OpenSocket(Setting.authorizationInfo.AuthorizationNum, Setting.authorizationInfo.MachineCode);
                }
                return _isConnected;
            }

            set
            {
                _isConnected = value;
            }
        }
        public static string GetAuthorizationError(string Json)
        {
            var authorizationInfo = JsonConvert.DeserializeObject<AuthorizationInfo>(Json);
            if (authorizationInfo == null) return  ("指定的文本不是可识别格式");
            var mac = CommonUtil.GetMacAddressByNetworkInformation();
            if (authorizationInfo.MachineCode != mac) return  (string.Format("Mac地址不匹配,源：{0},本机:{1}", authorizationInfo.MachineCode,mac));
            var content = authorizationInfo.MachineCode + authorizationInfo.AuthorizationNum + authorizationInfo.AuthStartTime.ToString("yyyyMMddhhmmss") + authorizationInfo.LastUseTime.ToString("yyyyMMddhhmmss") + authorizationInfo.AuthDays + "wanjian";
            var md5 = CommonUtil.GetMD5(content);
            if (authorizationInfo.HashCode != md5) return  (string.Format("哈希码,源：{0},本机:{1}", authorizationInfo.HashCode, md5));
            if (authorizationInfo.LastUseTime > DateTime.Now) return  ("最后使用时间大于当前时间");
            if (authorizationInfo.AuthStartTime > DateTime.Now) return  ("激活时间大于当前时间");
            var span = DateTime.Now - authorizationInfo.AuthStartTime;
            if (span.Days * 24 + span.Hours > authorizationInfo.AuthDays * 24) return ("注册码已过期");
            return string.Empty;
        }
        public static float dgvFontSize
        {
            get
            {
                return 10.5f;
            }
        }
        public static string dgvFontFamliy
        {
            get
            {
                return "微软雅黑";
            }
        }
        public static string GetFilePath(string StetName, DateTime RecordTime, string Guid)
        {
            return   StetName + "\\" + RecordTime.Year + "\\" + RecordTime.Month + "\\" + RecordTime.Day + "\\" + Guid + ".MP3";
        }
        public static string GetFilePath2(  DateTime RecordTime, string Guid)
        {
            return   RecordTime.Year + "\\" + RecordTime.Month + "\\" + RecordTime.Day + "\\" + Guid + ".MP3";
        }
        public static string Version
        {
            get
            {
                return  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        public static string GetPatientNameByType(int type)
        {
            switch (type)
            {
                case 1:
                    return "儿科";
                case 2:
                    return "成人";
            }
            return "未知患者类型";
        }
        public static int GetPatientTypeByName(string name)
        {
            switch (name)
            {
                case "儿科":
                    return 1;
                case "成人":
                    return 2;
            }
            return 0;
        }
        public static string GetPatientAgeByGUID(string guid)
        {
            string sql = "select PatientAge from PatientInfo where PatientGUID={0}";
            var obj = Mediator.sqliteHelper.ExecuteScalar(sql, guid);
            return obj+"";
        }
        public static string GetPatientSexByGUID(string guid)
        {
            string sql = "select PatientSex from PatientInfo where PatientGUID={0}";
            var obj = Mediator.sqliteHelper.ExecuteScalar(sql, guid);
            return obj + "";
        }
        public static Image getImage(string filePath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open, FileAccess.Read);
            int byteLength = (int)fs.Length;
            byte[] fileBytes = new byte[byteLength];
            fs.Read(fileBytes, 0, byteLength);

            //文件流关閉,文件解除锁定
            fs.Close();
            Image image = Image.FromStream(new MemoryStream(fileBytes));
            return image;
        }
       
    }
}

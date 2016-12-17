using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using BluetoothAuscultation.Utilities;
using Newtonsoft.Json;
using BluetoothAuscultation.Models;
using ProtocalData.Utilities;
using ProtocalData.Protocol;
using ProtocalData.Protocol.Derive;
using System.IO;
using BluetoothAuscultation.Devices;
using WeifenLuo.WinFormsUI.Docking;
using System.Data;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Channels;
 
 
namespace BluetoothAuscultation.Forms
{
    public partial class FormMain : Form, IShow
        //, IHandleMessage<RegistCode>
         , IHandleMessage<RequestGetDeviceInfoCode>
         , IHandleMessage<RequestRemoteAuscultateCode>
      , IHandleMessage<GetDownedAudioCode>
    {

        public FormMain()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMain_Load);
            
        }
        public void HandleMessage(RegistCode message)
        {
            Invoke(new MethodInvoker(() =>
            {
                var path = Path.Combine(Application.StartupPath, "applicense.txt");
                System.IO.File.WriteAllText(path, message.License);
                if (!message.isLegal)
                {
                    MessageBox.Show(string.Format("注册码 {0} 过期、无效或者与您的机器不匹配", message.SN));
                }

            }));
        }
        void InitSocket()
        {
            Mediator.SuperSocket.OpenSocket(Setting.authorizationInfo.AuthorizationNum,Setting.authorizationInfo.MachineCode);
            Mediator.SuperSocket.Opened += new EventHandler(SuperSocket_Opened);
            Mediator.SuperSocket.Closed += new EventHandler(SuperSocket_Closed);
            Mediator.SuperSocket.DataReceived += new EventHandler<WebSocket4Net.DataReceivedEventArgs>(SuperSocket_DataReceived);
            Mediator.SuperSocket.MessageReceived += new EventHandler<WebSocket4Net.MessageReceivedEventArgs>(SuperSocket_MessageReceived);
            Mediator.SuperSocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(SuperSocket_Error);
        }
      
          void SuperSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Mediator.ShowMsg("套接字通信发生异常:" + e.Exception.ToString());

        }

          void SuperSocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            Mediator.ShowMsg("系统消息:" + e.Message);
        }

          void SuperSocket_DataReceived(object sender, WebSocket4Net.DataReceivedEventArgs e)
          {
              try
              {
              var code = SerializaHelper.DeSerialize<CodeBase>(e.Data);
              if (code == null) Mediator.ShowMsg("无法处理的未知消息类型");
              //消息分发处理
              for (int i = 0; i < Application.OpenForms.Count; i++)
              {
                  var from = Application.OpenForms[i];
                  var interFaces = from.GetType().GetInterfaces().Where(iface => iface.Name == "IHandleMessage`1");
                  // var interFace = from.GetType().GetInterface("IHandleMessage`1");
                  foreach (var interFace in interFaces)
                  {
                      var codeType = code.GetType();
                      var argTypes = interFace.GetGenericArguments();
                      if (argTypes != null && argTypes.Length == 1 && argTypes[0].Name == codeType.Name)
                      {
                          System.Reflection.MethodInfo methodInfo = interFace.GetMethod("HandleMessage");
                          var result = methodInfo.Invoke(from, new object[] { code });

                      }
                  }
              } }
              catch
              {

              }
          }

          void SuperSocket_Closed(object sender, EventArgs e)
        {
            Setting.isConnected = false;
            Mediator.ShowMsg("网络异常,关闭套接字...");
        }
          void SuperSocket_Opened(object sender, EventArgs e)
        {
            Setting.isConnected = true;
            //Mediator.ShowMsg("连接服务器成功...");
        }
        void UpLoadStetInfo()
        {
            foreach (var item in StethoscopeManager.StethoscopeList)
            {
                var stetInfo = Setting.GetStetInfoByStetName(item.Name);
                var code = Newtonsoft.Json.JsonConvert.SerializeObject(stetInfo);
                using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                {
                    MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    Mediator.remoteService.UpdateStetInfo(code);
                }
            }
        }
        void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = "奔达医学听诊软件";
            btnTZLY.Image = Setting.ImageTZLY;
            btnTZPZ.Image = Setting.ImageTZPZ;
            btnTZJX.Image = Setting.ImageTZJX;
            btnYDTZ.Image = Setting.ImageYDTZ;
            btnYCJX.Image = Setting.ImageYCJX;

            听诊录音ToolStripMenuItem.Checked =
        btnTZLY.Visible;
            听诊教学ToolStripMenuItem.Checked =
            btnTZJX.Visible;
            云端听诊ToolStripMenuItem.Checked =
            btnYDTZ.Visible;
            远程教学ToolStripMenuItem.Checked =
            btnYCJX.Visible;
       //     信息输出toolStripMenuItem.Checked =
       //!this.dockPanel1.Contents.Any(t => t.GetType().Name == typeof(FormOutPut).Name);

            FormMenu formMenu = new FormMenu();
            formMenu.Show(this.dockPanel1);
            formMenu.DockTo(this.dockPanel1, DockStyle.Fill | DockStyle.Left);

            FormOutPut formOutPut = new FormOutPut();
            formOutPut.Show(this.dockPanel1);
            formOutPut.DockTo(this.dockPanel1, DockStyle.Bottom);

           // this.Text += ",本机MAC:" + Setting.authorizationInfo.MachineCode;

            timer1.Start();
            if (Setting.isConnected)
            {
              
                InitSocket();
              //  LoadMenu();
                LoadStetInfo();
            }
            Thread thread = new Thread(LoadAudioFile);
            thread.Start("DevicesData\\AudioFiles");
        }
        void LoadAudioFile(object dirPath)
        {
            try
            {
                var dir = dirPath as string;
                if (!Directory.Exists(dir)) return;
                DateTime dt = DateTime.MinValue;
                if (File.Exists(Path.Combine(dir, "flag.txt")))
                {
                    dt = DateTime.Parse(File.ReadAllText(Path.Combine(dir, "flag.txt")));
                }
                var files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
                var needUploadFiles = files.Where(f => new FileInfo(f).CreationTime > dt).ToArray();
                File.WriteAllText(Path.Combine(dir, "flag.txt"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                {
                    MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    var root = Mediator.remoteService.GetRoot();
                    var remoteFile = Path.Combine(root, "AllFiles\\" + CommonUtil.GetMachineName());
                    foreach (var localFile in needUploadFiles)
                    {
                        var remoteFilePath = Path.Combine(remoteFile, Path.GetFileName(localFile));
                        if (Mediator.remoteService.isExistFile(remoteFilePath))
                        {
                            Mediator.remoteService.DeleteFile(remoteFilePath);
                        }
                        using (var stream = new FileStream(localFile, FileMode.OpenOrCreate, FileAccess.Read))
                        {
                            while (true)
                            {
                                var readBytes = new byte[1024 * 24];
                                var readed = stream.Read(readBytes, 0, readBytes.Length);
                                if (readed <= 0) break;
                                Mediator.remoteService.UpLoadFile(remoteFilePath, stream.Position - readed, readBytes);
                            }
                            stream.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            { 
                var msg=ex.ToString();
            }
        }
        /// <summary>
        /// 同步配置的听诊配置信息,不存在就下载，存在就上传
        /// </summary>
        void LoadStetInfo()
        {
            string sql = "select count(*) from StetInfo";
            var count = int.Parse(Mediator.sqliteHelper.ExecuteScalar(sql).ToString());
            if (count == 0)
            {
                string sqlQuery = " select * from StethoscopeManager where MAC={0} and IfDel=0";
                using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                {
                    MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    var ds = Mediator.remoteService.ExecuteDataset(sqlQuery, new string[] { Setting.authorizationInfo.MachineCode });
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                       var  stetName=row["StetName"]+"";
                       var  stetChineseName=row["StetChineseName"]+"";
                       var  owner=row["Owner"]+"";
                       var  funcDescript=row["FuncDescript"]+"";
                       var  reMark=row["ReMark"]+"";
                       string sqlUpdate = @"update StetInfo set PCName={0},StetChineseName={1},Owner={2},FuncDescript={3},
                       ReMark={4} where StetName={5} ";
                       string sqlInsert = @"insert into StetInfo(StetName,SN,MAC,PCName,StetChineseName,Owner,FuncDescript,ReMark,RecordTime)
                        select {0},{1},{2},{3},{4},{5},{6},{7},{8}";
                       if (int.Parse(Mediator.sqliteHelper.ExecuteScalar("select count(*) from StetInfo where StetName={0}", stetName).ToString()) > 0)
                       {
                           var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlUpdate, CommonUtil.GetMachineName(), stetChineseName
                         , owner, funcDescript, reMark, stetName);
                       }
                       else
                       {
                            var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsert,stetName,Setting.authorizationInfo.AuthorizationNum,Setting.authorizationInfo.MachineCode,CommonUtil.GetMachineName()
                                ,stetChineseName,owner,funcDescript,reMark,DateTime.Now);
                       }
                    }
                }
            }
            else
            {
                UpLoadStetInfo();
            }
        }
        

        public string[] GetInfo()
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            foreach (var item in this.dockPanel1.Contents)
            {
                list.Add(item.GetType().Name);
            }
            return list.ToArray();
        }
        public void DockShow(DockContent form, DockStyle dockStyle)
        {
            form.Show(this.dockPanel1);
            form.DockTo(this.dockPanel1, dockStyle);
        }
        public void OpenWindow(string formName, params string[] args)
        {
            switch (formName)
            {
                case "听诊录音":
                    {
                        if (!Mediator.GetInfo().Contains(typeof(FormEducation).Name))
                        {
                            FormEducation formEducation = new FormEducation(args[0]);
                            Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
                        }
                        else
                        {
                            foreach (var item in this.dockPanel1.Contents)
                            {
                                if (item.GetType().Name == typeof(FormEducation).Name)
                                {
                                    item.DockHandler.Activate();
                                    break;
                                }
                            }

                        }
                    }
                    break;
                case "云端听诊":
                    {
                        if (!Mediator.GetInfo().Contains(typeof(FormCloud).Name))
                        {
                            FormCloud formEducation = new FormCloud();
                            Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
                        }
                        else
                        {
                            foreach (var item in this.dockPanel1.Contents)
                            {
                                if (item.GetType().Name == typeof(FormCloud).Name)
                                {
                                    item.DockHandler.Activate();
                                    break;
                                }
                            }

                        }
                    }
                    break;
                case "听诊教学":
                    {
                        if (!Mediator.GetInfo().Contains(typeof(FormTeaching).Name))
                        {
                            FormTeaching formEducation = new FormTeaching();
                            Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
                        }
                        else
                        {
                            foreach (var item in this.dockPanel1.Contents)
                            {
                                if (item.GetType().Name == typeof(FormTeaching).Name)
                                {
                                    item.DockHandler.Activate();
                                    break;
                                }
                            }

                        }
                    }
                    break;
                case "远程教学":
                    {
                        if (!Mediator.GetInfo().Contains(typeof(FormRemoteAuscultation).Name))
                        {
                            FormRemoteAuscultation formRemoteAuscultation = new FormRemoteAuscultation();
                            Mediator.Show(formRemoteAuscultation, DockStyle.Fill | DockStyle.Left);
                        }
                        else
                        {
                            foreach (var item in this.dockPanel1.Contents)
                            {
                                if (item.GetType().Name == typeof(FormRemoteAuscultation).Name)
                                {
                                    item.DockHandler.Activate();
                                    break;
                                }
                            }

                        }
                    }
                    break;
                case "听诊器配置":
                    {
                        if (!Mediator.GetInfo().Contains(typeof(FormStethoscope).Name))
                        {
                            FormStethoscope formStethoscope = new FormStethoscope();
                            Mediator.Show(formStethoscope, DockStyle.Fill | DockStyle.Left);
                            formStethoscope.ConfigStet(args[0]);
                        }
                        else
                        {
                            foreach (var item in this.dockPanel1.Contents)
                            {
                                if (item.GetType().Name == typeof(FormStethoscope).Name)
                                {
                                    item.DockHandler.Activate();
                                    var formStethoscope =item as FormStethoscope;
                                    formStethoscope.ConfigStet(args[0]);

                                    break;
                                }
                            }

                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 听诊录音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTZLY_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormEducation).Name))
            {
                FormEducation formEducation = new FormEducation();
                Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormEducation).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }

        private void btnTZYPT_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormCloud).Name))
            {
                FormCloud formEducation = new FormCloud();
                Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormCloud).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }

        private void btnTZJX_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormTeaching).Name))
            {
                FormTeaching formEducation = new FormTeaching();
                Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormTeaching).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }

        private void btnYCTZ_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormRemoteAuscultation).Name))
            {
                FormRemoteAuscultation formEducation = new FormRemoteAuscultation();
                Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormRemoteAuscultation).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }

        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
            Application.Exit();
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  var r = Mediator.remoteService.GetData(8);
          //var b = Mediator.remoteService.isExistFile("dd");
          //  //FormEx formEx = new FormEx();
          //  //formEx.ShowDialog();
          //throw new Exception("异常测试");
        }

        private void 信息栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.dockPanel1.Contents)
            {
                if (item.GetType().Name == typeof(FormOutPut).Name)
                {
                    if (!item.DockHandler.Form.Visible)
                    {
                        item.DockHandler.Form.Visible = true;
                        item.DockHandler.Activate();
                    }
                    break;
                }
            }
        }

        private void 首页ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Mediator.GetInfo().Contains(typeof(FormIndex).Name))
            {
                FormIndex formIndex = new FormIndex();
                Mediator.Show(formIndex, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormIndex).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
            //foreach (var item in this.dockPanel1.Contents)
            //{
            //    if (item.GetType().Name == typeof(FormIndex).Name)
            //    {
            //        if (!item.DockHandler.Form.Visible)
            //        {
            //            item.DockHandler.Form.Visible = true;
            //            item.DockHandler.Activate();
            //        }
            //        break;
            //    }
            //}
        }
        private void 主菜单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!Mediator.GetInfo().Contains(typeof(FormStethoscope).Name))
            {
                FormStethoscope formEducation = new FormStethoscope();
                Mediator.Show(formEducation, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormStethoscope).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
            //foreach (var item in this.dockPanel1.Contents)
            //{
            //    if (item.GetType().Name == typeof(FormStethoscope).Name)
            //    {
            //        if (!item.DockHandler.Form.Visible)
            //        {
            //            item.DockHandler.Form.Visible = true;
            //            item.DockHandler.Activate();
            //        }
            //        break;
            //    }
            //}
        }
        public int Times = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Times++;
            lblCurTime.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //if (Mediator.SuperSocket.WebSocketState != WebSocket4Net.WebSocketState.Open)
            //{
            //    if (Times % 30 == 0)
            //    {
            //        Mediator.ShowMsg("与服务器重连中...");
            //        Mediator.SuperSocket.OpenSocket();
            //    }
            //}
        }
        
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
             //e.Cancel = true;
            timer1.Stop();
        }

        private void 注册机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 处理其他客户端的设备信息请求
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(RequestGetDeviceInfoCode message)
        {
            var resGetDeviceInfoCode = new ResGetDeviceInfoCode()
            {
                SrcMac=Setting.authorizationInfo.MachineCode,
                PCName=CommonUtil.GetMachineName(),
                ToSessionID = message.SessionID,
                StetNames = StethoscopeManager.StethoscopeList.Select(s => s.Name).ToArray(),
                isConnected = StethoscopeManager.StethoscopeList.Select(s => s.IsConnected).ToArray(),
                StetOwners = StethoscopeManager.StethoscopeList.Select(s => Setting.GetStetInfoByStetName(s.Name).Owner ?? string.Empty).ToArray(),
                StetChineseNames = StethoscopeManager.StethoscopeList.Select(s => Setting.GetStetInfoByStetName(s.Name).StetChineseName??string.Empty).ToArray()
            
            };
            var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resGetDeviceInfoCode);
            Mediator.SuperSocket.Send(bytes);
        }
        /// <summary>
        /// 接收到请求远程邀请
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(RequestRemoteAuscultateCode message)
        {
            Invoke(new MethodInvoker(() =>
                      {
                          
                          if (!Mediator.GetInfo().Contains(typeof(FormRemoteAuscultation).Name))
                          {
                              FormRemoteAuscultation formRemoteAuscultation = new FormRemoteAuscultation();
                              Mediator.Show(formRemoteAuscultation, DockStyle.Fill | DockStyle.Left);
                          }
                          else
                          {

                              foreach (var item in this.dockPanel1.Contents)
                              {
                                  if (item.GetType().Name == typeof(FormRemoteAuscultation).Name)
                                  {
                                      item.DockHandler.Activate();
                                      break;
                                  }
                              }



                          }
                      }));
        }


        public void HandleMessage(GetDownedAudioCode message)
        {
            //Invoke(new MethodInvoker(() =>
            //{
            //    {
            //        foreach (DataRow dr in message.DTable.Rows)
            //        {
            //            dgv_DownLoad.Rows.Add(
            //                dr["GUID"]
            //            , dr["StetSerialNumber"]
            //            , dr["StetName"]
            //            , dr["PatientID"]
            //            , dr["PatientName"]
            //            , dr["Posture"]
            //            , dr["Part"]
            //            , dr["RecordTime"]
            //            , dr["TakeTime"]
            //            , dr["Remark"]
            //            , dr["StetName"]
            //            , dr["ShareTime"]
            //            , dr["DownLoadTime"]
            //            );
            //        }
            //    }
            //}));
        }

        private void btnSkin_Click(object sender, EventArgs e)
        {
            ColorDialog cd=new ColorDialog();
            cd.Color = Setting.themeColor;
            cd.FullOpen = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.SetBackColor(cd.Color);
                    menuStrip1.BackColor = cd.Color;
                    toolStrip1.BackColor = cd.Color;
                }
            }
        }

        private void 关于公司ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.StartPosition = FormStartPosition.CenterScreen;
            formAbout.ShowDialog();
        }

        private void 皮肤设置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Setting.themeColor;
            cd.FullOpen = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.SetBackColor(cd.Color);
                    menuStrip1.BackColor = cd.Color;
                    toolStrip1.BackColor = cd.Color;
                }
            }
        }

        private void 听诊器配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormStethoscope).Name))
            {
                FormStethoscope formStethoscope = new FormStethoscope();
                Mediator.Show(formStethoscope, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormStethoscope).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }

        private void btnTZQPZ_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormStethoscope).Name))
            {
                FormStethoscope formStethoscope = new FormStethoscope();
                Mediator.Show(formStethoscope, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormStethoscope).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }

        private void 听诊录音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            听诊录音ToolStripMenuItem.Checked = !听诊录音ToolStripMenuItem.Checked;
            btnTZLY.Visible = 听诊录音ToolStripMenuItem.Checked;
        }

        private void 听诊教学ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            听诊教学ToolStripMenuItem.Checked = !听诊教学ToolStripMenuItem.Checked;
            btnTZJX.Visible = 听诊教学ToolStripMenuItem.Checked;
        }

        private void 云端听诊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            云端听诊ToolStripMenuItem.Checked = !云端听诊ToolStripMenuItem.Checked;
            btnYDTZ.Visible = 云端听诊ToolStripMenuItem.Checked;
        }

        private void 远程教学ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            远程教学ToolStripMenuItem.Checked = !远程教学ToolStripMenuItem.Checked;
            btnYCJX.Visible = 远程教学ToolStripMenuItem.Checked;
        }

        private void 信息输出toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormOutPut).Name))
            {
                FormOutPut formStethoscope = new FormOutPut();
                Mediator.Show(formStethoscope, DockStyle.Bottom);
            }
            else
            foreach (var item in this.dockPanel1.Contents)
            {
                if (item.GetType().Name == typeof(FormOutPut).Name)
                {
                    if (!item.DockHandler.Form.Visible)
                    {
                        item.DockHandler.Form.Visible = true;
                        item.DockHandler.Activate();
                    }
                    break;
                }
            }
            //信息输出toolStripMenuItem.Checked = !信息输出toolStripMenuItem.Checked;
        }

        private void 上传文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
            string filePath = @"C:\Users\wanjian\Downloads\haozip_5.9.1.10697_setup.exe";
            if (File.Exists(filePath))
            {
                Thread thrad = new Thread(() =>
                {
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        long offset = 0 * 1024L;
                        stream.Seek(offset, SeekOrigin.Begin);
                        while (true)
                        {
                            var readBytes = new byte[32 * 1024];
                            var readed = stream.Read(readBytes, 0, readBytes.Length);
                            if (readed <= 0) break;
                            Mediator.remoteService.UpLoadFile(@"C:\Users\admin\Desktop\WorkSpace\DB\haozip_5.9.1.10697_setup2.exe", 
                                stream.Position - readed, readBytes.Take(readed).ToArray());
                            Invoke(new MethodInvoker(() =>
                                {
                                    this.Text = stream.Position/1024 + "/" + stream.Length/1024;
                                }));
                        }
                        stream.Close();
                        MessageBox.Show("上传成功!");
                    }

                });
                thrad.Start();
            }
            
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            //if (!Mediator.GetInfo().Contains(typeof(FormAdmin).Name))
            //{
            //    FormAdmin formAdmin = new FormAdmin();
            //    Mediator.Show(formAdmin, DockStyle.Fill | DockStyle.Left);
            //}
            //else
            //{
            //    foreach (var item in this.dockPanel1.Contents)
            //    {
            //        if (item.GetType().Name == typeof(FormAdmin).Name)
            //        {
            //            item.DockHandler.Activate();
            //            break;
            //        }
            //    }

            //}
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mediator.ClearInfo();
        }

        private void 查看操作日志toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Mediator.GetInfo().Contains(typeof(FormLog).Name))
            {
                FormLog formLog = new FormLog();
                Mediator.Show(formLog, DockStyle.Fill | DockStyle.Left);
            }
            else
            {
                foreach (var item in this.dockPanel1.Contents)
                {
                    if (item.GetType().Name == typeof(FormLog).Name)
                    {
                        item.DockHandler.Activate();
                        break;
                    }
                }

            }
        }
    }
}

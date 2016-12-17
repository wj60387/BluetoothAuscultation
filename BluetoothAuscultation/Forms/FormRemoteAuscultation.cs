using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using ProtocalData.Protocol.Derive;
using BluetoothAuscultation.Utilities;
using BluetoothAuscultation.Devices;
using System.Data;
using System.Threading;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BluetoothAuscultation.Forms
{
    public partial class FormRemoteAuscultation : DockContentEx
         , IHandleMessage<RequestRemoteAuscultateCode>
        , IHandleMessage<ResGetDeviceInfoCode>
       // , IHandleMessage<RefleshStatusCode>
        //, IHandleMessage<ResRemoteAuscultateCode>
       //   , IHandleMessage<RemoteStartAudioOutputCode>
       // , IHandleMessage<RemoteStopAudioOutputCode>
      //  , IHandleMessage<RemoteAudioCode>
    {
        public FormRemoteAuscultation()
        {
            InitializeComponent();
            this.SetBackColor(Setting.themeColor);
            this.Load += new EventHandler(FormRemoteAuscultation_Load);
        }
        void FormRemoteAuscultation_Load(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            //btnSelect.Enabled = StethoscopeManager.StethoscopeList.Where(s => s.Name == cboStethoscope.Text).First().IsConnected;
            //btnSelect.Enabled = false;
            this.dgvRemote.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dgvRemote.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
            cboStethoscope.Items.Clear();
            foreach (var item in StethoscopeManager.StethoscopeList)
            {
                var stetInfo = Setting.GetStetInfoByStetName(item.Name);
#if !DEBUG
                  if (item.IsConnected)
#endif
                cboStethoscope.Items.Add(new StetItem()
                {
                    image = item.IsConnected ? Setting.OnlineImage : Setting.OfflineImage,
                    stetName = item.Name,
                    stetChineseName = stetInfo == null ? "" : stetInfo.StetChineseName,
                    Owner = stetInfo == null ? "" : stetInfo.Owner
                });
            }
            if (cboStethoscope.Items.Count > 0)
                cboStethoscope.SelectedIndex = 0;
        }
        private void cboStethoscope_DropDown(object sender, EventArgs e)
        {
            cboStethoscope.Items.Clear();
            foreach (var item in StethoscopeManager.StethoscopeList)
            {
                var stetInfo = Setting.GetStetInfoByStetName(item.Name);
                 #if !DEBUG
                  if (item.IsConnected)
#endif
                cboStethoscope.Items.Add(new StetItem()
                {
                    image = item.IsConnected ? Setting.OnlineImage : Setting.OfflineImage,
                    stetName = item.Name,
                    stetChineseName = stetInfo == null ? "" : stetInfo.StetChineseName,
                    Owner = stetInfo == null ? "" : stetInfo.Owner
                });
            }
            //if (cboStethoscope.Items.Count > 0)
            //    cboStethoscope.SelectedIndex = 0;
        }

        public DataTable DgvTable
        {
            get
            {
                DataTable dt = new DataTable();
                for (int count = 0; count < dgvRemote.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgvRemote.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                for (int count = 0; count < dgvRemote.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgvRemote.Columns.Count; countsub++)
                    {
                        dr[countsub] = dgvRemote.Rows[count].Cells[countsub].Value.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.cboStethoscope.Text))
            {
                MessageBox.Show("请选择一个听诊器！");
                return;
            }
            if (!StethoscopeManager.StethoscopeList.Where(s => s.Name == this.cboStethoscope.Text && s.IsConnected).Any())
            {
                MessageBox.Show(string.Format("听诊器{0}尚未连接",this.cboStethoscope.Text));
                return;
            }
            if (!DgvTable.Select().Where(r => r[0].ToString() == "True").Any())
            {
                MessageBox.Show("请选择在线的听诊器！");
                return;
            }
            while (true)
            {
                bool isHas = false;
                for (int i = 0; i < dgvRemote.Rows.Count; i++)
                {
                    if (!(bool)dgvRemote.Rows[i].Cells[0].Value)
                    {
                        isHas = true;
                        dgvRemote.Rows.RemoveAt(i);
                        break;
                    }
                }
                if (!isHas) break;
            }
            var macs = DgvTable.Select().Where(r => r[0].ToString() == "True").GroupBy(row => row["MAC"]).Select(g => g.Key.ToString()).ToArray();
            var guid = Guid.NewGuid().ToString();
            foreach (var mac in macs)
            {
                var rows = DgvTable.Select("MAC='" + mac + "' and Checked='True'");
                if (rows.Length > 0)
                {
                    var code = new RequestRemoteAuscultateCode()
                    {
                        Guid = guid,
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        SrcPCName = CommonUtil.GetMachineName(),
                        SrcStetName = this.cboStethoscope.Text,
                        SrcStetOwner = Setting.GetStetInfoByStetName(this.cboStethoscope.Text).Owner,
                        DestMac = mac,
                        DestStetNames = rows.Select(r => r["StetName"].ToString()).ToArray(),
                        InvestList = DgvTable

                    };
                    var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                    Mediator.SuperSocket.Send(bytes);
                    Mediator.ShowMsg(string.Format("{0}向{1}发起远程听诊请求...", cboStethoscope.Text, mac));
                }
            }
            var formAuscultation = new FormAuscultation()
            {
                InvestList = DgvTable,
                Sender = this.cboStethoscope.Text,
                Guid = guid,
                isSender = true
            };
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                string sqlInsert = "insert into RemoteAuscultateList(SrcGUID,Acceptor,MAC) select {0},{1},{2}";
                foreach (DataRow dr in DgvTable.Rows)
                {

                    int count = Mediator.remoteService.ExecuteNonQuery(sqlInsert, new string[] { guid, dr["StetName"] + "", dr["MAC"] + "" });
                    if (count <= 0)
                    {
                        MessageBox.Show("远程听诊创建失败...");
                        return;
                    }

                }
                //write record
                string sql = " insert into RemoteAuscultate(GUID,Createor,SN,MAC) select {0},{1},{2},{3}";
                if (Mediator.remoteService.ExecuteNonQuery(sql, new string[] { guid, this.cboStethoscope.Text, Setting.authorizationInfo.AuthorizationNum, Setting.authorizationInfo.MachineCode }) > 0)
                {
                    // formAuscultation.LoadMettingList(DgvTable);
                    Mediator.isBusy = true;
                    formAuscultation.ShowDialog();
                    //关闭了此次听诊会议
                    string sqlClose = "update RemoteAuscultate set  OverTime=getdate(),Status =2 where GUID={0}";
                    Mediator.remoteService.ExecuteNonQuery(sqlClose, new string[] { guid });
                    Mediator.ShowMsg(string.Format("远程听诊结束，编号{0}", formAuscultation.Guid));
                    foreach (var mac in formAuscultation.DgvTable.Select().Select(r=>r["MAC"]+"").ToArray().Distinct())
                    {
                        var code = new RemoteExitCode()
                        {
                            Guid=guid,
                            SrcMac=Setting.authorizationInfo.MachineCode,
                            DestMac = mac,
                            ExitMac=Setting.authorizationInfo.MachineCode
                        };
                        var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                        Mediator.SuperSocket.Send(bytes);
                    }
                    Mediator.isBusy = false;
                }
            }
        }
        private void cboStethoscope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboStethoscope.Text))
            {
                MessageBox.Show("请选择一个听诊器");
                return;
            }

            RequestGetDeviceInfoCode requsetGetDeviceInfoCode = new RequestGetDeviceInfoCode();
            var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(requsetGetDeviceInfoCode);
            Mediator.SuperSocket.Send(bytes);
            dgvRemote.Rows.Clear();
            Mediator.ShowMsg("刷新在线的听诊器...");
            //Mediator.isBusy = false;
            btnStart.Visible = false;
            Mediator.WriteLog(this.Name, string.Format("远程听诊,{0}刷新在线的听诊器...", cboStethoscope.Text));

        }
        /// <summary>
        /// 接收到设备信息
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(ResGetDeviceInfoCode message)
        {
            if (message.StetNames != null)
            {
                Invoke(new MethodInvoker(() =>
                {
                    for (int i = 0; i < message.StetNames.Length; i++)
                    {
#if DEBUG
                        if (message.StetNames[i] != this.cboStethoscope.Text  )
#endif
                        #if !DEBUG
                        if (message.StetNames[i] != this.cboStethoscope.Text && message.isConnected[i])
#endif
                        {
                            if (!isExistDeviceInfo(message.SrcMac,message.StetNames[i]))
                            {
                                dgvRemote.Rows.Add(false, message.StetChineseNames[i],message.StetNames[i], message.PCName, message.SrcMac,  message.StetOwners[i],
                                    message.isConnected[i] ? "已连接" : "未连接", "否");
                               // dgvRemote.Rows[dgvRemote.Rows.Count - 1].DefaultCellStyle.BackColor = message.isConnected[i] ? Color.Green : Color.White;
                            }
                        }
                    }
                }));


            }


        }
        bool isExistDeviceInfo(string Mac, string StetName)
        {
            for (int i = 0; i < dgvRemote.Rows.Count; i++)
            {
                var mac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString();
                var stetName = dgvRemote.Rows[i].Cells["StetName"].Value.ToString();
                if (stetName.Equals(StetName) && Mac.Equals(mac))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 接收到请求远程邀请
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(RequestRemoteAuscultateCode message)
        {
         
            Invoke(new MethodInvoker(() =>
            {
                Mediator.WriteLog(this.Name, string.Format("处理来自{0}的听诊器{1}处理远程邀请请求...", message.SrcMac, message.SrcStetName));
                this.DockHandler.Activate();
                //非本机，接收到远程请求,但本机有其他设备在使用中,自动拒绝
                if (message.SrcMac != Setting.authorizationInfo.MachineCode && Mediator.isBusy)
                {
                    var msg = string.Format("设备忙,自动拒绝了{0}的{1}远程听诊请求...", message.SrcMac, message.SrcStetName);
                    var _resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                    {
                        Guid = message.Guid,
                        SrcPCName = CommonUtil.GetMachineName(),
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        DestMac = message.SrcMac,
                        isAccept = false,
                        Comment = msg
                    };
                    var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(_resRemoteAuscultateCode);
                    Mediator.SuperSocket.Send(_bytes);
                    Mediator.ShowMsg(msg);
                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                    {
                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        foreach (var stetName in message.DestStetNames)
                        {
                            string sql = "update RemoteAuscultateList set Comment={0} where SrcGUID={1} and Acceptor={2}";
                            var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { msg, message.Guid, stetName });
                            if (count < 0)
                            {

                            }
                        }

                    }
                    return;
                }
                //本机
                if(message.SrcMac == Setting.authorizationInfo.MachineCode)
                {
                    var resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                   {
                       Guid = message.Guid,
                       SrcPCName=CommonUtil.GetMachineName(),
                       SrcMac = Setting.authorizationInfo.MachineCode,
                       DestMac = message.SrcMac,
                       isAccept = true,
                       Comment="本机远程邀请自动接受..."
                   };
                   var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resRemoteAuscultateCode);
                   Mediator.SuperSocket.Send(bytes);
                   using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                   {
                       MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       foreach (var stetName in message.DestStetNames)
                       {
                           string sql = "update RemoteAuscultateList set isAccept=1,Comment={0} where SrcGUID={1} and Acceptor={2}";
                           var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { "本机远程邀请自动接受...", message.Guid, stetName });
                           if (count < 0)
                           {

                           }
                       }

                   }
                    return;
                }
                var stetNames = string.Join(",", message.DestStetNames);
                if (!StethoscopeManager.StethoscopeList.Where(s => message.DestStetNames.Contains(s.Name) && s.IsConnected).Any())
                {
                    Mediator.ShowMsg(string.Format("来自{3}的听诊器{0} {1}邀请您远程听诊,邀请的听诊器{2},都处于未连接状态，自动拒绝！", message.SrcStetName, string.IsNullOrEmpty(message.SrcStetOwner) ? "" : ",(" + message.SrcStetOwner + ")", stetNames, message.SrcPCName));
                    var resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                    {
                        Guid = message.Guid,
                        SrcPCName = CommonUtil.GetMachineName(),
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        DestMac = message.SrcMac,
                        isAccept = false,
                        Comment = "邀请的听诊器都处于未连接状态，自动拒绝..."
                    };
                    var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resRemoteAuscultateCode);
                    Mediator.SuperSocket.Send(bytes);
                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                    {
                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        foreach (var stetName in message.DestStetNames)
                        {
                            string sql = "update RemoteAuscultateList set isAccept=1,Comment={0} where SrcGUID={1} and Acceptor={2}";
                            var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { "邀请的听诊器都处于未连接状态，自动拒绝...", message.Guid, stetName });
                            if (count < 0)
                            {

                            }
                        }

                    }
                    return;
                }

                Mediator.ShowMsg(string.Format("来自{3}的听诊器{0} {1}邀请您远程听诊,邀请的听诊器有{2},是否接受?", message.SrcStetName, string.IsNullOrEmpty(message.SrcStetOwner) ? "" : ",(" + message.SrcStetOwner + ")", stetNames, message.SrcPCName));
                //非本机,同意请求请求,但得知道当前远程回话是否还在
                if (DialogResult.OK == MessageBox.Show(string.Format("来自{3}的听诊器{0} {1}邀请您远程听诊,邀请的听诊器有{2},是否接受?", message.SrcStetName, string.IsNullOrEmpty(message.SrcStetOwner) ? "" : ",(" + message.SrcStetOwner + ")", stetNames, message.SrcPCName)
                    , "远程听诊提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
               {
                   //1.同意远程请求,但得知道当前远程回话是否还在
                   using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                   {
                       MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       string sql = "select Status from RemoteAuscultate where GUID={0}";
                       // Status默认为0 -就绪 1-进行中 2-结束
                       var status=int.Parse(""+Mediator.remoteService.ExecuteScalar(sql, new string[] { message .Guid}));
                       if (status == 2)
                      {
                          var _msg = string.Format("此次远程听诊已经结束...远程听诊编号{0}", message.Guid);
                          Mediator.ShowMsg(_msg);
                          return;
                      }
                       else if (status == 1)
                       {
                           var _msg = string.Format("此次远程听诊正在进行中您来晚了...远程听诊编号{0}", message.Guid);
                           Mediator.ShowMsg(_msg);
                           return;
                           // 已经开始
                       }
                   }

                  
                   var   msg=string.Format("{2}接受了{0}的{1}远程听诊请求...", message.SrcMac, message.SrcStetName, CommonUtil.GetMachineName());
                   var resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                   {
                       Guid = message.Guid,
                       SrcPCName = CommonUtil.GetMachineName(),
                       SrcMac = Setting.authorizationInfo.MachineCode,
                       DestMac = message.SrcMac,
                       isAccept = true,
                       Comment = msg //Mediator.isBusy ? string.Format("设备忙,拒绝了{0},{1}远程听诊请求...", message.SrcMac, message.SrcStetName):
                         
                   };
                   var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resRemoteAuscultateCode);
                   Mediator.SuperSocket.Send(bytes);

                   using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                   {
                       MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       foreach (var stetName in message.DestStetNames)
                       {
                           var isAccept = StethoscopeManager.StethoscopeList.Where(s => s.Name == stetName && s.IsConnected).Any() ? "1" : "0";
                           string sql = "update RemoteAuscultateList set AcceptTime=getdate(), isAccept={3},Comment={0} where SrcGUID={1} and Acceptor={2}";
                           var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { msg, message.Guid, stetName, isAccept });
                           if (count < 0)
                           {

                           }
                       }

                   }
                  
                    //2.实时更新听诊器连接状态
                   Mediator.StethcopeConnectChangedEvent += (name, b) =>
                   {
                       if (message.DestStetNames.Contains(name))
                       {
                           var code = new RefleshStatusCode()
                           {
                               SrcMac = Setting.authorizationInfo.MachineCode,
                               SrcStetName = name,
                               DestMac = message.SrcMac,
                               isConnect = b
                           };
                           var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                           Mediator.SuperSocket.Send(_bytes);
                       }
                   };
                   Mediator.ShowMsg(string.Format("接受了{0}的{1}远程听诊请求...", message.SrcMac, message.SrcStetName));
                    //3.进入会议室
                   Thread thread = new Thread(() => {
                       this.Invoke(new MethodInvoker(() => {
                           var formAuscultation = new FormAuscultation()
                           {
                               Sender = message.SrcStetName,
                               Guid = message.Guid,
                               isSender = false
                           };
                           Mediator.isBusy = true;
                           //发进入消息
                           {
                               var code = new RemoteEnterCode()
                               {
                                   Guid = message.Guid,
                                   SrcMac=Setting.authorizationInfo.MachineCode,
                                   DestMac = message.SrcMac
                               };
                               var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                               Mediator.SuperSocket.Send(_bytes);
                           }
                           formAuscultation.ShowDialog();
                           //写离开状态
                           using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                           {
                               MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                               OperationContext.Current.OutgoingMessageHeaders.Add(header);
                               header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                               OperationContext.Current.OutgoingMessageHeaders.Add(header);
                               foreach (var stetName in message.DestStetNames)
                               {
                                   string sql = "update RemoteAuscultateList set ExitTime=getdate(), isExit=1,Comment={0} where SrcGUID={1} and Acceptor={2}";
                                   var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { msg, message.Guid, stetName });
                                   if (count < 0)
                                   {

                                   }
                               }

                           }
                           Mediator.ShowMsg(string.Format("远程听诊结束，编号{0}", message.Guid));
                           //发退出消息
                           Mediator.isBusy = false;
                           foreach (var stetName in message.DestStetNames)
                           {
                               var code = new RemoteExitCode()
                               {
                                   Guid = message.Guid,
                                   SrcMac = Setting.authorizationInfo.MachineCode,
                                   DestMac = message.SrcMac,
                                   ExitMac = Setting.authorizationInfo.MachineCode
                               };
                               var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                               Mediator.SuperSocket.Send(_bytes);
                           }
                       }));
                      
                   });
                   thread.Start();
                   
                  
               }
               else
               {
                   var msg =  string.Format("主动拒绝了{0}的{1}远程听诊请求...", message.SrcMac, message.SrcStetName);
                   var resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                   {
                       Guid = message.Guid,
                       SrcPCName = CommonUtil.GetMachineName(),
                       SrcMac = Setting.authorizationInfo.MachineCode,
                       DestMac = message.SrcMac,
                       isAccept = false
                       ,
                       Comment = msg //Mediator.isBusy ? string.Format("设备忙,拒绝了{0},{1}远程听诊请求...", message.SrcMac, message.SrcStetName):
                        
                   };
                   var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resRemoteAuscultateCode);
                   Mediator.SuperSocket.Send(bytes);

                   using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                   {
                       MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                       OperationContext.Current.OutgoingMessageHeaders.Add(header);
                       foreach (var stetName in message.DestStetNames)
                       {
                           string sql = "update RemoteAuscultateList set Comment={0} where SrcGUID={1} and Acceptor={2}";
                           var count = Mediator.remoteService.ExecuteNonQuery(sql, new string[] { msg, message.Guid, stetName });
                           if (count < 0)
                           {

                           }
                       }

                   }
               }

            }));
        }
        public void HandleMessage(ResRemoteAuscultateCode message)
        {
            Invoke(new MethodInvoker(delegate()
            {
                //Mediator.WriteLog(this.Name, string.Format("处理来自{0}远程邀请请求的回应...", message.SrcPCName));
                Mediator.WriteLog(this.Name, message.Comment);
                foreach (DataGridViewRow row in dgvRemote.Rows)
                {
                    if (row.Cells["MAC"].Value.ToString() == message.SrcMac && (bool)row.Cells[0].Value)
                    {
                        row.Cells["isAccept"].Value = message.isAccept ? "是" : "否";
                        Mediator.ShowMsg(string.Format("{0}的{1}{2}了远程听诊请求", message.SrcPCName, row.Cells["StetName"].Value, message.isAccept ? "接受" : "拒绝"));
                        btnStart.Visible = message.isAccept;
                    }
                }
            }));
        }

        private void dgvRemote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            switch (dgvRemote.Columns[e.ColumnIndex].Name)
            {
                case "btnAccept":
                    {
                        Mediator.ShowMsg("接受远程听诊请求...");
                        Mediator.WriteLog(this.Name, string.Format("接受远程听诊请求....."));
                        var guid = dgvRemote.Rows[e.RowIndex].Cells["dgvGUID"].Value.ToString();
                        var toStetName = dgvRemote.Rows[e.RowIndex].Cells["ToStetName"].Value.ToString();
                        var srcStetName = dgvRemote.Rows[e.RowIndex].Cells["SrcStetName"].Value.ToString();
                        var srcMac = dgvRemote.Rows[e.RowIndex].Cells["SrcMac"].Value.ToString();
                        var stethoscopes = StethoscopeManager.StethoscopeList.Where(s => s.Name == toStetName).ToArray();
                        if (stethoscopes.Length > 0 && stethoscopes.First().IsConnected)
                        {
                            var resRemoteAuscultateCode = new ResRemoteAuscultateCode()
                            {
                                Guid = guid,
                                SrcMac = Setting.authorizationInfo.MachineCode,
                                //SrcStetName = toStetName,
                                DestMac = srcMac,
                                isAccept = true
                            };
                            var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(resRemoteAuscultateCode);
                            Mediator.SuperSocket.Send(bytes);
                            var formAuscultation = new FormAuscultation()
                                {
                                    Sender = srcStetName,
                                    Guid = guid,
                                    isSender = false
                                };
                            formAuscultation.ShowDialog();
                            dgvRemote.Rows.RemoveAt(e.RowIndex);
                            Mediator.ShowMsg("结束远程听诊...");
                        }
                        else
                        {
                            MessageBox.Show("请先连接听诊器:" + toStetName);
                        }

                    }
                    break;
                case "Checked":
                    {
                        var b = (bool)dgvRemote.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        dgvRemote.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !b;
                    }
                    break;
            }
        }

        public void HandleMessage(RefleshStatusCode message)
        {
            foreach (DataGridViewRow row in dgvRemote.Rows)
            {
                if (row.Cells["isAccept"].Value.ToString() == "否") continue;
                if (row.Cells["StetName"].Value.ToString() == message.SrcStetName)
                {
                    row.Cells["StetStatus"].Value = message.isConnect ? "已连接" : "未连接";
                   // row.DefaultCellStyle.BackColor = message.isConnect ? Color.Green : Color.White;
                    Mediator.ShowMsg(message.SrcMac + ",的" + message.SrcStetName+"听诊器"+(message.isConnect?"连接上了...":"断开连接..."));
                }
            }
        }

        private List<RemoteStartAudioOutputCode> listRemoteStartAudioOutputCode()
        {
                var list = new List<RemoteStartAudioOutputCode>();
                for (int i = 0; i < dgvRemote.Rows.Count; i++)
                {
                    if (dgvRemote.Rows[i].Cells["isAccept"].Value.ToString() == "否") continue;
                    var code = new RemoteStartAudioOutputCode()
                    {
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        SrcStetName = this.cboStethoscope.Text,
                        DestStetName = dgvRemote.Rows[i].Cells["StetName"].Value.ToString(),
                        DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString()
                    };
                    list.Add(code);
                }
                return list;
        }
        private List<RemoteAudioCode> ListRemoteAudioCode()
        {
                List<RemoteAudioCode> list = new List<RemoteAudioCode>();
                for (int i = 0; i < dgvRemote.Rows.Count; i++)
                {
                    if (dgvRemote.Rows[i].Cells["isAccept"].Value.ToString() == "否") continue;
                    var code = new RemoteAudioCode()
                    {
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        SrctetName = this.cboStethoscope.Text,
                        DestStetName = dgvRemote.Rows[i].Cells["StetName"].Value.ToString(),
                        DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString()
                    };
                    list.Add(code);
                }
                return list;
        }
        private List<RemoteStopAudioOutputCode> listRemoteStopAudioOutputCode()
        {
                var list = new List<RemoteStopAudioOutputCode>();
                for (int i = 0; i < dgvRemote.Rows.Count; i++)
                {
                    if (dgvRemote.Rows[i].Cells["isAccept"].Value.ToString() == "否") continue;
                    var code = new RemoteStopAudioOutputCode()
                    {
                        SrcMac = Setting.authorizationInfo.MachineCode,
                        SrctetName = this.cboStethoscope.Text,
                        DestStetName = dgvRemote.Rows[i].Cells["StetName"].Value.ToString(),
                        DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString()
                    };
                    list.Add(code);
                }
                return list;
        }

        MemoryStream SendStream = new MemoryStream();
        MemoryStream ReceiveStream = new MemoryStream();
        public bool isStart = false;
        private void btnStart_Click(object sender, EventArgs e)
        {
            isStart = true;
           // Mediator.isBusy = true;
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == this.cboStethoscope.Text);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                return;
            }
            //Send Start To Remote
            foreach (var code in listRemoteStartAudioOutputCode())
            {
                var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                Mediator.SuperSocket.Send(bytes);
            }
            var listRemoteAudioCode = ListRemoteAudioCode();
            Thread.Sleep(500);
            FormProcessBar formProcessBar = new FormProcessBar(true)
            {
                BtnText = "停止远程"
            };
            Thread pairThread = new Thread(() =>
            {
                formProcessBar.TimerCallBackEvent += () =>
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                        formProcessBar.Title = string.Format("远程听诊中... {0} 秒", formProcessBar.Times);
                    }));
                };
                stethoscope.StartAudioInput();
                Mediator.ShowMsg(string.Format("听诊器 {0} 开始远程听诊...", stethoscope.Name));
                // Stream audio from the stethoscope to the computer.
               
                while (formProcessBar.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    byte[] packet = new byte[128];
                    int bytesRead = stethoscope.AudioInputStream.Read(packet, 0, packet.Length);
                    //Send to Remote
                    if (bytesRead > 0)
                        Invoke(new MethodInvoker(() =>
                        {
                            foreach (var code in listRemoteAudioCode)
                            {
                                code.AudioData = packet.Take(bytesRead).ToArray();
                                var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                                Mediator.SuperSocket.Send(bytes);
                            }
                        }));
                }
                Mediator.ShowMsg(string.Format("听诊器 {0} 远程听诊完毕，时长 {1} 秒", stethoscope.Name, formProcessBar.Times));

            });

            pairThread.Start();
            formProcessBar.ShowDialog();
            formProcessBar.TimerEnable = false;
            stethoscope.StopAudioInput();
            //Send Over To Remote
            foreach (var code in listRemoteStopAudioOutputCode())
            {
                var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                Mediator.SuperSocket.Send(bytes);
            }
            Mediator.ShowMsg("远程听诊完毕...");
            //Mediator.isBusy = false;
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(RemoteAudioCode message)
        {
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == message.DestStetName);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                return;
            }
            else
            stethoscope.AudioOutputStream.Write(message.AudioData, 0, message.AudioData.Length);
            //Mediator.isBusy = true;

        }
        public void HandleMessage(RemoteStartAudioOutputCode message)
        {
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == message.DestStetName);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                return;
            }
            else
            stethoscope.StartAudioOutput();
           // Mediator.isBusy = true;

        }
        public void HandleMessage(RemoteStopAudioOutputCode message)
        {
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == message.DestStetName);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                return;
            }
            else
            stethoscope.StopAudioOutput();
            //Mediator.isBusy = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvRemote.Rows.Count; i++)
            {
                dgvRemote.Rows[i].Cells[0].Value = checkBox1.Checked;
            }
        }
    }
}

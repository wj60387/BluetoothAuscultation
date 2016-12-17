using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProtocalData.Protocol.Derive;
using BluetoothAuscultation.Devices;
using System.Threading;
using BluetoothAuscultation.Utilities;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BluetoothAuscultation.Forms
{
    public partial class FormAuscultation : Form
         , IHandleMessage<ResRemoteAuscultateCode>
        , IHandleMessage<RemoteAudioCode>
        , IHandleMessage<RemoteStartAudioOutputCode>
        , IHandleMessage<RemoteStopAudioOutputCode>
        , IHandleMessage<RemoteExitCode>
        , IHandleMessage<RemoteEnterCode>
        , IHandleMessage<OffLineCode>
    {
        public string Guid { get; set; }
        public DataTable InvestList { get; set; }
        public FormAuscultation()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormAuscultation_Load);

        }

        void FormAuscultation_Load(object sender, EventArgs e)
        {
            this.dgvRemote.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dgvRemote.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
        }
        public void LoadMettingList(DataTable table)
        {
            for (int j = 0; j < table.Rows.Count; j++)
            {
                if (table.Rows[j]["MAC"].Equals(Setting.authorizationInfo.MachineCode))
                {
                    table.Rows[j]["isAccept"] = "是";
                }
                List<object> list = new List<object>();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    list.Add(table.Rows[j][i]);
                }

                dgvRemote.Rows.Add(list.ToArray());
            }
        }
        void dgvRemote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public string Sender
        {
            get
            {
                return txtStetName.Text;
            }
            set
            {
                txtStetName.Text = value;

            }
        }
        public bool isSender
        {
            get
            {
                return btnAuscultate.Visible;
            }
            set
            {
                btnAuscultate.Visible = value;
                btnExit.Visible = !value;

            }
        }
        private List<RemoteStartAudioOutputCode> listRemoteStartAudioOutputCode()
        {
            var list = new List<RemoteStartAudioOutputCode>();
            for (int i = 0; i < dgvRemote.Rows.Count; i++)
            {
                if (dgvRemote.Rows[i].Cells["isAccept"].Value.ToString() == "否") continue;

                var DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString();
                if (list.Any(l => l.DestMac == DestMac)) continue;
                var code = new RemoteStartAudioOutputCode()
                {
                    Guid = this.Guid,
                    SrcMac = Setting.authorizationInfo.MachineCode,
                    SrcStetName = this.txtStetName.Text,
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
                var DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString();
                if (list.Any(l => l.DestMac == DestMac)) continue;
                var code = new RemoteAudioCode()
                {
                    Guid = this.Guid,
                    SrcMac = Setting.authorizationInfo.MachineCode,
                    SrctetName = this.txtStetName.Text,
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
                var DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString();
                if (list.Any(l => l.DestMac == DestMac)) continue;
                var code = new RemoteStopAudioOutputCode()
                {
                    Guid = this.Guid,
                    SrcMac = Setting.authorizationInfo.MachineCode,
                    SrctetName = this.txtStetName.Text,
                    DestStetName = dgvRemote.Rows[i].Cells["StetName"].Value.ToString(),
                    DestMac = dgvRemote.Rows[i].Cells["MAC"].Value.ToString()
                };
                list.Add(code);
            }
            return list;
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
                        dr[countsub] = dgvRemote.Rows[count].Cells[countsub].Value+"";
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }
        /// <summary>
        /// 处理回应远程请求，接受添加，不接受删除
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(ResRemoteAuscultateCode message)
        {
            //过期回应
            if (message.Guid != this.Guid)
            {
                return;
            }
            Invoke(new MethodInvoker(() =>
            {
                if (message.isAccept)
                {
                    foreach (DataRow dr in InvestList.Select("MAC='" + message.SrcMac + "'"))
                    {
                        dr["isAccept"] = "是";
                        List<object> list = new List<object>();
                        for (int i = 0; i < InvestList.Columns.Count; i++)
                        {
                            list.Add(dr[i]);
                        }
                        dgvRemote.Rows.Add(list.ToArray());
                    }
                }
                Mediator.ShowMsg(message.Comment);
            }));

        }
        /// <summary>
        /// 退出会诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 开始会诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuscultate_Click(object sender, EventArgs e)
        {
            if (this.DgvTable.Rows.Count == 0)
            {
                MessageBox.Show(string.Format("目前尚未有听诊器加入..."));
                return;
            }
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == this.txtStetName.Text);
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
                while (formProcessBar.DialogResult != System.Windows.Forms.DialogResult.Cancel
                    && dgvRemote.Rows.Count > 0)
                {
                    byte[] packet = new byte[128];
                    int bytesRead = stethoscope.AudioInputStream.Read(packet, 0, packet.Length);
                    //Send to Remote
                    if (bytesRead <= 0)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    //Invoke(new MethodInvoker(() =>
                    //{
                    foreach (var code in listRemoteAudioCode)
                    {
                        code.AudioData = packet.Take(bytesRead).ToArray();
                        var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                        Mediator.SuperSocket.Send(bytes);
                    }
                    Thread.Sleep(1);
                    //}));
                }
                Mediator.ShowMsg(string.Format("听诊器 {0} 远程听诊完毕，时长 {1} 秒", stethoscope.Name, formProcessBar.Times));
                Invoke(new MethodInvoker(delegate()
                {
                    formProcessBar.Close();
                }));
            });
            pairThread.Start();

            //开始了
            string sql = "update RemoteAuscultate set Status =1 where GUID={0}";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                Mediator.remoteService.ExecuteNonQuery(sql, new string[] { this.Guid });
            }
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
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="message"></param>
        public void HandleMessage(RemoteAudioCode message)
        {
            if (this.IsDisposed || this.Disposing) return;
            if (message.Guid != this.Guid) return;
            this.Invoke(new MethodInvoker(() =>
            {
                var rows = this.DgvTable.Select("MAC='" + Setting.authorizationInfo.MachineCode + "'");
                foreach (DataRow row in rows)
                {
                    var stetName = row["StetName"] + "";
                    var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == stetName);
                    if (!stethoscopeArr.Any())
                        throw new Exception("目前没有检测到听诊器,请检测设备设置！");
                    var stethoscope = stethoscopeArr.First();
                    if (!stethoscope.IsConnected)
                    {
                        Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                        return;
                    }
                    else
                        stethoscope.AudioOutputStream.Write(message.AudioData, 0, message.AudioData.Length);
                }

            }));
            IsReceived = true;
            //var MSG = "TotalBytes:" + stethoscope.TotalBytesWritten + "PerBytes:" + stethoscope.BytesWrittenPerSecond;
            //Mediator.ShowMsg("处理音频消息:" + message.AudioData.Length+"\r\n"+MSG);
        }
        public void HandleMessage(RemoteStartAudioOutputCode message)
        {
            if (this.IsDisposed || this.Disposing) return;
            if (message.Guid != this.Guid) return;
            this.Invoke(new MethodInvoker(() =>
            {
               var rows= this.DgvTable.Select("MAC='" + Setting.authorizationInfo.MachineCode + "'");

               foreach (DataRow row in rows)
               {
                   var stetName=row["StetName"]+"";
                   Mediator.ShowMsg(string.Format("远程听诊开始接收数据,编号为{0}", this.Guid));
                   var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == stetName);
                   if (!stethoscopeArr.Any())
                       throw new Exception("目前没有检测到听诊器,请检测设备设置！");
                   var stethoscope = stethoscopeArr.First();
                   if (!stethoscope.IsConnected)
                   {
                       Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                       return;
                   }
                   else
                       stethoscope.StartAudioOutput();
               }
               
            }));
            //本机远程
            if (message.SrcMac == Setting.authorizationInfo.MachineCode)
                return;
            // Mediator.isBusy = true;
            Thread thread = new Thread(() =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    FormProcessBar formProcessBar = new FormProcessBar(true)
                    {
                        BtnText = "停止远程",
                        CancelBtnVisible = true
                    };
                    OnReceivedEvent += ((b) =>
                    {
                        if (!b)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                if (!formProcessBar.IsDisposed)
                                    formProcessBar.Close();
                            }));
                        }
                    });
                    formProcessBar.OnActiveClose += () =>
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            var rows = this.DgvTable.Select("MAC='" + Setting.authorizationInfo.MachineCode + "'");
                            foreach (DataRow row in rows)
                            {
                                var stetName = row["StetName"] + "";
                                var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == stetName);
                                if (!stethoscopeArr.Any())
                                    throw new Exception("目前没有检测到听诊器,请检测设备设置！");
                                var stethoscope = stethoscopeArr.First();
                                if (!stethoscope.IsConnected)
                                {
                                    Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                                    return;
                                }
                                else
                                    stethoscope.StopAudioOutput();
                            }
                            Mediator.ShowMsg(string.Format("主动退出听诊器,编号为{0}", this.Guid));
                            this.Close();
                        }));
                    };
                    formProcessBar.TimerCallBackEvent += () =>
                      {
                          Invoke(new MethodInvoker(() =>
                            {
                                formProcessBar.Title = string.Format("远程听诊中... {0} 秒", formProcessBar.Times);
                            }));
                      };
                    formProcessBar.ShowDialog();
                }));
            });
            thread.Start();
        }
        private event Action<bool> OnReceivedEvent;
        private bool isReceived = false;
        private bool IsReceived
        {
            get
            {
                return isReceived;
            }
            set
            {
                isReceived = value;
                if (OnReceivedEvent != null)
                    OnReceivedEvent(value);
            }
        }
        public void HandleMessage(RemoteStopAudioOutputCode message)
        {
            if (this.IsDisposed || this.Disposing) return;
            if (message.Guid != this.Guid) return;

            this.Invoke(new MethodInvoker(() =>
            {
                var rows = this.DgvTable.Select("MAC='" + Setting.authorizationInfo.MachineCode + "'");

                foreach (DataRow row in rows)
                {
                    var stetName = row["StetName"] + "";
                    Mediator.ShowMsg(string.Format("远程听诊停止接收数据,编号为{0}", this.Guid));
                    var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == stetName);
                    if (!stethoscopeArr.Any())
                        throw new Exception("目前没有检测到听诊器,请检测设备设置！");
                    var stethoscope = stethoscopeArr.First();
                    if (!stethoscope.IsConnected)
                    {
                        Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                        return;
                    }
                    else
                        stethoscope.StopAudioOutput();
                }

            }));
            //Mediator.isBusy = false;
            IsReceived = false;
        }

        private void FormAuscultation_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void HandleMessage(RemoteExitCode message)
        {
            if (this.Disposing || IsDisposed || message.SrcMac==Setting.authorizationInfo.MachineCode)
            {
                return;
            }
            Invoke(new MethodInvoker(() =>
            {
                if (message.Guid != this.Guid) return;
                Mediator.ShowMsg(string.Format("远程机器{0}退出了远程听诊", message.ExitMac));
                if (this.isSender)//广播出去
                {
                    foreach (var mac in this.DgvTable.Select().Select(r => r["MAC"] + "").ToArray().Distinct())
                    {
                        var code = new RemoteExitCode()
                        {
                            Guid = message.Guid,
                            SrcMac = Setting.authorizationInfo.MachineCode,
                            DestMac = mac,
                            ExitMac = message.ExitMac
                        };
                        var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                        Mediator.SuperSocket.Send(_bytes);
                    }
                }
                else//作为接收者
                {
                    if (message.ExitMac == message.SrcMac)//远程发起者退出
                    {
                        //MessageBox.Show(string.Format("远程听诊关闭了,本窗体即将关闭...编号为{0}", message.Guid));
                        Mediator.ShowMsg(string.Format("远程听诊关闭了,编号为{0}", message.Guid));
                        this.Close();
                        return;
                    }
                }
                //刷新
                string sql = @"select b.StetName,b.MAC,b.PCName,b.StetChineseName,b.Owner from RemoteAuscultateList a left join StethoscopeManager b
  on a.Acceptor=b.StetName and a.MAC=b.MAC where a.SrcGUID={0} and a.isAccept=1 and isExit=0";
                using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                {
                    MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                    OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { this.Guid });
                    if (ds.Tables[0].Rows.Count == 0) this.Close();
                    dgvRemote.Rows.Clear();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dgvRemote.Rows.Add(true, row["StetChineseName"], row["StetName"], row["PCName"], row["MAC"], row["Owner"], "已连接", "是");
                    }
                     
                }
            }));

        }

        public void HandleMessage(RemoteEnterCode message)
        {
            if (this.IsDisposed || this.Disposing) return;
            Invoke(new MethodInvoker(() =>
            {

                if (message.Guid != this.Guid) return;
                Mediator.ShowMsg(string.Format("远程机器{0}加入了远程听诊", message.SrcMac));
                if (this.isSender && message.SrcMac!=Setting.authorizationInfo.MachineCode)
                {
                    foreach (var mac in this.DgvTable.Select().Select(r => r["MAC"] + "").ToArray().Distinct())
                    {
                        var code = new RemoteEnterCode()
                        {
                            Guid = message.Guid,
                            SrcMac = Setting.authorizationInfo.MachineCode,
                            DestMac = mac
                        };
                        var _bytes = ProtocalData.Utilities.SerializaHelper.Serialize(code);
                        Mediator.SuperSocket.Send(_bytes);
                    }
                }
                else
                {
                    string sql = @"select b.StetName,b.MAC,b.PCName,b.StetChineseName,b.Owner from RemoteAuscultateList a left join StethoscopeManager b
  on a.Acceptor=b.StetName and a.MAC=b.MAC where a.SrcGUID={0} and a.isAccept=1 and isExit=0";
                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                    {
                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { this.Guid });
                        dgvRemote.Rows.Clear();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            dgvRemote.Rows.Add(true, row["StetChineseName"], row["StetName"], row["PCName"], row["MAC"], row["Owner"], "已连接", "是");
                           // dgvRemote.Rows.Add(true, row[0], row[1], row[2], row[3], row[4], "已连接", "是");
                        }
                    }
                }
            }));

        }
        //有机器掉线了
        public void HandleMessage(OffLineCode message)
        {
            if (this.IsDisposed || this.Disposing) return;
            this.Invoke(new MethodInvoker(() =>
            {
                for (int i = 0; i < dgvRemote.Rows.Count; i++)
                {
                    if (dgvRemote.Rows[i].Cells["MAC"].Value.Equals(message.OffLineMac))
                    {
                        Mediator.ShowMsg(string.Format("远程机器{0}掉线了...",message.OffLineMac));
                        dgvRemote.Rows.RemoveAt(i);
                    }
                }

            }));

        }
    }

}

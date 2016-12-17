using System;
using System.Collections.Generic;
using System.ComponentModel;
 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BluetoothAuscultation.ExtendControl;
using BluetoothAuscultation.Utilities;
using BluetoothAuscultation.Devices;
using MMM.HealthCare.Scopes.Device;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using BluetoothAuscultation.Models;
using ProtocalData.Protocol.Derive;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BluetoothAuscultation.Forms
{
    public partial class FormStethoscope : DockContentEx
    {
        //string[] args = null;
        public FormStethoscope()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMenu_Load);
        }
        public void ConfigStet(string StetName)
        {
                var info = Setting.GetStetInfoByStetName(StetName);
                var formStetInfo = new FormStetInfo()
                {
                    StetOwner = info.Owner,
                    StetName = info.StetName,
                    StetChineseName = info.StetChineseName,
                    StetFuncDescript = info.FuncDescript,
                    StetRemark = info.ReMark,
                    StetType=info.StetType
                };
                formStetInfo.StartPosition = FormStartPosition.CenterParent;
                if (DialogResult.OK == formStetInfo.ShowDialog())
                {
                    StetInfoCode stetInfoCode = new StetInfoCode()
                    {
                        StetName = formStetInfo.StetName,
                        StetChineseName = formStetInfo.StetChineseName,
                        SN = Setting.authorizationInfo.AuthorizationNum,
                        PCName = CommonUtil.GetMachineName(),
                        MAC = Setting.authorizationInfo.MachineCode,
                        Owner = formStetInfo.StetOwner,
                        FuncDescript = formStetInfo.StetFuncDescript,
                        ReMark = formStetInfo.StetRemark,
                        StetType = formStetInfo.StetType
                    };
                    var code = Newtonsoft.Json.JsonConvert.SerializeObject(stetInfoCode);
                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                    {
                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);

                        if (Mediator.remoteService.UpdateStetInfo(code))
                        {
                            HandleMessage(stetInfoCode);
                            Mediator.ShowMsg("新增听诊器 " + formStetInfo.StetName + " 信息，并上传服务器成功...");
                            return;
                        }
                        Mediator.ShowMsg("新增听诊器 " + formStetInfo.StetName + " 信息，并上传服务器失败...");
                    }
                }
                LoadStetInfo();
        }
        void FormMenu_Load(object sender, EventArgs e)
        {
            this.dgvStet.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
             this.btnEdit.Image = Setting.ImageEdit;
             this.dgvStet.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
            this.btnDelete.Image = Setting.ImageDelete;
            LoadStetInfo();
           
        }
        void LoadStetInfo()
        {
            cboStethoscope.Items.Clear();
            dgvStet.Rows.Clear();
            foreach (var item in StethoscopeManager.StethoscopeList)
            {
                var stetInfo = Setting.GetStetInfoByStetName(item.Name);
                if (stetInfo == null || string.IsNullOrEmpty(stetInfo.StetChineseName))
                    cboStethoscope.Items.Add(new StetItem()
                    {
                        image = item.IsConnected ? Setting.OnlineImage : Setting.OfflineImage,
                        stetName = item.Name,
                        stetChineseName = "",
                        Owner = ""
                    });
                else
                {
                    dgvStet.Rows.Add(stetInfo.StetName, stetInfo.MAC,stetInfo.PCName,Setting.GetPatientNameByType(stetInfo.StetType), stetInfo.StetChineseName, stetInfo.Owner, stetInfo.FuncDescript, stetInfo.ReMark);
                }
            }
            if (cboStethoscope.Items.Count > 0)
                cboStethoscope.SelectedIndex = 0;
        }
        private void cboStethoscope_DropDown(object sender, EventArgs e)
        {
           // LoadStetInfo();
        }
        public void HandleMessage(StetInfoCode message)
        {
            string querySql = "select * from StetInfo where StetName={0} and SN={1} and MAC={2} ";
            if (Mediator.sqliteHelper.ExecuteScalar(querySql, message.StetName, message.SN, message.MAC) != null)
            {
                string updateSql = "update StetInfo set StetChineseName={0},Owner={1},FuncDescript={2},ReMark={3},PCName={7} ,StetType={8} where StetName={4} and SN={5} and MAC={6}";
                var count = Mediator.sqliteHelper.ExecuteNonQuery(updateSql, message.StetChineseName,message.Owner, message.FuncDescript, message.ReMark, message.StetName, message.SN, message.MAC,message.PCName,message.StetType);
                if (count > 0)
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                            LoadStetInfo();
                        MessageBox.Show("修改信息成功..");
                    }));
                }
            }
            else
            {

                string insertSql = "insert into StetInfo(StetName,SN,MAC,PCName,StetChineseName,Owner,FuncDescript,ReMark,RecordTime,StetType) select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9}";
                    var count = Mediator.sqliteHelper.ExecuteNonQuery(insertSql, message.StetName, message.SN, message.MAC,message.PCName, message.StetChineseName,message.Owner, message.FuncDescript, message.ReMark, message.RecordTime.ToString("yyyy-MM-dd HH:mm:ss"),message.StetType);
                    if (count > 0)
                    {
                        Invoke(new MethodInvoker(delegate()
                        {
                            LoadStetInfo();
                            MessageBox.Show("新增信息成功..");
                        }));
                        
                    }
                 
            }
           
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.cboStethoscope.Text))
            {
                MessageBox.Show("请选择一个听诊器...");
                return;
            }
            var formStetInfo = new FormStetInfo()
                {
                    StetName = cboStethoscope.Text
                };
            formStetInfo.StartPosition = FormStartPosition.CenterParent;
            if (DialogResult.OK == formStetInfo.ShowDialog())
            {

                StetInfoCode stetInfoCode = new StetInfoCode()
                {
                    StetName = formStetInfo.StetName,
                    StetChineseName = formStetInfo.StetChineseName,
                    SN = Setting.authorizationInfo.AuthorizationNum,
                    PCName = CommonUtil.GetMachineName(),
                    MAC = Setting.authorizationInfo.MachineCode,
                    Owner = formStetInfo.StetOwner,
                    FuncDescript = formStetInfo.StetFuncDescript,
                    ReMark = formStetInfo.StetRemark,
                    StetType=formStetInfo.StetType
                };
                HandleMessage(stetInfoCode);
                if (Setting.isConnected)
                {
                    var code = Newtonsoft.Json.JsonConvert.SerializeObject(stetInfoCode);
                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                    {
                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                        if (Mediator.remoteService.UpdateStetInfo(code))
                        {
                            Mediator.ShowMsg("新增听诊器 " + formStetInfo.StetName + " 信息，并上传服务器成功...");
                            Mediator.WriteLog(this.Name, "新增听诊器 " + formStetInfo.StetName + " 信息成功...,调用的后台方法是UpdateStetInfo");
                            return;
                        }
                        Mediator.WriteLog(this.Name, "新增听诊器 " + formStetInfo.StetName + " 信息失败...");
                        Mediator.ShowMsg("新增听诊器 " + formStetInfo.StetName + " 信息，并上传服务器失败...,调用的后台方法是UpdateStetInfo");
                    }
                }
            }
        }
        private void dgvStet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var stetName = dgvStet.Rows[e.RowIndex].Cells[0].Value.ToString();
                var stetInfo=Setting.GetStetInfoByStetName(stetName);
                switch (dgvStet.Columns[e.ColumnIndex].Name)
                {
                    case "btnEdit":
                        {

                            var formStetInfo = new FormStetInfo()
                            {
                                StetName = stetName,
                                StetChineseName = stetInfo.StetChineseName,
                                StetOwner = stetInfo.Owner,
                                StetFuncDescript = stetInfo.FuncDescript,
                                StetRemark = stetInfo.ReMark,
                                StetType = stetInfo.StetType
                            };
                            formStetInfo.StartPosition = FormStartPosition.CenterParent;
                            if (DialogResult.OK == formStetInfo.ShowDialog())
                            {

                                StetInfoCode stetInfoCode = new StetInfoCode()
                                {
                                    StetName = formStetInfo.StetName,
                                    StetChineseName = formStetInfo.StetChineseName,
                                    SN = Setting.authorizationInfo.AuthorizationNum,
                                    MAC = Setting.authorizationInfo.MachineCode,
                                    Owner = formStetInfo.StetOwner,
                                    PCName = CommonUtil.GetMachineName(),
                                    FuncDescript = formStetInfo.StetFuncDescript,
                                    ReMark = formStetInfo.StetRemark,
                                    StetType = formStetInfo.StetType
                                };
                                HandleMessage(stetInfoCode);
                                if (Setting.isConnected)
                                {

                                    var code = Newtonsoft.Json.JsonConvert.SerializeObject(stetInfoCode);
                                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                                    {
                                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                        if (Mediator.remoteService.UpdateStetInfo(code))
                                        {
                                            Mediator.ShowMsg("编辑听诊器 " + stetName + " 信息，并上传服务器成功...");
                                            Mediator.WriteLog(this.Name, "编辑听诊器 " + formStetInfo.StetName + " 信息成功...,调用的后台方法是UpdateStetInfo");
                                            return;
                                        }
                                        Mediator.ShowMsg("编辑听诊器 " + stetName + " 信息，并上传服务器失败...");
                                        Mediator.WriteLog(this.Name, "编辑听诊器 " + formStetInfo.StetName + " 信息失败...,调用的后台方法是UpdateStetInfo");
                                    }
                                }
                            }
                        }
                        break;
                    case "btnDelete":
                        {

                            if (DialogResult.OK == MessageBox.Show("您确定要清空此听诊器信息？", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                            {
                                var code = new StetInfoDelCode()
                                {
                                    StetName = stetName,
                                    MAC = Setting.authorizationInfo.MachineCode
                                };
                                HandleMessage(code);
                                if (Setting.isConnected)
                                {
                                    string sql = "update StethoscopeManager set IfDel=1  where StetName={0}  and MAC={1} and IfDel=0";
                                    using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                                    {
                                        MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                        header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                        if (Mediator.remoteService.ExecuteNonQuery(sql, new string[] { stetName, Setting.authorizationInfo.MachineCode }) > 0)
                                        {
                                           
                                            Mediator.ShowMsg("删除听诊器 " + stetName + " 信息成功...");
                                            Mediator.WriteLog(this.Name, "删除听诊器 " + stetName + " 信息成功...执行的SQL是:" + sql + "参数是:" + stetName + Setting.authorizationInfo.MachineCode);
                                            return;
                                        }
                                        Mediator.ShowMsg("删除听诊器 " + stetName + " 信息失败...");
                                        Mediator.WriteLog(this.Name, "删除听诊器 " + stetName + " 信息成功...");
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        public void HandleMessage(StetInfoDelCode message)
        {
            string sql = "delete from StetInfo where MAC={0} and StetName={1}";
            int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, message.MAC, message.StetName);
           if (count > 0)
           {
               Invoke(new MethodInvoker(delegate()
               {
                   LoadStetInfo();
                   MessageBox.Show("删除信息成功..");
               }));
           }
        }
    }
}

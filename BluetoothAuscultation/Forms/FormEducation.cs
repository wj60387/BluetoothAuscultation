using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using MMM.HealthCare.Scopes.Device;
using BluetoothAuscultation.Devices;
using System.IO;
using System.Threading;
using BluetoothAuscultation.Models;
using Newtonsoft.Json;
using ProtocalData.Protocol.Derive;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BluetoothAuscultation.IGetAudioInfo;

namespace BluetoothAuscultation.Forms
{
    public partial class FormEducation : DockContentEx
    {
        public FormEducation( string arg="")
        {
            Arg = arg;
            InitializeComponent();
            this.Load += new EventHandler(FormEducation_Load);
            this.SetBackColor(Setting.themeColor);
        }
        string Arg = string.Empty;
        void FormEducation_Load(object sender, EventArgs e)
        {
            this.dgvLocal.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dgv_DownLoad.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            // this.btnDetail.Image = Setting.ImageEdit;
            this.btnDelete.Image = Setting.ImageDelete;
            this.btnDownDelete.Image = Setting.ImageDelete;
            btnDownDetail.Image = Setting.ImageDetail;
            this.btnDetail.Image = Setting.ImageDetail;
            this.dgvLocal.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
            this.dgv_DownLoad.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);


            int i = 0;
            int c = 0;
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

                if (stetInfo.StetName == Arg)
                    c = i;
                i++;
            }
            if (cboStethoscope.Items.Count > 0)
                cboStethoscope.SelectedIndex = c;



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
        }

      
        private void btnAuscultate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboStethoscope.Text))
            {
                MessageBox.Show("请选择一个听诊器..");
                return;
            }
            var stetInfo = Setting.GetStetInfoByStetName(this.cboStethoscope.Text);
            var formAudioRecord = new FormAudioRecord() {
                //DocName=stetInfo.Owner,
                PatientType = stetInfo.StetType, 
                StetName = this.cboStethoscope.Text };
            formAudioRecord.ShowDialog();
            LoadLocalPatientInfo();
        }
        private void btnShowPic_Click(object sender, EventArgs e)
        {

            var stethoscope = StethoscopeManager.StethoscopeList.Where(s => s.Name == StetName).First();
            if (!stethoscope.IsConnected)
            {
                stethoscope.Connect();
            }
            var bitmap = new Bitmap("Sample.png");
            stethoscope.Display = bitmap;
        }
        public string StetName
        {
            get
            {
                return cboStethoscope.Text.IndexOf('\t') > 0 ? cboStethoscope.Text.Substring(0, cboStethoscope.Text.IndexOf('\t')) : cboStethoscope.Text;
            }
        }
        private void cboStethoscope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StetName) && tabLocal.SelectedTab != null)
            {
              
                UpdateData(StetName, tabLocal.SelectedTab.Name);
            }
        }
        void LoadLocalPatientInfo()
        {
//            string sqlQuery = @"select * from PatientInfo a  inner  join 
//(select PGUID from AudioInfo where StetName={0} group by PGUID) b
//on a.PatientGUID=b.PGUID ";
            string sqlQuery = @"select * from PatientInfo where StetName={0} order by rowid desc";
            var dt = Mediator.sqliteHelper.ExecuteDatatable(sqlQuery,this.cboStethoscope.Text);
            dgvLocal.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                dgvLocal.Rows.Add(dr[0],
                    Setting.GetPatientNameByType(int.Parse(dr["PatientType"] + ""))
                    , dr["PatientID"], dr["PatientName"],
                     dr["DocName"], dr["DocDiagnose"], dr["DocRemark"], dr["Flag"],dr["CreateTime"]);
            }

        }
        private void dgvLocal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var PatientInfo = new
                {
                    PatientGUID=dgvLocal.Rows[e.RowIndex].Cells[0].Value+""
       ,
                    PatientType = dgvLocal.Rows[e.RowIndex].Cells[1].Value+""
       ,
                    PatientID = dgvLocal.Rows[e.RowIndex].Cells[2].Value+""
      ,
                    PatientName = dgvLocal.Rows[e.RowIndex].Cells[3].Value+""
       ,
                    DocName = dgvLocal.Rows[e.RowIndex].Cells[4].Value+""
      ,
                    DocDiagnose = dgvLocal.Rows[e.RowIndex].Cells[5].Value+""
      ,
                    DocRemark = dgvLocal.Rows[e.RowIndex].Cells[6].Value+""
                    ,
                    His = dgvLocal.Rows[e.RowIndex].Cells[7].Value + ""
                };

                //string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + audioInfo.StetName + "\\" + audioInfo.RecordTime.Year
                //    + "\\" + audioInfo.RecordTime.Month + "\\" + audioInfo.RecordTime.Day + "\\" + audioInfo.Guid + ".MP3");
                switch (dgvLocal.Columns[e.ColumnIndex].Name)
                {
                    case "btnDetail":
                        {
                            var formAudioRecord = new FormAudioRecord() { 
                                PatientType =Setting.GetPatientTypeByName( PatientInfo.PatientType.ToString())
                                ,StetName=this.cboStethoscope.Text,
                            PatientGUID= PatientInfo.PatientGUID,
                            PatientID= PatientInfo.PatientID,
                            PatientName= PatientInfo.PatientName,
                            DocName= PatientInfo.DocName,
                            DocDiagnose= PatientInfo.DocDiagnose,
                            DocRemark= PatientInfo.DocRemark,
                            His = PatientInfo.His,
                            CanEdit=false
                            };
                            formAudioRecord.ShowDialog();
                            LoadLocalPatientInfo();
                        }
                        break;
                    case "btnUpLoad":
                        {
                            if (!Setting.isConnected)
                            {
                                MessageBox.Show("网络连接异常...");
                                return;
                            }
                            if ((bool)dgvLocal.Rows[e.RowIndex].Tag)
                            {
                                MessageBox.Show("该听诊录音已经上传了!");
                                return;
                            }
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            }
                        }
                        break;
                    case "btnDelete":
                        {
                            if (DialogResult.OK == MessageBox.Show("你确定要删除该记录及其文件吗","删除录音提示", MessageBoxButtons.OKCancel))
                            {
                                string sqlDelP = "delete from PatientInfo where PatientGUID={0}";
                                var k = Mediator.sqliteHelper.ExecuteNonQuery(sqlDelP, PatientInfo.PatientGUID);
                                if (k > 0)
                                {
                                    Mediator.ShowMsg(string.Format("删除患者{0}信息成功...", Path.GetFileName(PatientInfo.PatientName)));
                                    string sqlQueryAudio = "select GUID ,RecordTime from AudioInfo where PGUID={0}";
                                    var dt = Mediator.sqliteHelper.ExecuteDatatable(sqlQueryAudio, PatientInfo.PatientGUID);
                                    string sqlDelAudio = "delete from AudioInfo where PGUID={0}";
                                    var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlDelAudio, PatientInfo.PatientGUID);
                                    if (n > 0)
                                    {
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            string guid = row["GUID"] + "";
                                            DateTime recordTime = (DateTime)row["RecordTime"];
                                            string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + recordTime.Year
                  + "\\" + recordTime.Month + "\\" + recordTime.Day + "\\" + guid + ".MP3");
                                            if (File.Exists(filePath))
                                            {
                                                File.Delete(filePath);
                                                Mediator.ShowMsg(string.Format("删除文件{0}成功...", Path.GetFileName(filePath)));
                                            }
                                        }
                                    }
                                }
                            }
                            LoadLocalPatientInfo();
                            //if (string.IsNullOrEmpty(cboStethoscope.Text)) return;
                            //var control = contextMenuStrip1.SourceControl.Parent;
                            //if (control == null) return;
                            //UpdateData(StetName, control.Name);
                        }
                        break;
                }
            }

        }
        void PlayAudio(string filePath, int TakeTime)
        {
            Mediator.isBusy = true;
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == cboStethoscope.Text);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                MessageBox.Show(string.Format("听诊器 {0} 尚未连接!", stethoscope.Name));
                return;
            }
            Mediator.ShowMsg("开始播放文件..." + filePath);
            FormProcessBar formProcessBar = new FormProcessBar(true)
            {
                ProgressBarStyle = ProgressBarStyle.Continuous,
                ProgressBarMaxValue = TakeTime,
                BtnText="停止播放"
            };
            Thread pairThread = new Thread(() =>
              {
                  
                  formProcessBar.TimerCallBackEvent += () =>
                      {
                          if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
                          Invoke(new MethodInvoker(delegate()
                          {
                              formProcessBar.Title = string.Format("音频播放中... {0} 秒", formProcessBar.Times);
                              if (formProcessBar.Times <= formProcessBar.ProgressBarMaxValue)
                                  formProcessBar.ProgressBarValue = formProcessBar.Times;
                              else
                                  formProcessBar.Close();
                          }));
                      };
                  var bytes = File.ReadAllBytes(filePath);
                  stethoscope.StartAudioOutput();
                  stethoscope.AudioOutputStream.Write(bytes, 0, bytes.Length);
              });
            pairThread.Start();
            formProcessBar.ShowDialog();
            formProcessBar.TimerEnable = false;
            stethoscope.StopAudioOutput();
            Mediator.ShowMsg("播放文件完毕..." + filePath);
            Mediator.WriteLog(this.Name, string.Format("文件 {0} 播放成功...", Path.GetFileName(filePath)));
            Mediator.isBusy = false;
        }

         

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboStethoscope.Text)) return;
            var control = contextMenuStrip1.SourceControl.Parent;
            if (control == null) return;
            UpdateData(StetName, control.Name);
        }
        public void  UpdateData(string stetName, string tabPageName)
        {
            switch (tabPageName)
            {
                case "tpLocal":
                    {

                        LoadLocalPatientInfo();
                        //LoadLocalAudio();
                    }
                    break;
                case "tpDown":
                    {
                        LoadDownAudio();
                    }
                    break;
            }
        }
        void LoadDownAudio()
        {
            string querySql = @"select PatientGUID,PatientType,PatientID,PatientName,Sharer,ShareTime,DownTime from PatientInfoDown where Downer={0} order by DownTime desc ";
            var dt = Mediator.sqliteHelper.ExecuteDatatable(querySql, StetName);
            dgv_DownLoad.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                dgv_DownLoad.Rows.Add(
                    dr[0],   Setting.GetPatientNameByType((int)dr[1]),dr[2],dr[3],dr[4],dr[5],dr[6] 
                    );
            }
        }
        private void tabLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateData(StetName, tabLocal.SelectedTab.Name);
        }

        private void dgv_DownLoad_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var PGUID=dgv_DownLoad.Rows[e.RowIndex].Cells[0].Value+"";
                switch (dgv_DownLoad.Columns[e.ColumnIndex].Name)
                {
                    case "btnDownDetail":
                        {
                            var dt = Mediator.sqliteHelper.ExecuteDatatable("select * from PatientInfoDown where PatientGUID={0} and Downer={1}",  PGUID ,this.cboStethoscope.Text);
                            var row = dt.Rows[0];
                            var formAudioDetail = new FormAudioDetail()
                            {
                                StetName=this.cboStethoscope.Text,
                                PatientGUID = row["PatientGUID"] + "",
                                PatientID = row["PatientID"] + "",
                                PatientName = row["PatientName"] + "",
                                DocName = row["DocName"] + "",
                                DocDiagnose = row["DocDiagnose"] + "",
                                DocRemark = row["DocRemark"] + "",
                                His = row["Flag"] + "",
                                IInfo = new LocalDown()
                            };
                            formAudioDetail.ShowDialog();
                        }
                        break;
                    case "btnDownDelete":
                        {
                            if (DialogResult.OK == MessageBox.Show("你确定要删除该记录及其文件吗", "删除录音提示", MessageBoxButtons.OKCancel))
                            {
                                var k = Mediator.sqliteHelper.ExecuteNonQuery("delete from PatientInfoDown where PatientGUID={0} and Downer={1}", PGUID, this.cboStethoscope.Text);
                                var dt = Mediator.sqliteHelper.ExecuteDatatable("select * from AudioInfoDown where PGUID={0}", PGUID);
                                foreach (DataRow row in dt.Rows)
                                {
                                    var guid = row["GUID"] + "";
                                    var recordTime = (DateTime)row["RecordTime"];
                                    string filePath = Path.Combine(Setting.localData, @"DevicesData\DowmLoad\" + recordTime.Year
                      + "\\" + recordTime.Month + "\\" + recordTime.Day + "\\" + guid + ".MP3");
                                    if (File.Exists(filePath))
                                        File.Delete(filePath);
                                }
                                k = Mediator.sqliteHelper.ExecuteNonQuery("delete from AudioInfoDown where PGUID={0}", PGUID);
                                MessageBox.Show("删除成功");
                            }
                            LoadDownAudio();
                        }
                        break;
                }
            }
        }


         
       
    }
}

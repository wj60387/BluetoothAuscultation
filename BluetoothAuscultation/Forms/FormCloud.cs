using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using System.Threading;
using BluetoothAuscultation.Devices;
using MMM.HealthCare.Scopes.Device;
using ProtocalData.Protocol.Derive;
using System.Data;
using BluetoothAuscultation.Models;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BluetoothAuscultation.IGetAudioInfo;

namespace BluetoothAuscultation.Forms 
{
    public partial class FormCloud : DockContentEx
    {
        public FormCloud()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormCloud_Load);
            this.SetBackColor(Setting.themeColor);
            this.cboStethoscope.DropDown+=new EventHandler(cboStethoscope_DropDown);
             }

        void FormCloud_Load(object sender, EventArgs e)
        {
            this.dgv_upload_share.Image = Setting.ImageShare;
            this.dgv_upload_download.Image = Setting.ImageDownload;
            this.dgvShareDown.Image = Setting.ImageDownload;
            this.dgvShareDelete.Image = Setting.ImageDelete;
            this.dgv_upload_detail.Image = Setting.ImageDetail;
            this.dgv_upload_delete.Image = Setting.ImageDelete;
            this.dgvShareDetail.Image = Setting.ImageDetail;

            this.dgv_tpUpload.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dgv_ShareMe.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dgv_tpUpload.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
            this.dgv_ShareMe.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
            
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
            //if(cboStethoscope.Items.Count>0)
            //cboStethoscope.SelectedIndex = 0;
        }
        public string StetName
        {
            get
            {
                if (cboStethoscope.SelectedItem != null)
                {
                    var stetItem = cboStethoscope.SelectedItem as StetItem;
                    if (stetItem != null)
                        return stetItem.stetName;
                }
                return string.Empty;
            }
        }
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StetName))
            {
                UpdateData(StetName, tabMain.SelectedTab.Name);
                Mediator.ShowMsg(string.Format("刷新听诊器{0} {1} 数据", StetName, tabMain.SelectedTab.Text));
            }
        }
        void UpdateData(string StetName,string tabPageName)
        {
            switch (tabPageName)
            {

                case "tpUpload":
                    {
                        if (!Setting.isConnected)
                        {
                            MessageBox.Show("网络连接异常...");
                            return;
                        }
                        dgv_tpUpload.Rows.Clear();
                        string sql = @"select * from PatientInfo where StetName={0} order by CreateTime desc";
                        using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                        {
                            MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { StetName });
                            if (ds != null && ds.Tables.Count > 0)
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    dgv_tpUpload.Rows.Add(dr["PatientGUID"] + ""
                                    , Setting.GetPatientNameByType(int.Parse(dr["PatientType"] + ""))
                                    , dr["PatientID"] + ""
                                    , dr["PatientName"] + ""
                                    , dr["Flag"]
                                    , dr["CreateTime"]
                                    );
                                }

                            Mediator.WriteLog(this.Name, string.Format("刷新已上载数据,执行的sql语句是{0},参数是:{1}...", sql,StetName));


                        }
                    }
                    break;
                case "tpServer":
                    {
                        if (!Setting.isConnected)
                        {
                            MessageBox.Show("网络连接异常...");
                            return;
                        }
                        dgv_ShareMe.Rows.Clear();
//                        string sql = @"select a.*,b.SrcStetName,b.ToStetName,b.ShareTime from PatientInfo a
// inner join AudioShare b
//  on a.PatientGUID=b.PatientGUID   where b.ToStetName={0} order by ShareTime desc ";
                        string sql = @"select b.*,a.PatientID,a.Flag from AudioShare b left join PatientInfo a
  on a.PatientGUID=b.PatientGUID  where b.ToStetName={0} order by b.ShareTime desc";
                        using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                        {
                            MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { StetName });
                            if (ds != null && ds.Tables.Count > 0)
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    dgv_ShareMe.Rows.Add(
                                        dr["PatientGUID"] + ""
                                    , Setting.GetPatientNameByType(int.Parse(dr["PatientType"] + ""))
                                    , dr["PatientID"] + ""
                                    , dr["PatientName"] + ""
                                    , dr["Flag"] + ""
                                    , dr["SrcStetName"] + ""
                                    , dr["ShareTime"]
                                    );
                                }
                            Mediator.WriteLog(this.Name, string.Format("刷新分享给我数据数据,执行的sql语句是{0},参数是:{1}...", sql, StetName));

                        }
                    }
                    break;
              
            }
        }
       
        private void cboStethoscope_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StetName))
            {
                if (tabMain.SelectedTab != null)
                {
                    UpdateData(StetName, tabMain.SelectedTab.Name);
                    Mediator.ShowMsg(string.Format("刷新听诊器{0} {1} 数据", StetName, tabMain.SelectedTab.Text));
                }
            }
        }
      
        /// <summary>
        /// 上传的听诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_tpUpload_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Setting.isConnected)
            {
                MessageBox.Show("网络连接异常...");
                return;
            }
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                 var PGUID=dgv_tpUpload.Rows[e.RowIndex].Cells[0].Value + "";
                 var PatientType = Setting.GetPatientTypeByName(dgv_tpUpload.Rows[e.RowIndex].Cells[1].Value + "");
                 //var PatientID = dgv_tpUpload.Rows[e.RowIndex].Cells[2].Value + "";
                 var PatientName = dgv_tpUpload.Rows[e.RowIndex].Cells[3].Value + "";
                 var CreateTime = (DateTime)dgv_tpUpload.Rows[e.RowIndex].Cells[5].Value  ;
                switch (dgv_tpUpload.Columns[e.ColumnIndex].Name)
                {
                    case "dgv_upload_download":
                        {
                            Mediator.ShowMsg("开始下载已上传的录音文件...");
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                #region 下载患者信息
                                string sqlPatient = "select * from PatientInfo where PatientGUID={0}";
                                var ds = Mediator.remoteService.ExecuteDataset(sqlPatient, new string[] { PGUID });
                                if(ds.Tables[0].Rows.Count==0)
                                {
                                    MessageBox.Show("分享信息 " + PGUID + " 被分享者删除了...");
                                    return;
                                }
                                var r = ds.Tables[0].Rows[0];
                                var patientInfo = new
                                {
                                    StetName = r["StetName"] + "",
                                    PatientGroupID = (int)r["PatientGroupID"],
                                    PatientID = r["PatientID"]+"",
                                    PatientName = r["PatientName"] + "",
                                    PatientSex = r["PatientSex"] + "",
                                    PatientAge = (int)r["PatientAge"],
                                    DocName = r["DocName"] + "",
                                    DocDiagnose = r["DocDiagnose"] + "",
                                    DocRemark = r["DocRemark"] + "",
                                    Flag = r["Flag"] + "",
                                };
                                var sqlQueryDown = "select count(0) from PatientInfoDown where PatientGUID={0} and Downer={1}";
                                if (int.Parse(Mediator.sqliteHelper.ExecuteScalar(sqlQueryDown, PGUID,this.cboStethoscope.Text).ToString()) == 0)
                                {
                                    //下载自己上传的听诊  分享者=自己
                                    var h = Mediator.sqliteHelper.ExecuteNonQuery(@"insert into PatientInfoDown(PatientGUID,StetName,PatientType,PatientGroupID ,PatientID,PatientName ,PatientSex,PatientAge
  ,DocName,DocDiagnose,DocRemark,Flag,Sharer,ShareTime,Downer ) select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", PGUID, patientInfo.StetName, PatientType, patientInfo.PatientGroupID, patientInfo.PatientID, patientInfo.PatientName, patientInfo.PatientSex, patientInfo.PatientAge
     , patientInfo.DocName, patientInfo.DocDiagnose, patientInfo.DocRemark,patientInfo.Flag, null,null,this.cboStethoscope.Text);
                                    if (h > 0) Mediator.ShowMsg(string.Format("患者{0}信息下载成功...", patientInfo.PatientName));
                                    else return;
                                }
                                else
                                {
                                    var h = Mediator.sqliteHelper.ExecuteNonQuery(@"update PatientInfoDown set DocName={0}, DocDiagnose={1},DocRemark={2},PatientID={4},PatientName={5},Flag={6} ,PatientSex={7},PatientAge={8}
  where PatientGUID={3}", patientInfo.DocName, patientInfo.DocDiagnose, patientInfo.DocRemark, PGUID, patientInfo.PatientID, patientInfo.PatientName, patientInfo.Flag,patientInfo.PatientSex, patientInfo.PatientAge);
                                    // if (h > 0) Mediator.ShowMsg(string.Format("患者{0}信息更新成功...", PatientName));
                                    if (h <= 0) return;
                                }
                                //写下载记录
                                string sqlInsertDownLog = "insert into AudioDownLoadInfo (GUID,Downer) select {0},{1}";
                                Mediator.remoteService.ExecuteNonQuery(sqlInsertDownLog, new string[] { PGUID, this.cboStethoscope.Text });

                                #endregion
                                #region  下载录音信息 每次覆盖下载 保证最新
                                var sqlDelLocal = "delete from AudioInfoDown where PGUID={0}";
                                var c = Mediator.sqliteHelper.ExecuteNonQuery(sqlDelLocal, PGUID);
                                var sqlReomte = "select * from AudioInfo where PGUID={0}";
                                var dsAudioInfo = Mediator.remoteService.ExecuteDataset(sqlReomte, new string[] { PGUID });
                                
                                foreach (DataRow row in dsAudioInfo.Tables[0].Rows)
                                {
                                    var audioInfo = new
                                    {
                                        GUID = row["GUID"] + "",
                                        PGUID = row["PGUID"] + "",
                                        StetName = row["StetName"] + "",
                                        Part = row["Part"] + "",
                                        TakeTime = (int)row["TakeTime"],
                                        RecordTime = (DateTime)row["RecordTime"],
                                        UpLoadTime = (DateTime)row["CreateTime"],
                                    };
                                    string sqlInsert = @"insert into AudioInfoDown( GUID, PGUID, StetName , Part, TakeTime 
  , RecordTime ,UpLoadTime ) select {0},{1},{2} ,{3},{4}  ,{5},{6}";
                                    var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsert, audioInfo.GUID, audioInfo.PGUID, audioInfo.StetName, audioInfo.Part, audioInfo.TakeTime
  , audioInfo.RecordTime, audioInfo.UpLoadTime);
                                    if (n > 0)
                                    {
                                        string fileLocalPath = Path.Combine(Setting.localData, @"DevicesData\DowmLoad\" + audioInfo.RecordTime.Year
                      + "\\" + audioInfo.RecordTime.Month + "\\" + audioInfo.RecordTime.Day + "\\" + audioInfo.GUID + ".MP3");
                                        if (File.Exists(fileLocalPath))
                                        {
                                            File.Delete(fileLocalPath);
                                        }
                                        var fileRemotePath = Mediator.remoteService.GetFilePath2(audioInfo.RecordTime, audioInfo.GUID);
                                        var fileSize = Mediator.remoteService.GetFileLength(fileRemotePath);
                                        //下载文件
                                        if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                                        {
                                            Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                                        }
                                        using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                                        {
                                            long position = 0;
                                            while (position < fileSize)
                                            {
                                                var bytes = Mediator.remoteService.DownLoadFile(fileRemotePath, position, 24 * 1024);
                                                position += bytes.Length;
                                                stream.Write(bytes, 0, bytes.Length);
                                            }
                                            stream.Close();

                                        }
                                        Mediator.WriteLog(this.Name, string.Format("文件{0}文件下载成功,下载的听诊器序号为{1}...", audioInfo.GUID, this.cboStethoscope.Text));
                                        Mediator.ShowMsg(string.Format("文件{0}下载成功...", audioInfo.GUID));
                                    }
                                }
                                //删除本地所有
                                #endregion
                                #region 更新下载记录

                                #endregion

                                //下载附件
                                var remoteRoot=Mediator.remoteService.GetRoot();
                                var remoteDir = Path.Combine(remoteRoot,string.Format("Enclosure\\{0}", PGUID));
                                if(Mediator.remoteService.isExistFolder(remoteDir))
                                {
                                    var remoteFiles = Mediator.remoteService.GetFolderFiles(remoteDir, "*.*", true);
                                    var localFiles = Directory.GetFiles(string.Format("Enclosure\\Down\\{0}", PGUID));
                                   
                                    if(remoteFiles!=null)
                                    {
                                        foreach (var file in localFiles)
                                        {
                                            if (!remoteFiles.Select(f => Path.GetFileName(f)).Contains(Path.GetFileName(file)))
                                            {
                                                File.Delete(file);
                                            }
                                        }
                                        foreach (var remoteFile in remoteFiles)
                                        {
                                            var fileSize = Mediator.remoteService.GetFileLength(remoteFile);
                                            var fileLocalPath = string.Format("Enclosure\\Down\\{0}\\{1}", PGUID, Path.GetFileName(remoteFile));
                                            if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                                            {
                                                Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                                            }
                                            if (File.Exists(fileLocalPath))
                                            {
                                                File.Delete(fileLocalPath);
                                            }
                                            using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                                            {
                                                long position = 0;
                                                while (position < fileSize)
                                                {
                                                    var bytes = Mediator.remoteService.DownLoadFile(remoteFile, position, 24 * 1024);
                                                    position += bytes.Length;
                                                    stream.Write(bytes, 0, bytes.Length);
                                                }
                                                stream.Close();

                                            }
                                        }
                                    }
                                    Mediator.ShowMsg("下载完毕！");

                                }
                            }
                        }
                        break;
                    case "dgv_upload_share":
                        {
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                FormShare formShare = new FormShare(this.cboStethoscope.Text);
                                if (formShare.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    var selectStets = formShare.SelectedStethoscope;
                                    string sql = "SELECT * from  AudioShare where SrcStetName={0} and PatientGUID={1}";
                                    var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { this.cboStethoscope.Text, PGUID, });
                                    string insertSql = "insert into AudioShare(PatientGUID,PatientType,PatientName,SrcStetName,ToStetName) select {0},{1},{2},{3},{4}";
                                    foreach (var toName in formShare.SelectedStethoscope)
                                    {
                                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Select("ToStetName='" + toName + "'").Length > 0)
                                        {
                                            Mediator.ShowMsg(string.Format("患者{0}听诊记录已经被{1}分享过给{2}了...", PatientName, this.cboStethoscope.Text, toName));
                                            continue;
                                        }
                                        int n = Mediator.remoteService.ExecuteNonQuery(insertSql, new string[] { PGUID,PatientType+"",PatientName, this.cboStethoscope.Text, toName });
                                        if (n > 0)
                                            Mediator.ShowMsg(string.Format("患者{0}听诊记录成功被{1}分享给{2}...", PatientName, this.cboStethoscope.Text, toName));
                                        Mediator.WriteLog(this.Name, string.Format("患者{0}听诊记录被{1}分享给{2}...", PatientName, this.cboStethoscope.Text, toName));

                                    }

                                }
                            }
                        }
                        break;

                    case "dgv_upload_detail":
                        {
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                var ds = Mediator.remoteService.ExecuteDataset("select * from PatientInfo where PatientGUID={0}", new string[] { PGUID });
                                var row = ds.Tables[0].Rows[0];
                                var formAudioDetail = new FormAudioDetail()
                                {
                                    PatientGUID = row["PatientGUID"] + "",
                                    PatientID = row["PatientID"] + "",
                                    PatientName = row["PatientName"] + "",
                                    PatientSex = row["PatientSex"] + "",
                                    PatientAge = (int)row["PatientAge"],
                                    DocName = row["DocName"] + "",
                                    DocDiagnose = row["DocDiagnose"] + "",
                                    DocRemark = row["DocRemark"] + "",
                                    His = row["Flag"] + "",
                                  
                                    IInfo = new CloudUpload()
                                };
                                formAudioDetail.ShowDialog();
                            }
                        }
                        break;
                    case "dgv_upload_delete":
                        if (DialogResult.OK == MessageBox.Show("你确定要删除该记录及其文件吗", "删除录音提示", MessageBoxButtons.OKCancel))
                        {
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                var k = Mediator.remoteService.ExecuteNonQuery("delete from PatientInfo where PatientGUID={0}", new string[] { PGUID});
                                if (k > 0)
                                {
                                    dgv_tpUpload.Rows.RemoveAt(e.RowIndex);
                                    MessageBox.Show("删除上传记录成功...");
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 分享给我的听诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_ShareMe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var PGUID = dgv_ShareMe.Rows[e.RowIndex].Cells[0].Value + "";
            var PatientType = Setting.GetPatientTypeByName(dgv_ShareMe.Rows[e.RowIndex].Cells[1].Value + "");
            var PatientID = dgv_ShareMe.Rows[e.RowIndex].Cells[2].Value + "";
            var PatientName = dgv_ShareMe.Rows[e.RowIndex].Cells[3].Value + "";
            switch (dgv_ShareMe.Columns[e.ColumnIndex].Name)
            {
                case "dgvShareDown":
                    {
                        using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                        {
                            MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            #region 下载患者信息
                            string sqlPatient = @" select a.*,b.SrcStetName,b.ToStetName,b.ShareTime from PatientInfo a inner join AudioShare b
  on a.PatientGUID=b.PatientGUID where a.PatientGUID={0} and b.ToStetName={1}";
                            var ds = Mediator.remoteService.ExecuteDataset(sqlPatient, new string[] { PGUID ,this.cboStethoscope.Text});
                            var r = ds.Tables[0].Rows[0];
                            var patientInfo = new
                            {
                                PatientGroupID = (int)r["PatientGroupID"],
                                DocName = r["DocName"] + "",
                                DocDiagnose = r["DocDiagnose"] + "",
                                DocRemark = r["DocRemark"] + "",
                                Flag = r["Flag"] + "",
                                Sharer = r["SrcStetName"] + "",
                                ShareTime = (DateTime)r["ShareTime"] ,
                            };
                            var sqlQueryDown = "select count(0) from PatientInfoDown where PatientGUID={0} and Downer={1}";
                            if (int.Parse(Mediator.sqliteHelper.ExecuteScalar(sqlQueryDown, PGUID, this.cboStethoscope.Text).ToString()) == 0)
                            {
                                //下载别人分享的
                                var h = Mediator.sqliteHelper.ExecuteNonQuery(@"insert into PatientInfoDown(PatientGUID,PatientType,PatientGroupID ,PatientID,PatientName 
  ,DocName,DocDiagnose,DocRemark,Flag,Sharer,ShareTime,Downer ) select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", PGUID, PatientType, patientInfo.PatientGroupID, PatientID, PatientName
 , patientInfo.DocName, patientInfo.DocDiagnose, patientInfo.DocRemark,patientInfo.Flag, patientInfo.Sharer, patientInfo.ShareTime, this.cboStethoscope.Text);
                                if (h > 0) Mediator.ShowMsg(string.Format("患者{0}信息下载成功...", PatientName));
                                else return;
                            }
                            else
                            {
                                var h = Mediator.sqliteHelper.ExecuteNonQuery(@"update PatientInfoDown set DocName={0}, DocDiagnose={1},DocRemark={2}
  where PatientGUID={3}", patientInfo.DocName, patientInfo.DocDiagnose, patientInfo.DocRemark, PGUID);
                                // if (h > 0) Mediator.ShowMsg(string.Format("患者{0}信息更新成功...", PatientName));
                                if (h <= 0) return;
                            }
                            //写下载记录
                            string sqlInsertDownLog = "insert into AudioDownLoadInfo (GUID,Downer) select {0},{1}";
                            Mediator.remoteService.ExecuteNonQuery(sqlInsertDownLog, new string[] { PGUID, this.cboStethoscope.Text });

                            #endregion
                            #region  下载录音信息 每次覆盖下载 保证最新
                            var sqlDelLocal = "delete from AudioInfoDown where PGUID={0} ";
                            var c = Mediator.sqliteHelper.ExecuteNonQuery(sqlDelLocal, PGUID);
                            var sqlReomte = "select * from AudioInfo where PGUID={0}  ";
                            var dsAudioInfo = Mediator.remoteService.ExecuteDataset(sqlReomte, new string[] { PGUID });
                            foreach (DataRow row in dsAudioInfo.Tables[0].Rows)
                            {
                                var audioInfo = new
                                {
                                    GUID = row["GUID"] + "",
                                    PGUID = row["PGUID"] + "",
                                    StetName = row["StetName"] + "",
                                    Part = row["Part"] + "",
                                    TakeTime = (int)row["TakeTime"],
                                    RecordTime = (DateTime)row["RecordTime"],
                                    UpLoadTime = (DateTime)row["CreateTime"],
                                };
                                string sqlInsert = @"insert into AudioInfoDown( GUID, PGUID, StetName , Part, TakeTime 
  , RecordTime, UpLoadTime ) select {0},{1},{2} ,{3},{4}  ,{5},{6}";
                                var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsert, audioInfo.GUID, audioInfo.PGUID, audioInfo.StetName, audioInfo.Part, audioInfo.TakeTime
, audioInfo.RecordTime, audioInfo.UpLoadTime);
                                if (n > 0)
                                {
                                    string fileLocalPath = Path.Combine(Setting.localData, @"DevicesData\DowmLoad\" + audioInfo.RecordTime.Year
                  + "\\" + audioInfo.RecordTime.Month + "\\" + audioInfo.RecordTime.Day + "\\" + audioInfo.GUID + ".MP3");
                                    if (File.Exists(fileLocalPath))
                                    {
                                        File.Delete(fileLocalPath);
                                    }
                                    var fileRemotePath = Mediator.remoteService.GetFilePath2(audioInfo.RecordTime, audioInfo.GUID);
                                    var fileSize = Mediator.remoteService.GetFileLength(fileRemotePath);
                                    //下载文件
                                    if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                                    {
                                        Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                                    }
                                    using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                                    {
                                        long position = 0;
                                        while (position < fileSize)
                                        {
                                            var bytes = Mediator.remoteService.DownLoadFile(fileRemotePath, position, 24 * 1024);
                                            position += bytes.Length;
                                            stream.Write(bytes, 0, bytes.Length);
                                        }
                                        stream.Close();

                                    }
                                    Mediator.WriteLog(this.Name, string.Format("文件{0}文件下载成功,下载的听诊器序号为{1}...", audioInfo.GUID, this.cboStethoscope.Text));
                                    Mediator.ShowMsg(string.Format("文件{0}下载成功...", audioInfo.GUID));
                                }
                            }
                            #endregion

                            //下载附件
                            var remoteRoot = Mediator.remoteService.GetRoot();
                            var remoteDir = Path.Combine(remoteRoot, string.Format("Enclosure\\{0}", PGUID));
                            if (Mediator.remoteService.isExistFolder(remoteDir))
                            {
                                var remoteFiles = Mediator.remoteService.GetFolderFiles(remoteDir, "*.*", true);

                                var localFiles = Directory.GetFiles(string.Format("Enclosure\\Down\\{0}", PGUID));

                                
                                if (remoteFiles != null)
                                {
                                    foreach (var file in localFiles)
                                    {
                                        if (!remoteFiles.Select(f => Path.GetFileName(f)).Contains(Path.GetFileName(file)))
                                        {
                                            File.Delete(file);
                                        }
                                    }
                                    foreach (var remoteFile in remoteFiles)
                                    {
                                        var fileSize = Mediator.remoteService.GetFileLength(remoteFile);
                                        var fileLocalPath = string.Format("Enclosure\\Down\\{0}\\{1}", PGUID, Path.GetFileName(remoteFile));
                                        if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                                        {
                                            Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                                        }
                                        if (File.Exists(fileLocalPath))
                                        {
                                            File.Delete(fileLocalPath);
                                        }
                                        using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                                        {
                                            long position = 0;
                                            while (position < fileSize)
                                            {
                                                var bytes = Mediator.remoteService.DownLoadFile(remoteFile, position, 24 * 1024);
                                                position += bytes.Length;
                                                stream.Write(bytes, 0, bytes.Length);
                                            }
                                            stream.Close();

                                        }
                                    }
                                }
                                Mediator.ShowMsg("下载完毕！");
                            }

                        }
                    }
                    break;
                case "dgvShareDetail":
                        {
                            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                            {
                                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                                var ds = Mediator.remoteService.ExecuteDataset("select * from PatientInfo where PatientGUID={0}", new string[] { PGUID });
                                var row = ds.Tables[0].Rows[0];
                                var formAudioDetail = new FormAudioDetail()
                                {
                                    PatientGUID = row["PatientGUID"] + "",
                                    PatientID = row["PatientID"] + "",
                                    PatientName = row["PatientName"] + "",
                                    DocName = row["DocName"] + "",
                                    PatientSex = row["PatientSex"] + "",
                                    PatientAge = (int)row["PatientAge"],
                                    DocDiagnose = row["DocDiagnose"] + "",
                                    DocRemark = row["DocRemark"] + "",
                                    His = row["Flag"] + "",
                                    IInfo = new CloudShare()
                                };
                                formAudioDetail.ShowDialog();
                            }
                        }
                    break;
                case "dgvShareDelete":
                    if (DialogResult.OK == MessageBox.Show("你确定要删除该分享记录吗", "删除分享记录提示", MessageBoxButtons.OKCancel))
                    {
                        using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
                        {
                            MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            var k = Mediator.remoteService.ExecuteNonQuery("delete from AudioShare where PatientGUID={0} and ToStetName={1}", new string[] { PGUID ,this.cboStethoscope.Text});
                            if(k>0)
                            {
                                dgv_ShareMe.Rows.RemoveAt(e.RowIndex);
                                MessageBox.Show("删除分享记录成功...");
                            }
                        }
                        
                    }
                    break;
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StetName)) return;
            var control = contextMenuStrip1.SourceControl.Parent;
            if (control == null) return;
            UpdateData(StetName, control.Name);
        }

    }
}

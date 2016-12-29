using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BluetoothAuscultation.Devices;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BluetoothAuscultation.IGetAudioInfo;

namespace BluetoothAuscultation.Forms
{
    public partial class FormAudioRecord : Form
    {
        public FormAudioRecord()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormAudioRecord_Load);
        }

        void FormAudioRecord_Load(object sender, EventArgs e)
        {
            txtDocName.ReadOnly = true;
            txtPatientId.ReadOnly = true;
            if (string.IsNullOrEmpty(txtPatientId.Text))
                txtPatientId.ReadOnly = false;
            var filePaths = Directory.GetFiles(@"Image\Part");
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GUID", HeaderText = "GUID", Visible = false,Width=0 });
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { HeaderText = "缩略图" ,Width=60});
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Part", HeaderText = "部位",  Width = 80 });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "isRecord", HeaderText = "是否已录音", Width = 40, FillWeight = 150.0f });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "RecordTime", HeaderText = "录制时间", Width = 150 ,FillWeight= 250.0f});
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TakeTime", HeaderText = "时长(秒)", Width = 40, FillWeight = 120.0f });
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnRecord", HeaderText = "录音", Image = Setting.ImageRecord });
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnPlay", HeaderText = "播放", Image = Setting.ImagePlay });
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnDelete", HeaderText = "删除", Image = Setting.ImageDelete });
            dataGridViewEx1.RowTemplate.Height = 66;
            dataGridViewEx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoadAudio();
            this.dataGridViewEx1.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dataGridViewEx1.RowTemplate.DefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            //this.dataGridViewEx1.RowTemplate.DefaultCellStyle.ForeColor = Color.FromArgb(215, 228, 242);
            txtDocName.Text = Setting.GetStetInfoByStetName(StetName).Owner;
            PatientType = Setting.GetStetInfoByStetName(StetName).StetType;
            if(isSave)
            {
                PatientAge = int.Parse(Setting.GetPatientAgeByGUID(this.PatientGUID));
                PatientSex = Setting.GetPatientSexByGUID(PatientGUID);
            }
            LoadFile();
        }
        void LoadAudio()
        {
            dataGridViewEx1.Rows.Clear();
            var filePaths = Directory.GetFiles(@"Image\Part");
            foreach (var file in filePaths)
            {
                dataGridViewEx1.Rows.Add("",Image.FromFile(file).GetThumbnailImage(60, 66, () => { return true; }, IntPtr.Zero), Path.GetFileNameWithoutExtension(file), "否");
            }
            if (!string.IsNullOrEmpty(PatientGUID))
            {
                string sqlQuery = "select * from AudioInfo where PGUID={0}  and  DelFlag<>1";
                var dt = Mediator.sqliteHelper.ExecuteDatatable(sqlQuery, PatientGUID);
                foreach (DataGridViewRow row in dataGridViewEx1.Rows)
                {
                    var part = row.Cells["Part"].Value + "";
                    var drs = dt.Select("Part='" + part + "'");
                    if (drs.Length > 0)
                    {
                        row.Cells["GUID"].Value = drs[0]["GUID"];
                        row.Cells["isRecord"].Value = "是";
                        row.Cells["RecordTime"].Value = drs[0]["RecordTime"];
                        row.Cells["TakeTime"].Value = drs[0]["TakeTime"];
                    }
                }
            }
        }
        public string[] FilePaths
        {
            get
            {
                return Directory.GetFiles(@"Image\Part");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtPatientId.Text))
            //{
            //    MessageBox.Show("患者ID不能为空");
            //    return;
            //}
            if (!Setting.isConnected)
            {
                MessageBox.Show("网络连接异常...");
                return;
            }
            string sqlInsertAudio = @"insert into AudioInfo(GUID,PGUID,StetName,Part,TakeTime,RecordTime)
                                     select {0},{1},{2},{3},{4},{5}";
            if (isSave)
            {
                string sqlUpdate = "update PatientInfo set PatientID={0}, PatientName={1},DocName={2},DocDiagnose={3},DocRemark={4},Flag={5},PatientSex={7},PatientAge={8} where PatientGUID={6}";
                var k = Mediator.sqliteHelper.ExecuteNonQuery(sqlUpdate, PatientID, PatientName, DocName, DocDiagnose, DocRemark, His, PatientGUID, PatientSex, PatientAge);
                string sqlQueryAudio = "select count(0) from AudioInfo where GUID={0}";
                //string sqlUpdateAudio = "";
                foreach (DataGridViewRow dr in dataGridViewEx1.Rows)
                {
                    var guid = dr.Cells["GUID"].Value + "";
                    if (string.IsNullOrEmpty(guid)) continue;
                    var part = dr.Cells["Part"].Value + "";
                    var takeTime = (int)dr.Cells["TakeTime"].Value; 
                    var recordTime = (DateTime)dr.Cells["RecordTime"].Value;
                    var q =  Mediator.sqliteHelper.ExecuteScalar(sqlQueryAudio, guid)+"";
                    if (q.Equals("0"))
                    {
                        var d = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsertAudio, guid, PatientGUID,
                            StetName, part, takeTime,
                            recordTime);
                    }

                }
                MessageBox.Show("修改成功");
                Mediator.ShowMsg(string.Format("修改患者{0}信息成功", PatientID));
            }
            else
            {
                string sqlInsert = @"insert into PatientInfo(
      [PatientGUID]
      ,[StetName]
      ,[PatientType]
      ,[PatientGroupID]
      ,[PatientID]
      ,[PatientName]
      ,[PatientSex]
      ,[PatientAge]
      ,[DocName]
      ,[DocDiagnose]
      ,[DocRemark],[Flag]) select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}";
                PatientGUID=Guid.NewGuid().ToString();
                var n = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsert, PatientGUID,StetName, PatientType,
    PatientGroupID, PatientID, PatientName, PatientSex, PatientAge, DocName, DocDiagnose, DocRemark, His);
                Mediator.ShowMsg(string.Format("添加患者{0}信息成功", PatientID));
               
                
                foreach (DataGridViewRow dr in dataGridViewEx1.Rows)
                {
                    var guid = dr.Cells["GUID"].Value + "";
                    if (string.IsNullOrEmpty(guid)) continue;
                    var part = dr.Cells["Part"].Value + "";
                    var takeTime = (int)dr.Cells["TakeTime"].Value;
                    var recordTime = (DateTime)dr.Cells["RecordTime"].Value;
                    var k = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsertAudio, guid, PatientGUID,
                        StetName, part, takeTime,
                        recordTime);
                    
                }
                MessageBox.Show("添加成功");
            }
            CanEdit = false;
            txtPatientId.ReadOnly = true;
        }

        public string His { get { return txtHis.Text; } set { txtHis.Text = value; } }
        public string PatientGUID { get; set; }
        public string StetName { get; set; }
        public int PatientType { get; set; }
        public int PatientGroupID { get { return Setting.authorizationInfo.GroupID;} }
        public string PatientID { get { return txtPatientId.Text; } set { txtPatientId.Text = value; } }
        public string PatientSex { 
            get { return radioButton1.Checked?"男":"女"; } 
            set {
                if (!new string[] { "男", "女" }.Contains(value)) throw new Exception("性别为男女,不能为"+value);
                radioButton1.Checked = "男" == value;
                radioButton2.Checked = "男" != value;
            } 
        }
        public int PatientAge { get { return (int)numAge.Value; } set { numAge.Value = value; } }
        public string PatientName { get { return txtPatientName.Text; } set { txtPatientName.Text = value; } }
        public string DocName { get { return txtDocName.Text; } set { txtDocName.Text = value; } }
        public string DocDiagnose { get { return txtDocDiagnose.Text; } set { txtDocDiagnose.Text = value; } }
        public string DocRemark { get { return txtDocRemark.Text; } set { txtDocRemark.Text = value; } }
        /// <summary>
        /// 能否编辑
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return txtPatientName.ReadOnly;
                
            }
            set
            {
                txtHis.ReadOnly = !value;
                txtPatientId.ReadOnly = !value;
                txtDocDiagnose.ReadOnly = !value;
                if(string.IsNullOrEmpty(txtDocName.Text))
                 txtDocName.ReadOnly = !value;
                txtDocRemark.ReadOnly = !value;
                txtPatientName.ReadOnly = !value;
                radioButton1.Enabled = value;
                radioButton2.Enabled = value;
                numAge.ReadOnly = !value;
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            CanEdit = true;
        }
        public bool isSave
        {
            get {
                return !string.IsNullOrEmpty(PatientGUID);
            }
        }
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if (!isSave)
            {
                MessageBox.Show("请先保存再上传");
                return;
            }
            if (!Setting.isConnected)
            {
                MessageBox.Show("网络连接异常...");
                return;
            }
            //1.上传患者信息
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                string sqlInsert = "exec P_UpLoadPatientInfo {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11} ";
                var k = Mediator.remoteService.ExecuteNonQuery(sqlInsert, new string[]{PatientGUID,StetName, PatientType+"",
    PatientGroupID+"", PatientID, PatientName,PatientSex,PatientAge+"", DocName, DocDiagnose, DocRemark,His});
                if (k > 0) Mediator.ShowMsg(string.Format("上传患者{0}信息成功...", PatientName));
                #region  增加和修改
                string sqlLocalAudio = "select * from AudioInfo where PGUID={0} and DelFlag<>1";
                var dt = Mediator.sqliteHelper.ExecuteDatatable(sqlLocalAudio, PatientGUID);
                foreach (DataRow row in dt.Rows)
                {
                    var GUID = row["GUID"] + "";
                    var PGUID = row["PGUID"] + "";
                    var Part = row["Part"] + "";
                    var TakeTime = (int)row["TakeTime"];
                    var RecordTime = (DateTime)row["RecordTime"];

                    string sqlInsertAudioInfo = "exec P_UpLoadAudioInfo {0},{1},{2},{3},{4},{5}";
                    var n = Mediator.remoteService.ExecuteNonQuery(sqlInsertAudioInfo, new string[]{GUID, PGUID, StetName , Part, TakeTime+"" 
  , RecordTime.ToString("yyyy-MM-dd HH:mm:ss")});
                    if (n > 0)
                    {
                        string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + RecordTime.Year
                 + "\\" + RecordTime.Month + "\\" + RecordTime.Day + "\\" + GUID + ".MP3");

                        if (!File.Exists(filePath)) return;
                        using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            var remoteFilePath = Mediator.remoteService.GetFilePath2(RecordTime, GUID);
                            while (true)
                            {
                                var readBytes = new byte[1024 * 24];
                                var readed = stream.Read(readBytes, 0, readBytes.Length);
                                if (readed <= 0) break;
                                Mediator.remoteService.UpLoadFile(remoteFilePath, stream.Position - readed, readBytes);
                            }
                            stream.Close();
                        }
                        Mediator.WriteLog(this.Name, string.Format("文件 {0} 上传成功...", GUID));
                        Mediator.ShowMsg(string.Format("文件 {0} 上传成功...", GUID));
                    }
                }
                #endregion
                string sqlLocalAudioDel = "select GUID from AudioInfo where PGUID={0} and DelFlag=1";
                var dtDel = Mediator.sqliteHelper.ExecuteDatatable(sqlLocalAudioDel, PatientGUID);
                foreach (DataRow row in dtDel.Rows)
                {
                    var GUID = row[0] + "";
                    Mediator.remoteService.ExecuteNonQuery("delete from AudioInfo where GUID={0}", new string[] { GUID });
                }
                //上传附件
                var root = Mediator.remoteService.GetRoot();
                var remoteFile = Path.Combine(root, "Enclosure\\" + PatientGUID);
                var localDir = string.Format(@"Enclosure\Local\{0}", PatientGUID);
                if (Directory.Exists(localDir))
                {
                    var localFiles = Directory.GetFiles(localDir);

                    var remoteFiles = Mediator.remoteService.GetFolderFiles(remoteFile, "*.*", true);
                    foreach (var file in remoteFiles)
                    {
                        if(!localFiles.Select(f=>Path.GetFileName(f)).Contains(Path.GetFileName(file)))
                        {
                            Mediator.remoteService.DeleteFile(file);
                        }
                    }
                    foreach (var localFile in localFiles)
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
           
            MessageBox.Show("上传成功...");
            this.Close(); 

        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //var his=this.cbHis.Text;
                var part= dataGridViewEx1.Rows[e.RowIndex].Cells["Part"].Value + "";
                switch (dataGridViewEx1.Columns[e.ColumnIndex].Name)
                {
                    #region 录音
                    case "btnRecord":
                        {
                            if ((dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value+"").Equals("是"))
                            {
                                MessageBox.Show("该部位已经录音！");
                                return;
                            }
                            //if (!isSave)
                            //{
                            //    MessageBox.Show("请先保存患者信息");
                            //    return;
                            //}
                            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == StetName);
                            if (stethoscopeArr.Count() == 0)
                                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
                            var stethoscope = stethoscopeArr.First();
                            if (!stethoscope.IsConnected)
                            {
                                MessageBox.Show(string.Format("听诊器 {0} 尚未连接!", stethoscope.Name));
                                return ;
                            }
                            FormProcessBar formProcessBar = new FormProcessBar(true);
                            var dtNow = DateTime.Now;
                            var guid = Guid.NewGuid().ToString();
                            Thread pairThread = new Thread(() =>
                            {
                                formProcessBar.TimerCallBackEvent += () =>
                                {
                                    Invoke(new MethodInvoker(delegate()
                                    {
                                        formProcessBar.Title = string.Format("音频录制中... {0} 秒", formProcessBar.Times);
                                    }));
                                };
                                stethoscope.StartAudioInput();
                                Mediator.ShowMsg(string.Format("听诊器 {0} 开始录音...", stethoscope.Name));
                                using (var stream = new MemoryStream())
                                {
                                    while (formProcessBar.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                                    {
                                        byte[] packet = new byte[128];
                                        int bytesRead = stethoscope.AudioInputStream.Read(packet, 0, packet.Length);
                                        stream.Write(packet, 0, bytesRead);
                                    }
                                    stethoscope.StopAudioInput();
                                    var bytes = stream.GetBuffer();
                                    stream.Close();


                                   // string audioFilePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + stethoscope.Name + "\\" + dtNow.Year + "\\" + dtNow.Month + "\\" + dtNow.Day + "\\" + guid + ".MP3");
                                    string audioFilePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\"     + dtNow.Year + "\\" + dtNow.Month + "\\" + dtNow.Day + "\\" + guid + ".MP3");
                                    if (!Directory.Exists(Path.GetDirectoryName(audioFilePath)))
                                    {
                                        Directory.CreateDirectory(Path.GetDirectoryName(audioFilePath));
                                    }
                                    File.WriteAllBytes(audioFilePath, bytes);
                                    Mediator.ShowMsg(string.Format("听诊器 {0} 录音完毕，时长 {1} 秒", stethoscope.Name, formProcessBar.Times));
                                    Mediator.WriteLog(this.Name, "音频录制成功...");
//                                    string sqlInsert = @"insert into AudioInfo(GUID,PGUID,StetName,Part,TakeTime,RecordTime)
//                                    select {0},{1},{2},{3},{4},{5}";
//                                    var k = Mediator.sqliteHelper.ExecuteNonQuery(sqlInsert, guid, PatientGUID,
//                                        StetName, part, formProcessBar.Times,
//                                        dtNow );
                                }
                            });
                            pairThread.Start();
                            formProcessBar.ShowDialog();
                            dataGridViewEx1.Rows[e.RowIndex].Cells["GUID"].Value = guid;
                            dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value = "是";
                            dataGridViewEx1.Rows[e.RowIndex].Cells["RecordTime"].Value = dtNow;
                            dataGridViewEx1.Rows[e.RowIndex].Cells["TakeTime"].Value = formProcessBar.Times;

                        }
                        break;
                    #endregion
                    #region 播放
                    case "btnPlay":
                        {
                            if ((dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value+"").Equals("否"))
                            {
                                MessageBox.Show("该部位未录音！");
                                return;
                            }
                            var recordTime = (DateTime)dataGridViewEx1.Rows[e.RowIndex].Cells["RecordTime"].Value;
                            var takeTime = (int)dataGridViewEx1.Rows[e.RowIndex].Cells["TakeTime"].Value;
                            var guid = dataGridViewEx1.Rows[e.RowIndex].Cells["GUID"].Value + "";//Mediator.sqliteHelper.ExecuteScalar("select Guid from AudioInfo where PGUID={0} and Part={1}", PatientGUID, part) + "";
                            if (string.IsNullOrEmpty(guid))
                            {
                                MessageBox.Show("未找到录音");
                                return;
                            }
                            //string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + stetName + "\\" + recordTime.Year
                            string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\"   + recordTime.Year
                  + "\\" + recordTime.Month + "\\" + recordTime.Day + "\\" + guid + ".MP3");
                            PlayAudio(filePath,  takeTime);
                        }
                        break;
                    #endregion
                    #region 删除
                    case "btnDelete":
                        {
                            if ((dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value + "").Equals("否"))
                            {
                                MessageBox.Show("该部位未录音！");
                                return;
                            }
                            if (DialogResult.OK == MessageBox.Show("你确定要删除该记录及其文件吗", "删除录音提示", MessageBoxButtons.OKCancel))
                            {

                                var recordTime = (DateTime)dataGridViewEx1.Rows[e.RowIndex].Cells["RecordTime"].Value;
                                var takeTime = (int)dataGridViewEx1.Rows[e.RowIndex].Cells["TakeTime"].Value;
                                var guid = dataGridViewEx1.Rows[e.RowIndex].Cells["GUID"].Value + "";//Mediator.sqliteHelper.ExecuteScalar("select GUID from AudioInfo where PGUID={0} and Part={1}", PatientGUID, part) + "";
                                if (string.IsNullOrEmpty(guid))
                                {
                                    MessageBox.Show("未找到录音");
                                    return;
                                }
                                // string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + StetName + "\\" + recordTime.Year
                                string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + recordTime.Year
                      + "\\" + recordTime.Month + "\\" + recordTime.Day + "\\" + guid + ".MP3");
                                if (Mediator.sqliteHelper.ExecuteNonQuery("update AudioInfo set DelFlag=1 where GUID={0}", guid) > 0)
                                {
                                    if (File.Exists(filePath))
                                    {
                                        File.Delete(filePath);
                                    }
                                }
                                dataGridViewEx1.Rows[e.RowIndex].Cells["GUID"].Value = "";
                                dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value = "否";
                                dataGridViewEx1.Rows[e.RowIndex].Cells["RecordTime"].Value = null;
                                dataGridViewEx1.Rows[e.RowIndex].Cells["TakeTime"].Value = null;

                            }
                        }
                        break;
                    #endregion
                }
            }
        }

        void PlayAudio(string filePath, int TakeTime)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show(string.Format("音频文件{0}不存在...", Path.GetFileName(filePath)));
                return;
            }
            Mediator.isBusy = true;
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == StetName);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                MessageBox.Show(string.Format("听诊器 {0} 尚未连接!", stethoscope.Name));
                return;
            }
            Mediator.ShowMsg("开始播放文件..." + Path.GetFileNameWithoutExtension(filePath));
            FormProcessBar formProcessBar = new FormProcessBar(true)
            {
                ProgressBarStyle = ProgressBarStyle.Continuous,
                ProgressBarMaxValue = TakeTime,
                BtnText = "停止播放"
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
            Mediator.ShowMsg("播放文件完毕..." + Path.GetFileNameWithoutExtension(filePath));
            Mediator.WriteLog(this.Name, string.Format("文件 {0} 播放成功...", Path.GetFileName(filePath)));
            Mediator.isBusy = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //var newHis = (int.Parse(this.His) + 1);
            //cbHis.Items.Add(newHis+"");
            //cbHis.Text = newHis + "";
        }
        string dirPtah = @"Enclosure\{0}";

        private void btnReport_Click(object sender, EventArgs e)
        {
            if(isSave)
            {
                var formFJ = new FormFJ(this.PatientGUID) { IFJ=new LoaclReocrd()};
                formFJ.ShowDialog();
            }
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (DialogResult.OK == ofd.ShowDialog())
            //{
            //    if (!isSave) this.PatientGUID = Guid.NewGuid().ToString();
            //    if(!Directory.Exists(string.Format(dirPtah,PatientGUID)))
            //        Directory.CreateDirectory((string.Format(dirPtah,PatientGUID)));
            //    var files = ofd.FileNames;
            //    foreach (var file in files)
            //    {
            //        File.Copy(file, Path.Combine(string.Format(dirPtah, PatientGUID), Path.GetFileName(file)));
            //    }
            //    MessageBox.Show("上传成功！");
            //}
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            //if (!isSave)
            //{
            //    MessageBox.Show("尚未上传附件！");
            //    return;
            //}
            //if (!Directory.Exists(string.Format(dirPtah, PatientGUID)))
            //{
            //    MessageBox.Show("尚未上传附件！");
            //    return;
            //}
            //System.Diagnostics.Process.Start(string.Format(dirPtah, PatientGUID));
        }
        string Dir = @"Enclosure\Local\{0}";
        private void label8_Click(object sender, EventArgs e)
        {
              var loaclDir = string.Format(Dir, this.PatientGUID);
              if (Directory.Exists(loaclDir))
              {
                  System.Diagnostics.Process.Start(loaclDir);
              }
        }
        void InsertImage()
        {
            if (!isSave)
            {
                MessageBox.Show("请先保存!");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图像文件(*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp";
            //ofd.Filter = "(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jgp;*.png;*.jpeg;*.bmp;*.gif";  
            if (DialogResult.OK == ofd.ShowDialog())
            {
                if (!Directory.Exists(string.Format(Dir, this.PatientGUID)))
                    Directory.CreateDirectory((string.Format(Dir, PatientGUID)));
                var files = ofd.FileNames;
                foreach (var file in files)
                {
                    var dest = Path.Combine(string.Format(Dir, PatientGUID), Path.GetFileName(file));
                    if (File.Exists(dest))
                    {
                        if (DialogResult.Yes == MessageBox.Show("是否覆盖已有的文件?", "覆盖同名文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            File.Delete(dest);

                        }
                        else
                        {
                            continue;
                        }
                    }
                    File.Copy(file, dest, true);


                }
                LoadFile();
                MessageBox.Show("上传成功！");
            }
        }

        void LoadFile()
        {
            if (isSave)
            {
                var loaclDir = string.Format(Dir, this.PatientGUID);
                if (Directory.Exists(loaclDir))
                {
                    var files = Directory.GetFiles(loaclDir);
                    panelImages.Controls.Clear();

                    if (files.Length < 3)
                    {
                        var _image = Setting.ImageJCBG.Clone() as Image;
                        var _thumbnailImage = _image.GetThumbnailImage(panelImages.Height, panelImages.Height, () => { return true; }, IntPtr.Zero);
                        Panel _panel = new Panel() { BackgroundImage = _thumbnailImage, Size = _thumbnailImage.Size };
                        _panel.Click += (sender, e) =>
                        {
                            InsertImage();
                        };
                        _panel.Dock = DockStyle.Left;
                        panelImages.Controls.Add(_panel);
                    }


                    foreach (var file in files)
                    {
                        var image = Setting.getImage(file);
                        if (image == null) continue;
                        var thumbnailImage = image.GetThumbnailImage(panelImages.Height, panelImages.Height, () => { return true; }, IntPtr.Zero);

                        using (Graphics g = Graphics.FromImage(thumbnailImage))
                        {
                            StringFormat sf = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            g.DrawString("-", new Font("微软雅黑", 16f, FontStyle.Bold), new SolidBrush(Color.Red),
                                new Rectangle(new Point(24, 0), new Size(8, 8)), sf);
                            g.Dispose();
                        }
                        Panel panel = new Panel()
                        {
                            BackgroundImage = thumbnailImage,
                            Name = Path.GetFileName(file),
                            Size = thumbnailImage.Size


                        };
                        panel.Click += (sender, e) =>
                        {
                            var point = panel.PointToClient(MousePosition);
                            var rect = new Rectangle(new Point(24, 0), new Size(8, 8));
                            if (rect.Contains(point))
                            {
                                if (DialogResult.OK == MessageBox.Show("确定要删除此报告?", "删除提示",
                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                                {
                                    var fileName = ((Panel)sender).Name;
                                    var filePath = Path.Combine(loaclDir, fileName);
                                    if (File.Exists(filePath))
                                    {
                                        File.Delete(filePath);
                                        LoadFile();
                                    }
                                }
                            }
                        };
                        panel.DoubleClick += (sender, e) =>
                            {
                                var fileName = ((Panel)sender).Name;
                                var filePath = Path.Combine(loaclDir, fileName);
                                if (File.Exists(filePath))
                                {
                                    System.Diagnostics.Process.Start(filePath);
                                }
                            };
                        panel.Dock = DockStyle.Left;
                        panelImages.Controls.Add(panel);
                    }

                }
                else
                {
                    panelImages.Controls.Clear();
                    var _image = Setting.ImageJCBG.Clone() as Image;
                    var _thumbnailImage = _image.GetThumbnailImage(panelImages.Height, panelImages.Height, () => { return true; }, IntPtr.Zero);
                    Panel _panel = new Panel() { BackgroundImage = _thumbnailImage, Size = _thumbnailImage.Size };
                    _panel.Click += (sender, e) =>
                    {
                        InsertImage();
                    };
                    _panel.Dock = DockStyle.Left;
                    panelImages.Controls.Add(_panel);
                }
            }
        }
         
    }
}

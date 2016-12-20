using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.IGetAudioInfo;
using System.IO;
using System.Threading;
using BluetoothAuscultation.Devices;

namespace BluetoothAuscultation.Forms
{
    public partial class FormAudioDetail : Form
    {
        public FormAudioDetail()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormAudioDetail_Load);
        }
        public string His { get { return txtHis.Text; } set { txtHis.Text = value; } }
        public string PatientGUID { get; set; }
        public string StetName { get; set; }
        public int PatientType { get; set; }
        public int PatientGroupID { get { return Setting.authorizationInfo.GroupID; } }
        public string PatientID { get { return txtPatientId.Text; } set { txtPatientId.Text = value; } }
        public string PatientName { get { return txtPatientName.Text; } set { txtPatientName.Text = value; } }
        public int PatientAge { get { return (int)numAge.Value; } set { numAge.Value = value; } }
        public string PatientSex
        {
            get { return radioButton1.Checked ? "男" : "女"; }
            set
            {
                if (!new string[] { "男", "女" }.Contains(value)) throw new Exception("性别为男女,不能为" + value);
                radioButton1.Checked = "男" == value;
                radioButton2.Checked = "男" != value;
            }
        }
        public string DocName { get { return txtDocName.Text; } set { txtDocName.Text = value; } }
        public string DocDiagnose { get { return txtDocDiagnose.Text; } set { txtDocDiagnose.Text = value; } }
        public string DocRemark { get { return txtDocRemark.Text; } set { txtDocRemark.Text = value; } }
        void FormAudioDetail_Load(object sender, EventArgs e)
        {
            var filePaths = Directory.GetFiles(@"Image\Part");
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { HeaderText = "缩略图" });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Part", HeaderText = "部位", Width = 80 });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "isRecord", HeaderText = "是否已录音", Width = 40, FillWeight = 150.0f });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "RecordTime", HeaderText = "录制时间", Width = 150, FillWeight = 250.0f });
            dataGridViewEx1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TakeTime", HeaderText = "时长(秒)", Width = 40, FillWeight = 120.0f });
            if (IInfo.isPlay)
                dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnPlay", HeaderText = "播放", Image = Setting.ImagePlay });
            //dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnDelete", HeaderText = "删除", Image = Setting.ImageDelete });
            dataGridViewEx1.RowTemplate.Height = 66;
            dataGridViewEx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoadAudio();
            this.dataGridViewEx1.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            LoadFile();
               
        }
        void LoadAudio()
        {
            dataGridViewEx1.Rows.Clear();
            var filePaths = Directory.GetFiles(@"Image\Part");
            foreach (var file in filePaths)
            {
                dataGridViewEx1.Rows.Add(Image.FromFile(file).GetThumbnailImage(60, 66, () => { return true; }, IntPtr.Zero), Path.GetFileNameWithoutExtension(file), "否");
            }
            var dt = IInfo.GetAudioByHis(PatientGUID);
            foreach (DataGridViewRow row in dataGridViewEx1.Rows)
            {
                var part = row.Cells["Part"].Value + "";
                var drs = dt.Select("Part='" + part + "'");
                if (drs.Length > 0)
                {
                    row.Cells["isRecord"].Value = "是";
                    row.Cells["RecordTime"].Value = drs[0]["RecordTime"];
                    row.Cells["TakeTime"].Value = drs[0]["TakeTime"];
                }
            }
        }
        public IGetInfo IInfo { get; set; }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var part = dataGridViewEx1.Rows[e.RowIndex].Cells["Part"].Value + "";
                switch (dataGridViewEx1.Columns[e.ColumnIndex].Name)
                {

                    case "btnPlay":
                        {
                            if ((dataGridViewEx1.Rows[e.RowIndex].Cells["isRecord"].Value + "").Equals("否"))
                            {
                                MessageBox.Show("该部位未录音！");
                                return;
                            }
                            var recordTime = (DateTime)dataGridViewEx1.Rows[e.RowIndex].Cells["RecordTime"].Value;
                            var takeTime = (int)dataGridViewEx1.Rows[e.RowIndex].Cells["TakeTime"].Value;
                            var guid = Mediator.sqliteHelper.ExecuteScalar("select Guid from AudioInfoDown where PGUID={0} and Part={1}", PatientGUID, part) + "";
                            if (string.IsNullOrEmpty(guid))
                            {
                                MessageBox.Show("未找到录音");
                                return;
                            }
                            //string filePath = Path.Combine(Setting.localData, @"DevicesData\AudioFiles\" + stetName + "\\" + recordTime.Year
                            string filePath = Path.Combine(Setting.localData, @"DevicesData\DowmLoad\" + recordTime.Year
                  + "\\" + recordTime.Month + "\\" + recordTime.Day + "\\" + guid + ".MP3");
                            if (File.Exists(filePath))
                                PlayAudio(filePath, takeTime);
                        }
                        break;
                    case "btnDelete":
                        MessageBox.Show("业务待确认,暂时不实现");
                        break;
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
            Mediator.ShowMsg("开始播放文件..." + filePath);
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
            Mediator.ShowMsg("播放文件完毕..." + filePath);
            Mediator.WriteLog(this.Name, string.Format("文件 {0} 播放成功...", Path.GetFileName(filePath)));
            Mediator.isBusy = false;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            //var formFJ = new FormFJ(this.PatientGUID) { IFJ = getFJ(), isUpLoadVisable = false, isOpenFolderVisable = isOpenBtnVisable() };
            //formFJ.ShowDialog();
        }

        void LoadFile()
        {
            var imageList = IInfo.GetImage(this.PatientGUID);
            foreach (var image in imageList)
            {
                var thumbnailImage = image.GetThumbnailImage(panelImages.Height, panelImages.Height, () => { return true; }, IntPtr.Zero);
                Panel panel = new Panel() { BackgroundImage = thumbnailImage, Tag = image.Clone(), Size = thumbnailImage.Size };
                panel.DoubleClick += (sender, e) =>
                    {
                        var imageTag = ((Panel)sender).Tag as Image;
                        var guid = Guid.NewGuid().ToString();
                        var filePath = string.Format("Enclosure\\Temp\\{0}", guid + ".png");
                        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        }
                        imageTag.Save(filePath);
                        if (File.Exists(filePath))
                        System.Diagnostics.Process.Start(filePath);
                    };
                panel.Dock = DockStyle.Left;
                panelImages.Controls.Add(panel);
            }
        }


        IGetFJ getFJ()
        {
            if (IInfo is LocalDown)
                return new LoaclDown();
            if (IInfo is CloudUpload)
                return new CloudReocrd();
            return new CloudDown();

        }
        bool isOpenBtnVisable()
        {
            if (IInfo is LocalDown)
                return true;
            return false;
        }

    }
}

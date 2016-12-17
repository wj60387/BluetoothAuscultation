using System;
using System.Collections.Generic;
using System.ComponentModel;
 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using BluetoothAuscultation.Devices;
using System.Threading;
using System.IO;
using MMM.HealthCare.Scopes.Device;
using System.Data;

namespace BluetoothAuscultation.Forms
{
    public partial class FormTeaching : DockContentEx
    {
        public FormTeaching()
        {
            InitializeComponent();
            this.SetBackColor(Setting.themeColor);
            this.Load += new EventHandler(FormTeaching_Load);
        }

        void FormTeaching_Load(object sender, EventArgs e)
        {
            this.dataGridViewEx1.RowsDefaultCellStyle = new DataGridViewCellStyle() { SelectionForeColor = Color.Black, Alignment = DataGridViewContentAlignment.MiddleCenter };
            this.dataGridViewEx1.Font = new Font(Setting.dgvFontFamliy, Setting.dgvFontSize);
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
        IEnumerable<Stethoscope> GetStethoscope()
        {
            List<string> listStetName=new List<string>();
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
			{
			  if((bool) dataGridViewEx1.Rows[i].Cells[0].Value)
              {
                  listStetName.Add(dataGridViewEx1.Rows[i].Cells[1].Value.ToString());
              }
			}
            return StethoscopeManager.StethoscopeList.Where(s => s.Name != cboStethoscope.Text&& s.IsConnected && !listStetName.Contains(s.Name)).ToArray();
        }
        public DataTable DgvTable
        {
            get
            {
                DataTable dt = new DataTable();
                for (int count = 0; count < dataGridViewEx1.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dataGridViewEx1.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                for (int count = 0; count < dataGridViewEx1.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dataGridViewEx1.Columns.Count; countsub++)
                    {
                        dr[countsub] = dataGridViewEx1.Rows[count].Cells[countsub].Value+"";
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }
        private void btnTeach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboStethoscope.Text))
            {
                MessageBox.Show("请选择一个已连接的听诊器，如果列表为空,请先连接本地听诊器！");
                return;
            }
            if (!StethoscopeManager.StethoscopeList.Where(s => s.Name == this.cboStethoscope.Text && s.IsConnected).Any())
            {
                MessageBox.Show(string.Format("听诊器{0}尚未连接",this.cboStethoscope.Text));
                return;
            }
            var arrTHQ = DgvTable.Select().Where(r => r[0].ToString() == "True").Select(r => r[2] + "").ToArray();
            var arrRecvStethoscope = StethoscopeManager.StethoscopeList.Where(s => arrTHQ.Contains(s.Name)).ToArray();
            if (!arrRecvStethoscope.Any())
            {
                MessageBox.Show("请选择听诊器！");
                return;
            }
            //1.判断所有勾选的听诊器处于蓝牙连接成功状态
            //2.开启源听诊设备
            var stethoscopeArr = StethoscopeManager.StethoscopeList.Where(s => s.Name == cboStethoscope.Text);
            if (stethoscopeArr.Count() == 0)
                throw new Exception("目前没有检测到听诊器,请检测设备设置！");
            var stethoscope = stethoscopeArr.First();
            if (!stethoscope.IsConnected)
            {
                Mediator.ShowMsg(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                MessageBox.Show(string.Format("听诊器 {0} 尚未连接", stethoscope.Name));
                return;
            }
            //var arrRecvStethoscope=ucStetManager1.GetStethoscope().Where(item=>item.IsConnected);
            FormProcessBar formProcessBar = new FormProcessBar(true)
                {
                    BtnText = "停止教学"
                };
            Thread pairThread = new Thread(() =>
            {
                Mediator.isBusy = true;
                formProcessBar.TimerCallBackEvent += () =>
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                        formProcessBar.Title = string.Format("音频教学中... {0} 秒", formProcessBar.Times);
                    }));
                };
                byte[] packet = new byte[128];
                stethoscope.StartAudioInput();
                foreach (var recvStethoscope in arrRecvStethoscope)
                {
                    if (recvStethoscope.IsConnected)
                        recvStethoscope.StartAudioOutput();
                }
                Mediator.ShowMsg(string.Format("听诊器 {0} 开始教学...", stethoscope.Name));
                // Stream audio from the stethoscope to the computer.
                while (formProcessBar.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    int bytesRead = stethoscope.AudioInputStream.Read(packet, 0, packet.Length);
                    foreach (var recvStethoscope in arrRecvStethoscope)
                    {
                        if (recvStethoscope.IsConnected)
                            recvStethoscope.AudioOutputStream.Write(packet, 0, bytesRead);
                    }
                }
                Mediator.ShowMsg(string.Format("听诊器 {0} 教学完毕，时长 {1} 秒", stethoscope.Name, formProcessBar.Times));

            });
            pairThread.Start();
            formProcessBar.ShowDialog();
            formProcessBar.TimerEnable = false;
            stethoscope.StopAudioInput();
            foreach (var recvStethoscope in arrRecvStethoscope)
            {
                if (recvStethoscope.IsConnected)
                    recvStethoscope.StopAudioOutput();
            }
            Mediator.ShowMsg("音频教学完毕...");
            Mediator.isBusy = false;
            Mediator.WriteLog(this.Name, "本地教学完毕");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewEx1.Rows.Count; i++)
            {
                dataGridViewEx1.Rows[i].Cells[0].Value = checkBox1.Checked;
            }
        }

        

        private void cboStethoscope_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewEx1.Rows.Clear();
            foreach (var item in StethoscopeManager.StethoscopeList.Where(s=>s.Name!=this.cboStethoscope.Text ))
            {
#if !DEBUG
                  if (!item.IsConnected) 
                continue;
#endif
                var stetInfo = Setting.GetStetInfoByStetName(item.Name);
                dataGridViewEx1.Rows.Add( false,
                    stetInfo == null ? "" : stetInfo.StetChineseName,
                   item.Name,
                    stetInfo == null ? "" : stetInfo.Owner,
                    item.IsConnected?"已连接":"未连接"
                 );
            }

        }

        private void dataGridViewEx1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                switch (dataGridViewEx1.Columns[e.ColumnIndex].Name)
                {
                    case "Checked":
                        {
                            var b = (bool)dataGridViewEx1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                             dataGridViewEx1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value=!b;
                        }
                        break;
                }
            }
        }

       

         


    }
}

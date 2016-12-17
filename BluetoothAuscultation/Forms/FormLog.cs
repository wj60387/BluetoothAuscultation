using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using System.IO;

namespace BluetoothAuscultation.Forms
{
    public partial class FormLog : DockContentEx
    {
        public FormLog()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormLog_Load);
        }

        void FormLog_Load(object sender, EventArgs e)
        {
            ShowLog(DateTime.Now);
        }
        void ShowLog(DateTime Date)
        {
            var dir = Path.Combine(Application.StartupPath, "Logs" + "\\" + Date.Year + "\\" + Date.Month);
            var filePath = Path.Combine(dir, Date.ToString("yyyy-MM-dd") + ".txt");
            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath, Encoding.UTF8);
                this.txtLog.Text = text;
            }
            else
            {
                MessageBox.Show("今日无操作");
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowLog(dateTimePicker1.Value);
        }
    }
}

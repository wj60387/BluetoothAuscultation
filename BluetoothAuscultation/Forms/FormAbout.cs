using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluetoothAuscultation.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            this.waterTextBox1.Text = Setting.authorizationInfo.AuthorizationNum;
            this.waterTextBox2.Text = Setting.authorizationInfo.MachineCode;
            this.waterTextBox3.Text = Setting.authorizationInfo.AuthStartTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.waterTextBox4.Text = Setting.authorizationInfo.AuthDays.ToString();
            this.waterTextBox5.Text = "V"+Setting.Version;
            this.waterTextBox1.Select(0, 0);
        }

        private void waterTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void waterTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void waterTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void waterTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

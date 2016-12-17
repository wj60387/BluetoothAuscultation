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
    public partial class FormStetInfo : Form
    {
        public FormStetInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormStetInfo_Load);
        }

        void FormStetInfo_Load(object sender, EventArgs e)
        {
            this.SetBackColor(Setting.themeColor);
        }
        public string StetName
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
        public int StetType
        {
            get
            {
                return radioButton2.Checked ? 2 : 1;
            }
            set
            {
                if (value == 1)
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
        }
        public string StetChineseName
        {
            get
            {
                return txtStetChineseName.Text;
            }
            set
            {
                txtStetChineseName.Text = value;
            }
        }
        public string StetOwner
        {
            get
            {
                return txtStetOwner.Text;
            }
            set
            {
                txtStetOwner.Text = value;
            }
        }
        public string StetFuncDescript
        {
            get
            {
                return txtStetFunc.Text;
            }
            set
            {
                txtStetFunc.Text = value;
            }
        }
        public string StetRemark
        {
            get
            {
                return txtStetRemark.Text;
            }
            set
            {
                txtStetRemark.Text = value;
            }
        }
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStetChineseName.Text))
            {
                MessageBox.Show("请为听诊器取一个名字");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}

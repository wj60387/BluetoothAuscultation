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
    public partial class FormAuthInfo : Form
    {
        public FormAuthInfo()
        {
            InitializeComponent();
            txtAuthDays.Maximum = txtBindCount.Maximum = int.MaxValue;
        }
        public string AuthorizationNum
        {
            get
            {
                return txtSerNum.Text;
            }
            set
            {
                txtSerNum.Text = value;
            }
        }
        public decimal AuthDays
        {
            get
            {
                return txtAuthDays.Value;
            }
            set
            {
                txtAuthDays.Value = value;
            }
        }
        public decimal BindCount
        {
            get
            {

                return txtBindCount.Value;
            }
            set
            {
                txtBindCount.Value = value;
            }
        }
        public string GroupID
        {
            get
            {

                return txtGroupID.Text;
            }
            set
            {
                txtGroupID.Text = value;
            }
        }
        public string GroupName
        {
            get
            {

                return txtGroupName.Text;
            }
            set
            {
                txtGroupName.Text = value;
            }
        }
        public string GroupRemark
        {
            get
            {

                return txtGroupRemark.Text;
            }
            set
            {
                txtGroupRemark.Text = value;
            }
        }
        public string CustomName
        {
            get
            {

                return txtCoustomName.Text;
            }
            set
            {
                txtCoustomName.Text = value;
            }
        }
        public string CustomEmail
        {
            get
            {

                return txtCoustomEmail.Text;
            }
            set
            {
                txtCoustomEmail.Text = value;
            }
        }
        public string CustomTel
        {
            get
            {

                return txtCoustomTel.Text;
            }
            set
            {
                txtCoustomTel.Text = value;
            }
        }
        public string CustomAddress
        {
            get
            {

                return txtCustomAddress.Text;
            }
            set
            {
                txtCustomAddress.Text = value;
            }
        }
        public string CustomReamrk
        {
            get
            {
                return txtCoustomRemark.Text;
            }
            set
            {
                txtCoustomRemark.Text = value;
            }
        }
        private void btnSure_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

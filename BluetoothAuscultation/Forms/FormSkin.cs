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
    public partial class FormSkin : Form
    {
        public FormSkin()
        {
            InitializeComponent();
        }

        private void btnSetMenu_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Setting.themeColor;
            cd.FullOpen = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Form form in Application.OpenForms)
                {
                    

                }
            }
        }

        private void btnSetTool_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Setting.themeColor;
            cd.FullOpen = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.SetBackColor(cd.Color);

                }
            }
        }

        private void btnSetForm_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = Setting.themeColor;
            cd.FullOpen = true;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.SetBackColor(cd.Color);

                }
            }
        }
    }
}

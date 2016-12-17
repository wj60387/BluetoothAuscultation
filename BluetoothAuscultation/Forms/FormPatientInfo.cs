using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.Models;
using System.IO;

namespace BluetoothAuscultation.Forms
{
    public partial class FormPatientInfo : Form
    {
        public FormPatientInfo( )
        {
            
            InitializeComponent();
            this.pictureBoxEx1.Init(FilePaths.Select(f => Image.FromFile(f)).ToArray());
            pictureBoxEx1.ImageChanged += new EventHandler(pictureBoxEx1_ImageChanged);
            this.Load += new EventHandler(FormPatientInfo_Load);
            cboPatientPosture.Items.AddRange(new string[] {
                "仰卧","站立","坐姿","下蹲","其它"
            });
            cboPatientSites.Items.AddRange(new string[] {
                "大动脉位置","肺位置","三尖瓣位置","肺-尖前动脉","肺-尖后动脉"
                ,"肺-中前动脉","肺-中后动脉","心血管-颈动脉","心血管-锁骨","心血管-胸部"
                ,"心血管-肾动脉","心血管-主动脉","心血管-股动脉","其它"
            });
        }
        public string[] FilePaths
        {
            get
            {
                return Directory.GetFiles(@"Image\Part");
            }
        }
        void FormPatientInfo_Load(object sender, EventArgs e)
        {
            this.SetBackColor(Setting.themeColor);
          

           
        }
        public string PatientPartImageName {
            get
            {
                if (pictureBoxEx1.ImageIndex >= 0)
                    return Path.GetFileNameWithoutExtension(FilePaths[pictureBoxEx1.ImageIndex]);
                return string.Empty;
            }
            set
            {
                if (FilePaths.Select(f => Path.GetFileName(f)).Contains(value+".jpg"))
                {
                    this.pictureBoxEx1.CurImage = Image.FromFile(@"Image\Part\" + value + ".jpg");
                }
            }
        }
        
        void pictureBoxEx1_ImageChanged(object sender, EventArgs e)
        {
            //if (pictureBoxEx1.ImageIndex >= 0)
            //    PatientPartImageName =Path.GetFileName( FilePaths[pictureBoxEx1.ImageIndex]);
        }
        public int PatientType
        {
            get
            {
              return  radioButton2.Checked ? 2 : 1;
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
        public string PatientID
        {
            get
            {
                return  txtPatientId.Text;
            }
            set
            {
                txtPatientId.Text = value;
            }
        }
        public string PatientName
        {
            get
            {
                return  txtPatientName.Text;
            }
            set
            {
                txtPatientName.Text = value;
            }
        }
        public string Posture
        {
            get
            {
                return cboPatientPosture.Text;
            }
            set
            {
                cboPatientPosture.SelectedItem = value;
            }
        }
        public string Part
        {
            get
            {
                return cboPatientSites.Text;
            }
            set
            {
                        cboPatientSites.SelectedItem = value;
            }
        }
        public string Remark
        {
            get
            {
                return txtMemo.Text;
            }
            set
            {
                txtMemo.Text = value;
            }
        }

        public string DocName
        {
            get
            {
                return txtDocName.Text;
            }
            set
            {
                txtDocName.Text = value;
            }
        }
        public string DocDiagnosisTxt
        {
            get
            {
                return txtDocDiagnosis.Text;
            }
            set
            {
                txtDocDiagnosis.Text = value;
            }
        }
        private void btnSavePatient_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        
    }
}

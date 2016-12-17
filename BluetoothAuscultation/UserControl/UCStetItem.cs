using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MMM.HealthCare.Scopes.Device;

namespace BluetoothAuscultation.UserControl
{
    public partial class UCStetItem : System.Windows.Forms.UserControl
    {
        public UCStetItem()
        {
            InitializeComponent();

        }
        public Stethoscope stethoscope { get; set; }
        public bool isCheck
        {
            get
            {
                return checkBox1.Checked;
            }
            set
            {
                checkBox1.Checked = value;
            }
        }
        private void UCMenuItem_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("接收端听诊器：   " + "", Font, new SolidBrush(this.ForeColor), this.ClientRectangle, sf);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.LightSeaGreen)), this.ClientRectangle);
        }
         

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace BluetoothAuscultation.ExtendControl
{
    public class WaterTextBox:System.Windows.Forms.TextBox
    {
        string waterText=string.Empty;
        [Description("设置水印文本")]
        [DefaultValue(typeof(String), "提示文字")]
        public string WaterText
        {
            get { return waterText; }
            set {   waterText=value;
            this.Invalidate();
            }
        }
         
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                if (string.IsNullOrEmpty(this.Text))
                using (Graphics g=this.CreateGraphics())
                {
                    StringFormat sf = new StringFormat() {
                        Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                    g.DrawString(waterText,   this.Font, new SolidBrush(Color.Gray),this.ClientRectangle, sf);
                   g.Dispose();
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WaterTextBox
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Multiline = true;
            this.Size = new System.Drawing.Size(100, 22);
            this.ResumeLayout(false);
             //SetAutoSizeMode(AutoSizeMode.GrowOnly);
            this.AutoSize = false;
            this.Height = 22;

        }

    }
}

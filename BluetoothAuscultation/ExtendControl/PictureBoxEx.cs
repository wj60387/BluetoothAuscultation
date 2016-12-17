using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BluetoothAuscultation.ExtendControl
{
    public class PictureBoxEx : PictureBox
    {

        public PictureBoxEx()
        {
            InitializeComponent();
        }
        public void Init(Image[] ArrImgge)
        {
            if (ArrImgge != null && ArrImgge.Length > 0)
            {
                ImageIndex = 0;
                this.CurImage = ArrImgge[0];
                this.m_Image = ArrImgge;
                
            }
        }
        public int ImageIndex = -1;
        public Image[] m_Image = null;
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxEx
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxEx_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxEx_MouseClick);
            this.MouseLeave += new System.EventHandler(this.PictureBoxEx_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxEx_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        public event EventHandler ImageChanged;
        public   Image CurImage
        {
            get { return base.Image; }
            set { base.Image = value;
            if (ImageChanged != null)
                ImageChanged(this, new EventArgs()); 
            }
        }

        Size pageSize = new Size(20, 40);
        public Size PageSize { get { return pageSize; } set { pageSize = value; } }
        private Rectangle PageLeftRect
        {
            get
            {
                return new Rectangle(-1, (this.Height - PageSize.Height) / 2, PageSize.Width, PageSize.Height);
            }
        }
        private Rectangle PageRightRect
        {
            get
            {
                return new Rectangle(this.Width - PageSize.Width, (this.Height - PageSize.Height) / 2, PageSize.Width, PageSize.Height);
            }
        }
        private void PictureBoxEx_Paint(object sender, PaintEventArgs e)
        {
           
            if (PageLeftRect.Contains(this.PointToClient(MousePosition))
                || PageRightRect.Contains((this.PointToClient(MousePosition))))
            {

                using (var brush = new SolidBrush(Color.FromArgb(50, 53, 54)))
                {
                    var pen = new Pen(brush.Color);
                    e.Graphics.DrawRectangle(pen, PageLeftRect);
                    e.Graphics.FillRectangle(brush, PageLeftRect);
                    e.Graphics.DrawString("<", new Font("微软雅黑", 16.0f), new SolidBrush(Color.White),
           PageLeftRect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });


                    e.Graphics.DrawRectangle(pen, PageRightRect);
                    e.Graphics.FillRectangle(brush, PageRightRect);

                    e.Graphics.DrawString(">", new Font("微软雅黑", 16.0f), new SolidBrush(Color.White),
           PageRightRect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                }
            }
            //var txt = "MousePosition:" + MousePosition.X + "," + MousePosition.Y;
            //e.Graphics.DrawString(txt, new Font("微软雅黑", 9.0f), new SolidBrush(Color.Red),
            //    this.ClientRectangle, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            //  base.OnPaint(e);
        }

        private void PictureBoxEx_MouseMove(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }
        private void PictureBoxEx_MouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void PictureBoxEx_MouseClick(object sender, MouseEventArgs e)
        {
            //前翻
            if (PageLeftRect.Contains(this.PointToClient(MousePosition)))
            {
                if (ImageIndex > 0)
                {
                    this.CurImage = this.m_Image[--ImageIndex];
                }



            }//后翻
            else if (PageRightRect.Contains((this.PointToClient(MousePosition))))
            {
                if (ImageIndex >= 0 && ImageIndex < m_Image.Length - 1)
                {
                    this.CurImage = this.m_Image[++ImageIndex];
                }
            }
        }



    }
}

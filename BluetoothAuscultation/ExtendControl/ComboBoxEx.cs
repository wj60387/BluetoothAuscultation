using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BluetoothAuscultation.ExtendControl
{
    public class ComboBoxEx : ComboBox
    {
        public ComboBoxEx()
        {
            InitializeComponent();
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBoxEx_DrawItem);

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ComboBoxEx
            // 
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ItemHeight = 22;
            this.Size = new System.Drawing.Size(121, 28);
            this.ResumeLayout(false);

        }

        private void ComboBoxEx_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            //鼠标选中在这个项上
            if ((e.State & DrawItemState.Selected) != 0)
            {
                //渐变画刷
                LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(255, 251, 237),
                                                 Color.FromArgb(255, 236, 181), LinearGradientMode.Vertical);
                //填充区域
                Rectangle borderRect = new Rectangle(0, e.Bounds.Y, e.Bounds.Width , e.Bounds.Height );
                e.Graphics.FillRectangle(brush, borderRect);
                //画边框
                Pen pen = new Pen(Color.FromArgb(229, 195, 101));
                e.Graphics.DrawRectangle(pen, borderRect);
            }
            else
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            //获得项图片,绘制图片
            StetItem item = this.Items[e.Index] as StetItem;
            Image img = item.image.GetThumbnailImage(this.ItemHeight - 6, this.ItemHeight - 6, () => { return true; }, IntPtr.Zero);
            //图片绘制的区域
            Rectangle imgRect = new Rectangle(3, e.Bounds.Y+3, this.ItemHeight-6, this.ItemHeight-6);
            e.Graphics.DrawImage(img, imgRect);
            //文本内容显示区域
            Rectangle textRect =
                    new Rectangle(imgRect.Right + 2, imgRect.Y, e.Bounds.Width - imgRect.Width, e.Bounds.Height - 2);
            //获得项文本内容,绘制文本
           // String itemText = this.Items[e.Index].ToString();
            //文本格式垂直居中
            //StringFormat strFormat = new StringFormat();
            //strFormat.LineAlignment = StringAlignment.Center;
            StringFormat sfn = new StringFormat();
            sfn.Alignment = StringAlignment.Near;
            sfn.LineAlignment = StringAlignment.Center;
            if (string.IsNullOrEmpty(item.stetChineseName))
            {
                e.Graphics.DrawString(item.stetName, new Font("微软雅黑", 12), Brushes.Black, textRect, sfn);
                StringFormat sff = new StringFormat();
            }
            else
            {
                e.Graphics.DrawString(item.stetChineseName+"("+item.Owner+")", new Font("微软雅黑", 12), Brushes.Black, textRect, sfn);
                StringFormat sff = new StringFormat();
            }
            //e.Graphics.DrawString(item.stetName, new Font("微软雅黑", 12), Brushes.Black, textRect, sfn);
            //StringFormat sff = new StringFormat();
            //sff.Alignment = StringAlignment.Far;
            //sff.LineAlignment = StringAlignment.Center;
            //e.Graphics.DrawString(item.userName, new Font("微软雅黑", 12), Brushes.Black, textRect, sff);
        }

    }
    public class StetItem
    {
        public Image image { get; set; }
        public string stetName { get; set; }
        public string stetChineseName { get; set; }
        public string Owner { get; set; }
        public override string ToString()
        {
            return stetName;
        }
    }
}

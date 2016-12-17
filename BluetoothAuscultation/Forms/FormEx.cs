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
    public partial class FormEx : Form
    {
        public FormEx()
        {
            InitializeComponent();
          //  menuTreeViewEx1.DrawMode = TreeViewDrawMode.OwnerDrawText;
           // menuTreeViewEx1.DrawNode += new DrawTreeNodeEventHandler(menuTreeViewEx1_DrawNode);
            this.Load += new EventHandler(FormEx_Load);
           
        }
        public string[] FilePaths
        {
            get
            {
                return Directory.GetFiles(@"Image\Part");
            }
        }
        void FormEx_Load(object sender, EventArgs e)
        {
            var filePaths = Directory.GetFiles(@"Image\Part");
            this.pictureBoxEx1.ImageChanged += new EventHandler(pictureBoxEx1_ImageChanged);
            this.pictureBoxEx1.Init(filePaths.Select(f => Image.FromFile(f)).ToArray());
            dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { HeaderText = "缩略图" });
            dataGridViewEx1.Columns.Add("部位", "部位");
            dataGridViewEx1.Columns.Add("c1","c1");
            dataGridViewEx1.Columns.Add("c2", "c2");
            //dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnPlay", HeaderText = "播放", Image = Setting.ImagePlay });
            // dataGridViewEx1.Columns.Add(new DataGridViewImageColumn(false) { Name = "btnDelete", HeaderText = "删除", Image = Setting.ImageDelete });
          
            dataGridViewEx1.RowTemplate.Height = 90;
            foreach (var file in filePaths)
            {
                dataGridViewEx1.Rows.Add(Image.FromFile(file).GetThumbnailImage(75, 90, () => { return true; }, IntPtr.Zero)
                    , Path.GetFileNameWithoutExtension(file)
                    
                    );
            }
        }

        void pictureBoxEx1_ImageChanged(object sender, EventArgs e)
        {
            if(pictureBoxEx1.ImageIndex>=0)
                this.Text = FilePaths[pictureBoxEx1.ImageIndex];
        }

        void menuTreeViewEx1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            
            //e.DrawDefault = true;
            //if ((e.State & TreeNodeStates.Selected) != 0)
            //{
            //    e.Graphics.DrawString(e.Node.Text + "\t" + ((TreeNodeEx)e.Node).NicName, this.Font, new SolidBrush(this.ForeColor),
            //      new Rectangle(new Point(e.Node.Bounds.Location.X, e.Node.Bounds.Location.Y + 3)
            //           , new System.Drawing.Size(this.Width, e.Node.Bounds.Height)));
            //}
            
        }

        private void pictureBoxEx1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBoxEx1_Click(object sender, EventArgs e)
        {
            
            using (Graphics g = this.pictureBoxEx1.CreateGraphics())
            {
                g.DrawEllipse(new Pen(Color.Red),new Rectangle(pictureBoxEx1.PointToClient(MousePosition).X-4,
                    pictureBoxEx1.PointToClient(MousePosition).Y-4,18,18));
            }
            this.Text = pictureBoxEx1.PointToClient(MousePosition).X + "," + pictureBoxEx1.PointToClient(MousePosition).Y;
        }

        private void dataGridViewEx1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }
         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using BluetoothAuscultation.Devices;

namespace BluetoothAuscultation.ExtendControl
{
    public class MenuTreeViewEx : TreeView
    {
        public MenuTreeViewEx()
            : base()
        {
            InitializeComponent();
            this.ExpandAll();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MenuTreeViewEx
            // 
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FullRowSelect = true;
            this.HotTracking = true;
            this.ItemHeight = 22;
            this.LineColor = System.Drawing.Color.Black;
            this.ShowLines = false;
            this.ResumeLayout(false);
            this.DrawNode += new DrawTreeNodeEventHandler(MenuTreeViewEx_DrawNode);
            this.MouseMove += new MouseEventHandler(MenuTreeViewEx_MouseMove);
            this.MouseDown += new MouseEventHandler(MenuTreeViewEx_MouseDown);
             
        }
       
        void MenuTreeViewEx_MouseDown(object sender, MouseEventArgs e)
        {
            //TreeNode clickedNode = this.GetNodeAt(e.X, e.Y);
            //if (clickedNode.Bounds.Contains(e.X, e.Y))
            //{
            //    this.SelectedNode = clickedNode;
            //}
        }

        void MenuTreeViewEx_MouseMove(object sender, MouseEventArgs e)
        {
             
           
           
        }

        void MenuTreeViewEx_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle focusBounds = e.Bounds;
            focusBounds.Location = new Point(0, e.Bounds.Location.Y);
            focusBounds.Size = new Size(this.Width - 1,
            focusBounds.Height - 1);
            if (e.State != TreeNodeStates.Focused && focusBounds.Contains(this.PointToClient(Control.MousePosition)))
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                   
                    using (var g = Graphics.FromHwnd(this.Handle))
                    {
                        g.DrawRectangle(focusPen, focusBounds);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(173,214,255)), focusBounds);
                        g.Dispose();

                    }

                }
            if (e.Node.Level == 0)
            {
                e.Graphics.DrawImage(Setting.RootImage,
                  Rectangle.Inflate(new Rectangle(e.Node.Bounds.Location, new Size(e.Node.TreeView.ItemHeight, e.Node.TreeView.ItemHeight)), -3, -3));
                e.Graphics.DrawString(e.Node.Text + "\t" + "[" + StethoscopeManager.StethoscopeList.Where(s => s.IsConnected).Count() + "/" + e.Node.Nodes.Count + "]", this.Font, new SolidBrush(this.ForeColor),
         new Rectangle(new Point(e.Node.Bounds.Location.X + 22, e.Node.Bounds.Location.Y + 3)
              , new System.Drawing.Size(this.Width, e.Node.TreeView.ItemHeight)));
            }
            else if (e.Node.Level == 1)
            {

                e.Graphics.DrawImage(StethoscopeManager.StethoscopeList.Where(s => s.Name == e.Node.Text).First().IsConnected ? Setting.OnlineImage : Setting.OfflineImage,
                Rectangle.Inflate(new Rectangle(e.Node.Bounds.Location, new Size(e.Node.TreeView.ItemHeight, e.Node.TreeView.ItemHeight)), -3, -3));
                var info = Setting.GetStetInfoByStetName(e.Node.Text);
                string text = e.Node.Text;
                if (info != null && !string.IsNullOrEmpty(info.StetChineseName))
                {
                    text = info.StetChineseName + "(" + info.Owner + ")";
                }
                using (var brush = new SolidBrush(this.ForeColor))
                {
                    e.Graphics.DrawString(text, this.Font, brush,
             new Rectangle(new Point(e.Node.Bounds.Location.X + 22, e.Node.Bounds.Location.Y + 3)
                  , new System.Drawing.Size(this.Width, e.Node.TreeView.ItemHeight)));
                }
               
                 
            }
           

        }
    }
}

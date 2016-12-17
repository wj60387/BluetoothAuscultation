using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using BluetoothAuscultation.Utilities;
using BluetoothAuscultation.Devices;
using System.Threading;

namespace BluetoothAuscultation.Forms
{
    public partial class FormMenu : DockContentEx
    {
        public FormMenu()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMenu_Load);
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
            //this.treeView1.MouseDown += new MouseEventHandler(treeView1_MouseDown);
            //this.treeView1.MouseMove += new MouseEventHandler(treeView1_MouseMove);
        }

        void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            //TreeNode moveNode = treeView1.GetNodeAt(e.X, e.Y);
            //using (Pen focusPen = new Pen(Color.Black))
            //{
            //    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //    Rectangle focusBounds = moveNode.Bounds;
            //    focusBounds.Size = new Size(focusBounds.Width - 1,
            //    focusBounds.Height - 1);
            //    using(var g=Graphics.FromHwnd(treeView1.Handle))
            //    {
            //        g.DrawRectangle(focusPen, focusBounds);
            //        g.Dispose();
            //    }
            //}
        }

        void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            if ( clickedNode.Bounds.Contains(e.X, e.Y))
            {
                treeView1.SelectedNode = clickedNode;
            }
        }

        void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {

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
         
        void FormMenu_Load(object sender, EventArgs e)
        {
             
            TreeNode rootNode = new TreeNode(CommonUtil.GetMachineName());
            foreach (var stethoscope in StethoscopeManager.StethoscopeList)
            {
                var node = new TreeNode(stethoscope.Name);
                rootNode.Nodes.Add(node);
            }
            this.treeView1.Nodes.Add(rootNode);
            this.treeView1.ExpandAll();
            
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Mediator.OpenWindow("听诊录音",e.Node.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (OpenStethoscope(treeView1.SelectedNode.Text))
                {
                    treeView1.Invalidate();
                }
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (CloseStethoscope(treeView1.SelectedNode.Text))
                {
                    treeView1.Invalidate();
                }
            }
        }
        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (OpenStethoscope(treeView1.SelectedNode.Text))
                {
                    treeView1.Invalidate();
                    Mediator.OnStethcopeConnectChanged(treeView1.SelectedNode.Text, true);
                }
            }
        }

        private void 断开连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (CloseStethoscope(treeView1.SelectedNode.Text))
                {
                    treeView1.Invalidate();
                    Mediator.OnStethcopeConnectChanged(treeView1.SelectedNode.Text, false);
                    
                }
            }
        }
        bool OpenStethoscope(string stethoscopeName)
        {
            var stethoscopes = StethoscopeManager.StethoscopeList.Where(s => s.Name == stethoscopeName);
            if (stethoscopes.Count() == 0) return false;
            var stethoscope = stethoscopes.First();
            FormProcessBar formProcessBar = new FormProcessBar(false, string.Format("正在开启设备{0}连接！", stethoscopeName))
            {
                CancelBtnVisible = false
            }; ;
            Thread pairThread = new Thread(() =>
            {
                //var _stethoscope = obj as Stethoscope;
                try
                {
                    if (!stethoscope.IsConnected)
                    {
                        stethoscope.Connect();
                        Mediator.ShowMsg(string.Format("听诊器{0}连接成功", stethoscope.Name));
                    }

                }
                catch (Exception ex)
                {
                    Mediator.ShowMsg(string.Format("听诊器{0}连接失败", stethoscope.Name));
                    //MessageBox.Show("听诊器连接失败,请确认听诊器是否开启了蓝牙连接状态！");
                    Mediator.ShowMsg("听诊器连接失败,请确认听诊器是否开启了蓝牙连接状态！");

                }
                finally
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                        formProcessBar.Close();
                    }));

                }

            });
            pairThread.Start();
            formProcessBar.ShowDialog();
            return stethoscope.IsConnected;
        }

        bool CloseStethoscope(string stethoscopeName)
        {
            var stethoscopes = StethoscopeManager.StethoscopeList.Where(s => s.Name == stethoscopeName);
            if (stethoscopes.Count() == 0) return false;
            var stethoscope = stethoscopes.First();

            FormProcessBar formProcessBar = new FormProcessBar(false, string.Format("正在断设备 {0} 连接！", stethoscopeName))
            {
                CancelBtnVisible = false
            };  
            Thread pairThread = new Thread(() =>
            {
                //var _stethoscope = obj as Stethoscope;
                try
                {
                    if (stethoscope.IsConnected)
                    {
                        stethoscope.Disconnected += (s, arg) =>
                        {
                            Invoke(new MethodInvoker(() =>
                            {
                                formProcessBar.Close();
                            }));
                        };
                        stethoscope.Disconnect();
                        Mediator.ShowMsg(string.Format("听诊器{0}断开成功", stethoscope.Name));
                    }
                }
                catch (Exception ex)
                {
                    Mediator.ShowMsg(string.Format("听诊器{0}断开失败", stethoscope.Name));
                    //好像从来没有进来过
                   
                }
                finally
                {
                    Invoke(new MethodInvoker(delegate()
                    {
                        formProcessBar.Close();
                    }));
                }

                 

            });
            pairThread.Start();
            formProcessBar.ShowDialog();
            return stethoscope.IsConnected;

        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode!=null && !string.IsNullOrEmpty(treeView1.SelectedNode.Text) && 
                treeView1.SelectedNode.Level!=0)
            {
                Mediator.OpenWindow("听诊器配置", treeView1.SelectedNode.Text);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode.Level != 1) e.Cancel = true;
            else if (treeView1.SelectedNode != null)
            {
                if (StethoscopeManager.StethoscopeList.Where(s => s.Name == treeView1.SelectedNode.Text).First().IsConnected)
                {
                    连接ToolStripMenuItem.Enabled = false;
                    断开连接ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    连接ToolStripMenuItem.Enabled = true;
                    断开连接ToolStripMenuItem.Enabled = false;
                }
            }
        }

        

         

         

      
        
    }
}

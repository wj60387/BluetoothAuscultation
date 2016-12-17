using BluetoothAuscultation.IGetAudioInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluetoothAuscultation.Forms
{
    public partial class FormFJ : Form
    {
        public string PGUID { get; set; }
        public FormFJ(string PGUID)
        {
            this.PGUID = PGUID;
            InitializeComponent();
            this.Load += FormFJ_Load;
        }
        void FormFJ_Load(object sender, EventArgs e)
        {
            LoadFile();
        }
        void LoadFile()
        {
            treeView1.Nodes.Clear();
            var rootNode = new TreeNode(PGUID+"(听诊ID)") { ImageIndex = 1 };
            var files = IFJ.GetFileName(PGUID);
            if (files != null)
                foreach (var file in files)
                {
                    var node = new TreeNode(Path.GetFileName(file)) { ImageIndex = 0 };
                    rootNode.Nodes.Add(node);
                }
            this.treeView1.Nodes.Add(rootNode);
            this.treeView1.ExpandAll();
        }
        public IGetFJ IFJ { get; set; }
        public bool isUpLoadVisable
        {
            
            get
            {
                return btnUpLoad.Visible;
            }
            set
            {
                btnUpLoad.Visible = value;
                if(!value)
                {
                    btnOpen.Location = btnUpLoad.Location;
                }
            }
        }
        public bool isOpenFolderVisable
        {
            get
            {
                return btnOpen.Visible;
            }
            set
            {
                btnOpen.Visible = value;
                if (!value)
                {
                    btnClose.Location = new Point(this.Width/2-btnClose.Width/2,btnClose.Location.Y);
                }
            }
        }
        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if (IFJ is LoaclReocrd)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    if (!Directory.Exists(string.Format(IFJ.Dir, PGUID)))
                        Directory.CreateDirectory((string.Format(IFJ.Dir, PGUID)));
                    var files = ofd.FileNames;
                    foreach (var file in files)
                    {
                        File.Copy(file, Path.Combine(string.Format(IFJ.Dir, PGUID), Path.GetFileName(file)));
                    }
                    LoadFile();
                    MessageBox.Show("上传成功！");
                }
            }
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.treeView1.SelectedImageIndex = e.Node.Level == 0 ? 1 : 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var dir = string.Format(IFJ.Dir, PGUID);
            if (!Directory.Exists(dir))
            {
                MessageBox.Show(string.Format("目录{0}不存在", dir));
            }
            System.Diagnostics.Process.Start(dir);
        }
        
        
    }
}

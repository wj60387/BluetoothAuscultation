using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MMM.HealthCare.Scopes.Device;
using ProtocalData.Protocol.Derive;
using BluetoothAuscultation.ExtendControl;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace BluetoothAuscultation.Forms
{
    public partial class FormShare : Form
        //, IHandleMessage<ResGetDeviceInfoCode>
    {
        public string StetName { get; set; }
        public FormShare(string StetName)
        {
            this.StetName = StetName;
            InitializeComponent();
            treeView1.CheckBoxes = true;
            this.Load += new EventHandler(FormShare_Load);
            this.Shown += new EventHandler(FormShare_Shown);
            treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        void FormShare_Load(object sender, EventArgs e)
        {
            string sql = "select StetName,MAC,PCName,StetChineseName,Owner from StethoscopeManager where  IfDel=0 and   StetName!={0}";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
              var ds=  Mediator.remoteService.ExecuteDataset(sql, new string[] { StetName });
              if (ds != null && ds.Tables.Count > 0)
              {
                
                  var Macs = ds.Tables[0].Select().Select(r => r["MAC"] + "").Distinct().ToArray();
                  foreach (var mac in Macs)
                  {
                      var pcName = ds.Tables[0].Select("MAC='" + mac + "'").First()["PCName"] + "";
                      var rootNode = new TreeNodeEx(pcName);
                      foreach (DataRow row in ds.Tables[0].Select("MAC='"+mac+"'"))
                      {
                          rootNode.Nodes.Add(new TreeNodeEx()
                          {
                              StetName = row["StetName"] + "",
                              isConnected = true,
                              StetChineseName = row["StetChineseName"] + "",
                              StetOwner = row["Owner"] + "",
                          });
                      }
                      treeView1.Nodes.Add(rootNode);
                  }
                  treeView1.ExpandAll();
              }
            }
        }
        void FormShare_Shown(object sender, EventArgs e)
        {
            //RequestGetDeviceInfoCode requsetGetDeviceInfoCode = new RequestGetDeviceInfoCode();
            //var bytes = ProtocalData.Utilities.SerializaHelper.Serialize(requsetGetDeviceInfoCode);
            //Mediator.SuperSocket.Send(bytes);
        }
        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        public string BtnText
        {
            get
            {
                return btnShare.Text;
            }
            set
            {
                btnShare.Text = value;
            }

        }
        public IEnumerable<string> SelectedStethoscope
        {
            get
            {
                List<string> list = new List<string>();
                foreach (TreeNode Node in treeView1.Nodes)
                {

                    foreach (TreeNodeEx node in Node.Nodes)
                    {
                        if(node.Checked)
                            list.Add(node.StetName);
                    }
                }
                return list;
            }
        }
        /// <summary>
        /// Key:StethoscopeName,Value:MAC
        /// </summary>
        public Dictionary<string, string> SelectedStethoscopeDict
        {
            get
            {
                var dict = new Dictionary<string, string>();
                foreach (TreeNode Node in treeView1.Nodes)
                {

                    foreach (TreeNodeEx node in Node.Nodes)
                    {
                        if (node.Checked)
                            dict.Add(node.StetName, Node.Text);
                    }
                }
                return dict;
            }
        }
        public void HandleMessage(ResGetDeviceInfoCode message)
        {

            //var rootNode = new TreeNodeEx(message.SrcMac);
            //if (message.StetNames != null)
            //{
            //    for (int i = 0; i < message.StetNames.Length; i++)
            //    {
            //        rootNode.Nodes.Add(new TreeNodeEx()
            //        {
            //            StetName = message.StetNames[i],
            //            isConnected = message.isConnected[i],
            //            StetChineseName = message.StetChineseNames[i],
            //            StetOwner=message.StetOwners[i]
            //        });
            //    }
                
            //}
            //Invoke(new MethodInvoker(()=>
            //{
            //    treeView1.Nodes.Add(rootNode);
            //    treeView1.ExpandAll();
            //}));

        }
        List<TreeNodeEx> lstNode = new List<TreeNodeEx>();
        void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var info = e.Node as TreeNodeEx;
            if (e.Node.Level == 0)
            {
                int count = 0;
                foreach (var node in info.Nodes)
                {
                     var treeinfo = node as TreeNodeEx;
                     if (treeinfo.isConnected)
                         count++;
                }
                e.Graphics.DrawImage(Setting.RootImage,
                  Rectangle.Inflate(new Rectangle(e.Node.Bounds.Location, new Size(e.Node.TreeView.ItemHeight, e.Node.TreeView.ItemHeight)), -3, -3));
                e.Graphics.DrawString(e.Node.Text + "\t" + "[" + count + "/" + e.Node.Nodes.Count + "]", this.Font, new SolidBrush(this.ForeColor),
         new Rectangle(new Point(e.Node.Bounds.Location.X + 22, e.Node.Bounds.Location.Y + 3)
              , new System.Drawing.Size(this.Width, e.Node.TreeView.ItemHeight)));
            }
            else if (e.Node.Level == 1)
            {

                e.Graphics.DrawImage(info.isConnected ? Setting.OnlineImage : Setting.OfflineImage,
                Rectangle.Inflate(new Rectangle(e.Node.Bounds.Location, new Size(e.Node.TreeView.ItemHeight, e.Node.TreeView.ItemHeight)), -3, -3));
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(info.ToString(), this.Font, new SolidBrush(this.ForeColor),
         new Rectangle(new Point(e.Node.Bounds.Location.X + 22, e.Node.Bounds.Location.Y + 3)
              , new System.Drawing.Size(this.Width, e.Node.TreeView.ItemHeight)),sf);
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            e.CheckControl();
        }
        private void btnShare_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }



    }
}

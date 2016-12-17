using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothAuscultation.ExtendControl
{
    public class DockContentEx : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DockContentEx()
        {
            System.Windows.Forms.ContextMenuStrip cms = new System.Windows.Forms.ContextMenuStrip();
            cms.Opening += new System.ComponentModel.CancelEventHandler(cms_Opening);
            System.Windows.Forms.ToolStripMenuItem tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // cms
            // 
            tsmiClose.Name = "cms";
            tsmiClose.Size = new System.Drawing.Size(98, 22);
            tsmiClose.Text = "关闭";
            tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsmiClose
            // 
            cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            tsmiClose});
            cms.Name = "tsmiClose";
            cms.Size = new System.Drawing.Size(99, 26);

            this.TabPageContextMenuStrip = cms;

        }



        void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DockState == WeifenLuo.WinFormsUI.Docking.DockState.DockBottom ||
                this.DockState == WeifenLuo.WinFormsUI.Docking.DockState.DockLeft)
            {
                e.Cancel = true;
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

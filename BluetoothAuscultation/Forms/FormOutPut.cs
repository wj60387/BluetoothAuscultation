using System;
using System.Collections.Generic;
using System.ComponentModel;
 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BluetoothAuscultation.ExtendControl;
using System.IO;

namespace BluetoothAuscultation.Forms
{
    public partial class FormOutPut : DockContentEx
    {
        public FormOutPut()
        {
            InitializeComponent();
            Mediator.ShowMessageEvent += new ShowMessage(Mediator_ShowMessageEvent);
            Mediator.ClearMessageEvent += new Action(Mediator_ClearMessageEvent);
            this.FormClosing += new FormClosingEventHandler(FormOutPut_FormClosing);
        }

        void Mediator_ClearMessageEvent()
        {
            Invoke(new MethodInvoker(()=>
            {
                txtMessage.Clear();
            }));
        }

        void FormOutPut_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        void Mediator_ShowMessageEvent(string Msg)
        {
            Invoke(new MethodInvoker(()=>
            {
                var info = ">" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + Msg + "\r\n";
               // var msg = ">" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n使用的激活码:"+Setting.authorizationInfo.AuthorizationNum+"\r\n" + Msg + "\r\n";

                txtMessage.AppendText(info);

                var dir = Path.Combine(Application.StartupPath, "Logs" + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var filePath = Path.Combine(dir, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                File.AppendAllText(filePath, info, Encoding.UTF8);
            }));
        }
        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            } 
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtMessage.Clear();
        }

    }
}

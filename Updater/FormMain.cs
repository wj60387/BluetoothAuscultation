using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Updater
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMain_Load);
        }
        public string filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];
        void FormMain_Load(object sender, EventArgs e)
        {
            //if (!File.Exists(filePath)) return;
            //FileInfo fileInfo = new FileInfo(filePath);
            //textBox1.AppendText("\r\nCreationTime:" + fileInfo.CreationTime);
            //textBox1.AppendText("\r\nLastAccessTime:" + fileInfo.LastAccessTime);
            //textBox1.AppendText("\r\nLastWriteTime:" + fileInfo.LastWriteTime);
            //fileInfo.CreationTime = DateTime.Now.AddHours(-10);
            //fileInfo.LastAccessTime = DateTime.Now.AddHours(-10);
            //fileInfo.LastWriteTime = DateTime.Now.AddHours(-10);
            //fileInfo.Refresh();
            //textBox1.AppendText("\r\nCreationTime:" + fileInfo.CreationTime);
            //textBox1.AppendText("\r\nLastAccessTime:" + fileInfo.LastAccessTime);
            //textBox1.AppendText("\r\nLastWriteTime:" + fileInfo.LastWriteTime);
          
        }
        //private const int WS_EX_TOOLWINDOW = 0x00000080;
        //private const int WS_EX_NOACTIVATE = 0x08000000;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
        //        cp.Parent = IntPtr.Zero; // Keep this line only if you used UserControl
        //        return cp;

        //        //return base.CreateParams;
        //    }
        //} 
        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Focus();
            Process.Start(@"C:\WINDOWS\system32\osk.exe");
            //System.Windows.Forms.InputLanguage iLanguage = InputLanguage.CurrentInputLanguage;
            //RECT rect;
            //this.richTextBox1.Text = iLanguage.LayoutName;
            //GetWindowRect(iLanguage.Handle, out rect);
        }
        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int RegisterWindowMessage(string lpString);

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return  ;
                Process.Start(file);
                
            }
            catch (Exception)
            {
                
            }
        }
    }
    public struct GUITHREADINFO
    {
        public int cbSize;
        public int flags;
        public int hwndActive;
        public int hwndFocus;
        public int hwndCapture;
        public int hwndMenuOwner;
        public int hwndMoveSize;
        public int hwndCaret;
        public System.Drawing.Rectangle rcCaret;
    }
}

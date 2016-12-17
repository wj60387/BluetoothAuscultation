using System;
using System.Collections.Generic;
using System.ComponentModel;
 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProtocalData.Utilities;
using ProtocalData.Protocol;
using System.IO;
using ProtocalData.Protocol.Derive;

namespace BluetoothAuscultation.Forms
{
    public delegate void TimerCallBack();
    public partial class FormProcessBar : Form
        //,IStopStethoscope
    {
        public FormProcessBar(bool IsStartTimer = true, string Title = "")
        {
            IsAutoClose = false;
            InitializeComponent();
            lblMessage.Text = Title;
            if (IsStartTimer)
            {
                timer1.Interval = 1000;
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Start();
            }
            this.FormClosing += new FormClosingEventHandler(FormProcessBar_FormClosing);
        }

        void FormProcessBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
        public event TimerCallBack TimerCallBackEvent;
        #region pbProgress
        public ProgressBarStyle ProgressBarStyle
        {
            get { return this.pbProgress.Style; }
            set { this.pbProgress.Style = value; }
        }
        public int ProgressBarMaxValue
        {
            get { return this.pbProgress.Maximum; }
            set { this.pbProgress.Maximum = value; }
        }
        public int ProgressBarValue
        {
            get { return this.pbProgress.Value; }
            set { this.pbProgress.Value = value; }
        }
        #endregion
        public bool IsAutoClose { get; set; }
        public int  AutoCloseTime { get; set; }
        public bool IsStartTimer { get; set; }
        public bool TimerEnable
        {
            get
            {
                return timer1.Enabled;
            }
            set
            {
                timer1.Enabled = value;
            }
        }
        public bool CancelBtnVisible
        {
            get { return btnCancel.Visible; }
            set { btnCancel.Visible = value; }
        }
        public string Title
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }
        public string BtnText
        {
            get { return btnCancel.Text; }
            set { btnCancel.Text = value; }
        }

        private int times = 0;
        public int Times
        {
            get
            {
                return times  ;
            }
            set
            {
                times = value;
            }
        }
        void timer1_Tick(object sender, EventArgs e)
        {
             
            if (base.Disposing)
            {
                timer1.Stop();
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }
            
            
            if (TimerCallBackEvent != null && !base.Disposing)
            {
                TimerCallBackEvent();
            }
            times++;
            if (IsAutoClose && AutoCloseTime < Times)
            {
                this.Close();
            }
        }
        public event Action OnActiveClose;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (OnActiveClose != null)
            {
                OnActiveClose();
            }
            timer1.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public void SuperSocket_DataReceived(object sender, WebSocket4Net.DataReceivedEventArgs e)
        {
            var code = SerializaHelper.DeSerialize<CodeBase>(e.Data);
            if (code == null) Mediator.ShowMsg("无法处理的未知消息类型");
            if (code.Name == "Regist")
            {
                var registCode = code as RegistCode;
                File.WriteAllText(Path.Combine(Application.StartupPath, "appLicense.txt"), registCode.License);
                if (!registCode.isLegal)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        this.Title = "身份验证过期!,请重新激活此软件";
                        this.BtnText = "确定";
                        this.CancelBtnVisible = true;
                        this.btnCancel.Click += (a, b) => { System.Environment.Exit(0); };
                    }));

                }
                else
                {
                     
                    Invoke(new MethodInvoker(() =>
                          {
                              //this.Title = "身份验证通过...";
                              //this.BtnText = "确定";
                              //this.CancelBtnVisible = true;
                              this.Close();
                          }));
                }
            }

        }

        public void Stop()
        {
            timer1.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

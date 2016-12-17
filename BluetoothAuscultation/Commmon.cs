using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.Commucation;
using BluetoothAuscultation.SQLite;
using WeifenLuo.WinFormsUI.Docking;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BluetoothAuscultation
{
    public delegate void StethcopeConnectChanged(string StetName,bool isConnect);
    public delegate void ShowMessage(string Msg);
     
    public class Mediator
    {
        public static AuscultationService.AuscultationServiceClient remoteService = new AuscultationService.AuscultationServiceClient("WSHttpBinding_IAuscultationService");
        public static void Init()
        {
            using (OperationContextScope scope = new OperationContextScope(remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
              
            }
        }
        //public static AuscultationService.AuscultationServiceClient remoteService 
        //{

        //    get
        //    {
        //        //remoteService = new AuscultationService.AuscultationServiceClient("WSHttpBinding_IAuscultationService");
        //        //remoteService.ClientCredentials.UserName.UserName = "WksoInsl";
        //        //remoteService.ClientCredentials.UserName.Password = "asd@wj19900501";
        //        using (OperationContextScope scope = new OperationContextScope(remoteService.InnerChannel))
        //        {
        //            MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
        //            OperationContext.Current.OutgoingMessageHeaders.Add(header);
        //            header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
        //              OperationContext.Current.OutgoingMessageHeaders.Add(header);
        //              return _remoteService;
        //        }
        //    }
        //}
        [DefaultValue(false)]
        public static bool isBusy
        {
            get;
            set;
        }
        public static SqliteHelper sqliteHelper = new SqliteHelper(Setting.localSqliteConnectstring);
        public static SupSocket SuperSocket = new SupSocket(Setting.serverUrl);
        public static event ShowMessage ShowMessageEvent;
        public static event Action ClearMessageEvent;
        /// <summary>
        /// 听诊器连接状态变化事件
        /// </summary>
        public static event StethcopeConnectChanged StethcopeConnectChangedEvent;
        static IShow formMain;

        public static void OnStethcopeConnectChanged(string StetName, bool isConnect)
        {
            if (Mediator.StethcopeConnectChangedEvent != null)
            {
                Mediator.StethcopeConnectChangedEvent(StetName, isConnect);
            }
        }
        public static void InitMediator(IShow form)
        {
             
            formMain = form;
        }
        public static void Show(DockContent dockContent, DockStyle DockStyle)
        {
            formMain.DockShow(dockContent, DockStyle);
        }
        public static  string[] GetInfo()
        {
            return formMain.GetInfo();
        }
        public static void OpenWindow(string formName,params string[] args)
        {
            formMain.OpenWindow(formName, args);
        }
        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
           ShowMsg(e.Exception.ToString());
        }
        public static void ShowMsg(string Msg)
        {
            if (ShowMessageEvent != null)
            {
                ShowMessageEvent(Msg);
                 
            }
        }
        public static void ClearInfo()
        {
            if (ClearMessageEvent != null)
            {
                ClearMessageEvent();
            }
        }
        public static void WriteLog(string FormName,string Message)
        {
            if (!Setting.isConnected) return;
            using (OperationContextScope scope = new OperationContextScope(remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
            var endpoint = remoteService.GetEndPoint();
            string sql = "insert into UserOperLog(SN, MAC, FormName, EndPoint, Message) select {0},{1},{2},{3},{4}";
            Mediator.remoteService.ExecuteNonQuery(sql, new string[]{Setting.authorizationInfo.AuthorizationNum,
                Setting.authorizationInfo.MachineCode, FormName, endpoint, Message});
            }
        }
    }
    public interface IShow
    {
        void DockShow(DockContent form, DockStyle dockStyle);
         string[] GetInfo();
         void OpenWindow(string formName,params string[] args);
    }
}

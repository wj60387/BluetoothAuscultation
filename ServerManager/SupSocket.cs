using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;
using ProtocalData.Utilities;
using ProtocalData.Protocol;
using ProtocalData.Protocol.Derive;
using SuperSocket.ClientEngine;

namespace BluetoothAuscultation.Commucation
{

    public class SupSocket
    {
        WebSocket webSocket;
        public string serverPath=string.Empty;
        public SupSocket(string serverPath)
        {
            this.serverPath = serverPath;
        }
        public void OpenSocket(string Serialnumber = "", string MacheineCode="")
        {
            var SN = new KeyValuePair<string, string>("SN", Serialnumber);
            var MAC = new KeyValuePair<string, string>("MAC", MacheineCode);
            webSocket = new WebSocket(serverPath, new List<KeyValuePair<string, string>>() { SN,MAC });
           
            webSocket.ReceiveBufferSize = 1024 * 1024;
            webSocket.Open();
            webSocket.Opened += new EventHandler(webSocket_Opened);
            webSocket.Closed += new EventHandler(webSocket_Closed);
            webSocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(webSocket_Error);
        }
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler Closed;
        public event EventHandler Opened;
     
        

        
        void webSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (Error != null)
            {
                Error(sender,e);
            }
        }

        void webSocket_DataReceived(object sender, DataReceivedEventArgs e)
        {

            if (DataReceived != null)
            {
                DataReceived(sender, e);
            }
           
        }

        void webSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {

            if (MessageReceived != null)
            {
                MessageReceived(sender, e);
            }
        }

        void webSocket_Closed(object sender, EventArgs e)
        {
            if (Closed != null)
            {
                Closed(sender, e);
            }
            
            webSocket.MessageReceived -=    webSocket_MessageReceived;
            webSocket.DataReceived -=     webSocket_DataReceived;
      
        }

        void webSocket_Opened(object sender, EventArgs e)
        {
            if (Opened != null)
            {
                Opened(sender, e);
            }
           
            webSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(webSocket_MessageReceived);
            webSocket.DataReceived += new EventHandler<DataReceivedEventArgs>(webSocket_DataReceived);
      
        }
        public void Send(string message)
        {
                webSocket.Send(message);
        }
        public void Send(byte[] bytes)
        { 
                webSocket.Send(bytes, 0, bytes.Length);
        }
        public WebSocketState WebSocketState
        {
            get
            {
                
                return webSocket.State;
            }
        }

    }
}

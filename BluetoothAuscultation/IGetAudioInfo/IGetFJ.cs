using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IO;

namespace BluetoothAuscultation.IGetAudioInfo
{
    public interface IGetFJ
    {
        string Dir { get;   }
        string[] GetFileName(string Folder);

    }
    public class LoaclReocrd : IGetFJ
    {
        public string Dir { get { return @"Enclosure\Local\{0}"; } }
        public string[] GetFileName(string GUID)
        {
            var dir = string.Format(Dir, GUID);
            if (!Directory.Exists(dir))
                return null;
            return Directory.GetFiles(dir);
        }

    }
    public class LoaclDown : IGetFJ
    {
        public string Dir { get { return @"Enclosure\Down\{0}"; } }
        public string[] GetFileName(string GUID)
        {
            var dir = string.Format(Dir, GUID);
            if (!Directory.Exists(dir))
                return null;
            return Directory.GetFiles(dir);
        }
    }
    public class CloudReocrd : IGetFJ
    {
        public string Dir { get { return @"Enclosure\{0}"; } }

        public string[] GetFileName(string GUID)
        {
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var root = Mediator.remoteService.GetRoot();
                var dir = Path.Combine(root, string.Format(Dir, GUID));
                var files = Mediator.remoteService.GetFolderFiles(dir,"*.*",true);
                return files;
            }
        }
    }
    public class CloudDown : IGetFJ
    {
        public string Dir { get { return @"Enclosure\{0}"; } }

        public string[] GetFileName(string GUID)
        {
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var root = Mediator.remoteService.GetRoot();
                var dir = Path.Combine(root, string.Format(Dir, GUID));
                var files = Mediator.remoteService.GetFolderFiles(dir, "*.*", true);
                return files;
            }
        }
    }
}

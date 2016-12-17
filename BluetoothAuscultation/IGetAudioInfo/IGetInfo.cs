using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Drawing;
using System.IO;

namespace BluetoothAuscultation.IGetAudioInfo
{
    public interface IGetInfo
    {
        bool isPlay { get; }
        DataTable GetAudioByHis(string PGUID );

        Image[] GetImage(string PGUID); 

    }
    public class LocalDown : IGetInfo
    {

        public Image[] GetImage(string PGUID)
        {
            string Dir = @"Enclosure\Down\{0}";
            var downDir = string.Format(Dir, PGUID);
            List<Image> list = new List<Image>();
            if (Directory.Exists(downDir))
            {
               
                var files = Directory.GetFiles(downDir);
                foreach (var file in files)
                {
                    if (list.Count > 3) break;
                    Image image = Image.FromFile(file) ;
                    if (image != null)
                    {
                        list.Add(image);
                    }
                        
                }
            }
            return list.ToArray();
        }
        public bool isPlay
        {
            get
            {
                return true;
            }
        }

        

        public DataTable GetAudioByHis(string PGUID )
        {
            string sql = "select * from AudioInfoDown where PGUID={0}  ";
            var dt = Mediator.sqliteHelper.ExecuteDatatable(sql, PGUID );
            return dt;
        }
    }

    public class CloudUpload : IGetInfo
    {

        public bool isPlay
        {
            get { return false; }
        }
        
        public DataTable GetAudioByHis(string PGUID )
        {
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                string sql = "select * from AudioInfo where PGUID={0}  ";
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { PGUID });
                return ds.Tables[0];
            }
        }
        public Image[] GetImage(string PGUID)
        {
            List<Image> list = new List<Image>();
            var arrPic = new string[] { ".jpg", ".png", ".gif", ".bmp" };
            var Dir = @"Enclosure\{0}";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var root = Mediator.remoteService.GetRoot();
                var dir = Path.Combine(root, string.Format(Dir, PGUID));
                var files = Mediator.remoteService.GetFolderFiles(dir, "*.*", true);
                if (files == null) return list.ToArray();
                var picFile = files.Where(f => arrPic.Contains(Path.GetExtension(f))).ToArray();
                foreach (var remoteFile in picFile)
                {
                    if (list.Count > 3) break;
                    var fileSize = Mediator.remoteService.GetFileLength(remoteFile);
                    string guid = Guid.NewGuid().ToString();
                    var fileLocalPath = string.Format("Enclosure\\Temp\\{0}", guid+Path.GetExtension(remoteFile));
                    if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                    }
                    using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        long position = 0;
                        while (position < fileSize)
                        {
                            var bytes = Mediator.remoteService.DownLoadFile(remoteFile, position, 24 * 1024);
                            position += bytes.Length;
                            stream.Write(bytes, 0, bytes.Length);
                        }
                        stream.Close();

                    }
                    Image image = new Bitmap(fileLocalPath);
                    if(image!=null)
                    list.Add(image);
                  //  File.Delete(fileLocalPath);
                }
            }
            return list.ToArray();
        }
    }
    public class CloudShare : IGetInfo
    {
        public bool isPlay
        {
            get { return false; }
        }
      
        public DataTable GetAudioByHis(string PGUID )
        {
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                string sql = "select * from AudioInfo where PGUID={0}  ";
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { PGUID });
                return ds.Tables[0];
            }
        }
        //public Image[] GetImage(string PGUID)
        //{
        //    string Dir = @"Enclosure\Down\{0}";
        //    var downDir = string.Format(Dir, PGUID);
        //    List<Image> list = new List<Image>();
        //    if (Directory.Exists(downDir))
        //    {

        //        var files = Directory.GetFiles(downDir);
        //        foreach (var file in files)
        //        {
        //            if (list.Count > 3) break;
        //            Image image = new Bitmap(file);
        //            if (image != null)
        //            {
        //                list.Add(image);
        //            }

        //        }
        //    }
        //    return list.ToArray();
        //}
        public Image[] GetImage(string PGUID)
        {
            List<Image> list = new List<Image>();
            var arrPic = new string[] { ".jpg", ".png", ".gif", ".bmp" };
            var Dir = @"Enclosure\{0}";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", Setting.authorizationInfo.AuthorizationNum);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", Setting.authorizationInfo.MachineCode);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var root = Mediator.remoteService.GetRoot();
                var dir = Path.Combine(root, string.Format(Dir, PGUID));
                var files = Mediator.remoteService.GetFolderFiles(dir, "*.*", true);
                if (files == null) return list.ToArray();
                var picFile = files.Where(f => arrPic.Contains(Path.GetExtension(f))).ToArray();
                foreach (var remoteFile in picFile)
                {
                    if (list.Count > 3) break;
                    var fileSize = Mediator.remoteService.GetFileLength(remoteFile);
                    string guid = Guid.NewGuid().ToString();
                    var fileLocalPath = string.Format("Enclosure\\Temp\\{0}", guid+Path.GetExtension(remoteFile));
                    if (!Directory.Exists(Path.GetDirectoryName(fileLocalPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fileLocalPath));
                    }
                    using (var stream = new FileStream(fileLocalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        long position = 0;
                        while (position < fileSize)
                        {
                            var bytes = Mediator.remoteService.DownLoadFile(remoteFile, position, 24 * 1024);
                            position += bytes.Length;
                            stream.Write(bytes, 0, bytes.Length);
                        }
                        stream.Close();

                    }
                    Image image = new Bitmap(fileLocalPath);
                    if (image != null)
                        list.Add(image);
                    //File.Delete(fileLocalPath);
                }
            }
            return list.ToArray();
        }
    }
}

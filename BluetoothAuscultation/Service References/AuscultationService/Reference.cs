﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BluetoothAuscultation.AuscultationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuscultationService.IAuscultationService")]
    public interface IAuscultationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetEndPoint", ReplyAction="http://tempuri.org/IAuscultationService/GetEndPointResponse")]
        string GetEndPoint();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/Insert", ReplyAction="http://tempuri.org/IAuscultationService/InsertResponse")]
        void Insert(string tableName, System.Data.DataTable dataTable);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/Update", ReplyAction="http://tempuri.org/IAuscultationService/UpdateResponse")]
        void Update(string sqlText, System.Data.DataTable table);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/AccountCredentials", ReplyAction="http://tempuri.org/IAuscultationService/AccountCredentialsResponse")]
        string AccountCredentials(string MAC, string SN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/UpdateStetInfo", ReplyAction="http://tempuri.org/IAuscultationService/UpdateStetInfoResponse")]
        bool UpdateStetInfo(string stetInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetFilePath", ReplyAction="http://tempuri.org/IAuscultationService/GetFilePathResponse")]
        string GetFilePath(string StetName, System.DateTime RecordTime, string Guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetFilePath2", ReplyAction="http://tempuri.org/IAuscultationService/GetFilePath2Response")]
        string GetFilePath2(System.DateTime RecordTime, string Guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetRoot", ReplyAction="http://tempuri.org/IAuscultationService/GetRootResponse")]
        string GetRoot();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetData", ReplyAction="http://tempuri.org/IAuscultationService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/ExecuteDataset", ReplyAction="http://tempuri.org/IAuscultationService/ExecuteDatasetResponse")]
        System.Data.DataSet ExecuteDataset(string sql, string[] Args);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/ExecuteNonQuery", ReplyAction="http://tempuri.org/IAuscultationService/ExecuteNonQueryResponse")]
        int ExecuteNonQuery(string sqlText, string[] Args);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/ExecuteScalar", ReplyAction="http://tempuri.org/IAuscultationService/ExecuteScalarResponse")]
        string ExecuteScalar(string sqlText, string[] Args);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/DeleteFile", ReplyAction="http://tempuri.org/IAuscultationService/DeleteFileResponse")]
        void DeleteFile(string FileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/isExistFolder", ReplyAction="http://tempuri.org/IAuscultationService/isExistFolderResponse")]
        bool isExistFolder(string FolderPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/CreateFolder", ReplyAction="http://tempuri.org/IAuscultationService/CreateFolderResponse")]
        void CreateFolder(string FolderPath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetFolderFiles", ReplyAction="http://tempuri.org/IAuscultationService/GetFolderFilesResponse")]
        string[] GetFolderFiles(string FolderPath, string fileFilter, bool isCurLevel);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetFileLength", ReplyAction="http://tempuri.org/IAuscultationService/GetFileLengthResponse")]
        long GetFileLength(string FilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/isExistFile", ReplyAction="http://tempuri.org/IAuscultationService/isExistFileResponse")]
        bool isExistFile(string FilePath);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/UpLoadFile", ReplyAction="http://tempuri.org/IAuscultationService/UpLoadFileResponse")]
        void UpLoadFile(string FileName, long offset, byte[] bytes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/DownLoadFile", ReplyAction="http://tempuri.org/IAuscultationService/DownLoadFileResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="bytes")]
        byte[] DownLoadFile(string FileName, long offset, int len);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuscultationService/GetFilesInfo", ReplyAction="http://tempuri.org/IAuscultationService/GetFilesInfoResponse")]
        System.Collections.Generic.Dictionary<string, long> GetFilesInfo(out long totalLength, string FolderName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuscultationServiceChannel : BluetoothAuscultation.AuscultationService.IAuscultationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuscultationServiceClient : System.ServiceModel.ClientBase<BluetoothAuscultation.AuscultationService.IAuscultationService>, BluetoothAuscultation.AuscultationService.IAuscultationService {
        
        public AuscultationServiceClient() {
        }
        
        public AuscultationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuscultationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuscultationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuscultationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetEndPoint() {
            return base.Channel.GetEndPoint();
        }
        
        public void Insert(string tableName, System.Data.DataTable dataTable) {
            base.Channel.Insert(tableName, dataTable);
        }
        
        public void Update(string sqlText, System.Data.DataTable table) {
            base.Channel.Update(sqlText, table);
        }
        
        public string AccountCredentials(string MAC, string SN) {
            return base.Channel.AccountCredentials(MAC, SN);
        }
        
        public bool UpdateStetInfo(string stetInfo) {
            return base.Channel.UpdateStetInfo(stetInfo);
        }
        
        public string GetFilePath(string StetName, System.DateTime RecordTime, string Guid) {
            return base.Channel.GetFilePath(StetName, RecordTime, Guid);
        }
        
        public string GetFilePath2(System.DateTime RecordTime, string Guid) {
            return base.Channel.GetFilePath2(RecordTime, Guid);
        }
        
        public string GetRoot() {
            return base.Channel.GetRoot();
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Data.DataSet ExecuteDataset(string sql, string[] Args) {
            return base.Channel.ExecuteDataset(sql, Args);
        }
        
        public int ExecuteNonQuery(string sqlText, string[] Args) {
            return base.Channel.ExecuteNonQuery(sqlText, Args);
        }
        
        public string ExecuteScalar(string sqlText, string[] Args) {
            return base.Channel.ExecuteScalar(sqlText, Args);
        }
        
        public void DeleteFile(string FileName) {
            base.Channel.DeleteFile(FileName);
        }
        
        public bool isExistFolder(string FolderPath) {
            return base.Channel.isExistFolder(FolderPath);
        }
        
        public void CreateFolder(string FolderPath) {
            base.Channel.CreateFolder(FolderPath);
        }
        
        public string[] GetFolderFiles(string FolderPath, string fileFilter, bool isCurLevel) {
            return base.Channel.GetFolderFiles(FolderPath, fileFilter, isCurLevel);
        }
        
        public long GetFileLength(string FilePath) {
            return base.Channel.GetFileLength(FilePath);
        }
        
        public bool isExistFile(string FilePath) {
            return base.Channel.isExistFile(FilePath);
        }
        
        public void UpLoadFile(string FileName, long offset, byte[] bytes) {
            base.Channel.UpLoadFile(FileName, offset, bytes);
        }
        
        public byte[] DownLoadFile(string FileName, long offset, int len) {
            return base.Channel.DownLoadFile(FileName, offset, len);
        }
        
        public System.Collections.Generic.Dictionary<string, long> GetFilesInfo(out long totalLength, string FolderName) {
            return base.Channel.GetFilesInfo(out totalLength, FolderName);
        }
    }
}

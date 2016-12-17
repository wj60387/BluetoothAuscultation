using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothAuscultation.Models
{
    /// <summary>
    /// 描述一段录音
    /// </summary>
    public class AudioDownLoadInfo
    {
       
        public string Guid{get;set;}
        public string StetSerialNumber { get; set; }
        public string StetName { get; set; }
        public int PatientType { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Posture { get; set; }
        public string Part { get; set; }
        public float TakeTime { get; set; }
        public string Remark { get; set; }
        public string DocName { get; set; }
        public string DocDiagnose { get; set; }
        public DateTime RecordTime { get; set; }
        public string ShareStetName  { get; set; }
        public string ShareToStetName { get; set; }
        public DateTime ShareTime { get; set; }
        public int Insert()
        {
            string querySql = "select 1 from AudioDownLoadInfo where GUID={0} and ShareToStetName={1}";
            if (null != Mediator.sqliteHelper.ExecuteScalar(querySql, Guid, ShareToStetName))
                return 0;

            string sql = "insert into AudioDownLoadInfo select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}";
            int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid, StetSerialNumber, StetName,PatientType, PatientID, PatientName, Posture,
                Part, RecordTime.ToString("yyyy-MM-dd HH:mm:ss"), TakeTime.ToString(), Remark,DocName,DocDiagnose, ShareStetName, ShareToStetName, ShareTime == DateTime.MinValue ? "" : ShareTime.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return count;

        }
//        public int Update()
//        {
//            string sql = @"update AudioDownLoadInfo set  PatientID={1},
//            PatientName={2},Posture={3},Part={4},Remark={5} where Guid={0}";
//            int count = Mediator.sqliteHelper.ExecuteNonQuery(sql,Guid, PatientID, PatientName, Posture, Part,Remark);
//            return count;
//        }
        public int Delete()
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(ShareStetName))
            {
                sql = @"delete from AudioDownLoadInfo  where Guid={0} ";
                int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid);
                return count;
            }
            else
            {
                sql = @"delete from AudioDownLoadInfo  where Guid={0} and ShareStetName={1} and ShareToStetName={2}";
                int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid, ShareStetName, ShareToStetName);
                return count;
            }
        }
    }
}

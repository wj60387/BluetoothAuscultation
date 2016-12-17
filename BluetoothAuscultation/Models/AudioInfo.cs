using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothAuscultation.Models
{
    /// <summary>
    /// 描述一段录音
    /// </summary>
    public class AudioInfo
    {
        public string Guid{get;set;}
        public string StetSerialNumber { get; set; }
        public string StetName { get; set; }
        /// <summary>
        /// 患者类型：1-儿科,2-成人
        /// </summary>
        public int PatientType { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Posture { get; set; }
        public string Part { get; set; }
        public float TakeTime { get; set; }
        public string Remark { get; set; }
        public DateTime RecordTime { get; set; }
        public string DocName { get; set; }
        public string DocDiagnose { get; set; }
        public int Insert()
        {
            string sql = "insert into AudioInfo select {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}";
            int count=Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid, StetSerialNumber, StetName,PatientType, PatientID, PatientName,
                Posture, Part, TakeTime, Remark, RecordTime, DocName, DocDiagnose, 0);
            return count;
        }
        public int Update()
        {
            string sql = @"update AudioInfo set PatientType={8}, PatientID={1},
            PatientName={2},Posture={3},Part={4},Remark={5},DocName={6},DocDiagnose={7} where Guid={0}";
            int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid, PatientID, PatientName, Posture, Part, Remark, DocName, DocDiagnose, PatientType);
            return count;
        }
        public int Delete()
        {
            string sql = @"delete from AudioInfo  where Guid={0}";
            int count = Mediator.sqliteHelper.ExecuteNonQuery(sql, Guid);
            return count;
        }
    }
}

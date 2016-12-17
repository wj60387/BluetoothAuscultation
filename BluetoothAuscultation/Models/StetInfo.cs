using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothAuscultation.Models
{
    public class StetInfo
    {
        public string StetName { get; set; }
        public string SN { get; set; }
        public string MAC { get; set; }
        public string PCName { get; set; }
        public int StetType { get; set; }
        public string StetChineseName { get; set; }
        public string Owner { get; set; }
        public string FuncDescript { get; set; }
        public string ReMark { get; set; }
        public DateTime RecordTime { get; set; }
        public override string ToString()
        {
            return string.IsNullOrEmpty(StetChineseName) ? StetName : StetChineseName + "(" + Owner + ")";
        }
       
    }
}

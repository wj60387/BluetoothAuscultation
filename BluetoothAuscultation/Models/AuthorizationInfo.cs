using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothAuscultation.Models
{
    public class AuthorizationInfo
    {
        public string AuthorizationNum { get; set; }
        public string MachineCode { get; set; }
        public DateTime AuthStartTime { get; set; }
        public DateTime LastUseTime { get; set; }
        public float AuthDays { get; set; }
        public string HashCode { get; set; }
        public int GroupID { get; set; }
    }
}

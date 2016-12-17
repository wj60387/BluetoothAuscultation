using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluetoothAuscultation
{
    public class TreeNodeEx:TreeNode
    {
        public TreeNodeEx():base()
        {

        }
        public TreeNodeEx(string StetName)
            : base(StetName)
        {
            this.StetName = StetName;

        }
        public bool isConnected { get; set; }
        public string StetName { get; set; }
        public string StetChineseName { get; set; }
        public string StetOwner { get; set; }
        public override string ToString()
        {
            return string.IsNullOrEmpty(StetChineseName) ? StetName : StetChineseName + "(" + StetOwner + ")";
        }
    }
}

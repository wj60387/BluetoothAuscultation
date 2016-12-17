using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluetoothAuscultation.ExtendControl
{
    public static class Expand
    {
        /// <summary>
        /// 系列节点 Checked 属性控制
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(this TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    e.Node.CheckParentNode();
                    if (e.Node.Nodes.Count > 0)
                    {
                        e.Node.CheckAllChildNodes(e.Node.Checked);
                    }
                }
            }
        }
        

        #region 私有方法

        //改变所有子节点的状态
        private static void CheckAllChildNodes(this TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    CheckAllChildNodes(tn, IsChecked);
                }
            }
        }

        //改变父节点的选中状态，此处为所有子节点不选中时才取消父节点选中，可以根据需要修改
        private static void CheckParentNode(this TreeNode curNode)
        {
            bool bChecked = false;
            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }
                curNode.Parent.Checked = bChecked;
                curNode.Parent.CheckParentNode();
            }
        }

       
        #endregion
    }
}
 

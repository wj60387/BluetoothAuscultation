using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BluetoothAuscultation.ExtendControl
{
    public class PicControl:Control
    {
       public bool isAllowAI{get;set;}
       public  List<PicItem> lstPicItem {get;set;}
        public PicControl()
        {
            this.Size = new System.Drawing.Size(40,40);
            isAllowAI = false;
            lstPicItem=new List<PicItem>();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < lstPicItem.Count; i++)
            {
                Rectangle ract = new Rectangle(0, 1, this.Height, this.Height);    
            }
        }
    }
}

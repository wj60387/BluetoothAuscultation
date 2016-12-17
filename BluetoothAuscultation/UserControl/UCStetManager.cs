using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MMM.HealthCare.Scopes.Device;

namespace BluetoothAuscultation.UserControl
{
    public partial class UCStetManager : System.Windows.Forms.UserControl
    {
        public UCStetManager()
        {
            InitializeComponent();
        }
        public void Init(IEnumerable<Stethoscope> Stethoscopes)
        {

            foreach (var stethoscope in Stethoscopes)
            {
                UCStetItem item = new UCStetItem()
                    {
                        stethoscope = stethoscope
                    };
                InsertItem(item);
            }
        }
        public void InsertItem(Control control)
        {
            control.Height = 25;
            control.Dock = DockStyle.Top;
            this.Controls.Add(control);
        }
        public void RemoveItem(Control control)
        {
            this.Controls.Remove(control);
        }
        public void SelectAll(bool value)
        {
            foreach (var control in Controls)
            {
                var item = control as UCStetItem;
                item.isCheck = value;
            }
        }
        public List<Stethoscope> GetStethoscope()
        {
            List<Stethoscope> list = new List<Stethoscope>();
            foreach (var control in Controls)
            {
                var item = control as UCStetItem;
                if (item.isCheck)
                    list.Add(item.stethoscope);
            }
            return list;
        }
         
    }
}

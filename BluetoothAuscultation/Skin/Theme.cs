using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BluetoothAuscultation
{
    public static class  Theme
    {
        public static void SetBackColor(this Form form, Color color)
        {
            form.BackColor = color;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BluetoothAuscultation.Forms;
using BluetoothAuscultation;

namespace ServerManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Mediator.InitSuperSocket();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAdmin());
        }
    }
}

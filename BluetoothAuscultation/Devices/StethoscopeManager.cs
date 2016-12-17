using MMM.HealthCare.Scopes.Device;
using System;
using System.Collections.Generic;
using BluetoothAuscultation.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.Forms;
using System.Threading;
namespace BluetoothAuscultation.Devices
{
    public class StethoscopeManager
    {
        public static IEnumerable<Stethoscope> StethoscopeList=GetStethoscopeDevices();
        public static IEnumerable<Stethoscope> GetStethoscopeDevices()
        {

            try
            {
                IBluetoothManager manager = ConfigurationFactory.GetBluetoothManager();
                return manager.GetPairedDevices();
            }
            catch (PlatformNotSupportedException)
            {
                throw new Exception("找不到电脑中的听诊器， 请确认蓝牙设备已经开启！");
            }
            catch (Exception)
            {
                throw new Exception("取得听诊器失败！");
            }

        }

         
    }
}


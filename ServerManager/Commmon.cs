using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using BluetoothAuscultation.Commucation;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Net;

namespace BluetoothAuscultation
{
    public delegate void StethcopeConnectChanged(string StetName,bool isConnect);
    public delegate void ShowMessage(string Msg);
     
    public class Mediator
    {
        public static string MAC = GetMacAddressByNetworkInformation();
        public static string SN = System.Configuration.ConfigurationManager.AppSettings["SN"];
        public static ServerManager.AuscultationService.AuscultationServiceClient remoteService = 
            new ServerManager.AuscultationService.AuscultationServiceClient("WSHttpBinding_IAuscultationService");
       public static void InitAuscultationService()
       {
            
           //remoteService = new AuscultationService.AuscultationServiceClient("WSHttpBinding_IAuscultationService");
           //remoteService.ClientCredentials.UserName.UserName = "WksoInsl";
           //remoteService.ClientCredentials.UserName.Password = "asd@wj19900501";
       }
       public static string serAddress = System.Configuration.ConfigurationManager.AppSettings["serverAddress"];
       //public static SupSocket SuperSocket = new SupSocket("ws://114.55.248.163:2014");
      // public static SupSocket SuperSocket = new SupSocket(serAddress);
        //public static void InitSuperSocket()
        //{
        //    SuperSocket.OpenSocket(SN, MAC);
        //    //remoteService = new AuscultationService.AuscultationServiceClient("WSHttpBinding_IAuscultationService");
        //    //remoteService.ClientCredentials.UserName.UserName = "WksoInsl";
        //    //remoteService.ClientCredentials.UserName.Password = "asd@wj19900501";
        //}
        public static string GetMachineName()
        {
            return Dns.GetHostName();
        }
        public static string GetMacAddressByNetworkInformation()
        {

            string key = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            string macAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                        && adapter.GetPhysicalAddress().ToString().Length != 0)
                    {
                        string fRegistryKey = key + adapter.Id + "\\Connection";
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rk != null)
                        {
                            string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 && !fPnpInstanceID.Contains("VMWARE")
                                //&&  fPnpInstanceID.Substring(0, 3) == "PCI"
                                )
                            {
                                macAddress = adapter.GetPhysicalAddress().ToString();
                                for (int i = 1; i < 6; i++)
                                {
                                    macAddress = macAddress.Insert(3 * i - 1, ":");
                                }
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //这里写异常的处理  
            }
            ;
            return macAddress;
        }  
       
    }
     
}

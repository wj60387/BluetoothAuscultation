using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace BluetoothAuscultation.Utilities
{
   
    public class CommonUtil
    {
        private const string UNKOWN = "UNKOWN";

        public static string baseToString(string ascii)
        {
            string str = "";
            foreach (string str2 in ascii.Split(new char[] { ',' }))
            {
                str = str + Encoding.ASCII.GetString(new byte[] { (byte) int.Parse(str2) });
            }
            return str;
        }

        public static string Decode(string decryptString)
        {
            string str = "";
            try
            {
                string key = GetKey();
                byte[] inputBuffer = Convert.FromBase64String(decryptString);
                byte[] bytes = Encoding.ASCII.GetBytes(key.Substring(0, 0x18));
                ICryptoTransform transform = new TripleDESCryptoServiceProvider { Key = bytes, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }.CreateDecryptor();
                str = Encoding.Unicode.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static string Encode(string encryptString)
        {
            string str = "";
            try
            {
                string key = GetKey();
                byte[] bytes = Encoding.Unicode.GetBytes(encryptString);
                byte[] buffer2 = Encoding.ASCII.GetBytes(key.Substring(0, 0x18));
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider {
                    Key = buffer2,
                    Mode = CipherMode.ECB
                };
                str = Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static string GetBaseBoard()
        {
            ManagementClass class2 = null;
            ManagementObjectCollection instances = null;
            string str2;
            try
            {
                instances = new ManagementClass("Win32_BaseBoard").GetInstances();
                string str = null;
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                str2 = str;
            }
            catch (Exception)
            {
                str2 = "UNKOWN";
            }
            finally
            {
                class2 = null;
                instances = null;
            }
            return str2;
        }

        public static string GetBiosID()
        {
            ManagementClass class2 = null;
            ManagementObjectCollection instances = null;
            string str2;
            try
            {
                instances = new ManagementClass("Win32_BIOS").GetInstances();
                string str = null;
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                str2 = str;
            }
            catch (Exception)
            {
                str2 = "UNKOWN";
            }
            finally
            {
                class2 = null;
                instances = null;
            }
            return str2;
        }

        public static string GetCPUID()
        {
            ManagementClass class2 = null;
            ManagementObjectCollection objects = null;
            string str2;
            try
            {
                string str = "";
                class2 = new ManagementClass("Win32_Processor");
                foreach (ManagementObject obj2 in class2.GetInstances())
                {
                    str = obj2.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                str2 = str;
            }
            catch
            {
                str2 = "UNKOWN";
            }
            finally
            {
                objects = null;
                class2 = null;
            }
            return str2;
        }

        public static string GetDiskID()
        {
            ManagementClass class2 = null;
            ManagementObjectCollection objects = null;
            string str2;
            try
            {
                string str = "";
                class2 = new ManagementClass("Win32_DiskDrive");
                foreach (ManagementObject obj2 in class2.GetInstances())
                {
                    str = (string) obj2.Properties["Model"].Value;
                    break;
                }
                str2 = str;
            }
            catch
            {
                str2 = "UNKOWN";
            }
            finally
            {
                objects = null;
                class2 = null;
            }
            return str2;
        }

        private static string GetKey()
        {
            return "6Ft%&hj7x8(*jkP8H$yuBIvHUFH7lJ$lhj!y6fadsr43ffa332324=0=0UFH7Guz(j&0456Ft%&hj7";
        }
        public static string GetMachineName()
        {
          return  Dns.GetHostName();
        }
        public static string GetMacAddressByNetworkInformation()
        {
             #if DEBUG
             return Setting.virtualMac;
             #endif
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

        public static string[] GetAllMacAddress()
        {
            NetworkInterface[] networkInterface = NetworkInterface.GetAllNetworkInterfaces();
            List<string> arrMac = new List<string>();
            foreach (var adapter in networkInterface)
            {
                var mac = adapter.GetPhysicalAddress();
                byte[] bytes = mac.GetAddressBytes();
                string macAddress = string.Empty;
                for (int i = 0; i < bytes.Length; i++)
                {
                    macAddress+=":"+bytes[i].ToString("X2");
                }
              arrMac.Add(  macAddress.Substring(1));
            }
            return arrMac.ToArray();
        }

        public static string GetMacInfo()
        {
            string cPUID = GetCPUID();
            string baseBoard = GetBaseBoard();
            string biosID = GetBiosID();
            List<MacInfo> list2 = new List<MacInfo>();
            MacInfo item = new MacInfo {
                Name = "CPU",
                Value = cPUID
            };
            list2.Add(item);
            MacInfo info2 = new MacInfo {
                Name = "BaseBoard",
                Value = baseBoard
            };
            list2.Add(info2);
            MacInfo info3 = new MacInfo {
                Name = "Bios",
                Value = biosID
            };
            list2.Add(info3);
            List<MacInfo> list = list2;
            return null;
            //return new JavaScriptSerializer().Serialize(list);
        }

        public static string GetMD5(string myString)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(myString);
            byte[] buffer2 = md.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder(0x2a);
            for (int i = 0; i < buffer2.Length; i++)
            {
                builder.Append(buffer2[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static string GetRegistryInfo(string rootKey, string subKey, string targetKey)
        {
            Microsoft.Win32.RegistryKey currentUser = Registry.CurrentUser;
            Microsoft.Win32.RegistryKey key2 = currentUser.CreateSubKey(rootKey).CreateSubKey(subKey);
            object obj2 = key2.GetValue(targetKey);
            key2.Close();
            currentUser.Close();
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return "";
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        public static bool IsConnected()
        {
            int connectionDescription = 0;
            return InternetGetConnectedState(out connectionDescription, 0);
        }

        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping ping = new Ping();
                PingOptions options = new PingOptions {
                    DontFragment = true
                };
                string s = "";
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                int timeout = 120;
                return (ping.Send(strIpOrDName, timeout, bytes, options).Status.ToString() == "Success");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RegistryKey(string rootKey, string subKey, string targetKey, object targetValue)
        {
            try
            {
                Microsoft.Win32.RegistryKey currentUser = Registry.CurrentUser;
                Microsoft.Win32.RegistryKey key2 = currentUser.CreateSubKey(rootKey).CreateSubKey(subKey);
                key2.SetValue(targetKey, targetValue);
                key2.Close();
                currentUser.Close();
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static string stringToBase(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                int num2 = bytes[i];
                if ("".Equals(str))
                {
                    str = str + Convert.ToString(num2);
                }
                else
                {
                    str = str + "," + Convert.ToString(num2);
                }
            }
            return str;
        }

        public static bool TestConnectServer(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(url);
                response = (HttpWebResponse) request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                request = null;
                if (response != null)
                {
                    response.Close();
                }
                response = null;
            }
            return true;
        }

        public class MacInfo
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}


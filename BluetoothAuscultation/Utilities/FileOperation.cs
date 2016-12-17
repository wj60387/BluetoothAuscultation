using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
namespace RemoteAuscultation.Utilities
{
    public static class FileOperation
    {
        public static string CheckDataFile(string dataFile)
        {
            if (string.IsNullOrEmpty(dataFile))
            {
                throw new ArgumentNullException("data file");
            }
            dataFile = dataFile.Trim();
            if (dataFile.StartsWith("~"))
            {
                dataFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFile.TrimStart(new char[] { '~', '\\', '/' }));
            }
            if (!Path.IsPathRooted(dataFile))
            {
                dataFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFile);
            }
            string directoryName = Path.GetDirectoryName(dataFile);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            return dataFile;
        }

        public static bool DelDirectory(string dirPath)
        {
            bool flag = false;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DelFile(string filePath)
        {
            bool flag = false;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool ExeCompression(string strFileToCompress, string strCompressZipPath)
        {
            bool flag = false;
            try
            {
                using (ZipFile file = new ZipFile(strCompressZipPath, Encoding.Default))
                {
                    if (File.Exists(strFileToCompress))
                    {
                        file.AddFile(strFileToCompress, string.Empty);
                    }
                    else
                    {
                        file.AddDirectory(strFileToCompress);
                    }
                    file.Save();
                    file.Dispose();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool ExeDecompression(string strDecompressionZipPath, string strDecompressionPath)
        {
            bool flag = false;
            try
            {
                using (ZipFile file = new ZipFile(strDecompressionZipPath, Encoding.Default))
                {
                    file.ExtractAll(strDecompressionPath);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static int FlieNameWhitoutType(string FlieName)
        {
            return (FlieName.LastIndexOf(".") + 1);
        }

        public static List<string> GetAllFileByPath(string path, List<string> list, string filterType)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo info2 in info.GetFiles())
            {
                if (filterType == null)
                {
                    list.Add(info2.FullName);
                }
                else if (info2.Extension.Substring(info2.Extension.IndexOf(".") + 1).ToLower().Equals(filterType.ToLower()))
                {
                    list.Add(info2.FullName);
                }
            }
            foreach (DirectoryInfo info3 in info.GetDirectories())
            {
                if (Directory.Exists(info3.FullName))
                {
                    GetAllFileByPath(info3.FullName, list, filterType);
                }
            }
            return list;
        }

        public static bool MoveFile(string filePath, string targetFilePath)
        {
            bool flag = false;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Move(filePath, targetFilePath);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }
    }
}


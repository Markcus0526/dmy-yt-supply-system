using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Web.Security;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using YiTongBackend.Models;

namespace YiTongBackend.Models
{
    public class ComboBoxDataItem
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }

    public class Foreign_Operation
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Foreign_Allow
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Foreign_Priority
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Foreign_Sex
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchDateType
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class SearchOption
    {
        public long key { get; set; }
        public string value { get; set; }
    }

    public class SmtpSecureOption
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public static class RESULTS
    {
        public const int ERROR = -1;
    }

    public static class DBRESULT
    {
        public const int CANNOT_DELETE = 520;
    }
    
    public class CommonModel
    {
        public static YiTongDBDataContext _db;
        public static string _logFilename = "Log.txt";

        public static string GetDisplayDateFormat()
        {
            return "yyyy年M月d日";
        }

        public static string GetDisplayDateTimeFormat()
        {
            return "yyyy年M月d日 HH:mm:ss";
        }

        public static string GetDisplayTimeFormat()
        {
            return "HH - mm";
        }

        public static string GetParamDateFormat()
        {
            return "yyyy-MM-dd";
        }

        public static string GetParamDateTimeFormat()
        {
            return "yyyy-MM-dd HH:mm:ss";
        }

        public static string ConnectionString
        {
            get 
            {
                return ConfigurationManager.ConnectionStrings["YiTongConnectionString1"].ConnectionString;
            }
        }
        
        public string GetCurrentUserName()
        {
            FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

            if (id != null)
            {
                return id.Name;
            }

            return null;
        }

        public static void WriteLogFile(string fileName, string methodName, string message)
        {

            try
            {
                string filepath = HostingEnvironment.MapPath("~/") + "\\" + _logFilename;
                if (!string.IsNullOrEmpty(message))
                {
                    using (FileStream file = new FileStream(filepath, File.Exists(filepath)? FileMode.Append:FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter streamWriter = new StreamWriter(file);
                        streamWriter.WriteLine((((System.DateTime.Now + " - ") + fileName + " - ") + methodName + " - ") + message);
                        streamWriter.Close();
                    }
                }
            }
            catch {}

            return;
        }

        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        public static string GetContactPhone(string mobile_phone,
            string office_phone_region,
            string office_phone)
        {
            string ret = "";
            if (mobile_phone != null && mobile_phone.Trim().Length != 0)
                ret += mobile_phone.Trim();
            if (office_phone_region != null && office_phone_region.Trim().Length != 0)
            {
                if (office_phone != null && office_phone.Trim().Length != 0)
                {
                    if (ret != "")
                        ret = ret + ", ";
                    ret += office_phone_region.Trim() + "-";
                }
            }

            if (office_phone != null && office_phone.Trim().Length != 0)
            {
                if (ret != "")
                    if (office_phone_region == null || office_phone_region.Trim().Length == 0)
                        ret += ", ";
                ret += office_phone.Trim();
            }

            return ret;
        }

        public static bool CleanHistory()
        {
            string fileTempDir = HostingEnvironment.MapPath("~/FileTmp");
            if (Directory.Exists(fileTempDir))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fileTempDir);

                DeleteDirectory(dirInfo);
                return true;
            }

            return false;
        }

        public static bool DeleteDirectory(DirectoryInfo dirInfo)
        {
            foreach (System.IO.FileInfo file in dirInfo.GetFiles())
            {
                try
                {
                    if (file.IsReadOnly)
                        file.Attributes = FileAttributes.Normal;
                    file.Delete();
                }
                catch (System.Exception ex)
                {
                    CommonModel.WriteLogFile("CommonModel", "DeleteDirectory() -> Delete file", ex.ToString());
                }
            }

            foreach (System.IO.DirectoryInfo subDirectory in dirInfo.GetDirectories())
            {
                DeleteDirectory(subDirectory);

                try
                {
                    if (dirInfo.Attributes == FileAttributes.ReadOnly)
                        dirInfo.Attributes = FileAttributes.Normal;
                    dirInfo.Delete(true);
                }
                catch (System.Exception ex)
                {
                    CommonModel.WriteLogFile("CommonModel", "DeleteDirectory() -> Delete directory", ex.ToString());
                }
            }

            return true;
        }

        public static string GetSHA1Hash(string value)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] data = sha1Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static YiTongDBDataContext GetDBContext()
        {
            _db = new YiTongDBDataContext(ConfigurationManager.ConnectionStrings["YiTongConnectionString1"].ConnectionString);
            return _db;
        }
    }
}
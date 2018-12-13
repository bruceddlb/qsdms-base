using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QSDMS.Application.Web.HashSite
{
    /// <summary>
    /// GetFilesHash256 的摘要说明
    /// </summary>
    public class GetFilesHash256 : IHttpHandler
    {

        static string filePath = "HashSite" + System.IO.Path.DirectorySeparatorChar + "FileHash" + System.IO.Path.DirectorySeparatorChar;
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.AppendHeader("Content-Disposition", "attachment; filename=json.txt");
            //string user = this.User.Identity.UserName;
            string downpath = filePath.Replace(@"\", @"/");
            downpath = @"/" + downpath;
            downpath = context.Request.Url.Scheme + "://" + context.Request.Url.Host + ":" + context.Request.Url.Port + downpath + "/json.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory;

                if (Directory.Exists(directory))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(directory);
                    FileHash.Class.FileHash root = new FileHash.Class.FileHash() { Name = dirinfo.Name, Type = 2 };

                    LoadDirectory(directory, root);
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, Newtonsoft.Json.Formatting.Indented);
                    SaveFile(json);
                    //context.Response.Write(json);

                }
            }).ContinueWith(refTak =>
            {

            });

            context.Response.Write("正在生成hash JSON文件，生成完成后,请收到站内查看，链接地址：" + downpath);
        }

        /// <summary>
        /// 加载目录
        /// </summary>
        private void LoadDirectory(string dir, FileHash.Class.FileHash root)
        {
            if (Directory.Exists(dir))
            {
                LoadFiles(dir, root);

                string[] subDirs = Directory.GetDirectories(dir);
                foreach (var d in subDirs)
                {
                    DirectoryInfo sdinfo = new DirectoryInfo(d);
                    FileHash.Class.FileHash sbhash = new FileHash.Class.FileHash() { Name = sdinfo.Name, Type = 2, ParentDir = root.Name };
                    LoadDirectory(d, sbhash);
                    root.ChildList.Add(sbhash);
                }

            }
        }

        private void LoadFiles(string dir, FileHash.Class.FileHash root)
        {
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (var f in files)
                {
                    FileInfo finfo = new FileInfo(f);
                    FileHash.Class.FileHash sub = new FileHash.Class.FileHash() { Name = finfo.Name, Type = 1, ParentDir = root.Name };
                    sub.Hash256V = ReadFileHash256(f);
                    root.ChildList.Add(sub);
                }

            }
        }

        private string ReadFileHash256(string filepath)
        {
            string h256s = "";
            long hadRead = 0;
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                const int bufferlength = 1024 * 1024;
                byte[] bytetoread = new byte[bufferlength];
                SHA256Managed sha256 = new SHA256Managed();
                while (true)
                {
                    int rint = fileStream.Read(bytetoread, 0, bufferlength);
                    sha256.TransformBlock(bytetoread, 0, rint, bytetoread, 0);
                    hadRead += rint;

                    if (hadRead >= fileStream.Length)
                        break;
                }

                sha256.TransformFinalBlock(bytetoread, 0, 0);
                h256s = ToBitString(sha256.Hash);
            }

            return h256s;

        }

        private string ToBitString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.AppendFormat("{0:X2}", b);
            return sb.ToString();
        }

        private void SaveFile(string jsonstr)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = path + "/json.txt";
            using (FileStream fs = System.IO.File.Open(filepath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] datas = System.Text.Encoding.UTF8.GetBytes(jsonstr);
                fs.Write(datas, 0, datas.Length);
                fs.Flush();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
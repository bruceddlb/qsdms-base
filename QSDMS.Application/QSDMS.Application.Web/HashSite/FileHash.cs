using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QSDMS.Application.Web.FileHash.Class
{
    [Serializable]
    public class FileHash
    {
        private string _name;
        /// <summary>
        /// 文件名或者目录名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _parentDir;
        /// <summary>
        /// 父级
        /// </summary>
        public string ParentDir
        {
            get { return _parentDir; }
            set { _parentDir = value; }
        }

        private int _type;
        /// <summary>
        /// 类型1：文件； 2：目录
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private IList<FileHash> _childList;
        /// <summary>
        /// 目录下的文件
        /// </summary>
        public IList<FileHash> ChildList
        {
            get
            {
                if (_childList == null)
                {
                    _childList = new List<FileHash>();
                }
                return _childList;
            }
            set { _childList = value; }
        }

        private string _hash256V;
        /// <summary>
        /// 文件的hash256值
        /// </summary>
        public string Hash256V
        {
            get { return _hash256V; }
            set { _hash256V = value; }
        }

        private string _path;
        /// <summary>
        /// 文件的完整路径
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VTemplate.Engine;

namespace QSDMS.Application.Web
{
    public class VTemplateHelper
    {


        public VTemplateHelper(string ConfigFile)
        {
            this.ConfigFile = ConfigFile;
            LoadTemplateFile();
        }

        public VTemplateHelper(string ConfigFile, Encoding Encoding)
        {
            this.ConfigFile = ConfigFile;
            this.Encoding = Encoding;
            LoadTemplateFile();
        }

        /// <summary>
        /// 当前页面的模板文档的配置参数
        /// </summary>
        protected virtual TemplateDocumentConfig DocumentConfig
        {
            get
            {
                return TemplateDocumentConfig.Default;
            }
        }

        public TemplateDocument Document
        {
            get;
            private set;
        }

        public string ConfigFile { get; set; }

        public Encoding Encoding { get; set; }

        /// <summary>
        /// 是否读取缓存模板
        /// </summary>
        protected virtual bool IsLoadCacheTemplate
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 装载模板文件
        /// </summary>
        /// <param name="fileName"></param>
        protected virtual void LoadTemplateFile()
        {
            if (Encoding == null)
            {
                Encoding = System.Text.Encoding.UTF8;
            }
            this.Document = null;
            if (this.IsLoadCacheTemplate)
            {
                //测试缓存模板文档
                this.Document = TemplateDocument.FromFileCache(ConfigFile, Encoding, this.DocumentConfig);
            }
            else
            {
                //测试实例模板文档
                this.Document = new TemplateDocument(ConfigFile, Encoding, this.DocumentConfig);
            }
        }

        /// <summary>
        /// 请先调用LoadTemplateFile方法
        /// </summary>
        /// <returns></returns>
        public string RenderText()
        {
            return this.Document.GetRenderText();
        }

        public string RenderSimpleText(object obj, string Name)
        {
            this.LoadTemplateFile();
            this.Document.Variables.SetValue(Name, obj);
            return RenderText();
        }
    }
}
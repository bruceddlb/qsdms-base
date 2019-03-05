using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSDMS.Data.Base
{
    public class BaseProvider
    {
        //数据库连接串
        public static string ConnectionStr = "";

        public static string m_ConnectionString = "DMSConnectionString_SqlServer";

        /// <summary>
        /// Oracle连接字符串
        /// </summary>
        public static string ConnectionString
        {

            get
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings[m_ConnectionString] == null)
                {
                    throw new ArgumentNullException(string.Format("配置文件中不包含 {0} 字符串。", m_ConnectionString));
                }
                if (ConnectionStr == "")
                {
                    ConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings[m_ConnectionString].ConnectionString;
                }
                return ConnectionStr;
            }
            set { value = ConnectionStr; }
        }
    }
}

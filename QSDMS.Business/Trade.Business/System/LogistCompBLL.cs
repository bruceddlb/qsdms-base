using QSDMS.Util.WebControl;
using Trade.Data.IServices;
using Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Business
{

    public class LogistCompBLL : BaseBLL<ILogistCompService<LogistCompEntity, LogistCompEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static LogistCompBLL m_Instance = new LogistCompBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static LogistCompBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "LogistCompCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(LogistCompEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<LogistCompEntity> GetPageList(LogistCompEntity para, ref Pagination pagination)
        {
            List<LogistCompEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<LogistCompEntity> GetList(LogistCompEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(LogistCompEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(LogistCompEntity entity)
        {
            return InstanceDAL.Update(entity);
        }
       
        public bool Delete(string keyValue)
        {
            return InstanceDAL.Delete(keyValue);
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LogistCompEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

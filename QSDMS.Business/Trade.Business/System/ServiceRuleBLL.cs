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

    public class ServiceRuleBLL : BaseBLL<IServiceRuleService<ServiceRuleEntity, ServiceRuleEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ServiceRuleBLL m_Instance = new ServiceRuleBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ServiceRuleBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ServiceRuleCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(ServiceRuleEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ServiceRuleEntity> GetPageList(ServiceRuleEntity para, ref Pagination pagination)
        {
            List<ServiceRuleEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ServiceRuleEntity> GetList(ServiceRuleEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ServiceRuleEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ServiceRuleEntity entity)
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
        public ServiceRuleEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

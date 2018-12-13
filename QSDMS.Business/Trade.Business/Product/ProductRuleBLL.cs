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

    public class ProductRuleBLL : BaseBLL<IProductRuleService<ProductRuleEntity, ProductRuleEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ProductRuleBLL m_Instance = new ProductRuleBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ProductRuleBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ProductRuleCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(ProductRuleEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ProductRuleEntity> GetPageList(ProductRuleEntity para, ref Pagination pagination)
        {
            List<ProductRuleEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ProductRuleEntity> GetList(ProductRuleEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ProductRuleEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ProductRuleEntity entity)
        {
            return InstanceDAL.Update(entity);
        }

        public bool Delete(string keyValue)
        {
            return InstanceDAL.Delete(keyValue);
        }

        public void DeleteByObjectId(string objectid)
        {
            var list = this.GetList(new ProductRuleEntity() { ProductId = objectid });
            if (list != null)
            {
                foreach (var item in list)
                {
                    this.Delete(item.RuleId);
                }
            }
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ProductRuleEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

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

    public class SalePlanDetaiBLL : BaseBLL<ISalePlanDetaiService<SalePlanDetaiEntity, SalePlanDetaiEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static SalePlanDetaiBLL m_Instance = new SalePlanDetaiBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static SalePlanDetaiBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "SalePlanDetaiCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(SalePlanDetaiEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<SalePlanDetaiEntity> GetPageList(SalePlanDetaiEntity para, ref Pagination pagination)
        {
            List<SalePlanDetaiEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<SalePlanDetaiEntity> GetList(SalePlanDetaiEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(SalePlanDetaiEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(SalePlanDetaiEntity entity)
        {
            return InstanceDAL.Update(entity);
        }

        public bool Delete(string keyValue)
        {
            return InstanceDAL.Delete(keyValue);
        }
        public void DeleteByObjectId(string objectid)
        {
            var list = this.GetList(new SalePlanDetaiEntity() { SalePlanId = objectid });
            if (list != null)
            {
                foreach (var item in list)
                {
                    this.Delete(item.SalePlanDetaiId);
                }
            }
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SalePlanDetaiEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }

        /// <summary>
        /// 处理到货
        /// </summary>
        /// <param name="keys"></param>
        public bool DoDispose(string[] keys)
        {
            return InstanceDAL.DoDispose(keys);
        }
    }
}

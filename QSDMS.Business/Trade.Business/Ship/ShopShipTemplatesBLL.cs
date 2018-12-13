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

    public class ShopShipTemplatesBLL : BaseBLL<IShopShipTemplatesService<ShopShipTemplatesEntity, ShopShipTemplatesEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ShopShipTemplatesBLL m_Instance = new ShopShipTemplatesBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ShopShipTemplatesBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ShopShipTemplatesCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(ShopShipTemplatesEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ShopShipTemplatesEntity> GetPageList(ShopShipTemplatesEntity para, ref Pagination pagination)
        {
            List<ShopShipTemplatesEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ShopShipTemplatesEntity> GetList(ShopShipTemplatesEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ShopShipTemplatesEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ShopShipTemplatesEntity entity)
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
        public ShopShipTemplatesEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

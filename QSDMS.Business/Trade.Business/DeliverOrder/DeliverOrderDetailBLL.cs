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

    public class DeliverOrderDetailBLL : BaseBLL<IDeliverOrderDetailService<DeliverOrderDetailEntity, DeliverOrderDetailEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static DeliverOrderDetailBLL m_Instance = new DeliverOrderDetailBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static DeliverOrderDetailBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "DeliverOrderDetailCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(DeliverOrderDetailEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<DeliverOrderDetailEntity> GetPageList(DeliverOrderDetailEntity para, ref Pagination pagination)
        {
            List<DeliverOrderDetailEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<DeliverOrderDetailEntity> GetList(DeliverOrderDetailEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(DeliverOrderDetailEntity entity)
        {
            if (entity.ProductId != null)
            {
                var product = ProductBLL.Instance.GetEntity(entity.ProductId);
                if (product != null)
                {
                    entity.ProductNo = product.ProductNO;
                    entity.ProductName = product.ProductName;
                }
            }
            return InstanceDAL.Add(entity);
        }

        public bool Update(DeliverOrderDetailEntity entity)
        {
            return InstanceDAL.Update(entity);
        }
        public void DeleteByObjectId(string objectid)
        {
            var list = this.GetList(new DeliverOrderDetailEntity() { DeliverOrderId = objectid });
            if (list != null)
            {
                foreach (var item in list)
                {
                    this.Delete(item.DeliverOrderDetailId);
                }
            }
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
        public DeliverOrderDetailEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

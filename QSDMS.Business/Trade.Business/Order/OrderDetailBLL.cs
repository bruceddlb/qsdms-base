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

    public class OrderDetailBLL : BaseBLL<IOrderDetailService<OrderDetailEntity, OrderDetailEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static OrderDetailBLL m_Instance = new OrderDetailBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static OrderDetailBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "OrderDetailCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(OrderDetailEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<OrderDetailEntity> GetPageList(OrderDetailEntity para, ref Pagination pagination)
        {
            List<OrderDetailEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<OrderDetailEntity> GetList(OrderDetailEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(OrderDetailEntity entity)
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

        public bool Update(OrderDetailEntity entity)
        {
            return InstanceDAL.Update(entity);
        }
        public void DeleteByObjectId(string objectid)
        {
            var list = this.GetList(new OrderDetailEntity() { OrderId = objectid });
            if (list != null)
            {
                foreach (var item in list)
                {
                    this.Delete(item.OrderdetailId);
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
        public OrderDetailEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

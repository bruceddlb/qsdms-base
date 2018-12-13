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

    public class OrderBLL : BaseBLL<IOrderService<OrderEntity, OrderEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static OrderBLL m_Instance = new OrderBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static OrderBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "OrderCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(OrderEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<OrderEntity> GetPageList(OrderEntity para, ref Pagination pagination)
        {
            List<OrderEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<OrderEntity> GetList(OrderEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(OrderEntity entity)
        {
           
            if (entity.MemberId != null)
            {
                var member = MemberBLL.Instance.GetEntity(entity.MemberId);
                if (member != null)
                {
                    entity.MemberName = member.MemberName;
                    entity.MemberMobile = member.Mobile;
                }
            }
            return InstanceDAL.Add(entity);
        }

        public bool Update(OrderEntity entity)
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
        public OrderEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

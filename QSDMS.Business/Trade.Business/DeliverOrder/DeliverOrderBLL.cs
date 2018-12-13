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

    public class DeliverOrderBLL : BaseBLL<IDeliverOrderService<DeliverOrderEntity, DeliverOrderEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static DeliverOrderBLL m_Instance = new DeliverOrderBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static DeliverOrderBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "DeliverOrderCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(DeliverOrderEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<DeliverOrderEntity> GetPageList(DeliverOrderEntity para, ref Pagination pagination)
        {
            List<DeliverOrderEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<DeliverOrderEntity> GetList(DeliverOrderEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(DeliverOrderEntity entity)
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
            if (entity.OrderId != null)
            {
                var order = OrderBLL.Instance.GetEntity(entity.OrderId);
                if (order != null)
                {
                    entity.SaleOrderNo = order.BillCode;
                    entity.AddressId = order.AddressId;
                }
            }
            return InstanceDAL.Add(entity);
        }

        public bool Update(DeliverOrderEntity entity)
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
        public DeliverOrderEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

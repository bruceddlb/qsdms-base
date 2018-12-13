using QSDMS.Util;
using QSDMS.Util.WebControl;
using Trade.Data.IServices;
using Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.SqlServer
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderService : BaseSqlDataService, IOrderService<OrderEntity, OrderEntity, Pagination>
    {
        public int QueryCount(OrderEntity para)
        {
            throw new NotImplementedException();
        }

        public List<OrderEntity> GetPageList(OrderEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Order");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Order.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Order, OrderEntity>(pageList.ToList());
        }

        public List<OrderEntity> GetList(OrderEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Order");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Order.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Order, OrderEntity>(list.ToList());
        }

        public OrderEntity GetEntity(string keyValue)
        {
            var model = tbl_Order.SingleOrDefault("where OrderId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Order, OrderEntity>(model, null);
        }

        public bool Add(OrderEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<OrderEntity, tbl_Order>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(OrderEntity entity)
        {

            var model = tbl_Order.SingleOrDefault("where OrderId=@0", entity.OrderId);
            model = EntityConvertTools.CopyToModel<OrderEntity, tbl_Order>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Order.Delete("where OrderId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(OrderEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.BillCode != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',BillCode)>0)", para.BillCode);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',MemberName)>0 or charindex('{0}',MemberNo)>0)", para.KeyWord);
            }
            if (para.OrderStatus != null)
            {
                sbWhere.AppendFormat(" and OrderStatus='{0}'", para.OrderStatus);
            }
            if (para.OrderType != null)
            {
                sbWhere.AppendFormat(" and OrderType='{0}'", para.OrderType);
            }
            if (para.StartTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND OrderDate>='{0} 00:00:00'", Converter.ParseDateTime(para.StartTime).ToString("yyyy-MM-dd")));
            }
            if (para.EndTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND OrderDate<='{0} 23:59:59'", Converter.ParseDateTime(para.EndTime).ToString("yyyy-MM-dd")));
            }
           
            return sbWhere.ToString();
        }
    }
}

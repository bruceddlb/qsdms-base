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
    /// 订单详情
    /// </summary>
    public class OrderDetailService : BaseSqlDataService, IOrderDetailService<OrderDetailEntity, OrderDetailEntity, Pagination>
    {
        public int QueryCount(OrderDetailEntity para)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetailEntity> GetPageList(OrderDetailEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_OrderDetail");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_OrderDetail.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_OrderDetail, OrderDetailEntity>(pageList.ToList());
        }

        public List<OrderDetailEntity> GetList(OrderDetailEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_OrderDetail");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_OrderDetail.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_OrderDetail, OrderDetailEntity>(list.ToList());
        }

        public OrderDetailEntity GetEntity(string keyValue)
        {
            var model = tbl_OrderDetail.SingleOrDefault("where OrderdetailId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_OrderDetail, OrderDetailEntity>(model, null);
        }

        public bool Add(OrderDetailEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<OrderDetailEntity, tbl_OrderDetail>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(OrderDetailEntity entity)
        {

            var model = tbl_OrderDetail.SingleOrDefault("where OrderdetailId=@0", entity.OrderdetailId);
            model = EntityConvertTools.CopyToModel<OrderDetailEntity, tbl_OrderDetail>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_OrderDetail.Delete("where OrderdetailId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(OrderDetailEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.OrderId != null)
            {
                sbWhere.AppendFormat(" and OrderId='{0}'", para.OrderId);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductNo)>0 or charindex('{0}',ProductName)>0)", para.KeyWord);
            }

            return sbWhere.ToString();
        }
    }
}

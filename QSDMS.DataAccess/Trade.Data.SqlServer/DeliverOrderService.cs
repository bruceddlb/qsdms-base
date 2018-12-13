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
    /// 发货单
    /// </summary>
    public class DeliverOrderService : BaseSqlDataService, IDeliverOrderService<DeliverOrderEntity, DeliverOrderEntity, Pagination>
    {
        public int QueryCount(DeliverOrderEntity para)
        {
            throw new NotImplementedException();
        }

        public List<DeliverOrderEntity> GetPageList(DeliverOrderEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_DeliverOrder");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_DeliverOrder.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_DeliverOrder, DeliverOrderEntity>(pageList.ToList());
        }

        public List<DeliverOrderEntity> GetList(DeliverOrderEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_DeliverOrder");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_DeliverOrder.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_DeliverOrder, DeliverOrderEntity>(list.ToList());
        }

        public DeliverOrderEntity GetEntity(string keyValue)
        {
            var model = tbl_DeliverOrder.SingleOrDefault("where DeliverOrderId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_DeliverOrder, DeliverOrderEntity>(model, null);
        }

        public bool Add(DeliverOrderEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<DeliverOrderEntity, tbl_DeliverOrder>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(DeliverOrderEntity entity)
        {

            var model = tbl_DeliverOrder.SingleOrDefault("where DeliverOrderId=@0", entity.DeliverOrderId);
            model = EntityConvertTools.CopyToModel<DeliverOrderEntity, tbl_DeliverOrder>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_DeliverOrder.Delete("where DeliverOrderId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(DeliverOrderEntity para)
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
                sbWhere.AppendFormat(" and (charindex('{0}',MemberName)>0 or charindex('{0}',MemberMobile)>0)", para.KeyWord);
            }
            if (para.OrderStatus != null)
            {
                sbWhere.AppendFormat(" and OrderStatus='{0}'", para.OrderStatus);
            }

            if (para.StartTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND CreateDate>='{0} 00:00:00'", Converter.ParseDateTime(para.StartTime).ToString("yyyy-MM-dd")));
            }
            if (para.EndTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND CreateDate<='{0} 23:59:59'", Converter.ParseDateTime(para.EndTime).ToString("yyyy-MM-dd")));
            }

            if (para.LogisticsName != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',LogisticsName)>0 or charindex('{0}',LogisticsNo)>0)", para.LogisticsName);
            }
            if (para.SaleOrderNo != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',SaleOrderNo)>0)", para.SaleOrderNo);
            }

            return sbWhere.ToString();
        }
    }
}

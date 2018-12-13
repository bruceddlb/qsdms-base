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
    /// 发货订单明细
    /// </summary>
    public class DeliverOrderDetailService : BaseSqlDataService, IDeliverOrderDetailService<DeliverOrderDetailEntity, DeliverOrderDetailEntity, Pagination>
    {
        public int QueryCount(DeliverOrderDetailEntity para)
        {
            throw new NotImplementedException();
        }

        public List<DeliverOrderDetailEntity> GetPageList(DeliverOrderDetailEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_DeliverOrderDetail");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_DeliverOrderDetail.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_DeliverOrderDetail, DeliverOrderDetailEntity>(pageList.ToList());
        }

        public List<DeliverOrderDetailEntity> GetList(DeliverOrderDetailEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_DeliverOrderDetail");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_DeliverOrderDetail.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_DeliverOrderDetail, DeliverOrderDetailEntity>(list.ToList());
        }

        public DeliverOrderDetailEntity GetEntity(string keyValue)
        {
            var model = tbl_DeliverOrderDetail.SingleOrDefault("where DeliverOrderDetailId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_DeliverOrderDetail, DeliverOrderDetailEntity>(model, null);
        }

        public bool Add(DeliverOrderDetailEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<DeliverOrderDetailEntity, tbl_DeliverOrderDetail>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(DeliverOrderDetailEntity entity)
        {

            var model = tbl_DeliverOrderDetail.SingleOrDefault("where DeliverOrderDetailId=@0", entity.DeliverOrderDetailId);
            model = EntityConvertTools.CopyToModel<DeliverOrderDetailEntity, tbl_DeliverOrderDetail>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_DeliverOrderDetail.Delete("where DeliverOrderDetailId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(DeliverOrderDetailEntity para)
        {

            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.DeliverOrderId != null)
            {
                sbWhere.AppendFormat(" and DeliverOrderId='{0}'", para.DeliverOrderId);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductNo)>0 or charindex('{0}',ProductName)>0)", para.KeyWord);
            }
            return sbWhere.ToString();
        }
    }
}

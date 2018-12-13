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
    /// 销售计划
    /// </summary>
    public class SalePlanService : BaseSqlDataService, ISalePlanService<SalePlanEntity, SalePlanEntity, Pagination>
    {
        public int QueryCount(SalePlanEntity para)
        {
            throw new NotImplementedException();
        }

        public List<SalePlanEntity> GetPageList(SalePlanEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_SalePlan");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_SalePlan.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_SalePlan, SalePlanEntity>(pageList.ToList());
        }

        public List<SalePlanEntity> GetList(SalePlanEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_SalePlan");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_SalePlan.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_SalePlan, SalePlanEntity>(list.ToList());
        }

        public SalePlanEntity GetEntity(string keyValue)
        {
            var model = tbl_SalePlan.SingleOrDefault("where SalePlanId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_SalePlan, SalePlanEntity>(model, null);
        }

        public bool Add(SalePlanEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<SalePlanEntity, tbl_SalePlan>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(SalePlanEntity entity)
        {

            var model = tbl_SalePlan.SingleOrDefault("where SalePlanId=@0", entity.SalePlanId);
            model = EntityConvertTools.CopyToModel<SalePlanEntity, tbl_SalePlan>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_SalePlan.Delete("where SalePlanId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 重启计划
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool DoAgin(string keyValue)
        {
            try
            {

                using (var tran = Trade_SQLDB.GetInstance().GetTransaction())
                {
                    //查询产品对应的订单明细
                    var orderdetailList = tbl_SalePlanDetai.Fetch(string.Format("select * from tbl_SalePlanDetai where SalePlanId='{0}'", keyValue));
                    foreach (var orderdetail in orderdetailList)
                    {
                        orderdetail.Status = (int)Trade.Model.Enums.ArrivalStatus.未到货;
                        orderdetail.Update();
                    }
                    //更改对应计划明细状态
                    Trade_SQLDB.GetInstance().Execute(string.Format("update tbl_SalePlan set PlanStatus='{0}' where SalePlanId='{1}'", (int)Trade.Model.Enums.PlanStatus.已生效, keyValue));

                    // Commit
                    tran.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string ConverPara(SalePlanEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.PlanTitle != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',PlanTitle)>0)", para.PlanTitle);
            }
            if (para.StartTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND SaleStartTime>='{0} 00:00:00'", Converter.ParseDateTime(para.StartTime).ToString("yyyy-MM-dd")));
            }
            if (para.EndTime != null)
            {
                sbWhere.Append(base.FormatParameter(" AND SaleEndTime<='{0} 23:59:59'", Converter.ParseDateTime(para.EndTime).ToString("yyyy-MM-dd")));
            }
            return sbWhere.ToString();
        }
    }
}

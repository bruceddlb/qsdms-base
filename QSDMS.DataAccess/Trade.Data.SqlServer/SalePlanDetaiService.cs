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
    /// 销售计划明细
    /// </summary>
    public class SalePlanDetaiService : BaseSqlDataService, ISalePlanDetaiService<SalePlanDetaiEntity, SalePlanDetaiEntity, Pagination>
    {
        public int QueryCount(SalePlanDetaiEntity para)
        {
            throw new NotImplementedException();
        }

        public List<SalePlanDetaiEntity> GetPageList(SalePlanDetaiEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_SalePlanDetai");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_SalePlanDetai.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_SalePlanDetai, SalePlanDetaiEntity>(pageList.ToList());
        }

        public List<SalePlanDetaiEntity> GetList(SalePlanDetaiEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_SalePlanDetai");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_SalePlanDetai.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_SalePlanDetai, SalePlanDetaiEntity>(list.ToList());
        }

        public SalePlanDetaiEntity GetEntity(string keyValue)
        {
            var model = tbl_SalePlanDetai.SingleOrDefault("where SalePlanDetaiId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_SalePlanDetai, SalePlanDetaiEntity>(model, null);
        }

        public bool Add(SalePlanDetaiEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<SalePlanDetaiEntity, tbl_SalePlanDetai>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(SalePlanDetaiEntity entity)
        {

            var model = tbl_SalePlanDetai.SingleOrDefault("where SalePlanDetaiId=@0", entity.SalePlanDetaiId);
            model = EntityConvertTools.CopyToModel<SalePlanDetaiEntity, tbl_SalePlanDetai>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_SalePlanDetai.Delete("where SalePlanDetaiId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 处理到货业务
        /// 更改对应相关产品的订单状态为到货
        /// </summary>
        /// <param name="keys"></param>
        public bool DoDispose(string[] keys)
        {
            try
            {

                using (var tran = Trade_SQLDB.GetInstance().GetTransaction())
                {
                    foreach (var key in keys)
                    {
                        var plandetail = GetEntity(key);
                        if (plandetail != null)
                        {
                            //查询产品对应的订单明细
                            var orderdetailList = tbl_OrderDetail.Fetch(string.Format("select * from tbl_OrderDetail where ProductId='{0}' and Status='{1}'", plandetail.ProductId, (int)Trade.Model.Enums.OrderDetailStatus.未到货));
                            foreach (var orderdetail in orderdetailList)
                            {
                                // Trade_SQLDB.GetInstance().Execute(string.Format("update tbl_OrderDetail set Status='{0}' where OrderdetailId='{1}'", (int)Trade.Model.Enums.OrderDetailStatus.已到货, orderdetail.OrderdetailId));
                                tbl_OrderDetail.Update("set Status=@0 where OrderdetailId=@1", (int)Trade.Model.Enums.OrderDetailStatus.已到货, orderdetail.OrderdetailId);
                            }
                            
                            //更改对应计划明细状态
                            //Trade_SQLDB.GetInstance().Execute(string.Format("update tbl_SalePlanDetai set Status='{0}' where SalePlanDetaiId='{1}'", (int)Trade.Model.Enums.ArrivalStatus.已到货, plandetail.SalePlanDetaiId));
                            tbl_SalePlanDetai.Update("set Status=@0 where SalePlanDetaiId=@1", (int)Trade.Model.Enums.ArrivalStatus.已到货, plandetail.SalePlanDetaiId);

                        }
                    }

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
        public string ConverPara(SalePlanDetaiEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.SalePlanId != null)
            {
                sbWhere.AppendFormat(" and SalePlanId ='{0}'", para.SalePlanId);
            }
            if (para.ProductId != null)
            {
                sbWhere.AppendFormat(" and ProductId ='{0}'", para.ProductId);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductNo)>0 or charindex('{0}',ProductName)>0)", para.KeyWord);
            }

            return sbWhere.ToString();
        }
    }
}

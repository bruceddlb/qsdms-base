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
    /// 产品规格
    /// </summary>
    public class ProductRuleService : BaseSqlDataService, IProductRuleService<ProductRuleEntity, ProductRuleEntity, Pagination>
    {
        public int QueryCount(ProductRuleEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ProductRuleEntity> GetPageList(ProductRuleEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ProductRule");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_ProductRule.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_ProductRule, ProductRuleEntity>(pageList.ToList());
        }

        public List<ProductRuleEntity> GetList(ProductRuleEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ProductRule");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_ProductRule.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_ProductRule, ProductRuleEntity>(list.ToList());
        }

        public ProductRuleEntity GetEntity(string keyValue)
        {
            var model = tbl_ProductRule.SingleOrDefault("where RuleId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_ProductRule, ProductRuleEntity>(model, null);
        }

        public bool Add(ProductRuleEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ProductRuleEntity, tbl_ProductRule>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ProductRuleEntity entity)
        {

            var model = tbl_ProductRule.SingleOrDefault("where RuleId=@0", entity.RuleId);
            model = EntityConvertTools.CopyToModel<ProductRuleEntity, tbl_ProductRule>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_ProductRule.Delete("where RuleId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ProductRuleEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }

            if (para.ProductId != null)
            {
                sbWhere.AppendFormat(" and ProductId='{0}'", para.ProductId);
            }
            return sbWhere.ToString();
        }
    }
}

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
    /// 服务费规则设置
    /// </summary>
    public class ServiceRuleService : BaseSqlDataService, IServiceRuleService<ServiceRuleEntity, ServiceRuleEntity, Pagination>
    {
        public int QueryCount(ServiceRuleEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ServiceRuleEntity> GetPageList(ServiceRuleEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ServiceRule");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_ServiceRule.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_ServiceRule, ServiceRuleEntity>(pageList.ToList());
        }

        public List<ServiceRuleEntity> GetList(ServiceRuleEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ServiceRule");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_ServiceRule.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_ServiceRule, ServiceRuleEntity>(list.ToList());
        }

        public ServiceRuleEntity GetEntity(string keyValue)
        {
            var model = tbl_ServiceRule.SingleOrDefault("where ServiceRuleId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_ServiceRule, ServiceRuleEntity>(model, null);
        }

        public bool Add(ServiceRuleEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ServiceRuleEntity, tbl_ServiceRule>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ServiceRuleEntity entity)
        {

            var model = tbl_ServiceRule.SingleOrDefault("where ServiceRuleId=@0", entity.ServiceRuleId);
            model = EntityConvertTools.CopyToModel<ServiceRuleEntity, tbl_ServiceRule>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_ServiceRule.Delete("where ServiceRuleId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ServiceRuleEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.Title != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Title)>0 )", para.Title);
            }

            return sbWhere.ToString();
        }
    }
}

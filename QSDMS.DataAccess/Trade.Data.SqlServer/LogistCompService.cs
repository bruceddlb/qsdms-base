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
    /// 到货管理
    /// </summary>
    public class LogistCompService : BaseSqlDataService, ILogistCompService<LogistCompEntity, LogistCompEntity, Pagination>
    {
        public int QueryCount(LogistCompEntity para)
        {
            throw new NotImplementedException();
        }

        public List<LogistCompEntity> GetPageList(LogistCompEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_LogistComp");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_LogistComp.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_LogistComp, LogistCompEntity>(pageList.ToList());
        }

        public List<LogistCompEntity> GetList(LogistCompEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_LogistComp");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_LogistComp.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_LogistComp, LogistCompEntity>(list.ToList());
        }

        public LogistCompEntity GetEntity(string keyValue)
        {
            var model = tbl_LogistComp.SingleOrDefault("where LogistCompId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_LogistComp, LogistCompEntity>(model, null);
        }

        public bool Add(LogistCompEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<LogistCompEntity, tbl_LogistComp>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(LogistCompEntity entity)
        {

            var model = tbl_LogistComp.SingleOrDefault("where LogistCompId=@0", entity.LogistCompId);
            model = EntityConvertTools.CopyToModel<LogistCompEntity, tbl_LogistComp>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_LogistComp.Delete("where LogistCompId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(LogistCompEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',LogistCompName)>0 or charindex('{0}',LogistCompCode)>0)", para.KeyWord);
            }

            return sbWhere.ToString();
        }
    }
}

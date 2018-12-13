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
    public class ArrivalService : BaseSqlDataService, IArrivalService<ArrivalEntity, ArrivalEntity, Pagination>
    {
        public int QueryCount(ArrivalEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ArrivalEntity> GetPageList(ArrivalEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Arrival");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Arrival.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Arrival, ArrivalEntity>(pageList.ToList());
        }

        public List<ArrivalEntity> GetList(ArrivalEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Arrival");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Arrival.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Arrival, ArrivalEntity>(list.ToList());
        }

        public ArrivalEntity GetEntity(string keyValue)
        {
            var model = tbl_Arrival.SingleOrDefault("where ArrivalId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Arrival, ArrivalEntity>(model, null);
        }

        public bool Add(ArrivalEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ArrivalEntity, tbl_Arrival>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ArrivalEntity entity)
        {

            var model = tbl_Arrival.SingleOrDefault("where ArrivalId=@0", entity.ArrivalId);
            model = EntityConvertTools.CopyToModel<ArrivalEntity, tbl_Arrival>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Arrival.Delete("where ArrivalId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ArrivalEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',BillCode)>0 or charindex('{0}',ConectName)>0 or charindex('{0}',ConectTel)>0 or charindex('{0}',Address)>0)", para.KeyWord);
            }

            return sbWhere.ToString();
        }
    }
}

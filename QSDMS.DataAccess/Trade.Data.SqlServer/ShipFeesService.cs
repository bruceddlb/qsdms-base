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
    /// 运费模板设置
    /// </summary>
    public class ShipFeesService : BaseSqlDataService, IShipFeesService<ShipFeesEntity, ShipFeesEntity, Pagination>
    {
        public int QueryCount(ShipFeesEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ShipFeesEntity> GetPageList(ShipFeesEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ShipFees");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_ShipFee.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_ShipFee, ShipFeesEntity>(pageList.ToList());
        }

        public List<ShipFeesEntity> GetList(ShipFeesEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ShipFees");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_ShipFee.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_ShipFee, ShipFeesEntity>(list.ToList());
        }

        public ShipFeesEntity GetEntity(string keyValue)
        {
            var model = tbl_ShipFee.SingleOrDefault("where ShipFeesId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_ShipFee, ShipFeesEntity>(model, null);
        }

        public bool Add(ShipFeesEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ShipFeesEntity, tbl_ShipFee>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ShipFeesEntity entity)
        {

            var model = tbl_ShipFee.SingleOrDefault("where ShipFeesId=@0", entity.ShipFeesId);
            model = EntityConvertTools.CopyToModel<ShipFeesEntity, tbl_ShipFee>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_ShipFee.Delete("where ShipFeesId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ShipFeesEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.ShipTempId != null)
            {
                sbWhere.AppendFormat(" and ShipTempId='{0}'", para.ShipTempId);
            }
            if (para.ProvinceId != null)
            {
                sbWhere.AppendFormat(" and ProvinceId='{0}'", para.ProvinceId);
            }
            return sbWhere.ToString();
        }
    }
}

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
    /// 运费模板
    /// </summary>
    public class ShopShipTemplatesService : BaseSqlDataService, IShopShipTemplatesService<ShopShipTemplatesEntity, ShopShipTemplatesEntity, Pagination>
    {
        public int QueryCount(ShopShipTemplatesEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ShopShipTemplatesEntity> GetPageList(ShopShipTemplatesEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ShopShipTemplates");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_ShopShipTemplate.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_ShopShipTemplate, ShopShipTemplatesEntity>(pageList.ToList());
        }

        public List<ShopShipTemplatesEntity> GetList(ShopShipTemplatesEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_ShopShipTemplates");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_ShopShipTemplate.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_ShopShipTemplate, ShopShipTemplatesEntity>(list.ToList());
        }

        public ShopShipTemplatesEntity GetEntity(string keyValue)
        {
            var model = tbl_ShopShipTemplate.SingleOrDefault("where ShopShipTemplatesId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_ShopShipTemplate, ShopShipTemplatesEntity>(model, null);
        }

        public bool Add(ShopShipTemplatesEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ShopShipTemplatesEntity, tbl_ShopShipTemplate>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ShopShipTemplatesEntity entity)
        {

            var model = tbl_ShopShipTemplate.SingleOrDefault("where ShopShipTemplatesId=@0", entity.ShopShipTemplatesId);
            model = EntityConvertTools.CopyToModel<ShopShipTemplatesEntity, tbl_ShopShipTemplate>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_ShopShipTemplate.Delete("where ShopShipTemplatesId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ShopShipTemplatesEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.Title != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Title)>0)", para.Title);
            }

            return sbWhere.ToString();
        }
    }
}

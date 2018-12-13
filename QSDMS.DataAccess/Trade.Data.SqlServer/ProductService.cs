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
    /// 产品管理
    /// </summary>
    public class ProductService : BaseSqlDataService, IProductService<ProductEntity, ProductEntity, Pagination>
    {
        public int QueryCount(ProductEntity para)
        {
            throw new NotImplementedException();
        }

        public List<ProductEntity> GetPageList(ProductEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Product");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Product.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Product, ProductEntity>(pageList.ToList());
        }

        public List<ProductEntity> GetList(ProductEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Product");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Product.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Product, ProductEntity>(list.ToList());
        }

        public ProductEntity GetEntity(string keyValue)
        {
            var model = tbl_Product.SingleOrDefault("where ProductId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Product, ProductEntity>(model, null);
        }

        public bool Add(ProductEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<ProductEntity, tbl_Product>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(ProductEntity entity)
        {

            var model = tbl_Product.SingleOrDefault("where ProductId=@0", entity.ProductId);
            model = EntityConvertTools.CopyToModel<ProductEntity, tbl_Product>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Product.Delete("where ProductId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(ProductEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductNO)>0 or charindex('{0}',ProductName)>0)", para.KeyWord);
            }
            if (para.ProductNO != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductNO)>0)", para.ProductNO);
            }
            if (para.ProductName != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',ProductName)>0)", para.ProductName);
            }
            if (para.ProductCategoryId != null)
            {
                sbWhere.AppendFormat(" and ProductCategoryId='{0}'", para.ProductCategoryId);
            }
            if (para.ProductType != null)
            {
                sbWhere.AppendFormat(" and ProductType='{0}'", para.ProductType);
            }

            return sbWhere.ToString();
        }
    }
}

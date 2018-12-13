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
    /// 分类
    /// </summary>
    public class CategoryService : BaseSqlDataService, ICategoryService<CategoryEntity, CategoryEntity, Pagination>
    {
        public int QueryCount(CategoryEntity para)
        {
            throw new NotImplementedException();
        }

        public List<CategoryEntity> GetPageList(CategoryEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Category");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Category.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Category, CategoryEntity>(pageList.ToList());
        }

        public List<CategoryEntity> GetList(CategoryEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Category");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Category.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Category, CategoryEntity>(list.ToList());
        }

        public CategoryEntity GetEntity(string keyValue)
        {
            var model = tbl_Category.SingleOrDefault("where CategoryId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Category, CategoryEntity>(model, null);
        }

        public bool Add(CategoryEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<CategoryEntity, tbl_Category>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(CategoryEntity entity)
        {

            var model = tbl_Category.SingleOrDefault("where CategoryId=@0", entity.CategoryID);
            model = EntityConvertTools.CopyToModel<CategoryEntity, tbl_Category>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Category.Delete("where CategoryId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(CategoryEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.Name != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Name)>0)", para.Name);
            }

            return sbWhere.ToString();
        }
    }
}

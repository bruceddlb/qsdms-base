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
    /// 购物车
    /// </summary>
    public class MemberCartService : BaseSqlDataService, IMemberCartService<MemberCartEntity, MemberCartEntity, Pagination>
    {
        public int QueryCount(MemberCartEntity para)
        {
            throw new NotImplementedException();
        }

        public List<MemberCartEntity> GetPageList(MemberCartEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_MemberCart");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_MemberCart.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_MemberCart, MemberCartEntity>(pageList.ToList());
        }

        public List<MemberCartEntity> GetList(MemberCartEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_MemberCart");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_MemberCart.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_MemberCart, MemberCartEntity>(list.ToList());
        }

        public MemberCartEntity GetEntity(string keyValue)
        {
            var model = tbl_MemberCart.SingleOrDefault("where CartId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_MemberCart, MemberCartEntity>(model, null);
        }

        public bool Add(MemberCartEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<MemberCartEntity, tbl_MemberCart>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(MemberCartEntity entity)
        {

            var model = tbl_MemberCart.SingleOrDefault("where CartId=@0", entity.CartId);
            model = EntityConvertTools.CopyToModel<MemberCartEntity, tbl_MemberCart>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_MemberCart.Delete("where CartId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(MemberCartEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.MemberId != null)
            {
                sbWhere.AppendFormat(" and MemberId='{0}'", para.MemberId);
            }

            return sbWhere.ToString();
        }
    }
}

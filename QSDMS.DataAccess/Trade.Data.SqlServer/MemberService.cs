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
    /// 会员
    /// </summary>
    public class MemberService : BaseSqlDataService, IMemberService<MemberEntity, MemberEntity, Pagination>
    {
        public int QueryCount(MemberEntity para)
        {
            throw new NotImplementedException();
        }

        public List<MemberEntity> GetPageList(MemberEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Member");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Member.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Member, MemberEntity>(pageList.ToList());
        }

        public List<MemberEntity> GetList(MemberEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Member");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Member.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Member, MemberEntity>(list.ToList());
        }

        public MemberEntity GetEntity(string keyValue)
        {
            var model = tbl_Member.SingleOrDefault("where MemberId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Member, MemberEntity>(model, null);
        }

        public bool Add(MemberEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<MemberEntity, tbl_Member>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(MemberEntity entity)
        {

            var model = tbl_Member.SingleOrDefault("where MemberId=@0", entity.MemberId);
            model = EntityConvertTools.CopyToModel<MemberEntity, tbl_Member>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Member.Delete("where MemberId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(MemberEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.Mobile != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Mobile)>0)", para.Mobile);
            }
            if (para.MemberName != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',MemberName)>0)", para.MemberName);
            }
            if (para.NikeName != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',NikeName)>0)", para.NikeName);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Mobile)>0 or charindex('{0}',MemberName)>0 or charindex('{0}',AddressInfo)>0)", para.KeyWord);
            }
            if (para.LevId != null)
            {
                sbWhere.AppendFormat(" and LevId='{0}'", para.LevId);
            }
            return sbWhere.ToString();
        }
    }
}

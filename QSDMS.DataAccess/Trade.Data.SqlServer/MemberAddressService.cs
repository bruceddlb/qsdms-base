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
    /// 会员地址
    /// </summary>
    public class MemberAddressService : BaseSqlDataService, IMemberAddressService<MemberAddressEntity, MemberAddressEntity, Pagination>
    {
        public int QueryCount(MemberAddressEntity para)
        {
            throw new NotImplementedException();
        }

        public List<MemberAddressEntity> GetPageList(MemberAddressEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_MemberAddress");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_MemberAddress.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_MemberAddress, MemberAddressEntity>(pageList.ToList());
        }

        public List<MemberAddressEntity> GetList(MemberAddressEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_MemberAddress");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_MemberAddress.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_MemberAddress, MemberAddressEntity>(list.ToList());
        }

        public MemberAddressEntity GetEntity(string keyValue)
        {
            var model = tbl_MemberAddress.SingleOrDefault("where AddressId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_MemberAddress, MemberAddressEntity>(model, null);
        }

        public bool Add(MemberAddressEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<MemberAddressEntity, tbl_MemberAddress>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(MemberAddressEntity entity)
        {

            var model = tbl_MemberAddress.SingleOrDefault("where AddressId=@0", entity.AddressId);
            model = EntityConvertTools.CopyToModel<MemberAddressEntity, tbl_MemberAddress>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_MemberAddress.Delete("where AddressId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(MemberAddressEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.Address != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Address)>0)", para.Address);
            }
            if (para.KeyWord != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',Address)>0 or charindex('{0}',Consignee)>0 or charindex('{0}',Mobile)>0)", para.KeyWord);
            }
            if (para.MemberId != null)
            {
                sbWhere.AppendFormat(" and MemberId ='{0}'", para.MemberId);
            }

            return sbWhere.ToString();
        }
    }
}

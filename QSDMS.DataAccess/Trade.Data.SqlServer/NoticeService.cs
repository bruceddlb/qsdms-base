﻿using QSDMS.Util;
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
    /// 消息管理
    /// </summary>
    public class NoticeService : BaseSqlDataService, INoticeService<NoticeEntity, NoticeEntity, Pagination>
    {
        public int QueryCount(NoticeEntity para)
        {
            throw new NotImplementedException();
        }

        public List<NoticeEntity> GetPageList(NoticeEntity para, ref Pagination pagination)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Notice");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            if (!string.IsNullOrWhiteSpace(pagination.sidx))
            {
                sql.AppendFormat(" order by {0} {1}", pagination.sidx, pagination.sord);
            }
            var currentpage = tbl_Notice.Page(pagination.page, pagination.rows, sql.ToString());
            //数据对象
            var pageList = currentpage.Items;
            //分页对象
            pagination.records = Converter.ParseInt32(currentpage.TotalItems);
            return EntityConvertTools.CopyToList<tbl_Notice, NoticeEntity>(pageList.ToList());
        }

        public List<NoticeEntity> GetList(NoticeEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from tbl_Notice");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = tbl_Notice.Query(sql.ToString());
            return EntityConvertTools.CopyToList<tbl_Notice, NoticeEntity>(list.ToList());
        }

        public NoticeEntity GetEntity(string keyValue)
        {
            var model = tbl_Notice.SingleOrDefault("where NoticeId=@0", keyValue);
            return EntityConvertTools.CopyToModel<tbl_Notice, NoticeEntity>(model, null);
        }

        public bool Add(NoticeEntity entity)
        {
            var model = EntityConvertTools.CopyToModel<NoticeEntity, tbl_Notice>(entity, null);
            model.Insert();
            return true;
        }

        public bool Update(NoticeEntity entity)
        {

            var model = tbl_Notice.SingleOrDefault("where NoticeId=@0", entity.NoticeId);
            model = EntityConvertTools.CopyToModel<NoticeEntity, tbl_Notice>(entity, model);
            int count = model.Update();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool Delete(string keyValue)
        {
            int count = tbl_Notice.Delete("where PicId=@0", keyValue);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public string ConverPara(NoticeEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            if (para.NoticeTitle != null)
            {
                sbWhere.AppendFormat(" and (charindex('{0}',NoticeTitle)>0 )", para.NoticeTitle);
            }
            return sbWhere.ToString();
        }
    }
}

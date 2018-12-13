using AA.Data.IServices;
using AA.Model;
using QSDMS.Data.Base;
using QSDMS.Util;
using QSDMS.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Data.SqlServer
{
    /// <summary>
    /// 特殊处理类
    /// </summary>
    public class SpecialTestService : BaseDAL, ITestService<TestEntity, TestEntity, Pagination>
    {
        public int QueryCount(TestEntity para)
        {
            throw new NotImplementedException();
        }

        public List<TestEntity> GetPageList(TestEntity para, ref Pagination pagination)
        {
            throw new NotImplementedException();
        }

        public List<TestEntity> GetList(TestEntity para)
        {
            var sql = new StringBuilder();
            sql.Append(@"select * from web_test1");
            string where = ConverPara(para);
            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendFormat(" where 1=1 {0}", where);
            }
            var list = web_test.Query(sql.ToString());
            return EntityConvertTools.CopyToList<web_test, TestEntity>(list.ToList());
        }

        public TestEntity GetEntity(string keyValue)
        {
            throw new NotImplementedException();
        }

        public bool Add(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TestEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string keyValue)
        {
            throw new NotImplementedException();
        }
        public string ConverPara(TestEntity para)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (para == null)
            {
                return sbWhere.ToString();
            }
            return sbWhere.ToString();
        }
    }
}

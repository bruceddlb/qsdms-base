
using AA.Data.IServices;
using AA.Model;
using QSDMS.Business.Base;
using QSDMS.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Business
{
    public class TestBLL : BaseBLL<ITestService<TestEntity, TestEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static TestBLL m_Instance = new TestBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static TestBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ArrivalCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public List<TestEntity> GetPageList(TestEntity para, ref Pagination pagination)
        {
            List<TestEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }
        public List<TestEntity> GetList(TestEntity para)
        {
            List<TestEntity> list = InstanceDAL.GetList(para);

            return list;
        }

    }
}

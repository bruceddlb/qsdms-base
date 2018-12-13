using QSDMS.Util.WebControl;
using Trade.Data.IServices;
using Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Business
{

    public class ArrivalBLL : BaseBLL<IArrivalService<ArrivalEntity, ArrivalEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ArrivalBLL m_Instance = new ArrivalBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ArrivalBLL Instance
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

        public int QueryCount(ArrivalEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ArrivalEntity> GetPageList(ArrivalEntity para, ref Pagination pagination)
        {
            List<ArrivalEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ArrivalEntity> GetList(ArrivalEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ArrivalEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ArrivalEntity entity)
        {
            return InstanceDAL.Update(entity);
        }

        public bool Delete(string keyValue)
        {
            return InstanceDAL.Delete(keyValue);
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ArrivalEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

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

    public class MemberBLL : BaseBLL<IMemberService<MemberEntity, MemberEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static MemberBLL m_Instance = new MemberBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static MemberBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "MemberCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(MemberEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<MemberEntity> GetPageList(MemberEntity para, ref Pagination pagination)
        {
            List<MemberEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<MemberEntity> GetList(MemberEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(MemberEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(MemberEntity entity)
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
        public MemberEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

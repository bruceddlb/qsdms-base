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

    public class MemberAddressBLL : BaseBLL<IMemberAddressService<MemberAddressEntity, MemberAddressEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static MemberAddressBLL m_Instance = new MemberAddressBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static MemberAddressBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "MemberAddressCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(MemberAddressEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<MemberAddressEntity> GetPageList(MemberAddressEntity para, ref Pagination pagination)
        {
            List<MemberAddressEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<MemberAddressEntity> GetList(MemberAddressEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(MemberAddressEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(MemberAddressEntity entity)
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
        public MemberAddressEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

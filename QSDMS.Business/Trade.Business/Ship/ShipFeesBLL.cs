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

    public class ShipFeesBLL : BaseBLL<IShipFeesService<ShipFeesEntity, ShipFeesEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ShipFeesBLL m_Instance = new ShipFeesBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ShipFeesBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ShipFeesCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(ShipFeesEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ShipFeesEntity> GetPageList(ShipFeesEntity para, ref Pagination pagination)
        {
            List<ShipFeesEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ShipFeesEntity> GetList(ShipFeesEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ShipFeesEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ShipFeesEntity entity)
        {
            return InstanceDAL.Update(entity);
        }

        public bool Delete(string keyValue)
        {
            return InstanceDAL.Delete(keyValue);
        }
        public void DeleteByObjectId(string objectid)
        {
            var list = this.GetList(new ShipFeesEntity() { ShipTempId = objectid });
            if (list != null)
            {
                foreach (var item in list)
                {
                    this.Delete(item.ShipFeesId);
                }
            }
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ShipFeesEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

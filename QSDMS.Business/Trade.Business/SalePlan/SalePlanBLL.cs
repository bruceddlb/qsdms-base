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

    public class SalePlanBLL : BaseBLL<ISalePlanService<SalePlanEntity, SalePlanEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static SalePlanBLL m_Instance = new SalePlanBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static SalePlanBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "SalePlanCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(SalePlanEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<SalePlanEntity> GetPageList(SalePlanEntity para, ref Pagination pagination)
        {
            List<SalePlanEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<SalePlanEntity> GetList(SalePlanEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(SalePlanEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(SalePlanEntity entity)
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
        public SalePlanEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }

        /// <summary>
        /// 修改计划状态
        /// </summary>
        public void UpdatePlanStatus(string planid)
        {
            var saleplandetailList = SalePlanDetaiBLL.Instance.GetList(new SalePlanDetaiEntity()
            {
                SalePlanId = planid
            });
            bool flag = false;
            foreach (var item in saleplandetailList)
            {
                if (item.Status == (int)Trade.Model.Enums.ArrivalStatus.未到货)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                //修改计划单状态为已失效
                var entity = GetEntity(planid);
                if (entity != null)
                {
                    entity.PlanStatus = (int)Trade.Model.Enums.PlanStatus.已失效;
                    entity.SalePlanId = planid;
                    this.Update(entity);
                }
            }
        }

        /// <summary>
        /// 重啓計劃
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool DoAgin(string keyValue)
        {
            return InstanceDAL.DoAgin(keyValue);
        }
    }
}

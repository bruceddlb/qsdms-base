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

    public class CategoryBLL : BaseBLL<ICategoryService<CategoryEntity, CategoryEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static CategoryBLL m_Instance = new CategoryBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static CategoryBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "CategoryCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(CategoryEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<CategoryEntity> GetPageList(CategoryEntity para, ref Pagination pagination)
        {
            List<CategoryEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<CategoryEntity> GetList(CategoryEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(CategoryEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(CategoryEntity entity)
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
        public CategoryEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

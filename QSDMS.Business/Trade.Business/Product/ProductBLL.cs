﻿using QSDMS.Util.WebControl;
using Trade.Data.IServices;
using Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Business
{

    public class ProductBLL : BaseBLL<IProductService<ProductEntity, ProductEntity, Pagination>>
    {

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ProductBLL m_Instance = new ProductBLL();

        /// <summary>
        /// 访问实例
        /// </summary>
        public static ProductBLL Instance
        {
            get { return m_Instance; }
        }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ProductCache";


        /// <summary>
        /// 构造方法
        /// </summary>

        public int QueryCount(ProductEntity para)
        {
            return InstanceDAL.QueryCount(para);
        }

        public List<ProductEntity> GetPageList(ProductEntity para, ref Pagination pagination)
        {
            List<ProductEntity> list = InstanceDAL.GetPageList(para, ref pagination);

            return list;
        }

        public List<ProductEntity> GetList(ProductEntity para)
        {
            return InstanceDAL.GetList(para);
        }

        public bool Add(ProductEntity entity)
        {
            return InstanceDAL.Add(entity);
        }

        public bool Update(ProductEntity entity)
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
        public ProductEntity GetEntity(string keyValue)
        {
            return InstanceDAL.GetEntity(keyValue);
        }
    }
}

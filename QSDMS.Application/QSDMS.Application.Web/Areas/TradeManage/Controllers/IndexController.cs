using QSDMS.Application.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSDMS.Util;
using QSDMS.Util.Extension;
using Trade.Business;
using QSDMS.Model;
using QSDMS.Business;
using QSDMS.Business.Cache;

namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class IndexController : BaseController
    {
        private DataItemCache dataItemCache = new DataItemCache();
        //
        // GET: /TradeManage/Index/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetHomeJson()
        {
            HomeEntity data = new HomeEntity();
            data.CpysCount = ProductBLL.Instance.GetList(null).Where(o => o.ProductStatus != (int)Trade.Model.Enums.ProductStatus.删除 && o.ProductType == dataItemCache.GetDataItemList("cpsx").Where(p => p.ItemValue == "ys").FirstOrDefault().ItemDetailId).Count();
            data.CpzsCount = ProductBLL.Instance.GetList(null).Where(o => o.ProductStatus != (int)Trade.Model.Enums.ProductStatus.删除 && o.ProductType == dataItemCache.GetDataItemList("cpsx").Where(p => p.ItemValue == "zs").FirstOrDefault().ItemDetailId).Count();
            data.DdysCount = OrderBLL.Instance.GetList(null).Where(o => o.OrderStatus != (int)Trade.Model.Enums.OrderStatus.待支付 && o.OrderType == dataItemCache.GetDataItemList("ddsx").Where(p => p.ItemValue == "ys").FirstOrDefault().ItemDetailId).Count();
            data.DdzsCount = OrderBLL.Instance.GetList(null).Where(o => o.OrderStatus != (int)Trade.Model.Enums.OrderStatus.待支付 && o.OrderType == dataItemCache.GetDataItemList("ddsx").Where(p => p.ItemValue == "zs").FirstOrDefault().ItemDetailId).Count();
            data.DdfhCount = DeliverOrderBLL.Instance.GetList(null).Where(o => o.OrderStatus != (int)Trade.Model.Enums.DeliverOrderSatus.待支付).Count();
            return Content(data.ToJson());
        }
    }

    public class HomeEntity
    {

        public int CpysCount { get; set; }

        public int CpzsCount { get; set; }
        public int DdysCount { get; set; }

        public int DdzsCount { get; set; }

        public int DdfhCount { get; set; }

    }
}

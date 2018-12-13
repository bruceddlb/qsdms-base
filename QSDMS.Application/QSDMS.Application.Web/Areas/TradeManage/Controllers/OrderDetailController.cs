using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSDMS.Util;
using QSDMS.Util.Extension;
using QSDMS.Util.WebControl;
using iFramework.Framework;
using Trade.Model;
using Trade.Business;
using QSDMS.Application.Web.Controllers;
using QSDMS.Business;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class OrderDetailController : BaseController
    {
        //
        // GET: /TradeManage/OrderDetail/

        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Dispose()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();

            var queryParam = queryJson.ToJObject();
            var para = new OrderDetailEntity();

            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            if (!queryParam["orderId"].IsEmpty())
            {
                para.OrderId = queryParam["orderId"].ToString();
            }

            //数据对象
            var pageList = OrderDetailBLL.Instance.GetPageList(para, ref pagination);
            var JsonData = new
            {
                rows = pageList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }

        /// <summary>
        /// 处理已到货
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoDispose(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    bool flag = true;
                    foreach (var key in keys)
                    {
                        var entity = OrderDetailBLL.Instance.GetEntity(key);
                        if (entity != null && (entity.Status != (int)Trade.Model.Enums.OrderDetailStatus.未到货))
                        {
                            flag = false;
                            return Error("非[未到货]状态的订单不能此操作");
                        }
                    }
                    if (flag)
                    {
                        foreach (var key in keys)
                        {
                            var detail = OrderDetailBLL.Instance.GetEntity(key);
                            detail.Status = (int)Trade.Model.Enums.OrderDetailStatus.已到货;
                            OrderDetailBLL.Instance.Update(detail);
                        }
                    }
                }
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "OrderDetailController>>Dispose";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }
        }

    }
}

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
    public class DeliverOrderDetailController : BaseController
    {
        //
        // GET: /TradeManage/DeliverOrderDetail/

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
            var para = new DeliverOrderDetailEntity();

            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            if (!queryParam["orderId"].IsEmpty())
            {
                para.DeliverOrderId = queryParam["orderId"].ToString();
            }

            //数据对象
            var pageList = DeliverOrderDetailBLL.Instance.GetPageList(para, ref pagination);
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
        public ActionResult DoDispose(string keyValue, DeliverOrderEntity entity)
        {
            try
            {
                var deliverorder = DeliverOrderBLL.Instance.GetEntity(keyValue);
                if (deliverorder != null)
                {
                    if (deliverorder.OrderStatus != (int)Trade.Model.Enums.DeliverOrderSatus.已支付)
                    {
                        return Error("非[已支付]状态的订单不能此操作");
                    }
                    deliverorder.LogisticsNo = entity.LogisticsNo;
                    deliverorder.LogisticsName = entity.LogisticsName;
                    deliverorder.LogisticsTime = entity.LogisticsTime;
                    deliverorder.OrderStatus = (int)Trade.Model.Enums.DeliverOrderSatus.已发货;
                    if (DeliverOrderBLL.Instance.Update(deliverorder))
                    {
                        //修改对应修改销售订单产品状态
                        var deliverdetaillist = DeliverOrderDetailBLL.Instance.GetList(new DeliverOrderDetailEntity() { DeliverOrderId = deliverorder.DeliverOrderId });
                        foreach (var deliverdetailitem in deliverdetaillist)
                        {
                            var orderdetail = OrderDetailBLL.Instance.GetEntity(deliverdetailitem.OrderdetailId);
                            if (orderdetail != null)
                            {
                                orderdetail.Status = (int)Trade.Model.Enums.OrderDetailStatus.已发货;
                                OrderDetailBLL.Instance.Update(orderdetail);
                            }
                        }
                        //修改销售订单状态
                        var order = OrderBLL.Instance.GetEntity(deliverorder.OrderId);
                        if (order != null)
                        {
                            var detailList = OrderDetailBLL.Instance.GetList(new OrderDetailEntity() { OrderId = deliverorder.OrderId });
                            detailList = detailList.Where(p => p.Status == (int)Trade.Model.Enums.OrderDetailStatus.未到货 || p.Status == (int)Trade.Model.Enums.OrderDetailStatus.已到货).ToList();
                            if (detailList.Count > 0)
                            {

                                order.OrderStatus = (int)Trade.Model.Enums.OrderStatus.部分发货;
                            }
                            else
                            {
                                order.OrderStatus = (int)Trade.Model.Enums.OrderStatus.完成;
                            }
                            OrderBLL.Instance.Update(order);
                        }
                    }
                }
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "DeliverOrderDetailController>>Dispose";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }
        }


    }
}

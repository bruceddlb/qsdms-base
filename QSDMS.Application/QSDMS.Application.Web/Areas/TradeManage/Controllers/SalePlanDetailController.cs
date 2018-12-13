using iFramework.Framework;
using QSDMS.Application.Web.Controllers;
using QSDMS.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSDMS.Util;
using QSDMS.Util.Extension;
using Trade.Model;
using Trade.Business;
using QSDMS.Business;

namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class SalePlanDetailController : BaseController
    {
        //
        // GET: /TradeManage/SalePlanDetai/

        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult SelectProduct()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var queryParam = queryJson.ToJObject();
            var para = new SalePlanDetaiEntity();
            if (!queryParam["planId"].IsEmpty())
            {
                para.SalePlanId = queryParam["planId"].ToString();
            }
            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            //数据对象
            var pageList = SalePlanDetaiBLL.Instance.GetPageList(para, ref pagination);
            if (pageList != null)
            {
                foreach (var o in pageList)
                {

                    if (o.ProductId != null)
                    {
                        var product = ProductBLL.Instance.GetEntity(o.ProductId);
                        if (product != null)
                        {
                            o.ProductNo = product.ProductNO;
                            o.ProductName = product.ProductName;
                            o.Price = product.ProductPrice;
                        }
                    }
                }
            }
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
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = SalePlanDetaiBLL.Instance.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpPost]
        public ActionResult RemoveForm(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        SalePlanDetaiBLL.Instance.Delete(key);
                    }
                }
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanDetaiController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult AddSalePlanProduct(string planId, string ids)
        {
            try
            {
                int num = 0;
                if (planId != "" && ids != "")
                {
                    string[] keys = ids.Split(',');
                    if (keys != null)
                    {
                        var flag = false;
                        foreach (var key in keys)
                        {
                            var plist = SalePlanDetaiBLL.Instance.GetList(new SalePlanDetaiEntity()
                            {
                                ProductId = key,
                                SalePlanId = planId
                            });
                            if (plist == null)
                            {
                                flag = true;
                                break;
                            }
                            if (plist.Count > 0)
                            {
                                flag = true;
                                break;
                            }

                        }
                        if (flag)
                        {
                            return Error("产品不能重复添加计划");
                        }
                        foreach (var key in keys)
                        {
                            var detail = SalePlanDetaiBLL.Instance.GetList(new SalePlanDetaiEntity()
                            {
                                ProductId = key,
                                SalePlanId=planId
                            }).FirstOrDefault();
                            if (detail == null)
                            {
                                SalePlanDetaiEntity entity = new SalePlanDetaiEntity();
                                entity.SalePlanDetaiId = Util.Util.NewUpperGuid();
                                entity.SalePlanId = planId;
                                entity.ProductId = key;
                                entity.Status = (int)Trade.Model.Enums.ArrivalStatus.未到货;
                                ProductEntity product = ProductBLL.Instance.GetEntity(key);
                                if (product != null)
                                {
                                    entity.ProductName = product.ProductName;
                                    entity.ProductNo = product.ProductNO;
                                    entity.Price = product.ProductPrice;
                                }
                                SalePlanDetaiBLL.Instance.Add(entity);
                                num++;
                            }
                        }
                    }

                }
                return Success(string.Format("成功添加{0}个产品", num));
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanDetaiController>>AddSalePlanProduct";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }
        }

        /// <summary>
        /// 预售计划处理已到货
        /// 对于计划产品操作订单状态，已到货状态 对应更改所有订单明细相关的此产品状态为已到货
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoDispose(string keyValue, string planid)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    bool flag = true;
                    foreach (var key in keys)
                    {
                        var entity = SalePlanDetaiBLL.Instance.GetEntity(key);
                        if (entity != null && (entity.Status != (int)Trade.Model.Enums.ArrivalStatus.未到货))
                        {
                            flag = false;
                            return Error("非[未到货]状态不能此操作");
                        }
                    }
                    if (flag)
                    {
                        var bl = SalePlanDetaiBLL.Instance.DoDispose(keys);
                        if (bl)
                        {
                            //处理计划状态，如果明细所有产品都已到货计划对应处理成失效状态
                            SalePlanBLL.Instance.UpdatePlanStatus(planid);
                        }
                    }
                }
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanDetaiController>>DoDispose";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }
        }
    }
}

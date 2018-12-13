using QSDMS.Application.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSDMS.Util;
using QSDMS.Util.Extension;
using Trade.Model;
using QSDMS.Business;
using Trade.Business;
using QSDMS.Application.Web.Controllers;
using QSDMS.Util.WebControl;
using iFramework.Framework;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class ShopShipTemplatesController : BaseController
    {
        //
        // GET: /TradeManage/ShopShipTemplates/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();

            var queryParam = queryJson.ToJObject();
            var para = new ShopShipTemplatesEntity();
            if (!queryParam["keyword"].IsEmpty())
            {
                para.Title = queryParam["keyword"].ToString();
            }

            //数据对象
            var pageList = ShopShipTemplatesBLL.Instance.GetPageList(para, ref pagination);

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

        public ActionResult GetListJson()
        {
            var watch = CommonHelper.TimerStart();
            var list = ShopShipTemplatesBLL.Instance.GetList(null);
            var costtime = CommonHelper.TimerEnd(watch);
            Logger.Debug("获取模板请求时间：" + costtime);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = ShopShipTemplatesBLL.Instance.GetEntity(keyValue);
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
                ShipFeesBLL.Instance.DeleteByObjectId(keyValue);
                ShopShipTemplatesBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ShopShipTemplatesController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ShopShipTemplatesEntity entity)
        {
            try
            {

                if (keyValue != "")
                {
                    entity.ShopShipTemplatesId = keyValue;
                    ShopShipTemplatesBLL.Instance.Update(entity);

                }
                else
                {
                    entity.ShopShipTemplatesId = Util.Util.NewUpperGuid();
                    ShopShipTemplatesBLL.Instance.Add(entity);
                }
                return Success("保存成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ShopShipTemplatesController>>Save";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
            }

        }
    }
}

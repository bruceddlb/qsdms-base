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
    public class ShipFeesController : BaseController
    {
        //
        // GET: /TradeManage/ShipFees/

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
            var para = new ShipFeesEntity();
            if (!queryParam["tempid"].IsEmpty())
            {
                para.ShipTempId = queryParam["tempid"].ToString();
            }

            //数据对象
            var pageList = ShipFeesBLL.Instance.GetPageList(para, ref pagination);

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
            var data = new ShipFeesEntity();
            try
            {
                data = ShipFeesBLL.Instance.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ShipFeesController>>GetFormJson";
                new ExceptionHelper().LogException(ex);
            }

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
                ShipFeesBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ShipFeesController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ShipFeesEntity entity)
        {
            try
            {

                if (keyValue != "")
                {
                    entity.ShipFeesId = keyValue;
                    ShipFeesBLL.Instance.Update(entity);

                }
                else
                {
                    if (entity.ProvinceId == "0")
                    {
                        var count = ShipFeesBLL.Instance.GetList(new ShipFeesEntity() { ProvinceId = entity.ProvinceId, ShipTempId = entity.ShipTempId }).Count;
                        if (count > 0)
                        {
                            return Error("已设置默认区域");
                        }
                    }
                    entity.ShipFeesId = Util.Util.NewUpperGuid();
                    ShipFeesBLL.Instance.Add(entity);
                }
                return Success("保存成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ShipFeesController>>Save";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
            }

        }
    }
}

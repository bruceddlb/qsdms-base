using QSDMS.Application.Web.Controllers;
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

namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class ServiceRuleController : BaseController
    {
        //
        // GET: /SystemManage/Settings/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Form()
        {

            return View();
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var queryParam = queryJson.ToJObject();
            ServiceRuleEntity para = new ServiceRuleEntity();
            if (!queryParam["keyword"].IsEmpty())
            {
                para.Title = queryParam["keyword"].ToString();
            }
            var pageList = ServiceRuleBLL.Instance.GetPageList(para, ref pagination);
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

        [HttpGet]
        public ActionResult GetListJson()
        {
            var list = ServiceRuleBLL.Instance.GetList(null);
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
            var data = ServiceRuleBLL.Instance.GetEntity(keyValue);
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
                ServiceRuleBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ServiceRuleController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ServiceRuleEntity entity)
        {
            try
            {
                if (keyValue == "")
                {
                    //新增
                    entity.ServiceRuleId = Util.Util.NewUpperGuid();
                    ServiceRuleBLL.Instance.Add(entity);
                }
                else
                {
                    entity.ServiceRuleId = keyValue;
                    ServiceRuleBLL.Instance.Update(entity);

                }
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ServiceRuleController>>SaveForm";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }
    }
}

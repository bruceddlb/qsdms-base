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
    public class LogisticsController : BaseController
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
            LogistCompEntity para = new LogistCompEntity();
            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            var pageList = LogistCompBLL.Instance.GetPageList(para, ref pagination);
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
            var list = LogistCompBLL.Instance.GetList(null);
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
            var data = LogistCompBLL.Instance.GetEntity(keyValue);
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
                LogistCompBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "LogisticsController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, LogistCompEntity entity)
        {
            try
            {
                if (keyValue == "")
                {
                    //新增
                    entity.LogistCompId = Util.Util.NewUpperGuid();
                    LogistCompBLL.Instance.Add(entity);
                }
                else
                {
                    entity.LogistCompId = keyValue;
                    LogistCompBLL.Instance.Update(entity);

                }
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "LogisticsController>>SaveForm";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }
    }
}

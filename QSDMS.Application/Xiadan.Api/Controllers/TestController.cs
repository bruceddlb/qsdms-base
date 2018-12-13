using iFramework.Framework;
using QSDMS.Util;
using QSDMS.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trade.Business;

namespace Xiadan.Api.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult List()
        {
            var result = new ReturnMessage(false) { Message = "获取信息!" };
            try
            {
                var list = BannerBLL.Instance.GetList(null);
                result.IsSuccess = true;
                result.Message = "获取成功";
                result.ResultData["List"] = list;
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "TestController>>List";
                new ExceptionHelper().LogException(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductList(int size, int page)
        {
            var result = new ReturnMessage(false) { Message = "获取信息!" };
            try
            {
                Pagination pageion = new Pagination();
                pageion.page = page;
                pageion.rows = size;
                var list = ProductBLL.Instance.GetPageList(null,ref pageion);
                result.IsSuccess = true;
                result.Message = "获取成功";
                result.ResultData["List"] = list;
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "TestController>>List";
                new ExceptionHelper().LogException(ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

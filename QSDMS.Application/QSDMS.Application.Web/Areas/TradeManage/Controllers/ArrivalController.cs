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
using QSDMS.Util.Excel;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class ArrivalController : BaseController
    {
        #region 视图功能
        /// <summary>
        /// 区域管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 区域表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 区域详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var queryParam = queryJson.ToJObject();
            ArrivalEntity para = new ArrivalEntity();
            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            var pageList = ArrivalBLL.Instance.GetPageList(para, ref pagination);
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
            var data = ArrivalBLL.Instance.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion


        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]

        public ActionResult RemoveForm(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        ArrivalBLL.Instance.GetEntity(key);
                    }
                }
                return Success("删除成功");
            }
            catch (Exception ex)
            {

                ex.Data["Method"] = "ArrivalController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ArrivalEntity entity)
        {
            try
            {

                if (keyValue == "")
                {
                    //新增
                    entity.ArrivalId = Util.Util.NewUpperGuid();
                    ArrivalBLL.Instance.Add(entity);
                }
                else
                {
                    entity.ArrivalId = keyValue;
                    ArrivalBLL.Instance.Update(entity);

                }
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                return Error("操作失败。");
            }
        }
        #endregion

        /// <summary>
        /// 导出EXCEL
        /// </summary>       
        public void ExportExcel(string queryJson)
        {
            string cacheKey = Request["cacheid"] as string;
            HttpRuntime.Cache[cacheKey + "-state"] = "processing";
            HttpRuntime.Cache[cacheKey + "-row"] = "0";
            try
            {
                //这里要url解码
                var queryParam = Server.UrlDecode(queryJson).ToJObject();

                var para = new ArrivalEntity();


                if (!queryParam["keyword"].IsEmpty())
                {
                    para.KeyWord = queryParam["keyword"].ToString();
                }

                var list = ArrivalBLL.Instance.GetList(para);
                foreach (var item in list)
                {
                    if (item.ArrivalStatus != null)
                    {
                        item.ArrivalStatusName = ((Trade.Model.Enums.ArrivalStatus)item.ArrivalStatus).ToString();
                    }
                }

                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = "到货列表信息";
                excelconfig.TitleFont = "微软雅黑";
                excelconfig.TitlePoint = 10;
                excelconfig.FileName = "到货列表信息.xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BillCode", ExcelColumn = "订单号", Width = 20 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Title", ExcelColumn = "标题", Width = 15 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ArrivalStatusName", ExcelColumn = "到货状态", Width = 20 });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Remark", ExcelColumn = "备注", Width = 50 });
                //需合并索引
                //excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                //调用导出方法
                ExcelHelper<ArrivalEntity>.ExcelDownload(list, excelconfig);
                HttpRuntime.Cache[cacheKey + "-state"] = "done";
            }


            catch (Exception ex)
            {
                HttpRuntime.Cache[cacheKey + "-state"] = "error";
            }
        }
    }
}

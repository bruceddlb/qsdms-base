using iFramework.Framework;
using QSDMS.Util.WebControl;
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
using iFramework.Framework.Security;
using QSDMS.Util.Excel;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class SalePlanController : BaseController
    {
        //
        // GET: /TradeManage/SalePlan/

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
            var para = new SalePlanEntity();
            if (!queryParam["keyword"].IsEmpty())
            {
                para.PlanTitle = queryParam["keyword"].ToString();
            }
            if (!queryParam["StartTime"].IsEmpty())
            {
                para.StartTime = queryParam["StartTime"].ToString();
            }
            if (!queryParam["EndTime"].IsEmpty())
            {
                para.EndTime = queryParam["EndTime"].ToString();
            }
            var pageList = new List<SalePlanEntity>();
            try
            {
                //数据对象
                pageList = SalePlanBLL.Instance.GetPageList(para, ref pagination);
            }
            catch (Exception ex)
            {


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
            var data = SalePlanBLL.Instance.GetEntity(keyValue);
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
                SalePlanDetaiBLL.Instance.DeleteByObjectId(keyValue);
                SalePlanBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, SalePlanEntity entity)
        {
            try
            {
                if (keyValue != "")
                {
                    entity.SalePlanId = keyValue;
                    SalePlanBLL.Instance.Update(entity);
                }
                else
                {
                    entity.SalePlanId = Util.Util.NewUpperGuid();
                    entity.PlanStatus = (int)Trade.Model.Enums.PlanStatus.已生效;
                    SalePlanBLL.Instance.Add(entity);
                }

                return Success("保存成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanController>>SaveForm";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
            }
        }


        /// <summary>
        /// 重启计划
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DoAgin(string keyValue)
        {
            try
            {
                var entity = SalePlanBLL.Instance.GetEntity(keyValue);
                if (entity != null && (entity.PlanStatus != (int)Trade.Model.Enums.PlanStatus.已失效))
                {
                    return Error("非[已失效]状态不能此操作");
                }
                SalePlanBLL.Instance.DoAgin(keyValue);
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "SalePlanController>>DoAgin";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }
        }

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

                var para = new SalePlanEntity();

                if (!queryParam["keyword"].IsEmpty())
                {
                    para.PlanTitle = queryParam["keyword"].ToString();
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    para.StartTime = queryParam["StartTime"].ToString();
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    para.EndTime = queryParam["EndTime"].ToString();
                }
                List<SalePlanExportEntity> listnew = new List<SalePlanExportEntity>();
                var list = SalePlanBLL.Instance.GetList(para);
                if (list != null)
                {
                    foreach (var o in list)
                    {
                        //明细
                        var detailList = SalePlanDetaiBLL.Instance.GetList(new SalePlanDetaiEntity() { SalePlanId = o.SalePlanId });
                        foreach (var detailitem in detailList)
                        {
                            SalePlanExportEntity entity = new SalePlanExportEntity();

                            entity.PlanTitle = o.PlanTitle.ToString();
                            entity.StartTime = o.SaleStartTime.ToString();
                            entity.EndTime = o.SaleEndTime.ToString();
                            entity.Remark = o.Remark;
                            entity.ProductPrice = detailitem.Price.ToString();
                            var product = ProductBLL.Instance.GetEntity(detailitem.ProductId);
                            if (product != null)
                            {
                                entity.ProductNO = product.ProductNO;
                                entity.ProductName = product.ProductName;
                                entity.ProductPrice = product.ProductPrice.ToString();
                            }
                            listnew.Add(entity);
                        }
                    }

                    //设置导出格式
                    ExcelConfig excelconfig = new ExcelConfig();
                    excelconfig.Title = "销售计划列表";
                    excelconfig.TitleFont = "微软雅黑";
                    excelconfig.TitlePoint = 10;
                    excelconfig.FileName = "销售计划列表.xls";
                    excelconfig.IsAllSizeColumn = true;
                    //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                    List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                    excelconfig.ColumnEntity = listColumnEntity;
                    ColumnEntity columnentity = new ColumnEntity();
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "PlanTitle", ExcelColumn = "计划名称", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "StartTime", ExcelColumn = "开始时间", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "EndTime", ExcelColumn = "结束时间", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Remark", ExcelColumn = "备注", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductNO", ExcelColumn = "产品编号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductName", ExcelColumn = "产品名称", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductPrice", ExcelColumn = "产品价格", Width = 10 });
                    //需合并索引
                    excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3 };
                    //调用导出方法
                    ExcelHelper<SalePlanExportEntity>.ExcelDownload(listnew, excelconfig);
                    HttpRuntime.Cache[cacheKey + "-state"] = "done";
                }

            }
            catch (Exception ex)
            {
                HttpRuntime.Cache[cacheKey + "-state"] = "error";
            }
        }

        /// <summary>
        /// 导出扩展类
        /// </summary>
        public class SalePlanExportEntity
        {
            //订单信息
            public string PlanTitle { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Remark { get; set; }

            /// <summary>
            /// 产品
            /// </summary>
            public string ProductNO { get; set; }
            public string ProductName { get; set; }
            public string ProductPrice { get; set; }
        }
    }

}

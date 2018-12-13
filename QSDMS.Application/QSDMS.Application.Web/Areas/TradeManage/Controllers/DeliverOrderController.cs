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
using QSDMS.Util.Excel;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class DeliverOrderController : BaseController
    {
        //
        // GET: /TradeManage/Order/

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
            var para = new DeliverOrderEntity();

            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            if (!queryParam["orderno"].IsEmpty())
            {
                para.BillCode = queryParam["orderno"].ToString();
            }
            if (!queryParam["saleorderno"].IsEmpty())
            {
                para.SaleOrderNo = queryParam["saleorderno"].ToString();
            }

            if (!queryParam["orderstatus"].IsEmpty())
            {
                para.OrderStatus = int.Parse(queryParam["orderstatus"].ToString());
            }

            if (!queryParam["StartTime"].IsEmpty())
            {
                para.StartTime = queryParam["StartTime"].ToString();
            }
            if (!queryParam["EndTime"].IsEmpty())
            {
                para.EndTime = queryParam["EndTime"].ToString();
            }

            //数据对象
            var pageList = DeliverOrderBLL.Instance.GetPageList(para, ref pagination);
            if (pageList != null)
            {
                foreach (var o in pageList)
                {
                    if (o.AddressId != null)
                    {
                        var address = MemberAddressBLL.Instance.GetEntity(o.AddressId);
                        if (address != null)
                        {
                            o.Address = address;
                            o.Address.AddressExtInfo = address.ProvinceName + address.CityName + address.CountyName + address.Address;
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
            var data = DeliverOrderBLL.Instance.GetEntity(keyValue);
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
                DeliverOrderDetailBLL.Instance.DeleteByObjectId(keyValue);
                DeliverOrderBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "DeliverOrderController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
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
            var para = new DeliverOrderEntity();
            try
            {
                //这里要url解码
                var queryParam = Server.UrlDecode(queryJson).ToJObject();

                if (!queryParam["keyword"].IsEmpty())
                {
                    para.KeyWord = queryParam["keyword"].ToString();
                }
                if (!queryParam["orderno"].IsEmpty())
                {
                    para.BillCode = queryParam["orderno"].ToString();
                }
                if (!queryParam["orderstatus"].IsEmpty())
                {
                    para.OrderStatus = int.Parse(queryParam["orderstatus"].ToString());
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    para.StartTime = queryParam["StartTime"].ToString();
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    para.EndTime = queryParam["EndTime"].ToString();
                }
                List<DeliverOrderExportEntity> listnew = new List<DeliverOrderExportEntity>();
                //数据对象
                var list = DeliverOrderBLL.Instance.GetList(para);
                if (list != null)
                {
                    foreach (var o in list)
                    {
                        //明细
                        var detailList = DeliverOrderDetailBLL.Instance.GetList(new DeliverOrderDetailEntity() { DeliverOrderId = o.DeliverOrderId });
                        foreach (var detailitem in detailList)
                        {
                            DeliverOrderExportEntity entity = new DeliverOrderExportEntity();
                            entity.BillCode = o.BillCode;
                            entity.CreateDate = o.CreateDate == null ? "" : o.CreateDate.ToString();
                            entity.Freight = o.Freight == null ? "" : o.Freight.ToString();
                            entity.Remark = o.Remark;
                            entity.LogisticsName = o.LogisticsName;
                            entity.LogisticsNo = o.LogisticsNo;
                            entity.SaleOrderNo = o.SaleOrderNo;
                            entity.LogisticsTime = o.LogisticsTime == null ? "" : o.LogisticsTime.ToString();



                            if (o.AddressId != null)
                            {
                                var address = MemberAddressBLL.Instance.GetEntity(o.AddressId);
                                if (address != null)
                                {
                                    entity.AddressExtInfo = address.ProvinceName + address.CityName + address.CountyName + address.Address;
                                    entity.Consignee = address.Consignee;
                                    entity.Mobile = address.Mobile;
                                }
                            }
                            if (o.OrderStatus != null)
                            {
                                entity.OrderStatusName = ((Trade.Model.Enums.DeliverOrderSatus)o.OrderStatus).ToString();
                            }

                            entity.ProductCount = detailitem.BuyCount.ToString();
                            entity.ProductPrice = detailitem.Price.ToString();
                            var product = ProductBLL.Instance.GetEntity(detailitem.ProductId);
                            if (product != null)
                            {
                                entity.ProductNO = product.ProductNO;
                                entity.ProductName = product.ProductName;
                            }

                            listnew.Add(entity);
                        }
                    }

                    //设置导出格式
                    ExcelConfig excelconfig = new ExcelConfig();
                    excelconfig.Title = "发货订单信息列表";
                    excelconfig.TitleFont = "微软雅黑";
                    excelconfig.TitlePoint = 10;
                    excelconfig.FileName = "发货订单信息列表.xls";
                    excelconfig.IsAllSizeColumn = true;
                    //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                    List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                    excelconfig.ColumnEntity = listColumnEntity;
                    ColumnEntity columnentity = new ColumnEntity();

                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BillCode", ExcelColumn = "业务单号", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CreateDate", ExcelColumn = "订单时间", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "SaleOrderNo", ExcelColumn = "销售订单号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Freight", ExcelColumn = "配送费用", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "OrderStatusName", ExcelColumn = "订单状态", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Remark", ExcelColumn = "客户留言", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Consignee", ExcelColumn = "收货人", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Mobile", ExcelColumn = "收货人电话", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "AddressExtInfo", ExcelColumn = "收货地址", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "LogisticsNo", ExcelColumn = "物流单号", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "LogisticsName", ExcelColumn = "物流公司", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "LogisticsTime", ExcelColumn = "发货时间", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductNO", ExcelColumn = "产品编号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductName", ExcelColumn = "产品名称", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductCount", ExcelColumn = "产品数量", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductPrice", ExcelColumn = "产品价格", Width = 10 });
                    //需合并索引
                    excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                    //调用导出方法
                    ExcelHelper<DeliverOrderExportEntity>.ExcelDownload(listnew, excelconfig);
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
        public class DeliverOrderExportEntity
        {
            //订单信息          
            public string BillCode { get; set; }
            public string CreateDate { get; set; }
            public string Freight { get; set; }
            public string SaleOrderNo { get; set; }
            public string OrderStatusName { get; set; }
            public string Remark { get; set; }
            //收货人信息
            public string Consignee { get; set; }
            public string Mobile { get; set; }
            public string AddressExtInfo { get; set; }

            public string LogisticsNo { get; set; }

            public string LogisticsName { get; set; }

            public string LogisticsTime { get; set; }
            /// <summary>
            /// 产品
            /// </summary>
            public string ProductNO { get; set; }
            public string ProductName { get; set; }
            public string ProductCount { get; set; }
            public string ProductPrice { get; set; }
        }
    }
}

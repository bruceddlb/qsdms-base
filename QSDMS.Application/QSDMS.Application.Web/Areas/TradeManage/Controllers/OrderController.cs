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
    public class OrderController : BaseController
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
            var para = new OrderEntity();

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
            if (!queryParam["ordertype"].IsEmpty())
            {
                para.OrderType = queryParam["ordertype"].ToString();
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
            var pageList = OrderBLL.Instance.GetPageList(para, ref pagination);
            if (pageList != null)
            {
                foreach (var o in pageList)
                {
                    if (o.OrderType != null)
                    {
                        var dataitem = DataItemDetailBLL.Instance.GetEntity(o.OrderType);
                        if (dataitem != null)
                        {
                            o.OrderTypeName = dataitem.ItemName;
                        }
                    }
                    if (o.PayWay != null)
                    {
                        o.PayWayName = ((Trade.Model.Enums.PayType)o.PayWay).ToString();
                    }
                    //if (o.AddressId != null)
                    //{
                    //    var address = MemberAddressBLL.Instance.GetEntity(o.AddressId);
                    //    if (address != null)
                    //    {
                    //        o.Address = address;
                    //        o.Address.AddressExtInfo = address.ProvinceName + address.CityName + address.CountyName + address.Address;
                    //    }
                    //}
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
            var data = OrderBLL.Instance.GetEntity(keyValue);
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
                OrderDetailBLL.Instance.DeleteByObjectId(keyValue);
                OrderBLL.Instance.Delete(keyValue);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "OrderController>>RemoveForm";
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
            var para = new OrderEntity();
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
                if (!queryParam["ordertype"].IsEmpty())
                {
                    para.OrderType = queryParam["ordertype"].ToString();
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    para.StartTime = queryParam["StartTime"].ToString();
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    para.EndTime = queryParam["EndTime"].ToString();
                }
                List<OrderExportEntity> listnew = new List<OrderExportEntity>();
                //数据对象
                var list = OrderBLL.Instance.GetList(para);
                if (list != null)
                {
                    foreach (var o in list)
                    {
                        //明细
                        var detailList = OrderDetailBLL.Instance.GetList(new OrderDetailEntity() { OrderId = o.OrderId });
                        foreach (var detailitem in detailList)
                        {
                            OrderExportEntity entity = new OrderExportEntity();
                            entity.BillCode = o.BillCode;
                            entity.OrderDate = o.OrderDate.ToString();
                            entity.OrderMoney = o.OrderMoney.ToString();
                            entity.Remark = o.Remark;
                            if (o.OrderType != null)
                            {
                                var dataitem = DataItemDetailBLL.Instance.GetEntity(o.OrderType);
                                if (dataitem != null)
                                {
                                    entity.OrderTypeName = dataitem.ItemName;
                                }
                            }

                            if (o.PayWay != null)
                            {
                                entity.PayWayName = ((Trade.Model.Enums.PayType)o.PayWay).ToString();
                            }
                            //if (o.AddressId != null)
                            //{
                            //    var address = MemberAddressBLL.Instance.GetEntity(o.AddressId);
                            //    if (address != null)
                            //    {
                            //        entity.AddressExtInfo = address.ProvinceName + address.CityName + address.CountyName + address.Address;
                            //        entity.Consignee = address.Consignee;
                            //        entity.Mobile = address.Mobile;
                            //    }
                            //}
                            if (o.OrderStatus != null)
                            {
                                entity.OrderStatusName = ((Trade.Model.Enums.OrderStatus)o.OrderStatus).ToString();
                            }

                            entity.ProductCount = detailitem.BuyCount.ToString();
                            entity.ProductPrice = detailitem.Price.ToString();
                            var product = ProductBLL.Instance.GetEntity(detailitem.ProductId);
                            if (product != null)
                            {
                                entity.ProductNO = product.ProductNO;
                                entity.ProductName = product.ProductName;

                            }
                            if (detailitem.Status != null)
                            {
                                entity.ProductStatus = ((Trade.Model.Enums.OrderDetailStatus)detailitem.Status).ToString();
                            }
                            listnew.Add(entity);
                        }
                    }

                    //设置导出格式
                    ExcelConfig excelconfig = new ExcelConfig();
                    excelconfig.Title = "订单信息列表";
                    excelconfig.TitleFont = "微软雅黑";
                    excelconfig.TitlePoint = 10;
                    excelconfig.FileName = "订单信息列表.xls";
                    excelconfig.IsAllSizeColumn = true;
                    //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                    List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                    excelconfig.ColumnEntity = listColumnEntity;
                    ColumnEntity columnentity = new ColumnEntity();
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "OrderTypeName", ExcelColumn = "订单类型", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BillCode", ExcelColumn = "业务单号", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "OrderDate", ExcelColumn = "订单时间", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "PayWayName", ExcelColumn = "支付方式", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "OrderMoney", ExcelColumn = "支付金额", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "OrderStatusName", ExcelColumn = "订单状态", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "MemberNo", ExcelColumn = "客户编号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "MemberName", ExcelColumn = "客户名称", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Remark", ExcelColumn = "客户留言", Width = 10 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Consignee", ExcelColumn = "收货人", Width = 10 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Mobile", ExcelColumn = "收货人电话", Width = 10 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "AddressExtInfo", ExcelColumn = "收货地址", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductNO", ExcelColumn = "产品编号", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductName", ExcelColumn = "产品名称", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductCount", ExcelColumn = "产品数量", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductPrice", ExcelColumn = "产品价格", Width = 10 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductStatus", ExcelColumn = "库存状态", Width = 10 });
                    //需合并索引
                    excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    //调用导出方法
                    ExcelHelper<OrderExportEntity>.ExcelDownload(listnew, excelconfig);
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
        public class OrderExportEntity
        {
            //订单信息
            public string OrderTypeName { get; set; }
            public string BillCode { get; set; }
            public string OrderDate { get; set; }
            public string PayWayName { get; set; }
            public string OrderMoney { get; set; }
            public string OrderStatusName { get; set; }
            public string Remark { get; set; }
            //收货人信息
            public string Consignee { get; set; }
            public string Mobile { get; set; }
            public string AddressExtInfo { get; set; }

            /// <summary>
            /// 产品
            /// </summary>
            public string ProductNO { get; set; }
            public string ProductName { get; set; }
            public string ProductCount { get; set; }
            public string ProductPrice { get; set; }

            public string ProductStatus { get; set; }
        }
    }
}

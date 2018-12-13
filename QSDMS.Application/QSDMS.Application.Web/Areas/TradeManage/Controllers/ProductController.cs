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
    public class ProductController : BaseController
    {
        //
        // GET: /TradeManage/Product/

        public ActionResult List()
        {
            return View();
        }
        public ActionResult Form()
        {
            var num = GenerateUniqueNumText(8);
            ViewBag.ProCode = num;
            return View();
        }

        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();

            var queryParam = queryJson.ToJObject();
            var para = new ProductEntity();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                var condition = queryParam["condition"].ToString().ToLower();
                switch (condition)
                {
                    case "productname":
                        para.ProductName = queryParam["keyword"].ToString();
                        break;
                    case "productno":
                        para.ProductNO = queryParam["keyword"].ToString();
                        break;
                }
            }
            if (!queryParam["keyword"].IsEmpty())
            {
                para.KeyWord = queryParam["keyword"].ToString();
            }
            if (!queryParam["type"].IsEmpty())
            {
                para.ProductType = queryParam["type"].ToString();
            }
            if (!queryParam["cateid"].IsEmpty())
            {
                para.ProductCategoryId = queryParam["cateid"].ToString();
            }

            //数据对象
            var pageList = ProductBLL.Instance.GetPageList(para, ref pagination);
            if (pageList != null)
            {
                foreach (var o in pageList)
                {

                    if (o.ProductType != null)
                    {
                        var dataitem = DataItemDetailBLL.Instance.GetEntity(o.ProductType);
                        if (dataitem != null)
                        {
                            o.ProductTypeName = dataitem.ItemName;
                        }
                    }
                    if (o.ProductCategoryId != null)
                    {
                        var cate = CategoryBLL.Instance.GetEntity(o.ProductCategoryId);
                        if (cate != null)
                        {
                            o.CateName = cate.Name;
                        }
                    }
                    var ruleList = ProductRuleBLL.Instance.GetList(new ProductRuleEntity() { ProductId = o.ProductId });
                    var rulename = "";
                    foreach (var item in ruleList)
                    {
                        rulename += string.Format("{0}-{1},", item.RuleName,item.Price);
                    }
                    if (rulename != "")
                    {
                        o.RuleExtName = rulename.Substring(0, rulename.Length - 1);
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
            var data = ProductBLL.Instance.GetEntity(keyValue);
            if (data != null)
            {
                var imageList = AttachmentPicBLL.Instance.GetList(new AttachmentPicEntity() { ObjectId = data.ProductId });
                if (imageList != null)
                {
                    data.AttachmentPicList = imageList.OrderBy(i => i.SortNum).ThenBy(i => i.SortNum).ToList();
                }
                var ruleList = ProductRuleBLL.Instance.GetList(new ProductRuleEntity() { ProductId = data.ProductId });
                data.RuleList = ruleList;
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
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    foreach (var key in keys)
                    {
                        ProductBLL.Instance.Delete(key);
                        ProductRuleBLL.Instance.DeleteByObjectId(key);
                    }
                }

                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ProductController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Audit(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    bool flag = true;
                    foreach (var key in keys)
                    {
                        var entity = ProductBLL.Instance.GetEntity(key);
                        if (entity != null && (entity.AuditStatus != (int)Trade.Model.Enums.AuditStatus.未审核))
                        {
                            flag = false;
                            return Error("非[未审核]状态的产品不能此操作");
                        }
                    }
                    if (flag)
                    {
                        foreach (var key in keys)
                        {
                            var entity = ProductBLL.Instance.GetEntity(key);
                            if (entity != null)
                            {
                                entity.AuditStatus = (int)Trade.Model.Enums.AuditStatus.已审核;

                                ProductBLL.Instance.Update(entity);
                            }
                        }
                    }
                }

                return Success("操作成功");

            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ProductController>>Audit";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }
        [HttpPost]
        public ActionResult Online(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    bool flag = true;
                    foreach (var key in keys)
                    {
                        var entity = ProductBLL.Instance.GetEntity(key);
                        if (entity != null && (entity.ProductStatus != (int)Trade.Model.Enums.ProductStatus.下架))
                        {
                            flag = false;
                            return Error("非[下架]状态的产品不能此操作");
                        }
                    }
                    if (flag)
                    {
                        foreach (var key in keys)
                        {
                            var entity = ProductBLL.Instance.GetEntity(key);
                            if (entity != null)
                            {
                                entity.ProductStatus = (int)Trade.Model.Enums.ProductStatus.上架;

                                ProductBLL.Instance.Update(entity);
                            }
                        }
                    }
                }

                return Success("操作成功");

            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ProductController>>Online";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }
        [HttpPost]
        public ActionResult UnOnline(string keyValue)
        {
            try
            {
                string[] keys = keyValue.Split(',');
                if (keys != null)
                {
                    bool flag = true;
                    foreach (var key in keys)
                    {
                        var entity = ProductBLL.Instance.GetEntity(key);
                        if (entity != null && (entity.ProductStatus != (int)Trade.Model.Enums.ProductStatus.上架))
                        {
                            flag = false;
                            return Error("非[上架]状态的产品不能此操作");
                        }
                    }
                    if (flag)
                    {
                        foreach (var key in keys)
                        {
                            var entity = ProductBLL.Instance.GetEntity(key);
                            if (entity != null)
                            {
                                entity.ProductStatus = (int)Trade.Model.Enums.ProductStatus.下架;

                                ProductBLL.Instance.Update(entity);
                            }
                        }
                    }
                }


                return Success("操作成功");

            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ProductController>>UnOnline";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string json)
        {
            try
            {
                //反序列化
                var entity = Serializer.DeserializeJson<ProductEntity>(json, true);
                if (entity != null)
                {
                    if (keyValue == "")
                    {
                        entity.ProductId = Util.Util.NewUpperGuid();
                        entity.CreateTime = DateTime.Now;
                        entity.ProductStatus = (int)Trade.Model.Enums.ProductStatus.上架;
                        entity.AuditStatus = (int)Trade.Model.Enums.AuditStatus.已审核;
                        entity.ProductDescription = entity.ProductDescription == null ? "" : entity.ProductDescription.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");

                        if (ProductBLL.Instance.Add(entity))
                        {
                            if (entity.AttachmentPicList != null)
                            {
                                int index = 0;
                                AttachmentPicBLL.Instance.DeleteByObjectId(entity.ProductId);
                                foreach (var picitem in entity.AttachmentPicList)
                                {
                                    if (picitem != null)
                                    {
                                        AttachmentPicEntity pic = new AttachmentPicEntity();
                                        pic.PicId = Util.Util.NewUpperGuid();
                                        pic.PicName = picitem.PicName;
                                        pic.SortNum = index;
                                        pic.ObjectId = entity.ProductId;
                                        pic.Type = (int)Trade.Model.Enums.AttachmentPicType.产品;
                                        AttachmentPicBLL.Instance.Add(pic);
                                    }
                                    index++;
                                }
                            }
                            if (entity.RuleList != null)
                            {
                                ProductRuleBLL.Instance.DeleteByObjectId(entity.ProductId);
                                foreach (var ruleitem in entity.RuleList)
                                {
                                    if (ruleitem != null)
                                    {
                                        ProductRuleEntity rule = new ProductRuleEntity();
                                        rule.RuleId = Util.Util.NewUpperGuid();
                                        rule.ProductId = entity.ProductId;
                                        rule.RuleName = ruleitem.RuleName;
                                        rule.Price = ruleitem.Price;
                                        ProductRuleBLL.Instance.Add(rule);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //修改
                        entity.ProductId = keyValue;
                        entity.ProductDescription = entity.ProductDescription == null ? "" : entity.ProductDescription.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<");
                        if (ProductBLL.Instance.Update(entity))
                        {

                            if (entity.AttachmentPicList != null)
                            {
                                int index = 0;
                                AttachmentPicBLL.Instance.DeleteByObjectId(entity.ProductId);
                                foreach (var picitem in entity.AttachmentPicList)
                                {

                                    if (picitem != null)
                                    {
                                        AttachmentPicEntity pic = new AttachmentPicEntity();
                                        pic.PicId = Util.Util.NewUpperGuid();
                                        pic.PicName = picitem.PicName;
                                        pic.SortNum = index;
                                        pic.ObjectId = entity.ProductId;
                                        pic.Type = (int)Trade.Model.Enums.AttachmentPicType.产品;
                                        AttachmentPicBLL.Instance.Add(pic);
                                    }
                                    index++;
                                }
                            }
                            if (entity.RuleList != null)
                            {
                                ProductRuleBLL.Instance.DeleteByObjectId(entity.ProductId);
                                foreach (var ruleitem in entity.RuleList)
                                {
                                    if (ruleitem != null)
                                    {
                                        ProductRuleEntity rule = new ProductRuleEntity();
                                        rule.RuleId = Util.Util.NewUpperGuid();
                                        rule.ProductId = entity.ProductId;
                                        rule.RuleName = ruleitem.RuleName;
                                        rule.Price = ruleitem.Price;
                                        ProductRuleBLL.Instance.Add(rule);
                                    }
                                }
                            }

                        }
                    }


                }
                return Success("保存成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "ProductController>>Save";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
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
            var para = new ProductEntity();
            try
            {
                //这里要url解码
                var queryParam = Server.UrlDecode(queryJson).ToJObject();

                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    var condition = queryParam["condition"].ToString().ToLower();
                    switch (condition)
                    {
                        case "productname":
                            para.ProductName = queryParam["keyword"].ToString();
                            break;
                        case "productno":
                            para.ProductNO = queryParam["keyword"].ToString();
                            break;
                    }
                }

                if (!queryParam["type"].IsEmpty())
                {
                    para.ProductType = queryParam["type"].ToString();
                }
                if (!queryParam["cateid"].IsEmpty())
                {
                    para.ProductCategoryId = queryParam["cateid"].ToString();
                }

                //数据对象
                var list = ProductBLL.Instance.GetList(para);
                if (list != null)
                {
                    foreach (var o in list)
                    {

                        if (o.ProductType != null)
                        {
                            var dataitem = DataItemDetailBLL.Instance.GetEntity(o.ProductType);
                            if (dataitem != null)
                            {
                                o.ProductTypeName = dataitem.ItemName;
                            }
                        }
                        if (o.ProductCategoryId != null)
                        {
                            var cate = CategoryBLL.Instance.GetEntity(o.ProductCategoryId);
                            if (cate != null)
                            {
                                o.CateName = cate.Name;
                            }
                        }
                        //if (o.ProductStatus != null)
                        //{
                        //    o.ProductStatusName = ((Trade.Model.Enums.ProductStatus)o.ProductStatus).ToString();
                        //}
                        //if (o.AuditStatus != null)
                        //{
                        //    o.AuditStatusName = ((Trade.Model.Enums.AuditStatus)o.AuditStatus).ToString();
                        //}
                        var ruleList = ProductRuleBLL.Instance.GetList(new ProductRuleEntity() { ProductId = o.ProductId });
                        var rulename = "";
                        foreach (var item in ruleList)
                        {
                            rulename += string.Format("{0}-{1},", item.RuleName, item.Price);
                        }
                        if (rulename != "")
                        {
                            o.RuleExtName = rulename.Substring(0, rulename.Length - 1);
                        }
                    }


                    //设置导出格式
                    ExcelConfig excelconfig = new ExcelConfig();
                    excelconfig.Title = "产品信息";
                    excelconfig.TitleFont = "微软雅黑";
                    excelconfig.TitlePoint = 10;
                    excelconfig.FileName = "产品信息.xls";
                    excelconfig.IsAllSizeColumn = true;
                    //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                    List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                    excelconfig.ColumnEntity = listColumnEntity;
                    ColumnEntity columnentity = new ColumnEntity();
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductNO", ExcelColumn = "编号", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductName", ExcelColumn = "名称", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductTypeName", ExcelColumn = "属性", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CateName", ExcelColumn = "分类", Width = 50 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductPrice", ExcelColumn = "价格", Width = 50 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductCostPrice", ExcelColumn = "活动价格", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductUnit", ExcelColumn = "单位", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "RuleExtName", ExcelColumn = "规格", Width = 20 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductStock", ExcelColumn = "库存", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CreateTime", ExcelColumn = "上传时间", Width = 20 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "ProductStatusName", ExcelColumn = "产品状态", Width = 20 });
                    //excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "AuditStatusName", ExcelColumn = "审核状态", Width = 20 });
                    //需合并索引
                    //excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                    //调用导出方法
                    ExcelHelper<ProductEntity>.ExcelDownload(list, excelconfig);
                    HttpRuntime.Cache[cacheKey + "-state"] = "done";
                }
            }
            catch (Exception ex)
            {
                HttpRuntime.Cache[cacheKey + "-state"] = "error";
            }
        }

        private string GenerateUniqueNumText(int num)
        {
            string randomResult = string.Empty;
            string readyStr = "01234567899876543210";
            char[] rtn = new char[num];
            string gid = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            var ba = gid.ToArray();
            for (var i = 0; i < num; i++)
            {
                rtn[i] = readyStr[((ba[i] + ba[num + i]) % 18)];
            }

            foreach (char r in rtn)
            {
                randomResult += r;
            }

            return randomResult;
        }
    }
}

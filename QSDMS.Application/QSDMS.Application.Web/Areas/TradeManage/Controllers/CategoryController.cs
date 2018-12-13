using QSDMS.Application.Web.Controllers;
using QSDMS.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trade.Business;
using QSDMS.Util;
using QSDMS.Util.Extension;
using iFramework.Framework;
using Trade.Model;
using Trade.Business.Cache;
using QSDMS.Cache.Factory;
namespace QSDMS.Application.Web.Areas.TradeManage.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryCache categoryCache = new CategoryCache();
        //
        // GET: /TradeManage/Category/

        public ActionResult List()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 树形下拉框数据
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult GetCategoryTreeJson()
        {
            var watch = CommonHelper.TimerStart();

            var categorydata = categoryCache.GetList(null).ToList(); //CategoryBLL.Instance.GetList(null).ToList();
            var treeList = new List<TreeEntity>();
            foreach (CategoryEntity item in categorydata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = categorydata.Count(t => t.ParentID == item.CategoryID) == 0 ? false : true;
                tree.id = item.CategoryID.ToString();
                tree.text = item.Name;
                tree.value = item.ParentID == null ? "0" : item.ParentID.ToString();
                if (item.ParentID == "0")
                {
                    tree.parentId = "0";
                }
                else
                {
                    tree.parentId = item.ParentID == null ? "0" : item.ParentID.ToString();
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Category";
                treeList.Add(tree);

            }
            var costtime = CommonHelper.TimerEnd(watch);
            Logger.Debug("获取分类请求时间：" + costtime);

            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// gridtree 列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string keyword)
        {
            try
            {
                var categorydata = categoryCache.GetList(null).ToList(); //CategoryBLL.Instance.GetList(null).ToList();

                //if (!string.IsNullOrWhiteSpace(keyword))
                //{
                //    categorydata.TreeWhere(t => t.Name.Contains(keyword), "CategoryID", "ParentID");
                //}

                var treeList = new List<TreeGridEntity>();
                if (categorydata != null && categorydata.Count > 0)
                {
                    foreach (CategoryEntity item in categorydata)
                    {
                        TreeGridEntity tree = new TreeGridEntity();
                        bool hasChildren = categorydata.Count(t => t.ParentID == item.CategoryID) == 0 ? false : true;
                        tree.id = item.CategoryID.ToString();
                        tree.parentId = item.ParentID == null ? "0" : item.ParentID.ToString();
                        if (item.ParentID == "0")
                        {
                            tree.parentId = "0";
                        }
                        else
                        {
                            tree.parentId = item.ParentID == null ? "0" : item.ParentID.ToString();
                        }
                        tree.expanded = true;
                        tree.hasChildren = hasChildren;
                        string itemJson = item.ToJson();
                        itemJson = itemJson.Insert(1, "\"Sort\":\"Category\",");
                        tree.entityJson = itemJson;
                        treeList.Add(tree);
                    }
                }
                return Content(treeList.TreeJson());
            }
            catch (Exception ex)
            {
                return Content("{ \"rows\": []}");
            }

        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = CategoryBLL.Instance.GetEntity(keyValue);
            return Content(data.ToJson());
        }
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
            CategoryBLL.Instance.Delete(keyValue);
            CacheFactory.Cache().RemoveCache(CategoryBLL.Instance.cacheKey);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, CategoryEntity entity)
        {
            try
            {
                if (keyValue != "")
                {
                    entity.CategoryID = keyValue;
                    CategoryBLL.Instance.Update(entity);
                }
                else
                {
                    entity.Code = "";
                    entity.CategoryID = Util.Util.NewUpperGuid();
                    CategoryBLL.Instance.Add(entity);
                }

                CacheFactory.Cache().RemoveCache(CategoryBLL.Instance.cacheKey);
                return Success("操作成功。");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "AddressController>>SaveForm";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
            }
        }

    }
}

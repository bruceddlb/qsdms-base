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
    public class MemberController : BaseController
    {
        //
        // GET: /TradeManage/Member/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Form()
        {
            ViewBag.AccountId = Request.Params["keyValue"];
            return View();
        }
        public ActionResult UpdatePwd()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();

            var queryParam = queryJson.ToJObject();
            var para = new MemberEntity();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                var condition = queryParam["condition"].ToString().ToLower();
                switch (condition)
                {
                    case "membername":
                        para.MemberName = queryParam["keyword"].ToString();
                        break;
                    case "mobile":
                        para.Mobile = queryParam["keyword"].ToString();
                        break;
                }
            }

            //等级
            if (!queryParam["lev"].IsEmpty())
            {
                string lev = queryParam["lev"].ToString();
                para.LevId = lev;
            }

            //数据对象
            var pageList = MemberBLL.Instance.GetPageList(para, ref pagination);
            if (pageList != null)
            {
                foreach (var o in pageList)
                {
                    if (o.LevId != null)
                    {
                        var lev = DataItemDetailBLL.Instance.GetEntity(o.LevId);
                        if (lev != null)
                        {
                            o.LevName = lev.ItemName;
                        }
                    }
                    if (o.ProvinceId != null)
                    {
                        o.ProvinceName = AreaBLL.Instance.GetEntity(o.ProvinceId).AreaName;
                    }
                    if (o.CityId != null)
                    {
                        o.CityName = AreaBLL.Instance.GetEntity(o.CityId).AreaName;
                    }
                    if (o.CountyId != null)
                    {
                        o.CountyName = AreaBLL.Instance.GetEntity(o.CountyId).AreaName;
                    }
                    o.AddressInfo = o.ProvinceName + o.CityName + o.CountyName + o.AddressInfo;

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
            var data = MemberBLL.Instance.GetEntity(keyValue);
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
                var member = MemberBLL.Instance.GetEntity(keyValue);
                member.MemberId = keyValue;
                member.Status = (int)Trade.Model.Enums.UseStatus.删除;
                MemberBLL.Instance.Update(member);
                return Success("删除成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "MemberController>>RemoveForm";
                new ExceptionHelper().LogException(ex);
                return Error("删除失败");
            }

        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult LockAccount(string keyValue)
        {
            try
            {
                var member = MemberBLL.Instance.GetEntity(keyValue);
                member.MemberId = keyValue;
                member.Status = (int)Trade.Model.Enums.UseStatus.锁定;
                MemberBLL.Instance.Update(member);
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "MemberController>>LockAccount";
                new ExceptionHelper().LogException(ex);
                return Error("锁定失败");
            }

        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnLockAccount(string keyValue)
        {
            try
            {
                var member = MemberBLL.Instance.GetEntity(keyValue);
                member.MemberId = keyValue;
                member.Status = (int)Trade.Model.Enums.UseStatus.正常;
                MemberBLL.Instance.Update(member);
                return Success("操作成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "MemberController>>UnLockAccount";
                new ExceptionHelper().LogException(ex);
                return Error("操作失败");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, MemberEntity entity)
        {
            try
            {

                if (keyValue != "")
                {
                    entity.MemberId = keyValue;
                    MemberBLL.Instance.Update(entity);

                }
                else
                {
                    entity.MemberId = Util.Util.NewUpperGuid();
                    entity.CreateTime = DateTime.Now;
                    entity.Status = (int)Trade.Model.Enums.UseStatus.正常;
                    entity.Pwd = DESEncrypt.Encrypt("123456");

                    MemberBLL.Instance.Add(entity);
                }
                return Success("保存成功");
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "MemberController>>Save";
                new ExceptionHelper().LogException(ex);
                return Error("保存失败");
            }

        }

        [HttpPost]
        public JsonResult UpdatePwd(string keyValue, string oldUserPwd, string newUserPwd)
        {
            var result = new ReturnMessage(false) { Message = "密码修改失败!" };
            try
            {
                var member = MemberBLL.Instance.GetEntity(keyValue);
                if (member != null)
                {
                    if (member.Pwd != DESEncrypt.Encrypt(oldUserPwd))
                    {
                        result.Message = "请输入正确的密码!";
                        return Json(result);
                    }
                    member.Pwd = DESEncrypt.Encrypt(newUserPwd);
                    MemberBLL.Instance.Update(member);
                    result.IsSuccess = true;
                    result.Message = "密码修改成功!";
                }
            }
            catch (Exception ex)
            {
                ex.Data["Method"] = "MemberController>>UpdatePwd";
                new ExceptionHelper().LogException(ex);
            }
            return Json(result);
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

                var para = new MemberEntity();


                //等级
                if (!queryParam["lev"].IsEmpty())
                {
                    string lev = queryParam["lev"].ToString();
                    para.LevId = lev;
                }
                //类型
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    var condition = queryParam["condition"].ToString().ToLower();
                    switch (condition)
                    {
                        case "membername":
                            para.MemberName = queryParam["keyword"].ToString();
                            break;
                        case "mobile":
                            para.Mobile = queryParam["keyword"].ToString();
                            break;
                    }
                }

                var list = MemberBLL.Instance.GetList(para);
                if (list != null)
                {
                    list.ForEach((o) =>
                    {
                        if (o.ProvinceId != null)
                        {
                            o.ProvinceName = AreaBLL.Instance.GetEntity(o.ProvinceId).AreaName;
                        }
                        if (o.CityId != null)
                        {
                            o.CityName = AreaBLL.Instance.GetEntity(o.CityId).AreaName;
                        }
                        if (o.CountyId != null)
                        {
                            o.CountyName = AreaBLL.Instance.GetEntity(o.CountyId).AreaName;
                        }
                        o.AddressInfo = o.ProvinceName + o.CityName + o.CountyName + o.AddressInfo;

                        if (o.Status != null)
                        {
                            o.StatusName = ((Trade.Model.Enums.UseStatus)o.Status).ToString();
                        }
                        if (o.Sex != null)
                        {
                            o.SexName = ((Trade.Model.Enums.SexState)o.Sex).ToString();
                        }
                    });

                    //设置导出格式
                    ExcelConfig excelconfig = new ExcelConfig();
                    excelconfig.Title = "会员账号信息";
                    excelconfig.TitleFont = "微软雅黑";
                    excelconfig.TitlePoint = 10;
                    excelconfig.FileName = "会员信息.xls";
                    excelconfig.IsAllSizeColumn = true;
                    //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                    List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                    excelconfig.ColumnEntity = listColumnEntity;
                    ColumnEntity columnentity = new ColumnEntity();
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "MemberNo", ExcelColumn = "会员编号", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Mobile", ExcelColumn = "会员账号", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "MemberName", ExcelColumn = "会员名称", Width = 15 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "LevName", ExcelColumn = "会员等级", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "NikeName", ExcelColumn = "会员昵称", Width = 50 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "SexName", ExcelColumn = "性別", Width = 50 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CreateTime", ExcelColumn = "注册时间", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BornDate", ExcelColumn = "生日", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Money", ExcelColumn = "账户余额", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Point", ExcelColumn = "账户积分", Width = 20 });
                    excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "AddressInfo", ExcelColumn = "地址", Width = 20 });
                    //需合并索引
                    //excelconfig.MergeRangeIndexArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                    //调用导出方法
                    ExcelHelper<MemberEntity>.ExcelDownload(list, excelconfig);
                    HttpRuntime.Cache[cacheKey + "-state"] = "done";
                }

            }
            catch (Exception ex)
            {
                HttpRuntime.Cache[cacheKey + "-state"] = "error";
            }
        }
    }

}

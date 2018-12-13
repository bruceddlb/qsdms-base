using Trade.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSDMS.Util.Extension;
using QSDMS.Util;
using QSDMS.Util.Attributes;

namespace QSDMS.Application.Web.Controllers
{
    public class DataItemEnumsController : Controller
    {

        /// <summary>
        /// 支付订单状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPayStatus()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.OrderStatus));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.OrderStatus));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }


        [HttpGet]
        public ActionResult GetYesOrNo()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.YesOrNo));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.YesOrNo));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSex()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.SexState));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.SexState));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }
        /// <summary>
        /// 使用状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUseStatus()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.UseStatus));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.UseStatus));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderStatus()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.OrderStatus));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.OrderStatus));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 发货单状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeliverOrderSatus()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.DeliverOrderSatus));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.DeliverOrderSatus));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }

        /// <summary>
        /// 到货状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArrivalStatus()
        {
            string[] names = System.Enum.GetNames(typeof(Trade.Model.Enums.ArrivalStatus));
            int[] values = (int[])System.Enum.GetValues(typeof(Trade.Model.Enums.ArrivalStatus));
            List<KeyValueEntity> list = new List<KeyValueEntity>();
            for (int i = 0; i < names.Length; i++)
            {
                list.Add(new KeyValueEntity() { ItemId = values[i].ToString(), ItemName = names[i] });
            }
            return Content(list.ToJson());
        }
    }
}

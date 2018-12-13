using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model
{
    /// <summary>
    /// 系统枚举类型值定义
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// 销售订单状态
        /// </summary>
        public enum OrderStatus
        {
            待支付 = 0,
            已支付 = 1,
            待发货 = 2,
            部分发货 = 3,
            完成 = 4,
            取消 = 101
        }

        /// <summary>
        /// 订单明细状态
        /// </summary>
        public enum OrderDetailStatus
        {
            未到货 = 0,
            已到货 = 1,
            已发货 = 2,
            已收货 = 3
        }

        /// <summary>
        /// 发货单状态
        /// </summary>
        public enum DeliverOrderSatus
        {
            待支付 = 0,
            已支付 = 1,
            待发货 = 2,
            已发货 = 3,
            取消 = 101
        }

        public enum YesOrNo
        {
            是 = 1,
            否 = 0
        }

        /// <summary>
        /// 计费类型
        /// </summary>
        public enum BillingType
        {
            按件数 = 0,
            按重量 = 1
        }

        /// <summary>
        /// 图片附件业务类型
        /// </summary>
        public enum AttachmentPicType
        {
            分类 = 1,
            产品 = 2,

        }

        /// <summary>
        /// 产品状态
        /// </summary>
        public enum ProductStatus
        {
            删除 = 2,
            上架 = 1,
            下架 = 0
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public enum AuditStatus
        {
            未审核 = 0,
            已审核 = 1
        }

        /// <summary>
        /// 到货状态
        /// </summary>
        public enum ArrivalStatus
        {
            已到货 = 1,
            未到货 = 0
        }

        public enum PlanStatus
        {
            已生效 = 1,
            已失效 = 0
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public enum PayType
        {
            微信支付 = 0,
            支付宝支付 = 1,
            花呗支付 = 2,
            线下支付 = 3
        }


        /// <summary>
        /// 距离
        /// </summary>
        public enum DistanceRange
        {

            [Description("一千米以内")]
            一千米内 = 1,
            [Description("三千米以内")]
            一千米至两千米 = 2,
            [Description("五千米以内")]
            二千米至五千米 = 3,
            [Description("五千米以上")]
            五千米以上 = 4,
        }

        /// <summary>
        /// 价格区间
        /// </summary>
        public enum PriceRange
        {
            [Description("3000以内")]
            三千以内 = 1,
            [Description("3000-4000")]
            三千到四千 = 2,
            [Description("4000-5000")]
            四千到五千 = 3,
            [Description("5000-6000")]
            五千到六千 = 4,
            [Description("6000以上")]
            六千以上 = 5,
        }

        /// <summary>
        /// 积分操作类型
        /// </summary>
        public enum PointOperateType
        {
            增加 = 1,
            减少 = 2
        }

        /// <summary>
        /// 财务类型
        /// </summary>
        public enum FinaceOperateType
        {
            增加 = 1,
            减少 = 2
        }

        /// <summary>
        /// 使用状态
        /// </summary>
        public enum UseStatus
        {
            删除 = 1,
            锁定 = 2,
            正常 = 3
        }

        /// <summary>
        ///性別
        /// </summary>
        public enum SexState
        {
            男 = 0,
            女 = 1
        }

    }
}

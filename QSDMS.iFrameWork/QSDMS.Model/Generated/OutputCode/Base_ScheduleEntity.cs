//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-06-11 15:18:04 by 群升科技
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
    
using System;
namespace QSDMS.Model 
{
    /// <summary>
    /// 数据表实体类：Base_ScheduleEntity 
    /// </summary>
    [Serializable()]
    public class Base_ScheduleEntity
    {    
		            
		/// <summary>
		/// varchar:日程主键
		/// </summary>	
                 
		public string ScheduleId { get; set; }

                    
		/// <summary>
		/// varchar:日程名称
		/// </summary>	
                 
		public string ScheduleName { get; set; }

                    
		/// <summary>
		/// varchar:日程内容
		/// </summary>	
                 
		public string ScheduleContent { get; set; }

                    
		/// <summary>
		/// varchar:类别
		/// </summary>	
                 
		public string Category { get; set; }

                    
		/// <summary>
		/// datetime:开始日期
		/// </summary>	
                 
		public DateTime? StartDate { get; set; }

                    
		/// <summary>
		/// varchar:开始时间
		/// </summary>	
                 
		public string StartTime { get; set; }

                    
		/// <summary>
		/// datetime:结束日期
		/// </summary>	
                 
		public DateTime? EndDate { get; set; }

                    
		/// <summary>
		/// varchar:结束时间
		/// </summary>	
                 
		public string EndTime { get; set; }

                    
		/// <summary>
		/// int:提前提醒
		/// </summary>	
                 
		public int? Early { get; set; }

                    
		/// <summary>
		/// int:邮件提醒
		/// </summary>	
                 
		public int? IsMailAlert { get; set; }

                    
		/// <summary>
		/// int:手机提醒
		/// </summary>	
                 
		public int? IsMobileAlert { get; set; }

                    
		/// <summary>
		/// int:微信提醒
		/// </summary>	
                 
		public int? IsWeChatAlert { get; set; }

                    
		/// <summary>
		/// int:日程状态
		/// </summary>	
                 
		public int? ScheduleState { get; set; }

                    
		/// <summary>
		/// int:排序码
		/// </summary>	
                 
		public int? SortCode { get; set; }

                    
		/// <summary>
		/// int:删除标记
		/// </summary>	
                 
		public int? DeleteMark { get; set; }

                    
		/// <summary>
		/// int:有效标志
		/// </summary>	
                 
		public int? EnabledMark { get; set; }

                    
		/// <summary>
		/// varchar:备注
		/// </summary>	
                 
		public string Description { get; set; }

                    
		/// <summary>
		/// datetime:创建日期
		/// </summary>	
                 
		public DateTime? CreateDate { get; set; }

                    
		/// <summary>
		/// varchar:创建用户主键
		/// </summary>	
                 
		public string CreateUserId { get; set; }

                    
		/// <summary>
		/// varchar:创建用户
		/// </summary>	
                 
		public string CreateUserName { get; set; }

                    
		/// <summary>
		/// datetime:修改日期
		/// </summary>	
                 
		public DateTime? ModifyDate { get; set; }

                    
		/// <summary>
		/// varchar:修改用户主键
		/// </summary>	
                 
		public string ModifyUserId { get; set; }

                    
		/// <summary>
		/// varchar:修改用户
		/// </summary>	
                 
		public string ModifyUserName { get; set; }

           
    }    
}
	
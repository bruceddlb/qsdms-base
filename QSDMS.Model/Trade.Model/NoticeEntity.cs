//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2018-12-06 10:09:18 by bruced
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
    
using System;
namespace Trade.Model
{
    /// <summary>
    /// 数据表实体类：NoticeEntity 
    /// </summary>
    [Serializable()]
    public partial class NoticeEntity:BaseModel
    {  
		            
		/// <summary>
		/// NoticeId:string
		/// </summary>	
		public string NoticeId { get; set; }
                    
		/// <summary>
		/// NoticeTitle:string
		/// </summary>	
		public string NoticeTitle { get; set; }
                    
		/// <summary>
		/// CreateTime:DateTime
		/// </summary>	
		public DateTime? CreateTime { get; set; }
                    
		/// <summary>
		/// NoticeContent:string
		/// </summary>	
		public string NoticeContent { get; set; }
           
    }    
}
	
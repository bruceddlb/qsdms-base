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
    /// 数据表实体类：MyPointLogEntity 
    /// </summary>
    [Serializable()]
    public partial class MyPointLogEntity:BaseModel
    {  
		            
		/// <summary>
		/// MyPointLogId:string
		/// </summary>	
		public string MyPointLogId { get; set; }
                    
		/// <summary>
		/// Descn:string
		/// </summary>	
		public string Descn { get; set; }
                    
		/// <summary>
		/// BillCode:string
		/// </summary>	
		public string BillCode { get; set; }
                    
		/// <summary>
		/// CreateTime:DateTime
		/// </summary>	
		public DateTime? CreateTime { get; set; }
                    
		/// <summary>
		/// Pt:decimal
		/// </summary>	
		public decimal? Pt { get; set; }
                    
		/// <summary>
		/// MemberId:string
		/// </summary>	
		public string MemberId { get; set; }
           
    }    
}
	
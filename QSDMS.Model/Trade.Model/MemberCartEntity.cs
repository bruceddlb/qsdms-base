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
    /// 数据表实体类：MemberCartEntity 
    /// </summary>
    [Serializable()]
    public partial class MemberCartEntity:BaseModel
    {  
		            
		/// <summary>
		/// CartId:string
		/// </summary>	
		public string CartId { get; set; }
                    
		/// <summary>
		/// ProductId:string
		/// </summary>	
		public string ProductId { get; set; }
                    
		/// <summary>
		/// ProductCount:int
		/// </summary>	
		public int? ProductCount { get; set; }
                    
		/// <summary>
		/// MemberId:string
		/// </summary>	
		public string MemberId { get; set; }
                    
		/// <summary>
		/// ProductPrice:decimal
		/// </summary>	
		public decimal? ProductPrice { get; set; }
                    
		/// <summary>
		/// FaceImag:string
		/// </summary>	
		public string FaceImag { get; set; }
                    
		/// <summary>
		/// ProductType:string
		/// </summary>	
		public string ProductType { get; set; }
                    
		/// <summary>
		/// ProductName:string
		/// </summary>	
		public string ProductName { get; set; }
                    
		/// <summary>
		/// RuleName:string
		/// </summary>	
		public string RuleName { get; set; }
                    
		/// <summary>
		/// RuleId:string
		/// </summary>	
		public string RuleId { get; set; }
                    
		/// <summary>
		/// ServiceRuleId:string
		/// </summary>	
		public string ServiceRuleId { get; set; }
                    
		/// <summary>
		/// ServicePrice:decimal
		/// </summary>	
		public decimal? ServicePrice { get; set; }
                    
		/// <summary>
		/// FreeServiceCount:decimal
		/// </summary>	
		public decimal? FreeServiceCount { get; set; }
                    
		/// <summary>
		/// ServiceTotal:decimal
		/// </summary>	
		public decimal? ServiceTotal { get; set; }
           
    }    
}
	
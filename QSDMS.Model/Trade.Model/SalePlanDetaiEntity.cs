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
    /// 数据表实体类：SalePlanDetaiEntity 
    /// </summary>
    [Serializable()]
    public partial class SalePlanDetaiEntity:BaseModel
    {  
		            
		/// <summary>
		/// SalePlanDetaiId:string
		/// </summary>	
		public string SalePlanDetaiId { get; set; }
                    
		/// <summary>
		/// SalePlanId:string
		/// </summary>	
		public string SalePlanId { get; set; }
                    
		/// <summary>
		/// ProductId:string
		/// </summary>	
		public string ProductId { get; set; }
                    
		/// <summary>
		/// ProductNo:string
		/// </summary>	
		public string ProductNo { get; set; }
                    
		/// <summary>
		/// ProductName:string
		/// </summary>	
		public string ProductName { get; set; }
                    
		/// <summary>
		/// Price:decimal
		/// </summary>	
		public decimal? Price { get; set; }
                    
		/// <summary>
		/// Status:int
		/// </summary>	
		public int? Status { get; set; }
           
    }    
}
	
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
    /// 数据表实体类：Base_AuthorizeDataEntity 
    /// </summary>
    [Serializable()]
    public class Base_AuthorizeDataEntity
    {    
		            
		/// <summary>
		/// varchar:授权数据主键
		/// </summary>	
                 
		public string AuthorizeDataId { get; set; }

                    
		/// <summary>
		/// int:1-仅限本人2-仅限本人及下属3-所在部门4-所在公司5-按明细设置
		/// </summary>	
                 
		public int? AuthorizeType { get; set; }

                    
		/// <summary>
		/// int:对象分类:1-部门2-角色3-岗位4-职位5-工作组
		/// </summary>	
                 
		public int? Category { get; set; }

                    
		/// <summary>
		/// varchar:对象主键
		/// </summary>	
                 
		public string ObjectId { get; set; }

                    
		/// <summary>
		/// varchar:项目Id
		/// </summary>	
                 
		public string ItemId { get; set; }

                    
		/// <summary>
		/// varchar:项目Id
		/// </summary>	
                 
		public string ItemName { get; set; }

                    
		/// <summary>
		/// varchar:项目名称
		/// </summary>	
                 
		public string ResourceId { get; set; }

                    
		/// <summary>
		/// int:只读
		/// </summary>	
                 
		public int? IsRead { get; set; }

                    
		/// <summary>
		/// varchar:约束表达式
		/// </summary>	
                 
		public string AuthorizeConstraint { get; set; }

                    
		/// <summary>
		/// int:排序码
		/// </summary>	
                 
		public int? SortCode { get; set; }

                    
		/// <summary>
		/// datetime:创建时间
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

           
    }    
}
	
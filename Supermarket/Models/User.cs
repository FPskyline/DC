using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 用户表
  /// </summary>
  public class User
  {

   /// <summary>
   /// 用户ID
   /// </summary>
   [Key]
        public long UserId { get; set; }
        /// <summary>
        /// 第三方唯一标识Id
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }
    /// <summary>
    /// 登录ip
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpDate { get; set; }
    /// <summary>
    /// 备用1
    /// </summary>
    public string Comment1 { get; set; }
    /// <summary>
    /// 备用2
    /// </summary>
    public string Comment2 { get; set; }
    /// <summary>
    /// 备用3
    /// </summary>
    public string Comment3 { get; set; }
    /// <summary>
    /// 地址管理表
    /// </summary>
    public virtual ICollection<Address> Addresss { get; set; }
    /// <summary>
    /// 订单表
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; }
    /// <summary>
    /// FormId表
    /// </summary>
    public virtual ICollection<FormId> FormIds { get; set; }
    }
}

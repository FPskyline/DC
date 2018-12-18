using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 地址管理表
  /// </summary>
  public class Address
  {
    /// <summary>
    /// 地址id
    /// </summary>
    [Key]
    public long AddressId { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
    /// <summary>
    /// 联系人
    /// </summary>
    public string ContactName { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string ContactPhone { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpDate { get; set; }
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
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
        /// 外键关联
        /// </summary>
        [ForeignKey("UserId")]
    public User Users { get; set; }
  }
}

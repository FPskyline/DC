using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 支付方式
  /// </summary>
  public enum PayType
  {
    在线支付 = 1,
    货到付款 = 2,
  }
  /// <summary>
  /// 交易状态
  /// </summary> 
  public enum PayState
  {
    未付款 = 1,
    已付款 = 2,
    申请退款 = 3,
    已退款 = 4,
    已发货 = 5,
    交易完成 = 6,
    已接单 =7,
    已拒绝 = 8
    }
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order
  {
    /// <summary>
    /// 订单id
    /// </summary>
    public long OrderId { get; set; }
    /// <summary>
    /// 订单编号
    /// </summary>
    public string OrderNo { get; set; }
    /// <summary>
    /// 订单名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 总额
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// 注释
    /// </summary> 
    public string Comments { get; set; }
    /// <summary>
    /// 支付方式
    /// </summary>
    public PayType PayType { get; set; }
    /// <summary>
    /// 交易状态
    /// </summary>
    public PayState PayState { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
        /// <summary>
        /// 地址ID
        /// </summary>
        public long AddressId { set; get; }
        /// <summary>
        /// 外键关联
        /// </summary>
        [ForeignKey("AddressId")]
        public Address Addresss { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { set; get; }
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
        /// 拒绝原因
        /// </summary>
        public string RefuseReason { get; set; }
        /// <summary>
        /// 外键关联
        /// </summary>
        [ForeignKey("UserId")]
    public User Users { get; set; }
    /// <summary>
    /// 系统用户Id
    /// </summary>
    public long SysUserId { set; get; }
    /// <summary>
    /// 外键关联
    /// </summary>
    [ForeignKey("SysUserId")]
    public SysUser SysUsers { get; set; }
    /// <summary>
    /// 多对多订单商品表
    /// </summary>
    public virtual ICollection<OrderGoods> OrderGoodss { get; set; }
  }
}

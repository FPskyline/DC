using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 订单、商品多对多表
  /// </summary>
    public class OrderGoods
    {
    /// <summary>
    /// 订单id
    /// </summary>
    [Key, Column(Order = 1)]
    public long OrderId { get; set; }
    /// <summary>
    /// 数量
    /// </summary>
    public int Num { get; set; }
    /// <summary>
    /// 备用2
    /// </summary>
    public string Comment2 { get; set; }
    /// <summary>
    /// 备用3
    /// </summary>
    public string Comment3 { get; set; }
        /// <summary>
        /// 订单表
        /// </summary>
        public Order Order { get; set; }
    /// <summary>
    /// 商品id
    /// </summary>
    [Key, Column(Order = 2)]
    public long GoodsId { get; set; }
    /// <summary>
    /// 商品表
    /// </summary>
    public Goods Goods { get; set; }
  }
}

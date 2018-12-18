using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 商品表
  /// </summary>
  public class Goods
  {
    /// <summary>
    /// 商品id
    /// </summary>
    [Key]
    public long GoodsId { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Notice { get; set; }
    /// <summary>
    /// 现价
    /// </summary>
    public double NewPrice { get; set; }
    /// <summary>
    /// 原价
    /// </summary>
    public int OldPrice { get; set; }
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
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpDate { get; set; }
    /// <summary>
    /// 商品种类Id
    /// </summary>
    public long GoodsKindId { set; get; }
    /// <summary>
    /// 外键关联
    /// </summary>
    [ForeignKey("GoodsKindId")]
    public GoodsKind GoodsKinds { get; set; }
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

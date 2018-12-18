using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 商品种类表
  /// </summary>
  public class GoodsKind
  {
    /// <summary>
    /// 商品种类id
    /// </summary>
    [Key]
    public long GoodsKindId { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 排序值
    /// </summary>
    public string Index { get; set; }
    /// <summary>
    /// 图片
    /// </summary>
    public string Img { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpDate { get; set; }
    /// <summary>
    /// 系统用户Id
    /// </summary>
    public long SysUserId { set; get; }
    /// <summary>
    ///  商品大类id
    /// </summary>
    public long GoodsBigKindId { get; set; }
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
    [ForeignKey("SysUserId")]
    public SysUser SysUsers { get; set; }
    /// <summary>
    /// 外键关联
    /// </summary>
    [ForeignKey("GoodsBigKindId")]
    public GoodsBigKind GoodsBigKinds { get; set; }
    /// <summary>
    /// 商品表
    /// </summary>
    public virtual ICollection<Goods> Goodss { get; set; }
  }
}

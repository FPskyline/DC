using Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 商品种类视图模型
  /// </summary>
  public class GoodsKindDto
  {

    /// <summary>
    /// 商品种类id
    /// </summary>
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
    /// 系统用户名称
    /// </summary>
    public string SysUserName { set; get; }
    /// <summary>
    ///  商品大类id
    /// </summary>
    public long GoodsBigKindId { get; set; }
  }
}

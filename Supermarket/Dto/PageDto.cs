using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 分页器视图模型
  /// </summary>
  public class GetPageDto<T>
  {
    /// <summary>
    /// 大字段列表
    /// </summary>
    public T BigField { get; set; }
    /// <summary>
    /// 列表总数
    /// </summary>
    public int TotalCount { get; set; }
  }
  /// <summary>
  /// 分页获取视图模型
  /// </summary>
  public class PageDto
  {
    /// <summary>
    /// 页数
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// 当前条数
    /// </summary>
    public int Number { get; set; }
    /// <summary>
    /// 传入系统用户ID
    /// </summary>
   public long Cid { get; set; }
   /// <summary>
   /// 子类ID
   /// </summary>
   public long GoodsKindId { get; set; }
    /// <summary>
    /// 大类ID
    /// </summary>
    public long GoodsBigKindId { get; set; }
    /// <summary>
    /// 搜索名称
    /// </summary>
    public string SearchName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 奖品表
  /// </summary>
  public class Lottery
  {
    /// <summary>
    /// 奖品id
    /// </summary>
    [Key]
    public long Id { get; set; }
    /// <summary>
    /// 奖品名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 中奖概率
    /// </summary>
    public int Probability { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime EditDate { get; set; }
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
    /// 所属商家id
    /// </summary>
    public long SysUserId { set; get; }

  }
}

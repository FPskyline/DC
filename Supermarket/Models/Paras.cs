using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  /// <summary>
  /// 参数表
  /// </summary>
  public class Paras
  {

   /// <summary>
   /// 参数ID
   /// </summary>
   [Key]
        public long Id { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Describe { get; set; }

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

  }
}

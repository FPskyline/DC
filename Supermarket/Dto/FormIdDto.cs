using Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// FormId 视图模型
  /// </summary>
  public class FormIdDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 传入ID
        /// </summary>
        public string FormIds { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
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
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

    }
}

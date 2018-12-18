using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 用户数据传输对象
  /// </summary>
  public class ParasDto
  {

        /// <summary>
        /// 参数ID
        /// </summary>
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

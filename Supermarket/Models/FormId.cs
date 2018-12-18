using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    /// <summary>
    /// 用户FormId
    /// </summary>
    public class FormId
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 用户名称
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
        /// 外键关联
        /// </summary>
        [ForeignKey("UserId")]
        public User Users { get; set; }
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

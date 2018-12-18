using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    /// <summary>
    /// 手机购物车
    /// </summary>
    public class AppCart
    {
        /// <summary>
        /// 购物车ID
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDate { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public Boolean Seclect { get; set; }
        /// <summary>
        /// 备用2
        /// </summary>
        public string Comment2 { get; set; }
        /// <summary>
        /// 备用3
        /// </summary>
        public string Comment3 { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 外键关联
        /// </summary>
        [ForeignKey("UserId")]
        public User Users { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public long GoodsId { set; get; }
        /// <summary>
        /// 商品列表
        /// </summary>
        [ForeignKey("GoodsId")]
        public Goods Goods { set; get; }
    }
}

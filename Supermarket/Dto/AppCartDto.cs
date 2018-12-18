using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
    /// <summary>
    /// 地址管理表
    /// </summary>
    public class AppCartDto
    {
        /// <summary>
        /// 地址id
        /// </summary>
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
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public long GoodsId { set; get; }
        /// <summary>
        /// 商品名称
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
        /// 商品种类Id
        /// </summary>
        public long GoodsKindId { set; get; }
        /// <summary>
        /// 第三方唯一Id
        /// </summary>
        public string OpenId { set; get; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public Boolean Seclect { get; set; }
    }
    /// <summary>
    /// 购物车全选dto
    /// </summary>
    public class SelectAllDto {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 全选按钮标志位
        /// </summary>
        public bool Select { set; get; }
    }
}

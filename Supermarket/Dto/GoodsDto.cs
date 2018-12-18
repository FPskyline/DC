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
    public class GoodsDto
    {

        /// <summary>  
        /// 商品id
        /// </summary>
        public long GoodsId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Num { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDate { get; set; }
        /// <summary>
        /// 商品种类Id
        /// </summary>
        public long GoodsKindId { set; get; }
        /// <summary>
        /// 商品大类id
        /// </summary>
        public long GoodsBigKindId { get; set; }
        /// <summary>
        /// 系统用户Id
        /// </summary>
        public long SysUserId { set; get; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 上架/下架
        /// </summary>
        public string Comment1 { get; set; }
        /// <summary>
        /// 商品描述图片
        /// </summary>
        public string Comment2 { get; set; }
    ///// <summary>
    ///// 刚开始显示个数
    ///// </summary>
    //public int searchPageNum { get; set; }

    ///// <summary>
    ///// 每次返回个数
    ///// </summary>   
    //public int callbackcount { get; set; }
  }
    /// <summary>
    /// 微信小程序搜索商品dto
    /// </summary>
    public class AppSearchGoodDto
    {
        /// <summary>
        /// 商家id
        /// </summary>
        public long SysUserId { get; set; }
        /// <summary>
        /// 商品关键字
        /// </summary>
        public string KeyWords { get; set; }
    }
}

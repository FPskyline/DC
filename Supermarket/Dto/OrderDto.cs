using Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 用户数据传输对象
  /// </summary>
  public class OrderDto
  {
        /// <summary>
        /// 订单id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 总额
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 注释
        /// </summary> 
        public string Comments { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayType PayType { get; set; }
        /// <summary>
        /// 支付方式str
        /// </summary>
        public string PayTypestr { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public PayState PayState { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 创建时间str
        /// </summary>
        public string CreatDatestr { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { set; get; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 地址ID
        /// </summary>
        public long AddressId { set; get; }
        /// <summary>
        /// 系统用户Id
        /// </summary>
        public long SysUserId { set; get; }
        /// <summary>
        /// 系统用户名称
        /// </summary>
        public string SysUserName { set; get; }
        /// <summary>
        /// 第三方登陆ID
        /// </summary>
        public string OpenId { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// id数组
        /// </summary>
        public string GoodsId { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public string FormId { get; set; }
    /// <summary>
    /// 判断传输类型
    /// </summary>
        public string ordertype { get; set; }


      //  public string SysUserName { get; set; }

    public List<GodDto> GodDtos { get; set; }
    }
    public class GodDto
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string GodName { get; set; }
        /// <summary>
        ///产品图片
        /// </summary>
        public string GodImg { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public double NewPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num{ get; set; }
    }
}

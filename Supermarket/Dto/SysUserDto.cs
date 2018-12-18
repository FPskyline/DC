using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 商家数据传输对象
  /// </summary>
  public class SysUserDto
  {
    /// <summary>
    /// 商家id
    /// </summary>
    public long SysUserId { get; set; }
    /// <summary>
    /// 商家名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd { get; set; }
    /// <summary>
    /// 令牌
    /// </summary>
    public string Token { get; set; }
    /// <summary>
    /// 盐
    /// </summary>
    public string Salt { get; set; }
    /// <summary>
    /// 商家电话
    /// </summary>
    public string Phone { get; set; }
    /// <summary>
    /// 商家地址
    /// </summary>
    public string Addr { get; set; }
    /// <summary>
    /// 注释
    /// </summary>
    public string Comments { get; set; }
    /// <summary>
    /// 登录Ip
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 验证码
    /// </summary>
    public string Code { get; set; }
   /// <summary>
   /// 纬度
   /// </summary>
    public double Latitude { get; set; }
    /// <summary>
    /// 经度
    /// </summary>
   public double Longitude { get; set; }
    /// <summary>
    /// 公共号用户ID
    /// </summary>
    public string Popenid { get; set; }
    /// <summary>
    /// 配送范围
    /// </summary>
    public string Comment1 { get; set; }
        /// <summary>
        /// 起送金额
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 模板Id订单
        /// </summary>
        public string TemIdOrder { get; set; }
        /// <summary>
        /// 模板Id发货
        /// </summary>
        public string TemIdSend { get; set; }
        /// <summary>
        /// 模板Id接单
        /// </summary>
        public string TemIdAccept { get; set; }
        /// <summary>
        /// 模板Id拒绝
        /// </summary>
        public string TemIdRefuse { get; set; }
        /// <summary>
        /// 是否开业
        /// </summary>
        public string IsOpen { get; set; }
    }
}

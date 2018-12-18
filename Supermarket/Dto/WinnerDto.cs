using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 中奖者数据传输对象
  /// </summary>
  public class WinnerDto
  {
    /// <summary>
    /// id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 第三方唯一标识Id
    /// </summary>
    public string OpenId { get; set; }
    /// <summary>
    /// 用户名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 用户头像
    /// </summary>
    public string Avatar { get; set; }
    /// <summary>
    /// 登录ip
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatDate { get; set; }
    /// <summary>
    /// 中奖日期
    /// </summary>
    public DateTime WinDate { get; set; }
    /// <summary>
    /// 奖品id
    /// </summary>
    public long LotteryId { get; set; }
    /// <summary>
    /// 奖品id
    /// </summary>
    public string LotteryName { get; set; }
    /// <summary>
    /// 所属商家
    /// </summary>
    public long SysUserId { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string Phone { get; set; }
  }
}

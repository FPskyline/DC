using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 商家密码管理
  /// </summary>
  public class PwdDto
  {
    /// <summary>
    /// 商家id
    /// </summary>
    public long SysUserId { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd { get; set; }
    /// <summary>
    /// 新密码
    /// </summary>
    public string NewPwd { get; set; }
    /// <summary>
    /// 确认新密码
    /// </summary>
    public string SurePwd { get; set; }
  }
}

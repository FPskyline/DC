using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceSys.Utility
{
  /// <summary>
  /// 密码转换
  /// </summary>
  public class PwdTransition
  {
    /// <summary>
    /// input pwd + salt to hash
    /// </summary>
    /// <param name="pwd">密码</param>
    /// <param name="Salt">盐</param>
    /// <returns>哈希密码</returns>
    public string ToHash(string pwd, string Salt)
    {
      byte[] passwordAndSaltBytes = Encoding.UTF8.GetBytes(pwd + Salt);
      byte[] hashBytes = SHA256.Create().ComputeHash(passwordAndSaltBytes);
      string hashString = Convert.ToBase64String(hashBytes);
      return hashString;
    }

  }
}
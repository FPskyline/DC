using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 用户数据传输对象
  /// </summary>
  public class UserDto
  {

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 第三方唯一id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 登录ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpDate { get; set; }

    }
}

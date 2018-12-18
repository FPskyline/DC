using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
  /// <summary>
  /// 用户数据传输对象
  /// </summary>
  public class AddressDto
  {
        public long AddressId { get; set; }
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
        public long UserId { set; get; }
        /// <summary>
        /// 第三方唯一标识
        /// </summary>
        public string OpenId { set; get; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }


    }
}

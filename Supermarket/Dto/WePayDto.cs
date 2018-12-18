using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceSys.Dto
{
    /// <summary>
    /// 微信支付数据传输对象
    /// </summary>
    public class WePayDto
    {
        public string timeStamp { get; set; }
        public string package { get; set; }
        public string paySign { get; set; }
        public string nonceStr { get; set; }
    }
}

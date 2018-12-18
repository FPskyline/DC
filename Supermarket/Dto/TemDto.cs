using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Dto
{
    /// <summary>
    /// 小程序模板
    /// </summary>
    public class TemDto
    {
        /// <summary>
        /// 接收用户OpID
        /// </summary>
        public string touser {get;set;}
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 表单提交ID
        /// </summary>
        public string form_id { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public Data1 data { get; set; }

    }
    public class Data1
    {
        /// <summary>
        /// 关键字1
        /// </summary>
        public Keyword first { get; set; }
        /// <summary>
        /// 关键字1
        /// </summary>
        public Keyword keyword1{ get; set; }
        /// <summary>
        /// 关键字2
        /// </summary>
        public Keyword keyword2 { get; set; }
        /// <summary>
        /// 关键字3
        /// </summary>
        public Keyword keyword3 { get; set; }
        /// <summary>
        /// 关键字3
        /// </summary>
        public Keyword keyword4 { get; set; }
        /// <summary>
        /// 关键字3
        /// </summary>
        public Keyword keyword5 { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public Keyword keyword6 { get; set; }
        /// <summary>
        /// 关键字1
        /// </summary>
        public Keyword remark { get; set; }

    }
    /// <summary>
    /// 关键字类型
    /// </summary>
    public class Keyword
    {
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }

    }
}

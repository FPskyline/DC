using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Utility
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class SmsSend
    {
        public IConfigurationRoot Configurations { get; }
        public RepData Sms(string phonelist, string content)
        {
            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           // Add "appsettings.json" to bootstrap EF config.
           .AddJsonFile("appsettings.json")
           .Build();
            string uid = config["SmsUid"];
            string passwd = config["SmsPasswd"];
            content = WebUtility.UrlEncode(content);
            var url = string.Format("http://sms.bamikeji.com:8890/mtPort/mt/normal/send?uid={0}&passwd={1}&phonelist={2}&content={3}", uid, passwd, phonelist, content);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            RepData repData = JsonConvert.DeserializeObject<RepData>(result.Content.ReadAsStringAsync().Result);
            return repData;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class RepData
    {
        /// <summary>
        /// 随机数
        /// </summary>
        public string bacthseq { get; set; }
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public bool success { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using WePay;
using LitJson;
using EcommerceSys.Dto;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace EcommerceSys.Controllers
{
  /// <summary>
  ///微信支付控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class ChuangYingController : Controller
  {
    /// <summary>
    /// 获取access token（core token）
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string GetToken()
    {
      using (HttpClient client = new HttpClient())
      {
        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxa21c1461c2b9430c&secret=998840fdfa2c24f1f82686c3c62f6d25").Result;
        string stringData = response.Content.ReadAsStringAsync().Result;

        var jObject = JObject.Parse(stringData);

        var access_token = jObject["access_token"].ToString();
        return access_token;
      }
    }
    /// <summary>
    /// 获取openid
    /// </summary>
    /// <param name="code">微信登录code</param>
    /// <returns></returns>
    [HttpGet]
    public string GetOpenId(string code)
    {
      using (HttpClient client = new HttpClient())
      {
        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wxa21c1461c2b9430c&secret=998840fdfa2c24f1f82686c3c62f6d25&code=" + code + "&grant_type=authorization_code").Result;
        string stringData = response.Content.ReadAsStringAsync().Result;

        var jObject = JObject.Parse(stringData);

        var OpenId = jObject["openid"].ToString();
        return OpenId;
      }
    }
    /// <summary>
    /// 获取Access Token 华为
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string GetToken_huawei()
    {
      using (HttpClient client = new HttpClient())
      {
        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        var url = "https://login.cloud.huawei.com/oauth2/v2/authorize?response_type=code&client_id=100213755&";
        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx998cfc2cc3850f9c&secret=22895edb0e22f7e46341e26b366fd258").Result;
        string stringData = response.Content.ReadAsStringAsync().Result;

        var jObject = JObject.Parse(stringData);

        var access_token = jObject["access_token"].ToString();
        return access_token;
      }
    }
    /// <summary>
    /// 查看用户是否已经关注公众号
    /// </summary>
    /// <param name="access_token"></param>
    /// <param name="OpenId"></param>
    /// <returns></returns>
    [HttpGet]
    public string IsCare(string access_token,string OpenId)
    {
      using (HttpClient client = new HttpClient())
      {
        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + OpenId + "&lang=zh_CN").Result;
        string stringData = response.Content.ReadAsStringAsync().Result;

        var jObject = JObject.Parse(stringData);

        var Subscribe = jObject["subscribe"].ToString();
        return Subscribe;
      }
    }
  }
}

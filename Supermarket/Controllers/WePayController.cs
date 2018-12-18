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
  public class WePayController : Controller
  {
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
        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/sns/jscode2session?appid=wx7bf5892635b902ce&secret=3e8985b16b0ec87fc95585267362a452&js_code=" + code + "&grant_type=authorization_code").Result;
        string stringData = response.Content.ReadAsStringAsync().Result;

        var jObject = JObject.Parse(stringData);

        var OpenId = jObject["openid"].ToString();
        return OpenId;
      }
    }
    /// <summary>
    /// 获取唤起微信支付信息
    /// </summary>
    /// <param name="body"></param>
    /// <param name="totalFee"></param>
    /// <param name="openId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<WePayDto> Pay([FromBody] TestDto testDto)
    {
        var jsApiPay = new JsApiPay();
        var outTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss");
        await jsApiPay.GetUnifiedOrderResult(testDto.body, outTradeNo, testDto.totalFee, testDto.openId);
        JsonData jd = JsonMapper.ToObject(jsApiPay.GetJsApiParameters());
        WePayDto wePayDto = new WePayDto();
        wePayDto.timeStamp = (string)jd["timeStamp"];
        wePayDto.package = (string)jd["package"];
        wePayDto.paySign = (string)jd["paySign"];
        wePayDto.nonceStr = (string)jd["nonceStr"];
        return wePayDto;
    }
    /// <summary>
    /// 申请退款
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Refund()
    {
      try
      {
        var jsApiPay = new JsApiPay();
        //var outTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss");
        await jsApiPay.GetRefundResult();
        //JsonData jd = JsonMapper.ToObject(jsApiPay.GetJsApiParameters());
        var result = jsApiPay.GetJsApiParameters();
        return Ok(result);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }

  }
  public class TestDto
  {
    public string body { get; set; }
    public int totalFee { get; set; }
    public string openId { get; set; }
  }
}

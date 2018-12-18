using System;
using System.Threading.Tasks;

namespace WePay
{
  /// <summary>
  /// 微信公众号支付
  /// </summary>
  internal class JsApiPay
  {
    /// <summary>
    /// 保存页面对象，因为要在类的方法中使用Page的Request对象
    /// </summary>
    //private HttpContext page { get; set; }

    /// <summary>
    /// openid用于调用统一下单接口
    /// </summary>
    //public string openid { get; set; }

    /// <summary>
    /// access_token用于获取收货地址js函数入口参数
    /// </summary>
    //public string access_token { get; set; }

    /// <summary>
    /// 商品金额，用于统一下单
    /// </summary>
    //public int total_fee { get; set; }

    /// <summary>
    /// 统一下单接口返回结果
    /// </summary>
    private WxPayData _unifiedOrderResult { get; set; }

    //public JsApiPay(HttpContext page)
    //{
    //    this.page = page;
    //}
    public JsApiPay()
    {

    }

    /**
    * 
    * 网页授权获取用户基本信息的全部过程
    * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
    * 第一步：利用url跳转获取code
    * 第二步：利用code去获取openid和access_token
    * 
    */
    //public void GetOpenidAndAccessToken(string tradeId)
    //{
    //    if (!string.IsNullOrEmpty(page.Request.Form["code"]))
    //    {
    //        //获取code码，以获取openid和access_token
    //        string code = page.Request.Form["code"];
    //        //Log.Debug(this.GetType().ToString(), "Get code : " + code);
    //        GetOpenidAndAccessTokenFromCode(code);
    //    }
    //    else
    //    {
    //        //构造网页授权获取code的URL
    //        string redirect_uri = WebUtility.UrlEncode("http://m.aiqinhaigou.com/payment/wechat_callback.html?tradeId=" + tradeId);
    //        WxPayData data = new WxPayData();
    //        data.SetValue("appid", WxPayConfig.APPID);
    //        data.SetValue("redirect_uri", redirect_uri);
    //        data.SetValue("response_type", "code");
    //        data.SetValue("scope", "snsapi_base");
    //        data.SetValue("state", "STATE" + "#wechat_redirect");
    //        string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
    //        //Log.Debug(this.GetType().ToString(), "Will Redirect to URL : " + url);
    //        try
    //        {
    //            //触发微信返回code码         
    //            page.Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.ToString());
    //        }
    //    }
    //}

    /// <summary>
    /// 获取回Openid
    /// </summary>
    /// <returns></returns>
    //public string GetOauthCode(string tradeId)
    //{
    //    //构造网页授权获取code的URL
    //    string redirect_uri = WebUtility.UrlEncode("http://m.aiqinhaigou.com/payment/wechat_callback.html?tradeId=" + tradeId);
    //    WxPayData data = new WxPayData();
    //    data.SetValue("appid", WxPayConfig.APPID);
    //    data.SetValue("redirect_uri", redirect_uri);
    //    data.SetValue("response_type", "code");
    //    data.SetValue("scope", "snsapi_base");
    //    data.SetValue("state", "STATE" + "#wechat_redirect");
    //    var url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
    //    //LogManager.GetLogger(typeof(JsApiPay)).Info("微信支付回调地址：{0}", redirect_uri);
    //    return url;
    //}
    /**
    * 
    * 通过code换取网页授权access_token和openid的返回数据，正确时返回的JSON数据包如下：
    * {
    *  "access_token":"ACCESS_TOKEN",
    *  "expires_in":7200,
    *  "refresh_token":"REFRESH_TOKEN",
    *  "openid":"OPENID",
    *  "scope":"SCOPE",
    *  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
    * }
    * 其中access_token可用于获取共享收货地址
    * openid是微信支付jsapi支付接口统一下单时必须的参数
    * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
    * @失败时抛异常WxPayException
    */
    //public void GetOpenidAndAccessTokenFromCode(string code)
    //{
    //    try
    //    {
    //        //构造获取openid及access_token的url
    //        WxPayData data = new WxPayData();
    //        data.SetValue("appid", WxPayConfig.APPID);
    //        data.SetValue("secret", WxPayConfig.APPSECRET);
    //        data.SetValue("code", code);
    //        data.SetValue("grant_type", "authorization_code");
    //        string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();

    //        //请求url以获取数据
    //        string result = HttpService.Get(url);

    //        //Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);

    //        //保存access_token，用于收货地址获取 
    //        var jd = JObject.Parse(result);
    //        access_token = (string)jd["access_token"];

    //        //获取用户openid
    //        openid = (string)jd["openid"];

    //        //Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
    //        //Log.Debug(this.GetType().ToString(), "Get access_token : " + access_token);
    //    }
    //    catch (Exception ex)
    //    {
    //        //Log.Error(this.GetType().ToString(), ex.ToString());
    //        throw new Exception(ex.ToString());
    //    }
    //}
    /**
     * 调用统一下单，获得下单结果
     * @return 统一下单结果
     * @失败时抛异常WxPayException
     */
    public async Task<WxPayData> GetUnifiedOrderResult(string body, string outTradeNo, int totalFee, string openId)
    {
      //统一下单
      var data = new WxPayData();
      data.SetValue("body", body);
      //data.SetValue("attach", outTradeIds);
      data.SetValue("out_trade_no", outTradeNo);
      data.SetValue("total_fee", totalFee);
      data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
      data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
      //data.SetValue("goods_tag", remark);
      data.SetValue("trade_type", "JSAPI");
      data.SetValue("openid", openId);

      var result = await WxPayApi.UnifiedOrder(data);
      if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
      {
        throw new Exception("UnifiedOrder response error!");
      }

      _unifiedOrderResult = result;
      return result;
    }
    public async Task<WxPayData> GetRefundResult()
    {
      //申请退款
      var data = new WxPayData();
      data.SetValue("out_trade_no", "20171208093422");
      data.SetValue("out_refund_no", "20171208093422");
      data.SetValue("total_fee", 2);
      data.SetValue("refund_fee", 2);
      data.SetValue("op_user_id", "1488443192");

      var result = await WxPayApi.Refund(data);
      //if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
      //{
      //  throw new Exception("UnifiedOrder response error!");
      //}
      _unifiedOrderResult = result;
      return result;
    }

    /**
    *  
    * 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
    * 微信浏览器调起JSAPI时的输入参数格式如下：
    * {
    *   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
    *   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
    *   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
    *   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
    *   "signType" : "MD5",         //微信签名方式:    
    *   "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
    * }
    * @return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
    * 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
    * 
    */
    public string GetJsApiParameters()
    {
      WxPayData jsApiParam = new WxPayData();
      jsApiParam.SetValue("appId", _unifiedOrderResult.GetValue("appid"));
      jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
      jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
      jsApiParam.SetValue("package", "prepay_id=" + _unifiedOrderResult.GetValue("prepay_id"));
      jsApiParam.SetValue("signType", "MD5");
      jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

      string parameters = jsApiParam.ToJson();
      return parameters;
    }
  }
}


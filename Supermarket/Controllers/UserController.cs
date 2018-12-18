using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Supermarket.Models.InitModels;
using Supermarket.Models;
using Microsoft.EntityFrameworkCore;
using Supermarket.Dto;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Supermarket.Utility;


namespace Supermarket.Controllers
{

  /// <summary>
  /// Home控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class UserController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public UserController(SupermarketDbContext context)
    {
      _context = context;
    }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
    public IActionResult CreateUser(UserDto model)
    {
            var user = _context.User.Where(x => x.OpenId == model.OpenId).FirstOrDefault();
            if (user == null)
            {
                string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                var UserInfo = new User()
                {

                    Ip = ip,
                    Name = WebUtility.UrlEncode(model.Name),
                    Avatar = model.Avatar,
                    OpenId = model.OpenId,
                    CreatDate =model.CreatDate,
                    UpDate =model.UpDate

                };
                _context.User.Add(UserInfo);
                _context.SaveChanges();
                return Ok("新增成功");

            }
            else {
                string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                User info = _context.User.Where(d => d.OpenId == model.OpenId).FirstOrDefault();
                info.Name = WebUtility.UrlEncode(model.Name);
                info.Avatar = model.Avatar;
                info.UpDate = model.UpDate;
                info.Ip = ip;
                _context.SaveChanges();
                return Ok("修改成功！");
            }
    }
        /// <summary>
        /// 发送code 解析openid
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public  string HttpPost(string js_code, string grant_type)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri ("https://api.weixin.qq.com/sns/jscode2session?appid=APPID&secret=SECRET&js_code=JSCODE&grant_type=authorization_code");
                MediaTypeWithQualityHeaderValue contentType =new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/sns/jscode2session?appid=wx003c752c0c4e829e&secret=e73afb2381e6335aa947a5ae354d0fe2&js_code=" + js_code + "&grant_type=authorization_code").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                var jObject = JObject.Parse(stringData); 

                 var OpenId = jObject["openid"].ToString();
                    return OpenId;            
            }
        }
      /// <summary>
      /// 发送code 解析openid
      /// </summary>
      /// <returns></returns>
      [HttpPost]
      public string NewHttpPost(string appid,string secret, string js_code, string grant_type)
      {
        using (HttpClient client = new HttpClient())
        {
          //client.BaseAddress = new Uri ("https://api.weixin.qq.com/sns/jscode2session?appid=APPID&secret=SECRET&js_code=JSCODE&grant_type=authorization_code");
          MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
          client.DefaultRequestHeaders.Accept.Add(contentType);
          HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/sns/jscode2session?appid="+ appid + "&secret="+ secret + "&js_code=" + js_code + "&grant_type=authorization_code").Result;
          string stringData = response.Content.ReadAsStringAsync().Result;

          var jObject = JObject.Parse(stringData);

          var OpenId = jObject["openid"].ToString();
          return OpenId;
        }
      }
    /// <summary>
    /// 获取用户
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
        public IActionResult GetUser([FromBody] PageDto pageDto)
        {
            try
            {
                GetPageDto<List<UserDto>> returnData = new GetPageDto<List<UserDto>>();
                returnData.TotalCount = _context.User.Count();
                if (pageDto.Page >= 1 && pageDto.Number > 0)
                {
                    var UserList = (from b in _context.User
                                    select new UserDto()
                                    {
                                        Ip = b.Ip,
                                        Name = WebUtility.UrlDecode(b.Name),
                                        Avatar = b.Avatar,
                                        OpenId = b.OpenId,
                                        CreatDate = b.CreatDate,
                                        UpDate = b.UpDate
                                    }).OrderByDescending(d => d.CreatDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
                    returnData.BigField = UserList;
                }
                return new ObjectResult(returnData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 发送短信测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SendSms(string phone, string content)
        {
            SmsSend sms = new SmsSend();
            var data = sms.Sms(phone, content);
            return Ok(data);
        }

        /// <summary>
        /// 发送模板消息小程序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string HttpTemplate(string access_token,TemDto model)
        {
            using (HttpClient client = new HttpClient())
            {
               
                
                //client.BaseAddress = new Uri ("https://api.weixin.qq.com/sns/jscode2session?appid=APPID&secret=SECRET&js_code=JSCODE&grant_type=authorization_code");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                HttpResponseMessage response = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token="+ access_token, content).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                //var jObject = JObject.Parse(stringData);
                //var OpenId = jObject["openid"].ToString();
                return stringData;
            }
        }




        /// <summary>
        /// 发送模板消息微信公众号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string HttpText(string access_token, TemDto model)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri ("https://api.weixin.qq.com/sns/jscode2session?appid=APPID&secret=SECRET&js_code=JSCODE&grant_type=authorization_code");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
             
                HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
               
                HttpResponseMessage response = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token, content).Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                //var jObject = JObject.Parse(stringData);
                //var OpenId = jObject["openid"].ToString();
                return stringData;
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult  GetToken()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var Toke = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
                    if (Toke == null)
                    {
                        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Add(contentType);
                        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx998cfc2cc3850f9c&secret=22895edb0e22f7e46341e26b366fd258").Result;
                        string stringData = response.Content.ReadAsStringAsync().Result;
                        if (stringData == null)
                        {
                            return BadRequest("没有数据");
                        }
                        else {
                            var jObject = JObject.Parse(stringData);
                            var Token = jObject["access_token"].ToString();
                            var Paras = new Paras()
                            {
                                Name = "Token",
                                Describe = Token,
                                CreatDate = DateTime.Now
                            };
                            _context.Parass.Add(Paras);
                            _context.SaveChanges();
                            return Ok(Paras);
                        }
                    }

                    else
                    {
                        var Time2 = Toke.CreatDate;
                        var Time1 = DateTime.Now;
                        TimeSpan ts1 = new TimeSpan(Time1.Ticks);
                        TimeSpan ts2 = new TimeSpan(Time2.Ticks);
                        TimeSpan ts = ts1.Subtract(ts2).Duration();
                        if (ts.Hours >= 2)
                        {
                            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType);
                            HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx998cfc2cc3850f9c&secret=22895edb0e22f7e46341e26b366fd258").Result;
                            string stringData = response.Content.ReadAsStringAsync().Result;
                            var jObject = JObject.Parse(stringData);
                            var Token1 = jObject["access_token"].ToString();

                            var Kind = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
                            Kind.CreatDate = DateTime.Now;
                            Kind.Describe = Token1;

                            _context.SaveChanges();
                            return Ok(Kind);
                        }
                        else
                        {
                            var Token3 = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
                            return Ok(Token3);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        /// <summary>
        /// 发送公众号模板消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public  IActionResult TempleHttp(string access_token, TemDto model)
        {
            try {
                using (HttpClient client = new HttpClient())
                {

                    var Toke = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(model));

                    HttpResponseMessage response = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token, content).Result;
                    string stringData = response.Content.ReadAsStringAsync().Result;

                    Token Fun = new Token(_context);
                    //var addr = "";

                   // var n = Fun.HttpMap(addr, model.SysUserId);

                    return Ok(stringData);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 存储FormID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveForm(FormIdDto model)
        {
            try
            {
                var user = _context.User.Where(x => x.OpenId == model.OpenId).FirstOrDefault();



                var FormIdInfo = new FormId()
                {
                    UserId = user.UserId,
                    FormIds = model.FormIds,
                    CreatDate = DateTime.Now,
                 };
                    _context.FormIds.Add(FormIdInfo);
                    _context.SaveChanges();
 




                return Ok("新增成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}

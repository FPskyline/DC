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

namespace Supermarket.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class AppToken
    {
        private SupermarketDbContext _context;

        public AppToken(SupermarketDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取小程序
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetAppToken(TemDto model)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var Toke = _context.Parass.Where(x => x.Name == "AppToken").FirstOrDefault();
                    if (Toke == null)
                    {
                        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Add(contentType);
                        HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx003c752c0c4e829e&secret=e73afb2381e6335aa947a5ae354d0fe2").Result;
                        string stringData = response.Content.ReadAsStringAsync().Result;
                        if (stringData == null)
                        {
                            return null;
                        }
                        else
                        {
                            var jObject = JObject.Parse(stringData);
                            var Token = jObject["access_token"].ToString();
                            var Paras = new Paras()
                            {
                                Name = "AppToken",
                                Describe = Token,
                                CreatDate = DateTime.Now
                            };
                            _context.Parass.Add(Paras);
                            _context.SaveChanges();
                            var Tok = Token;
                            MediaTypeWithQualityHeaderValue contentType2 = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType2);
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                            HttpResponseMessage response2 = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=" + Tok, content).Result;
                            string stringData2 = response2.Content.ReadAsStringAsync().Result;

                            return stringData2;
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
                            HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx003c752c0c4e829e&secret=e73afb2381e6335aa947a5ae354d0fe2").Result;
                            string stringData = response.Content.ReadAsStringAsync().Result;
                            var jObject = JObject.Parse(stringData);
                            var Token1 = jObject["access_token"].ToString();

                            var Kind = _context.Parass.Where(x => x.Name == "AppToken").FirstOrDefault();
                            Kind.CreatDate = DateTime.Now;
                            Kind.Describe = Token1;

                            _context.SaveChanges();
                            var Tok = Kind.Describe;
                        
                            MediaTypeWithQualityHeaderValue contentType1 = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType1);
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                            HttpResponseMessage response1 = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=" + Tok, content).Result;
                            string stringData1 = response1.Content.ReadAsStringAsync().Result;
                            return stringData1;
                        }
                        else
                        {
                            var Token3 = _context.Parass.Where(x => x.Name == "AppToken").FirstOrDefault();
                            var Tok = Token3.Describe;
                            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType);
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                            HttpResponseMessage response = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=" + Tok, content).Result;
                            string stringData = response.Content.ReadAsStringAsync().Result;
                            return stringData;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }




        



    }
}

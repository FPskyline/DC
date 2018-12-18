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
    public class Token
    {
        private SupermarketDbContext _context;

        public Token(SupermarketDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetToken(TemDto model)
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
                            return null;
                        }
                        else
                        {
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
                            var Tok = Token;
                            MediaTypeWithQualityHeaderValue contentType2 = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType2);
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                            HttpResponseMessage response2 = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + Tok, content).Result;
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
                            HttpResponseMessage response = client.GetAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx998cfc2cc3850f9c&secret=22895edb0e22f7e46341e26b366fd258").Result;
                            string stringData = response.Content.ReadAsStringAsync().Result;
                            var jObject = JObject.Parse(stringData);
                            var Token1 = jObject["access_token"].ToString();

                            var Kind = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
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
                            var Token3 = _context.Parass.Where(x => x.Name == "Token").FirstOrDefault();
                            var Tok = Token3.Describe;
                            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Add(contentType);
                            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
                            HttpResponseMessage response = client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + Tok, content).Result;
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

        /// <summary>
        /// 经纬度
        /// </summary>
        /// <returns></returns>
        public double MathLat(double lat1, double lng1, double lat2, double lng2)
        {
            var d = GetDistance(lat1, lng1, lat2, lng2);
            return d;
        }
        private const double EARTH_RADIUS = 6378.137;//地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        /// <summary>
        /// 计算距离接口
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
            Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }


     /// <summary>
     /// 
     /// </summary>
     /// <param name="addr"></param>
     /// <param name="SysUserId"></param>
     /// <returns></returns>
        public double HttpMap(string addr,long SysUserId)
        {
            using (HttpClient client = new HttpClient())
            {
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("http://apis.map.qq.com/ws/geocoder/v1/?address="+addr+ "&key=YPWBZ-HOV3U-PACVI-2ON7B-M67NJ-RPB7X").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;

                var jObject = JObject.Parse(stringData);
                var result = jObject["result"].ToString();

                var Obj = JObject.Parse(result);
                var lcation = Obj["location"].ToString();

                var W = JObject.Parse(lcation);
                var lat = ((JValue)((JProperty)W.First).Value).Value.ToString();
                var lng = ((JValue)((JProperty)W.Last).Value).Value.ToString();

                var  lat2 = System.Convert.ToDouble(lat);
                var lng2 = System.Convert.ToDouble(lng);


                var sys = _context.SysUsers.Where(x => x.SysUserId == SysUserId).FirstOrDefault();
                var lac = sys.Comment1;
                var lac1 = System.Convert.ToDouble(lac);
                var lat1 = sys.Longitude;
                 var lng1 = sys.Latitude;

                var d = GetDistance(lat2, lng2, lat1, lng1);

                if (d <= lac1)
                {
                    var x = 1;
                    return x;

                }
                else {
                    var x = 2;
                    return x;
                }
           
            }
        }



    }
}

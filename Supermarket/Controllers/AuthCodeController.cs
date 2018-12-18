using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.DrawingCore;
using System.DrawingCore.Imaging;

namespace EcommerceSys.Controllers
{
  /// <summary>
  /// 验证码生成控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class AuthCodeController : Controller
  {
    /// <summary>
    /// 验证码生成
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult Creat()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        string validateNum = CreateRandomNum(4);//成生4位随机字符串
        HttpContext.Session.SetString("Code_ValidateNum", validateNum);
        return Ok();
      }
      catch (Exception ex)
      {
        return StatusCode(500,ex);
      }
    }
    /// <summary>
    /// 生成随机字符串
    /// </summary>
    /// <param name="NumCount">位数</param>
    /// <returns></returns>
    private string CreateRandomNum(int NumCount)
    {
      string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
      string[] allCharArray = allChar.Split(',');//拆分成数组
      string randomNum = "";
      int temp = -1;//记录上次随机数的数值，尽量避免产生几个相同的随机数
      Random rand = new Random();
      for (int i = 0; i < NumCount; i++)
      {
        if (temp != -1)
        {
          rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
        }
        int t = rand.Next(35);
        if (temp == t)
        {
          return CreateRandomNum(NumCount);
        }
        temp = t;
        randomNum += allCharArray[t];
      }
      return randomNum;
    }
    /// <summary>
    /// 生成验证码图片
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public HttpResponseMessage Get()
    {
      string validateNum = HttpContext.Session.GetString("Code_ValidateNum");
      if (validateNum == null || validateNum.Trim() == string.Empty)
      {
        return null;
      }
      int codeW = 80;
      int codeH = 30;
      int fontSize = 16;
      Random rnd = new Random();
      //颜色列表，用于验证码、噪线、噪点 
      Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
      //字体列表，用于验证码 
      string[] font = { "Times New Roman" };
      //验证码的字符集，去掉了一些容易混淆的字符 

      //写入Session、验证码加密
      //WebHelper.WriteSession("session_verifycode", Md5Helper.MD5(chkCode.ToLower(), 16));
      //创建画布
      Bitmap bmp = new Bitmap(codeW, codeH);
      Graphics g = Graphics.FromImage(bmp);
      g.Clear(Color.White);
      //画噪线 
      for (int i = 0; i < 1; i++)
      {
        int x1 = rnd.Next(codeW);
        int y1 = rnd.Next(codeH);
        int x2 = rnd.Next(codeW);
        int y2 = rnd.Next(codeH);
        Color clr = color[rnd.Next(color.Length)];
        g.DrawLine(new Pen(clr), x1, y1, x2, y2);
      }
      //画验证码字符串 
      for (int i = 0; i < validateNum.Length; i++)
      {
        string fnt = font[rnd.Next(font.Length)];
        Font ft = new Font(fnt, fontSize);
        Color clr = color[rnd.Next(color.Length)];
        g.DrawString(validateNum[i].ToString(), ft, new SolidBrush(clr), (float)i * 18, (float)0);
      }
      //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
      MemoryStream ms = new MemoryStream();
      try
      {
        bmp.Save(ms, ImageFormat.Jpeg);
        var result = new HttpResponseMessage(HttpStatusCode.OK)
        {
          Content = new ByteArrayContent(ms.ToArray())
        };
        result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        return result;

      }
      catch (Exception)
      {
        return null;
      }
      finally
      {
        g.Dispose();
        bmp.Dispose();
      }
    }
  }
}

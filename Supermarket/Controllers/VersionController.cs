using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using Supermarket.Utility;

namespace EcommerceSys.Controllers
{
  /// <summary>
  /// app版本控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class VersionController : Controller
  {
    private readonly IHostingEnvironment _hostingEnvironment;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostingEnvironment"></param>
    public VersionController(IHostingEnvironment hostingEnvironment)
    {
      _hostingEnvironment = hostingEnvironment;
    }
    /// <summary>
    /// 获取服务器端版本号
    /// </summary>
    /// <returns>返回版本号</returns>
    [HttpGet]
    public IActionResult GetVersion()
    {
      try
      {
        string VersionUrl = _hostingEnvironment.WebRootPath + $@"\Files\Version\";
        string text = System.IO.File.ReadAllText(VersionUrl + "version.txt");
        return Ok(text);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}

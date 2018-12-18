using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Supermarket.Models.InitModels;
using Supermarket.Models;
using Microsoft.EntityFrameworkCore;
using Supermarket.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Cryptography;
using System.Text;
using EcommerceSys.Utility;

namespace Supermarket.Controllers
{
  /// <summary>
  /// Home控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class LoginController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public LoginController(SupermarketDbContext context)
    {
      _context = context;
    }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult Login([FromBody] SysUserDto sysUserDto)
        {
            try
            {
                var sysuserinfo = _context.SysUsers.Where(x => x.Account == sysUserDto.Account).FirstOrDefault();
                if (sysuserinfo == null)
                {
                    return BadRequest("不存在此账号！");
                }
                PwdTransition pwdTransition = new PwdTransition();
                var Hashpwd = pwdTransition.ToHash(sysUserDto.Pwd, sysuserinfo.Salt);
                if (sysuserinfo.Pwd != Hashpwd)
                {
                    return BadRequest("密码不正确！请重新输入");
                }
                string validateNum = HttpContext.Session.GetString("Code_ValidateNum");
                HttpContext.Session.Remove("Code_ValidateNum");
                if (validateNum != sysUserDto.Code.ToUpper())
                {
                    return BadRequest("输入验证码有误!");
                }
                var token = Guid.NewGuid().ToString();
                sysuserinfo.Token = token;
                sysuserinfo.Ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _context.SaveChanges();
                var loginInfo = sysuserinfo.SysUserId + "&" + token;
                return Ok(loginInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// APP商家登录
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult AppLogin([FromBody] SysUserDto sysUserDto)
        {
            try
            {
                var sysuserinfo = _context.SysUsers.Where(x => x.Account == sysUserDto.Account).FirstOrDefault();
                if (sysuserinfo == null)
                {
                    return BadRequest("不存在此账号！");
                }
                PwdTransition pwdTransition = new PwdTransition();
                var Hashpwd = pwdTransition.ToHash(sysUserDto.Pwd, sysuserinfo.Salt);
                if (sysuserinfo.Pwd != Hashpwd)
                {
                    return BadRequest("密码不正确！请重新输入");
                }
                var token = Guid.NewGuid().ToString();
                sysuserinfo.Token = token;
                sysuserinfo.Ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _context.SaveChanges();
                return Ok(sysuserinfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

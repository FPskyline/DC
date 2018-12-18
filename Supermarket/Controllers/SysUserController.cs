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
using Newtonsoft.Json;
using System.Web.Http;

namespace Supermarket.Controllers
{
  /// <summary>
  /// Home控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class SysUserController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 数据库链接库
    /// </summary>
    /// <param name="context"></param>
    public SysUserController(SupermarketDbContext context)
    {
      _context = context;
    }
    /// <summary>
    /// 获取系统用户列表
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetSysUsers([FromBody] PageDto pageDto)
    {
      try
      {
        GetPageDto<List<SysUserDto>> returnData = new GetPageDto<List<SysUserDto>>();
        returnData.TotalCount = _context.SysUsers.Count();
        if (pageDto.Page >= 1 && pageDto.Number > 0)
        {
          var sysuserList = (from a in _context.SysUsers
                             where a.SysUserId != 1
                             select new SysUserDto()
                             {
                               SysUserId = a.SysUserId,
                               Account = a.Account,
                               Addr = a.Addr,
                               Comments = a.Comments,
                               CreatDate = a.CreatDate,
                               Ip = a.Ip,
                               Name = a.Name,
                               Phone = a.Phone,
                               Longitude=a.Longitude,
                               Latitude=a.Latitude,
                               Popenid = a.Popenid,
                               Comment1 =a.Comment1,
                                Price = a.Price,
                                TemIdAccept = a.TemIdAccept,
                                TemIdRefuse = a.TemIdRefuse,
                                TemIdOrder = a.TemIdOrder,
                                TemIdSend = a.TemIdSend,
                                IsOpen = a.IsOpen
                             }).OrderBy(d => d.SysUserId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
          returnData.BigField = sysuserList;
        }
        return new ObjectResult(returnData);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 添加系统用户
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult AddSysUser([FromBody] SysUserDto sysUserDto)
    {
      try
      {
        PwdTransition pwdTransition = new PwdTransition();
        var Salt = Guid.NewGuid().ToString();
        var Hashpwd = pwdTransition.ToHash("123aaa", Salt);
        string ip = HttpContext.Connection.RemoteIpAddress.ToString();

        var SysUserInfo = new SysUser()
        {
          Account = sysUserDto.Account,
          Pwd = Hashpwd,
          Salt = Salt,
          Addr = sysUserDto.Addr,
          Comments = sysUserDto.Comments,
          CreatDate = DateTime.Now,
          Ip = ip,
          Name = sysUserDto.Name,
          Phone = sysUserDto.Phone,
          Latitude =sysUserDto.Latitude,
          Longitude =sysUserDto.Longitude,
          Popenid = sysUserDto.Popenid,
          Comment1 =sysUserDto.Comment1,
          Price = sysUserDto.Price,
          TemIdAccept = sysUserDto.TemIdAccept,
          TemIdRefuse = sysUserDto.TemIdRefuse,
          TemIdOrder = sysUserDto.TemIdOrder,
          TemIdSend = sysUserDto.TemIdSend,
          IsOpen = sysUserDto.IsOpen
        };
        _context.SysUsers.Add(SysUserInfo);
        _context.SaveChanges();
        return Ok("添加成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 修改系统用户
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult EditSysUser([FromBody] SysUserDto sysUserDto)
    {
      try
      {

        var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == sysUserDto.SysUserId).FirstOrDefault();
        sysUserinfo.Account = sysUserDto.Account;
        sysUserinfo.Addr = sysUserDto.Addr;
        sysUserinfo.Comments = sysUserDto.Comments;
        sysUserinfo.Name = sysUserDto.Name;
        sysUserinfo.Phone = sysUserDto.Phone;
        sysUserinfo.Latitude = sysUserDto.Latitude;
        sysUserinfo.Longitude = sysUserDto.Longitude;
        sysUserinfo.Popenid = sysUserDto.Popenid;
        sysUserinfo.Comment1 = sysUserDto.Comment1;
        sysUserinfo.Price = sysUserDto.Price;
        sysUserinfo.TemIdAccept = sysUserDto.TemIdAccept;
        sysUserinfo.TemIdRefuse = sysUserDto.TemIdRefuse;
        sysUserinfo.TemIdOrder = sysUserDto.TemIdOrder;
        sysUserinfo.TemIdSend = sysUserDto.TemIdSend;
        sysUserinfo.IsOpen = sysUserDto.IsOpen;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 删除系统用户
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpDelete]
    public IActionResult DelSysUser(long id)
    {
      try
      {
        var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == id).FirstOrDefault();
        _context.Remove(sysUserinfo);
        _context.SaveChanges();
        return Ok("删除成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500,ex.Message);

      }
    }
    /// <summary>
    /// 初始化商家密码
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPut]
    public IActionResult InitPwd(long id)
    {
      try
      {
        var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == id).FirstOrDefault();
        PwdTransition pwdTransition = new PwdTransition();
        var Salt = Guid.NewGuid().ToString();
        var Hashpwd = pwdTransition.ToHash("123aaa", Salt);
        sysUserinfo.Pwd = Hashpwd;
        sysUserinfo.Salt = Salt;
        _context.SaveChanges();
        return Ok("初始化密码成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 获取某一商家信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetSysUserInfo(long id)
    {
      try
      {
        var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == id).FirstOrDefault();
        return Ok(sysUserinfo);
      }
      catch(Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
        /// <summary>
        /// 商家修改密码
        /// </summary>
        /// <param name="pwdDto"></param>
        /// <returns></returns>
        [HttpPost]
    public IActionResult ChangePwd([FromBody] PwdDto pwdDto)
    {
      try
      {
        var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == pwdDto.SysUserId).FirstOrDefault();
        PwdTransition pwdTransition = new PwdTransition();
        var oldpwd = pwdTransition.ToHash(pwdDto.Pwd, sysUserinfo.Salt);
        if (oldpwd != sysUserinfo.Pwd)
        {
          return BadRequest("原密码错误！请重新输入");
        }
        if (pwdDto.NewPwd != pwdDto.SurePwd)
        {
          return BadRequest("二次密码输入不一致！请重新输入");
        }
        string salt = Guid.NewGuid().ToString();
        var newpwd = pwdTransition.ToHash(pwdDto.NewPwd, salt);
        sysUserinfo.Salt = salt;
        sysUserinfo.Pwd = newpwd;
        _context.SaveChanges();
        return Ok("修改密码成功！请重新登陆");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }


    }
}

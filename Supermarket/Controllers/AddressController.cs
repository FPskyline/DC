using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Supermarket.Models.InitModels;
using Supermarket.Models;
using Microsoft.EntityFrameworkCore;
using Supermarket.Dto;
using Supermarket.Utility;

namespace Applet.Controllers
{
  /// <summary>
  /// Home控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class AddressController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public AddressController(SupermarketDbContext context)
    {
      _context = context;
    }

    /// <summary>
    /// 新增地址
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult CreateAddr( AddressDto model)
    {
            var UserId = _context.User.Where(i => i.OpenId == model.OpenId).FirstOrDefault().UserId;
            var exits = _context.Addresss.Where(i => i.UserId == UserId).Count();
            if (exits == 0)
            {
                var Address = new Address()
                {
                    Addr = model.Addr,
                    ContactName = model.ContactName,
                    ContactPhone = model.ContactPhone,
                    CreatDate = DateTime.Now,
                    UpDate = DateTime.Now,
                    UserId = UserId,
                    IsDefault=true,
                    Longitude=model.Longitude, 
                    Latitude=model.Latitude 
            };
                _context.Addresss.Add(Address);
            }
            else
            {
               var Address = new Address()
               {
                Addr = model.Addr,
                ContactName = model.ContactName,
                ContactPhone=model.ContactPhone,
                CreatDate=DateTime.Now,
                UpDate=DateTime.Now,
                UserId = UserId,
                  IsDefault = false,
                   Longitude = model.Longitude,
                   Latitude = model.Latitude
               };
                _context.Addresss.Add(Address);
            }
            _context.SaveChanges();
      return Ok("新增成功");
    }

        /// <summary>
        /// 查询地址
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetAddr(string OpenId)
        {
            var User = from a in _context.User
                       where a.OpenId == OpenId
                       from b in _context.Addresss
                       where b.UserId == a.UserId
                       select new AddressDto()
                       {
                           Addr = b.Addr,
                           ContactName=b.ContactName,
                           CreatDate=b.CreatDate,
                           UpDate=b.UpDate,
                           ContactPhone=b.ContactPhone,
                           AddressId=b.AddressId,
                           UserId=b.UserId,
                           IsDefault=b.IsDefault,
                           Longitude = b.Longitude,
                           Latitude = b.Latitude
                       };
                       User.ToList();
            return Ok(User);
        }
        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetDefaultAddr(string OpenId)
        {
            var User = (from a in _context.User
                       where a.OpenId == OpenId
                       from b in _context.Addresss
                       where b.UserId == a.UserId && b.IsDefault==true
                       select new AddressDto()
                       {
                           Addr = b.Addr,
                           ContactName = b.ContactName,
                           CreatDate = b.CreatDate,
                           UpDate = b.UpDate,
                           ContactPhone = b.ContactPhone,
                           AddressId = b.AddressId,
                           UserId = b.UserId,
                           IsDefault = b.IsDefault,
                           Longitude = b.Longitude,
                           Latitude = b.Latitude
                       }).FirstOrDefault();
            return Ok(User);
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult DelAddr(long AddressId)
        {
            var DelAddr = _context.Addresss.Where(i => i.AddressId == AddressId).FirstOrDefault();
            if (DelAddr == null)
            {
                return BadRequest("没有信息");
            }
            _context.Addresss.Remove(DelAddr);
            _context.SaveChanges();
            return Ok("删除成功");
        }
        /// <summary>
        /// 设置默认
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult Change(AddressDto model)
        {
            var UserId = _context.User.Where(i => i.OpenId == model.OpenId).FirstOrDefault().UserId;
            var ChangeAddr = _context.Addresss.Where(i => i.AddressId == model.AddressId).FirstOrDefault();
            if (ChangeAddr.IsDefault == false)
            {
                var IsDefault = _context.Addresss.Where(i => i.UserId == UserId && i.IsDefault == true).FirstOrDefault();
                if(IsDefault != null)
                {
                    IsDefault.IsDefault = false;
                    ChangeAddr.IsDefault = true;
                }
            }
            _context.SaveChanges();
            return Ok("修改成功");
        }
    [HttpGet]
    public IActionResult Test()
    {
      try
      {
        var info = _context.Lotterys.ToList();     
        return Ok(info);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}

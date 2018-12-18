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
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace Supermarket.Controllers
{
  /// <summary>
  /// 商品种类控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class GoodsKindController : Controller
  {
    private SupermarketDbContext _context;

    public readonly IHostingEnvironment _Env;
    /// <summary>
    /// 配置数据库与文件流参数
    /// </summary>
    /// <param name="context"></param>
    /// <param name="env"></param>
    public GoodsKindController(SupermarketDbContext context, IHostingEnvironment env)
    {
      _context = context;
      _Env = env;
    }
    /// <summary>
    /// 获取当前登陆用户下的商品大类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetBigKind(long sysuserid)
    {
      try
      {
        var GoodsKindList = new List<GoodsBigKindDto>();
        if (sysuserid == 1)
        {

          GoodsKindList = (from c in _context.GoodsBigKinds
                           select new GoodsBigKindDto()
                           {
                             Name = c.Name,
                             Index = c.Index,
                             SysUserId = c.SysUserId,
                             SysUserName = c.SysUsers.Name,
                             GoodsBigKindId = c.GoodsBigKindId,
                           }).ToList();
        }
        else
        {
          GoodsKindList = (from c in _context.GoodsBigKinds
                           where c.SysUserId == sysuserid
                           select new GoodsBigKindDto()
                           {
                             Name = c.Name,
                             Index = c.Index,
                             SysUserId = c.SysUserId,
                             SysUserName = c.SysUsers.Name,
                             GoodsBigKindId = c.GoodsBigKindId,
                           }).ToList();
        }

        return new ObjectResult(GoodsKindList);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }


    /// <summary>
    /// 获取当前登陆用户下的商品子类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetKind([FromBody] PageDto pageDto)
    {
      try
      {
        var id = pageDto.Cid;
        GetPageDto<List<GoodsKindDto>> returnData = new GetPageDto<List<GoodsKindDto>>();
        if (id == 1)
        {
          if(pageDto.GoodsBigKindId == 0)
          {
            returnData.TotalCount = _context.GoodsKinds.Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsKindList = (from c in _context.GoodsKinds
                                   select new GoodsKindDto()
                                   {
                                     Name = c.Name,
                                     Index = c.Index,
                                     SysUserId = c.SysUserId,
                                     SysUserName = c.SysUsers.Name,
                                     GoodsKindId = c.GoodsKindId,
                                     GoodsBigKindId = c.GoodsBigKindId,
                                     Img = c.Img
                                   }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsKindList;
            }
          }
          else
          {
            returnData.TotalCount = _context.GoodsKinds.Where(x=>x.GoodsBigKindId == pageDto.GoodsBigKindId).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsKindList = (from c in _context.GoodsKinds
                                   where c.GoodsBigKindId == pageDto.GoodsBigKindId
                                   select new GoodsKindDto()
                                   {
                                     Name = c.Name,
                                     Index = c.Index,
                                     SysUserId = c.SysUserId,
                                     SysUserName = c.SysUsers.Name,
                                     GoodsKindId = c.GoodsKindId,
                                     GoodsBigKindId = c.GoodsBigKindId,
                                     Img = c.Img
                                   }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsKindList;
            }
          }
        
        }
        else
        {
          if (pageDto.GoodsBigKindId == 0)
          {
            returnData.TotalCount = _context.GoodsKinds.Where(x=>x.SysUserId == id).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsKindList = (from c in _context.GoodsKinds
                                   where c.SysUserId == id
                                   select new GoodsKindDto()
                                   {
                                     Name = c.Name,
                                     Index = c.Index,
                                     SysUserId = c.SysUserId,
                                     SysUserName = c.SysUsers.Name,
                                     GoodsKindId = c.GoodsKindId,
                                     GoodsBigKindId = c.GoodsBigKindId,
                                     Img = c.Img
                                   }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsKindList;
            }
          }
          else
          {
            returnData.TotalCount = _context.GoodsKinds.Where(x => x.GoodsBigKindId == pageDto.GoodsBigKindId && x.SysUserId == id).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsKindList = (from c in _context.GoodsKinds
                                   where c.GoodsBigKindId == pageDto.GoodsBigKindId && c.SysUserId == id
                                   select new GoodsKindDto()
                                   {
                                     Name = c.Name,
                                     Index = c.Index,
                                     SysUserId = c.SysUserId,
                                     SysUserName = c.SysUsers.Name,
                                     GoodsKindId = c.GoodsKindId,
                                     GoodsBigKindId = c.GoodsBigKindId,
                                     Img = c.Img
                                   }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsKindList;
            }
          }
        }

        return new ObjectResult(returnData);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 小程序端获取当前登陆用户下的商品种类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetUserKind(long SysUserId,long GoodsBigKindId)
    {
      var UserKind = from a in _context.GoodsKinds
                     where a.SysUserId == SysUserId && a.GoodsBigKindId == GoodsBigKindId
                     select new GoodsKindDto()
                     {
                       GoodsKindId = a.GoodsKindId,
                       GoodsBigKindId = a.GoodsBigKindId,
                       Name = a.Name,
                       Index = a.Index,
                       Img = a.Img,
                       CreatDate = a.CreatDate,
                       UpDate = a.UpDate,
                       SysUserId = a.SysUserId, 
                     };
      UserKind.ToList();
      return Ok(UserKind);
    }
    /// <summary>
    /// 添加商品小类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult PostKind([FromBody] GoodsKindDto model)
    {
      try
      {
        var Kind = new GoodsKind()
        {
          GoodsBigKindId = model.GoodsBigKindId,
          SysUserId = model.SysUserId,
          Name = model.Name,
          Index = model.Index,
          Img = model.Img,
          CreatDate = DateTime.Now,
          UpDate = DateTime.Now,
        };


        _context.GoodsKinds.Add(Kind);
        _context.SaveChanges();
        return Ok("添加成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 获取某一商品种类信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult EditGoodsKind([FromBody] GoodsKindDto model)
    {
      try
      {
        var KindInfos = _context.GoodsKinds.Where(x => x.GoodsKindId == model.GoodsKindId).FirstOrDefault();
        return Ok(KindInfos);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 修改产品类型
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult ModifyKind([FromBody] GoodsKindDto model)
    {


      try
      {
        var GoodKindInfo = _context.GoodsKinds.Where(x => x.GoodsKindId == model.GoodsKindId).FirstOrDefault();
        GoodKindInfo.Name = model.Name;
        GoodKindInfo.Index = model.Index;
        GoodKindInfo.Img = model.Img;
        GoodKindInfo.GoodsBigKindId = model.GoodsBigKindId;
        GoodKindInfo.UpDate = DateTime.Now;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 删除产品大类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpDelete]
    public IActionResult DeleteGoodsKind(long id)
    {
      try
      {
        var GoodInfo = _context.GoodsKinds.Where(x => x.GoodsKindId == id).FirstOrDefault();
        _context.Remove(GoodInfo);
        _context.SaveChanges();
        return Ok("删除成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);

      }
    }
  }
}


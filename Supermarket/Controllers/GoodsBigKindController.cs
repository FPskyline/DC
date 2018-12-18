using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using static Supermarket.Models.InitModels;
using Supermarket.Models;
using Microsoft.EntityFrameworkCore;
using Supermarket.Dto;
using Microsoft.AspNetCore.Hosting;

namespace Supermarket.Controllers
{
  /// <summary>
  /// 商品大类控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class GoodsBigKindController : Controller
  {
    private SupermarketDbContext _context;

    public readonly IHostingEnvironment _Env;
    /// <summary>
    /// 配置数据库与文件流参数
    /// </summary>
    /// <param name="context"></param>
    /// <param name="env"></param>
    public GoodsBigKindController(SupermarketDbContext context, IHostingEnvironment env)
    {
      _context = context;
      _Env = env;
    }


    /// <summary>
    /// 获取当前登陆用户下的商品大类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetKind([FromBody] PageDto pageDto)
    {
      try
      {
        var id = pageDto.Cid;
        GetPageDto<List<GoodsBigKindDto>> returnData = new GetPageDto<List<GoodsBigKindDto>>();
        if (id == 1)
        {
          returnData.TotalCount = _context.GoodsBigKinds.Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var GoodsKindList = (from c in _context.GoodsBigKinds
                                 select new GoodsBigKindDto()
                                 {
                                   Name =c.Name,
                                   Index = c.Index,
                                   SysUserId = c.SysUserId,
                                   SysUserName = c.SysUsers.Name,
                                   GoodsBigKindId = c.GoodsBigKindId,
                                   Img = c.Img
                                 }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = GoodsKindList;
          }
        }
        else
        {
          returnData.TotalCount = _context.GoodsBigKinds.Where(x => x.SysUserId == id).Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var GoodsKindList = (from c in _context.GoodsBigKinds
                                 where c.SysUserId == id
                                 select new GoodsBigKindDto()
                                 {
                                   Name = c.Name,
                                   Index = c.Index,
                                   SysUserId = c.SysUserId,
                                   GoodsBigKindId = c.GoodsBigKindId,
                                   Img = c.Img
                                 }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = GoodsKindList;
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
    /// 小程序端获取当前登陆用户下的商品大类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetUserBigKind(long SysUserId)
    {
      var UserBigKind = from a in _context.GoodsBigKinds
                     where a.SysUserId == SysUserId
                     select new GoodsBigKindDto()
                     {
                       GoodsBigKindId = a.GoodsBigKindId,
                       Name = a.Name,
                       Index = a.Index,
                       Img = a.Img,
                       CreatDate = a.CreatDate,
                       UpDate = a.UpDate,
                       SysUserId = a.SysUserId,
                     };
      UserBigKind.ToList();
      return Ok(UserBigKind);
    }
    /// <summary>
    /// 添加商品种类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult PostKind([FromBody] GoodsBigKindDto model)
    {
     try
      {
        var Kind = new GoodsBigKind()
        {
            SysUserId=model.SysUserId,
            Name = model.Name,
            Index =model.Index,
            Img=model.Img,
            CreatDate=DateTime.Now,
            UpDate= DateTime.Now,
        };


        _context.GoodsBigKinds.Add(Kind);
        _context.SaveChanges();
        return Ok("添加成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
        /// <summary>
        /// 获取某一商品大类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditGoodsKind([FromBody] GoodsBigKindDto model)
        {
            try
            {
                var KindInfos = _context.GoodsBigKinds.Where(x => x.GoodsBigKindId == model.GoodsBigKindId).FirstOrDefault();
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
    public IActionResult ModifyKind([FromBody] GoodsBigKindDto model)
    {


      try
      {
        var GoodKindInfo = _context.GoodsBigKinds.Where(x => x.GoodsBigKindId == model.GoodsBigKindId).FirstOrDefault();
        GoodKindInfo.Name = model.Name;
        GoodKindInfo.Index = model.Index;
        GoodKindInfo.Img = model.Img;
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
        var GoodInfo = _context.GoodsBigKinds.Where(x => x.GoodsBigKindId == id).FirstOrDefault();
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


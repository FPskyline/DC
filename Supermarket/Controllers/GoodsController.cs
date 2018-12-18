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
  /// 商品详情控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class GoodsController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 数据库连接
    /// </summary>
    /// <param name="context"></param>
    public GoodsController(SupermarketDbContext context)
    {
      _context = context;
    }
    /// <summary>
    /// 获取当前登陆用户下的商品种类
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
                                 }).OrderBy(d => d.Index).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = GoodsKindList;
          }
        }
        else
        {
          returnData.TotalCount = _context.GoodsKinds.Where(x => x.SysUserId == id).Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var GoodsKindList = (from c in _context.GoodsKinds
                                 where c.SysUserId == id
                                 select new GoodsKindDto()
                                 {
                                   Name = c.Name,
                                   Index = c.Index,
                                   SysUserId = c.SysUserId,
                                   GoodsKindId = c.GoodsKindId,
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
    /// 获取当前商品种类下的所有商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetGoods([FromBody] PageDto pageDto)
    {
      try
      {
        var id = pageDto.Cid;
        var GoodId = pageDto.GoodsKindId;
        GetPageDto<List<GoodsDto>> returnData = new GetPageDto<List<GoodsDto>>();
        if (id == 1)
        {
          if (GoodId == 0)
          {
            returnData.TotalCount = _context.Goodss.Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsList = (from d in _context.Goodss
                               select new GoodsDto()
                               {
                                 Name = d.Name,
                                 SysUserId = d.SysUserId,
                                 GoodsId = d.GoodsId,
                                 Img = d.Img,
                                 NewPrice = d.NewPrice,
                                 OldPrice = d.OldPrice,
                                 Notice = d.Notice,
                                 Comment1 = d.Comment1,
                                 Comment2 = d.Comment2,
                               }).OrderByDescending(d => d.GoodsId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsList;
            }
          }
          else
          {
            returnData.TotalCount = _context.Goodss.Where(x => x.GoodsKindId == GoodId).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsList = (from d in _context.Goodss
                               where d.GoodsKindId == GoodId
                               select new GoodsDto()
                               {
                                 Name = d.Name,
                                 SysUserId = d.SysUserId,
                                 GoodsId = d.GoodsId,
                                 Img = d.Img,
                                 NewPrice = d.NewPrice,
                                 OldPrice = d.OldPrice,
                                 Notice = d.Notice,
                                 Comment1 = d.Comment1,
                                 Comment2 = d.Comment2,
                               }).OrderByDescending(d => d.GoodsId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsList;
            }
          }
        }
        else
        {
          if (GoodId == 0)
          {
            returnData.TotalCount = _context.Goodss.Where(x => x.SysUserId == id).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsList = (from d in _context.Goodss
                               where d.SysUserId == id
                               select new GoodsDto()
                               {
                                 Name = d.Name,
                                 SysUserId = d.SysUserId,
                                 GoodsId = d.GoodsId,
                                 Img = d.Img,
                                 NewPrice = d.NewPrice,
                                 OldPrice = d.OldPrice,
                                 Notice = d.Notice,
                                 Comment1 = d.Comment1,
                                 Comment2 = d.Comment2,
                               }).OrderByDescending(d => d.GoodsId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsList;
            }
          }
          else
          {
            returnData.TotalCount = _context.Goodss.Where(x => x.SysUserId == id && x.GoodsKindId == GoodId).Count();
            if (pageDto.Page >= 1 && pageDto.Number > 0)
            {
              var GoodsList = (from d in _context.Goodss
                               where d.SysUserId == id
                               where d.GoodsKindId == GoodId
                               select new GoodsDto()
                               {
                                 Name = d.Name,
                                 SysUserId = d.SysUserId,
                                 GoodsId = d.GoodsId,
                                 Img = d.Img,
                                 NewPrice = d.NewPrice,
                                 OldPrice = d.OldPrice,
                                 Notice = d.Notice,
                                 Comment1 = d.Comment1,
                                 Comment2 = d.Comment2,
                               }).OrderByDescending(d => d.GoodsId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
              returnData.BigField = GoodsList;
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
    /// 新增商品
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult PostGoods([FromBody] GoodsDto model)
    {
      try
      {
        var Goods = new Goods()
        {
          SysUserId = model.SysUserId,
          GoodsKindId = model.GoodsKindId,
          NewPrice = model.NewPrice,
          OldPrice = model.OldPrice,
          Notice = model.Notice,
          Name = model.Name,
          Img = model.Img,
          CreatDate = DateTime.Now,
          Comment1 = "上架",
          Comment2 = model.Comment2
        };
        _context.Goodss.Add(Goods);
        _context.SaveChanges();
        return Ok("添加成功");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 删除产品小类
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpDelete]
    public IActionResult DeleteGoods(long id)
    {
      try
      {
        var GoodInfo = _context.Goodss.Where(x => x.GoodsId == id).FirstOrDefault();
        _context.Remove(GoodInfo);
        _context.SaveChanges();
        return Ok("删除成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);

      }
    }
    /// <summary>
    /// 获取某一商品详情信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult EditGoods([FromBody] GoodsDto model)
    {
      try
      {
        var GoodsInfo = _context.Goodss.Where(x => x.GoodsId == model.GoodsId).FirstOrDefault();
        return Ok(GoodsInfo);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 修改产品信息
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult Modify([FromBody] GoodsDto model)
    {
      try
      {

        var GoodInfo = _context.Goodss.Where(x => x.GoodsId == model.GoodsId).FirstOrDefault();
        GoodInfo.Name = model.Name;
        GoodInfo.GoodsKindId = model.GoodsKindId;
        GoodInfo.NewPrice = model.NewPrice;
        GoodInfo.OldPrice = model.OldPrice;
        GoodInfo.Notice = model.Notice;
        GoodInfo.Img = model.Img;
        GoodInfo.UpDate = DateTime.Now;
        GoodInfo.Comment1 = model.Comment1;
        GoodInfo.Comment2 = model.Comment2;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 小程序端获取商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetUserGoods(GoodsDto model)
    {
      //var TotalCount = _context.Goodss.Where(x => x.SysUserId == model.SysUserId && x.GoodsKindId == model.GoodsKindId).Count();
      var UserGoods = (from a in _context.Goodss
                       where a.SysUserId == model.SysUserId && a.GoodsKindId == model.GoodsKindId && a.Comment1 == "上架"
                       select new GoodsDto()
                       {
                         GoodsId = a.GoodsId,
                         Name = a.Name,
                         Img = a.Img,
                         Notice = a.Notice,
                         NewPrice = a.NewPrice,
                         OldPrice = a.OldPrice,
                         GoodsKindId = a.GoodsKindId,
                       }); 
      //.OrderBy(d => d.GoodsId).Skip(model.searchPageNum).Take(model.callbackcount).AsNoTracking().ToList();
      UserGoods.ToList();
      return Ok(UserGoods);
    }
    /// <summary>
    /// 小程序端获取单一商品详情
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult Getdetail(long GoodsId)
    {
      var UserGoods = (from a in _context.Goodss
                       where a.GoodsId == GoodsId
                       select new GoodsDto()
                       {
                         GoodsId = a.GoodsId,
                         Name = a.Name,
                         Img = a.Img,
                         Notice = a.Notice,
                         NewPrice = a.NewPrice,
                         OldPrice = a.OldPrice,
                         GoodsKindId = a.GoodsKindId,
                       }).FirstOrDefault();
      return Ok(UserGoods);
    }
    /// <summary>
    /// 模板过滤，取出模板商品别表
    /// </summary>
    /// <param name="Name">搜索框输入名字</param>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult SearchGoods(string Name)
    {
      try
      {
        var GoodsList = (from d in _context.Goodss
                         where d.Name.Contains(Name)
                         select new GoodsDto()
                         {
                           Name = d.Name,
                           SysUserId = d.SysUserId,
                           GoodsId = d.GoodsId,
                           Img = d.Img,
                           NewPrice = d.NewPrice,
                           OldPrice = d.OldPrice,
                           Notice = d.Notice,
                           IsChecked = false
                         }).OrderBy(d => d.GoodsId).ToList();
        return Ok(GoodsList);

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 新增产品(通过模板)
    /// </summary>
    /// <param name="goodsDtos">模板商品列表</param>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult PostGoodsTmp([FromBody] List<GoodsDto> goodsDtos)
    {
      try
      {
        foreach (var p in goodsDtos)
        {
          var Goods = new Goods()
          {
            SysUserId = p.SysUserId,
            GoodsKindId = p.GoodsKindId,
            NewPrice = p.NewPrice,
            Name = p.Name,
            Img = p.Img,
            CreatDate = DateTime.Now,
            Comment1 = "上架"
          };
          _context.Goodss.Add(Goods);
        }
        _context.SaveChanges();
        return Ok("添加成功");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 搜索所有商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult SearchAllGoods([FromBody] PageDto pageDto)
    {
      try
      {
        var id = pageDto.Cid;
        var GoodId = pageDto.GoodsKindId;
        GetPageDto<List<GoodsDto>> returnData = new GetPageDto<List<GoodsDto>>();
        returnData.TotalCount = _context.Goodss.Where(x => x.SysUserId == id && x.Name.Contains(pageDto.SearchName)).Count();
        if (pageDto.Page >= 1 && pageDto.Number > 0)
        {
          var GoodsList = (from d in _context.Goodss
                           where d.SysUserId == id && d.Name.Contains(pageDto.SearchName)
                           select new GoodsDto()
                           {
                             Name = d.Name,
                             SysUserId = d.SysUserId,
                             GoodsId = d.GoodsId,
                             GoodsKindId = d.GoodsKindId,
                             GoodsBigKindId = d.GoodsKinds.GoodsBigKindId,
                             Img = d.Img,
                             NewPrice = d.NewPrice,
                             OldPrice = d.OldPrice,
                             Notice = d.Notice,
                             Comment1 = d.Comment1,
                             CreatDate = d.CreatDate
                           }).OrderBy(d => d.GoodsId).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
          returnData.BigField = GoodsList;
        }

        return new ObjectResult(returnData);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 小程序搜索商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult AppSearchGoods(AppSearchGoodDto appSearchGoodDto)
    {
      try
      {
        var GoodsList = (from d in _context.Goodss
                         where d.Name.Contains(appSearchGoodDto.KeyWords) && d.SysUserId == appSearchGoodDto.SysUserId && d.Comment1 == "上架"
                         select new GoodsDto()
                         {
                           Name = d.Name,
                           SysUserId = d.SysUserId,
                           GoodsId = d.GoodsId,
                           Img = d.Img,
                           NewPrice = d.NewPrice,
                           OldPrice = d.OldPrice,
                           Notice = d.Notice,
                         }).OrderBy(d => d.GoodsId).AsNoTracking().ToList();

        return Ok(GoodsList);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 小程序获取当前用户全部商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult WeGetAllGoods(long SysUserId)
    {
      try
      {
        var GoodsList = _context.Goodss.Where(x => x.SysUserId == SysUserId).ToList();

        return Ok(GoodsList);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}

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
using Newtonsoft.Json;

namespace Applet.Controllers
{
  /// <summary>
  /// 奖品控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class LotteryController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public LotteryController(SupermarketDbContext context)
    {
      _context = context;
    }
    /// <summary>
    /// 获取奖品列表
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetLottery([FromBody] PageDto pageDto)
    {
      try
      {
        GetPageDto<List<LotteryDto>> returnData = new GetPageDto<List<LotteryDto>>();
        if(pageDto.Cid == 1)
        {
          returnData.TotalCount = _context.Lotterys.Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var LotteryList = (from b in _context.Lotterys
                               select new LotteryDto()
                               {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Probability = b.Probability,
                                 CreatDate = b.CreatDate,
                                 EditDate = b.EditDate
                               }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = LotteryList;
          }
        }
        else
        {
          returnData.TotalCount = _context.Lotterys.Where(x=>x.SysUserId == pageDto.Cid).Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var LotteryList = (from b in _context.Lotterys
                               where b.SysUserId == pageDto.Cid
                               select new LotteryDto()
                               {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Probability = b.Probability,
                                 CreatDate = b.CreatDate,
                                 EditDate = b.EditDate
                               }).OrderByDescending(d => d.Id).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = LotteryList;
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
    /// 新增奖品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult CreateLottery([FromBody] LotteryDto lotteryDto)
    {
      try
      {

          var LotteryInfo = new Lottery()
          {
            Name = lotteryDto.Name,
            Probability = lotteryDto.Probability,
            CreatDate = DateTime.Now,
            SysUserId = lotteryDto.SysUserId
          };
          _context.Lotterys.Add(LotteryInfo);
          _context.SaveChanges();
          return Ok("新增成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
      
    }
    /// <summary>
    /// 修改奖品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult EditLottery([FromBody] LotteryDto lotteryDto)
    {
      try
      {
        var lotteryinfo = _context.Lotterys.Where(x => x.Id == lotteryDto.Id).FirstOrDefault();
        if(lotteryinfo == null)
        {
          return BadRequest("无该奖品");
        }
        lotteryinfo.Name = lotteryDto.Name;
        lotteryinfo.Probability = lotteryDto.Probability;
        lotteryinfo.EditDate = DateTime.Now;    
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }
    /// <summary>
    /// 删除奖品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpDelete]
    public IActionResult DelLottery(long id)
    {
      try
      {
        var lotteryinfo = _context.Lotterys.Where(x => x.Id == id).FirstOrDefault();
        if (lotteryinfo == null)
        {
          return BadRequest("无该奖品");
        }
        _context.Lotterys.Remove(lotteryinfo);
        _context.SaveChanges();
        return Ok("删除成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }
    /// <summary>
    /// 测试随机数
    /// </summary>
    /// <returns>返回版本号</returns>
    [HttpGet]
    public IActionResult TestRan(string str)
    {
      try
      {
        long res = 0;
        Random ran = new Random();
        int n = ran.Next(1, 10001);

        List<LotteryDto> lotteryDtoList = JsonConvert.DeserializeObject<List<LotteryDto>>(str);

        var a = lotteryDtoList[0].Probability;
        var b = lotteryDtoList[1].Probability;
        var c = lotteryDtoList[2].Probability;
        var d = lotteryDtoList[3].Probability;
        var e = lotteryDtoList[4].Probability;
        var f = lotteryDtoList[5].Probability;

        if (n > 0 && n <= a)
        {
          res = lotteryDtoList[0].Id;
        }
        else if (n >= a && n < a + b)
        {
          res = lotteryDtoList[1].Id;
        }
        else if (n >= a + b && n <= a + b + c)
        {
          res = lotteryDtoList[2].Id;
        }
        else if (n >= a + b + c && n <= a + b + c + d)
        {
          res = lotteryDtoList[3].Id;
        }
        else if (n >= a + b + c + d && n <= a + b + c + d + e)
        {
          res = lotteryDtoList[4].Id;
        }
        else if (n >= a + b + c + d + e && n <= a + b + c + d + e + f)
        {
          res = lotteryDtoList[5].Id;
        }
        return Ok(res);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 获取某一商家奖品
    /// </summary>
    /// <returns>返回版本号</returns>
    [HttpGet]
    public IActionResult GetOneLottery(long id)
    {
      try
      {
        var lotteryinfo = _context.Lotterys.Where(x => x.SysUserId == id).ToList();    

        return Ok(lotteryinfo);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}

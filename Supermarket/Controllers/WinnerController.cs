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
  /// 中奖控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class WinnerController : Controller
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public WinnerController(SupermarketDbContext context)
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
    /// 新增中奖者
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult CreateWinner([FromBody] WinnerDto winnerDto)
    {
      try
      {

          var WinnerInfo = new Winner()
          {            
            OpenId = winnerDto.OpenId,
            WinDate = DateTime.Now,
            SysUserId = winnerDto.SysUserId,
            Phone = winnerDto.Phone,
            LotteryId = winnerDto.LotteryId
          };
          _context.Winners.Add(WinnerInfo);
          _context.SaveChanges();
          return Ok("新增成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
      
    }
    /// <summary>
    /// 获取本期中奖名单
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetWinners()
    {
      var info = (from a in _context.Winners
                  where a.WinDate.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd")
                  select new WinnerDto()
                  {
                    Id = a.Id,
                    Phone = a.Phone,
                    LotteryId = a.LotteryId,
                    LotteryName = _context.Lotterys.Where(x=>x.Id == a.LotteryId).FirstOrDefault().Name

                  }).OrderByDescending(x=>x.Id).ToList();
      
      return Ok(info);
    }
  }
}

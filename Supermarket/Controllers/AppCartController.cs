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
    public class AppCartController : Controller
    {
        private SupermarketDbContext _context;
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <param name="context"></param>
        public AppCartController(SupermarketDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取当前登陆用户下的购物车
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetCart(string OpenId)
        {
            try
            {
                var UserId = _context.User.Where(i => i.OpenId ==OpenId).FirstOrDefault().UserId;
                var shoppingCart = (from a in _context.AppCarts
                                    where a.UserId == UserId 
                                    select new AppCartDto()
                                   {
                                       Id = a.Id,
                                       Num = a.Num,
                                       GoodsId = a.GoodsId,
                                       GoodsKindId = a.GoodsId,
                                       Img = a.Goods.Img,
                                       Name = a.Goods.Name,
                                       NewPrice =a.Goods.NewPrice,
                                       OldPrice = a.Goods.OldPrice,
                                       Notice = a.Goods.Notice,
                                       UserId = a.UserId,
                                       UpDate = a.UpDate,
                                       CreatDate = a.CreatDate,
                                       Seclect=a.Seclect
                                    }).OrderBy(i => i.CreatDate).ToList();


                return new ObjectResult(shoppingCart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 订单详情购物车获取选中的商品
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpGet]
        public IActionResult GetOrderDetail(string OpenId)
        {
            try
            {
                var UserId = _context.User.Where(i => i.OpenId == OpenId).FirstOrDefault().UserId;
                var shoppingCart = (from a in _context.AppCarts
                                    where a.UserId == UserId && a.Seclect == true
                                    select new AppCartDto()
                                    {
                                        Id = a.Id,
                                        Num = a.Num,
                                        GoodsId = a.GoodsId,
                                        GoodsKindId = a.GoodsId,
                                        Img = a.Goods.Img,
                                        Name = a.Goods.Name,
                                        NewPrice = a.Goods.NewPrice,
                                        OldPrice = a.Goods.OldPrice,
                                        Notice = a.Goods.Notice,
                                        UserId = a.UserId,
                                        UpDate = a.UpDate,
                                        CreatDate = a.CreatDate,
                                        Seclect = a.Seclect
                                    }).OrderBy(i => i.CreatDate).ToList();


                return new ObjectResult(shoppingCart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    /// <summary>
    /// 订单详情购物车获取选中的商品
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetOrderOneDetail(long GoodsId)
    {
      try
      {
        //var UserId = _context.User.Where(i => i.OpenId == OpenId).FirstOrDefault().UserId;
        var shoppingCart = (from a in _context.Goodss
                            where a.GoodsId == GoodsId
                            select new AppCartDto()
                            {
                             // Id = a.Id,
                              Num = 1,
                              GoodsId = a.GoodsId,
                              //GoodsKindId = a.GoodsId,
                              Img = a.Img,
                              Name = a.Name,
                              NewPrice = a.NewPrice,
                             //OldPrice = a.Goods.OldPrice,
                             // Notice = a.Goods.Notice,
                              //UserId = a.UserId,
                             // UpDate = a.UpDate,
                             // CreatDate = a.CreatDate,
                              //Seclect = a.Seclect
                            }).OrderBy(i => i.CreatDate).ToList();


        return new ObjectResult(shoppingCart);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 添加购物车
    /// </summary>
    /// <param name="appCartDto"></param>
    /// <returns></returns>
    [HttpPost]
        public IActionResult AddCart(AppCartDto appCartDto)
        {
            try
            {
                var UserId = _context.User.Where(i => i.OpenId == appCartDto.OpenId).FirstOrDefault().UserId;
                var checkObj = _context.AppCarts.Where(x => x.GoodsId == appCartDto.GoodsId && x.UserId ==UserId).FirstOrDefault();
                //判断该人是否购物车里添加过该商品
                if (checkObj == null)
                {
                    var AppCartObj = new AppCart();
                    AppCartObj.Num = appCartDto.Num;
                    AppCartObj.Seclect = true;
                    AppCartObj.GoodsId = appCartDto.GoodsId;
                    AppCartObj.UserId =UserId;
                    AppCartObj.CreatDate = DateTime.Now;
                    AppCartObj.UpDate = DateTime.Now;
                    _context.AppCarts.Add(AppCartObj);
                }
                else {
                    checkObj.Num = appCartDto.Num + checkObj.Num;
                    checkObj.UpDate = DateTime.Now;
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
        /// 删除购物车
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DelCart(long Id)
        {
            try
            {
                var shoppingCart = _context.AppCarts.Where(x => x.Id == Id).FirstOrDefault();
                _context.AppCarts.Remove(shoppingCart);
                _context.SaveChanges();
                return Ok("删除成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 修改购物车件数
        /// </summary>
        /// <param name="appCartDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ModifyCart(AppCartDto appCartDto)
        {
            try
            {
                var AppCartObj = _context.AppCarts.Where(x => x.Id == appCartDto.Id).FirstOrDefault();
                if (AppCartObj == null) {
                    return BadRequest("没有该购物车");
                }
                AppCartObj.Num = appCartDto.Num;
                AppCartObj.UpDate = DateTime.Now;
                _context.SaveChanges();
                return Ok("修改成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        /// <summary>
        /// 获取当前登陆用户下的购物车物品数量
        /// </summary>
        /// <returns>返回结果</returns>
        [HttpPost]
        public IActionResult GetCartNum(AppCartDto appCartDto)
        {
            try
            {
                var UserId = _context.User.Where(i => i.OpenId == appCartDto.OpenId).FirstOrDefault().UserId;
                var shoppingCart = _context.AppCarts.Where(i => i.UserId == UserId && i.GoodsId== appCartDto.GoodsId).FirstOrDefault();
                if (shoppingCart==null)
                {
                    return Ok("0");
                }
                else
                {
                    return Ok(shoppingCart.Num);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
      /// <summary>
      /// 获取当前登陆用户下的购物车物品数量
      /// </summary>
      /// <returns>返回结果</returns>
      [HttpPost]
      public IActionResult GetCartAllNum(AppCartDto appCartDto)
      {
        try
        {
        var number = 0;
          var UserId = _context.User.Where(i => i.OpenId == appCartDto.OpenId).FirstOrDefault().UserId;
          var cartlist = _context.AppCarts.Where(i => i.UserId == UserId).ToList();
          foreach(var p in cartlist)
          {
            number += p.Num;
          }
          return Ok(number);


        }
        catch (Exception ex)
        {
          return StatusCode(500, ex.Message);
        }
      }
    /// <summary>
    /// 单选择购物车
    /// </summary>
    /// <param name="CartId"></param>
    /// <returns></returns>
    [HttpGet]
        public IActionResult SelectAppCart(long CartId) {
            try {
                var SelectedCart = _context.AppCarts.Where(x => x.Id == CartId).FirstOrDefault();
                SelectedCart.Seclect = !SelectedCart.Seclect;
                _context.SaveChanges();
                return Ok("选择成功");
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// 单选择购物车
        /// </summary>
        /// <param name="selectAllDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SelectAllAppCart(SelectAllDto selectAllDto)
        {
            try
            {
                var userId = _context.User.Where(x => x.OpenId == selectAllDto.OpenId).FirstOrDefault().UserId;
                var SelectedCart = _context.AppCarts.Where(x=>x.UserId== userId).ToList();
                SelectedCart.ForEach(i=>i.Seclect = selectAllDto.Select);
 
                _context.SaveChanges();
                return Ok("选择成功");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

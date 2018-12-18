using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Supermarket.Models.InitModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Supermarket.Dto;
using Supermarket.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Supermarket.Utility;
using System.Net;

namespace Supermarket.Controllers
{
  /// <summary>
  /// 相片控制器
  /// </summary>
  [Route("api/[controller]/[action]")]
  public class OrderController : Controller
  {
    private SupermarketDbContext _context;
    private readonly IHostingEnvironment _hostingEnvironment;
    string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="hostingEnvironment"></param>
    public OrderController(SupermarketDbContext context, IHostingEnvironment hostingEnvironment)
    {
      _context = context;
      _hostingEnvironment = hostingEnvironment;
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult PostImg()
    {
      var files = Request.Form.Files;
      long size = files.Sum(f => f.Length);
      string fileName = "";
      //size > 100MB refuse upload !
      if (size > 104857600)
      {
        return BadRequest("pictures total size > 100MB , server refused !");
      }


      foreach (var file in files)
      {
        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

        string filePath = _hostingEnvironment.WebRootPath + $@"\Files\Pictures\";

        if (!Directory.Exists(filePath))
        {
          Directory.CreateDirectory(filePath);
        }

        string suffix = fileName.Split('.')[1];

        if (!pictureFormatArray.Contains(suffix))
        {
          return BadRequest("the picture format not support ! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'.");
        }

        fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + suffix;

        string fileFullName = filePath + fileName;

        using (FileStream fs = System.IO.File.Create(fileFullName))
        {
          file.CopyTo(fs);
          fs.Flush();
        }

      }
      return Ok(fileName);
    }
    //public HttpResponseMessage GetPic(string filename)
    //{

    //  string filePath = _hostingEnvironment.WebRootPath + $@"\Files\Pictures\";
    //  var picfile = Directory.EnumerateFiles(filePath, filename, SearchOption.TopDirectoryOnly).FirstOrDefault();
    //  if (picfile == null)
    //  {
    //    return new HttpResponseMessage(HttpStatusCode.NotFound);
    //  }
    //  FileStream fileStream = new FileStream(picfile, FileMode.Open, FileAccess.Read, FileShare.Read);
    //  HttpResponseMessage response = new HttpResponseMessage();
    //  response.Content = new StreamContent(fileStream);
    //  response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeGuesser.GuessMimeType(picfile));
    //  response.Content.Headers.ContentLength = new FileInfo(picfile).Length;
    //  return response;
    //}

    /// <summary>
    /// 新增订单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult CreateOrder(OrderDto model)
    {
      try
      {
        Token Fun = new Token(_context);
        var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
        var UserId = _context.User.Where(i => i.OpenId == model.OpenId).FirstOrDefault().UserId;
        if (model.PayType == PayType.货到付款)
        {
          var Order = new Order()
          {
            OrderNo = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "" + UserId,
            Price = model.Price,
            PayType = model.PayType,
            PayState = PayState.未付款,
            CreatDate = DateTime.Now,
            UserId = UserId,
            SysUserId = model.SysUserId,
            AddressId = model.AddressId,
            Comments = model.Comments

          };
          var goodsID = model.GoodsId.Replace("[", "").Replace("]", "").Split(',');
          _context.Orders.Add(Order);
          foreach (var p in goodsID)
          {
            var num = 1;
            if (model.ordertype == "wu")
            {
              num = _context.AppCarts.Where(i => i.GoodsId == Convert.ToInt64(p) && i.UserId == UserId).FirstOrDefault().Num;
            }
            //if(_context.AppCarts.Where(i => i.UserId == UserId).ToList().Contains(Convert.ToInt64(p)))

            var ordergoods = new OrderGoods()
            {
              OrderId = Order.OrderId,
              GoodsId = Convert.ToInt64(p),
              Num = num

            };
            _context.OrderGoods.Add(ordergoods);

          }
          var CartInfo = _context.AppCarts.Where(x => x.UserId == UserId).ToList();
          if (CartInfo.Count != 0)
          {
            _context.AppCarts.RemoveRange(CartInfo);
            _context.SaveChanges();
          }
          else
          {
            _context.SaveChanges();
          }


          var Tem = new TemDto();
          var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == model.SysUserId).FirstOrDefault();

          var Good = _context.OrderGoods.Where(x => x.OrderId == Order.OrderId).ToList();
          var NT = "";
          foreach (var s in Good)
          {
            var Goodss = _context.Goodss.Where(x => x.GoodsId == s.GoodsId).FirstOrDefault();
            NT = NT + Goodss.Name + "*" + s.Num + "; ";
          }
          Tem.touser = sysUserinfo.Popenid;
          Tem.template_id = "xEh7eyNbqUA4REhnVT-aCCH0-NOwqrHFh4IyqFnkM-g";

          var data1 = new Data1();
          var keyword1 = new Keyword();
          var keyword2 = new Keyword();
          var keyword3 = new Keyword();
          var keyword4 = new Keyword();
          var keyword5 = new Keyword();
          var first = new Keyword();
          var remark = new Keyword();
          Tem.data = data1;
          Tem.data.keyword1 = keyword1;
          Tem.data.keyword2 = keyword2;
          Tem.data.keyword3 = keyword3;
          Tem.data.keyword4 = keyword4;
          Tem.data.keyword5 = keyword5;
          Tem.data.first = first;
          Tem.data.remark = remark;
          Tem.data.first.value = Order.OrderNo;
          Tem.data.keyword1.value = NT;
          Tem.data.keyword2.value = model.Price.ToString();
          Tem.data.keyword3.value = Addrs.ContactName + "   " + Addrs.ContactPhone;
          Tem.data.keyword4.value = Addrs.Addr;
          Tem.data.keyword5.value = model.Comments;
          Tem.data.remark.value ="";

          var Tnc = new TemDto();
          var data2 = new Data1();
          var first1 = new Keyword();
          var keyword11 = new Keyword();
          var keyword21 = new Keyword();
          var keyword31 = new Keyword();
          var keyword41 = new Keyword();
          var keyword51 = new Keyword();
          var keyword61 = new Keyword();
          Tnc.data = data2;
          Tnc.data.keyword1 = keyword11;
          Tnc.data.keyword2 = keyword21;
          Tnc.data.keyword3 = keyword31;
          Tnc.data.keyword4 = keyword41;
          Tnc.data.keyword5 = keyword51;
          Tnc.data.keyword6 = keyword61;

          Tnc.touser = model.OpenId;
          Tnc.template_id = "IOxfg-a_xjT3GSY8RHgF6qybqmV-4MlSM1IOpgwpWWg";
          Tnc.form_id = model.FormId;



          Tnc.data.keyword1.value = Order.OrderNo;
          Tnc.data.keyword2.value = model.Price.ToString();
          Tnc.data.keyword3.value = NT;
          Tnc.data.keyword4.value = DateTime.Now.ToString();
          Tnc.data.keyword5.value = Addrs.Addr;
          Tnc.data.keyword6.value = Addrs.ContactPhone;

          Fun.GetToken(Tem);
          AppToken Text = new AppToken(_context);
          Text.GetAppToken(Tnc);
          Jpush jpush = new Jpush();
          jpush.SendOrder(model.SysUserId,sysUserinfo.Name);


        }
        return Ok("2222！！");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }



    }
    /// <summary>
    /// 获取当前用户下的订单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetAllOrder([FromBody] PageDto pageDto)
    {
      try
      {
        var id = pageDto.Cid;
        GetPageDto<List<OrderDto>> returnData = new GetPageDto<List<OrderDto>>();
        if (id == 1)
        {
          returnData.TotalCount = _context.Orders.Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var OrderList = (from c in _context.Orders
                             select new OrderDto()
                             {
                               OrderId = c.OrderId,
                               Name = c.Name,
                               SysUserId = c.SysUserId,
                               SysUserName = c.SysUsers.Name,
                               OrderNo = c.OrderNo,
                               Price = c.Price,
                               PayType = c.PayType,
                               PayState = c.PayState,
                               CreatDate = c.CreatDate,
                               UserId = c.UserId,
                               AddressId = c.AddressId,
                               OpenId = c.Users.OpenId,
                               UserName = WebUtility.UrlDecode(c.Users.Name),
                             }).OrderByDescending(d => d.CreatDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = OrderList;
          }
        }
        else
        {
          returnData.TotalCount = _context.Orders.Where(x => x.SysUserId == id).Count();
          if (pageDto.Page >= 1 && pageDto.Number > 0)
          {
            var OrderList = (from c in _context.Orders
                             where c.SysUserId == id
                             select new OrderDto()
                             {
                               OrderId = c.OrderId,
                               Name = c.Name,
                               SysUserId = c.SysUserId,
                               OrderNo = c.OrderNo,
                               Price = c.Price,
                               PayType = c.PayType,
                               PayState = c.PayState,
                               CreatDate = c.CreatDate,
                               UserId = c.UserId,
                               OpenId = c.Users.OpenId,
                               AddressId = c.AddressId,
                               UserName = WebUtility.UrlDecode(c.Users.Name),
                             }).OrderByDescending(d => d.CreatDate).Skip((pageDto.Page - 1) * pageDto.Number).Take(pageDto.Number).AsNoTracking().ToList();
            returnData.BigField = OrderList;
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
    /// 修改用户订单状态
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult ModifyOrder([FromBody] OrderDto model)
    {
      try
      {

        var OrderInfo = _context.Orders.Where(x => x.OrderId == model.OrderId).FirstOrDefault();
        OrderInfo.PayState = PayState.交易完成;


        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 获取配送范围是否可送
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetRange(OrderDto model)
    {
      try
      {

        Token Fun = new Token(_context);
        var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
        var n = Fun.HttpMap(Addrs.Addr, model.SysUserId);
        if (n == 1)
        {
          return Ok("可以送");
        }
        else
        {
          return Ok("不在配送范围");
        }
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 是否达到起送价
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetPrice(OrderDto model)
    {
      try
      {
        var GetPrice = _context.SysUsers.Where(i => i.SysUserId == model.SysUserId).FirstOrDefault();
        return Ok(GetPrice);

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// 查询本人订单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetOrder(string OpenId)
    {


      var Order = (from a in _context.User
                   where a.OpenId == OpenId
                   from b in _context.Orders
                   where b.UserId == a.UserId
                   from c in _context.Addresss
                   where c.AddressId == b.AddressId
                   select new OrderDto()
                   {
                     OrderNo = b.OrderNo,
                     Price = b.Price,
                     PayType = b.PayType,
                     PayState = b.PayState,
                     CreatDate = b.CreatDate,
                     UserId = b.UserId,
                     Addr = c.Addr,
                     ContactName = c.ContactName,
                     ContactPhone = c.ContactPhone,
                     GodDtos = (from d in _context.OrderGoods
                                where d.OrderId == b.OrderId
                                from e in _context.Goodss
                                where e.GoodsId == d.GoodsId
                                select new GodDto()
                                {
                                  GodImg = e.Img,
                                  GodName = e.Name,
                                  NewPrice = e.NewPrice,
                                  Num = d.Num
                                }).ToList()
                   }).OrderByDescending(d => d.CreatDate).ToList();
      return Ok(Order);
    }
    /// <summary>
    /// 查询订单详情
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult GetOrderDetail(long orderId)
    {
      var OrderDetail = (from b in _context.Orders
                         where b.OrderId == orderId
                         from c in _context.Addresss
                         where c.AddressId == b.AddressId
                         select new OrderDto()
                         {
                           OrderNo = b.OrderNo,
                           Price = b.Price,
                           PayType = b.PayType,
                           PayState = b.PayState,
                           CreatDate = b.CreatDate,
                           SysUserId = b.SysUserId,
                           SysUserName = b.SysUsers.Name,
                           UserId = b.UserId,
                           UserName = WebUtility.UrlDecode(c.Users.Name),
                           Addr = c.Addr,
                           ContactName = c.ContactName,
                           ContactPhone = c.ContactPhone,
                           GodDtos = (from d in _context.OrderGoods
                                      where d.OrderId == b.OrderId
                                      from e in _context.Goodss
                                      where e.GoodsId == d.GoodsId
                                      select new GodDto()
                                      {
                                        GodImg = e.Img,
                                        GodName = e.Name,
                                        NewPrice = e.NewPrice,
                                        Num = d.Num
                                      }).ToList()
                         }).FirstOrDefault();
      return Ok(OrderDetail);
    }
    /// <summary>
    /// 接单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult OrderReceiving([FromBody] OrderDto model)
    {
      try
      {


        var OrderInfo = _context.Orders.Where(x => x.OrderId == model.OrderId).FirstOrDefault();
        OrderInfo.PayState = model.PayState;

        if (model.PayState == PayState.已接单)
        {

          var SysTem = _context.SysUsers.Where(x => x.SysUserId == OrderInfo.SysUserId).FirstOrDefault();
          var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
          AppToken Text = new AppToken(_context);

          var Fom = _context.FormIds.Where(x => x.UserId == model.UserId).FirstOrDefault();


          var TDN = new TemDto();
          var data2 = new Data1();
          var first1 = new Keyword();
          var keyword11 = new Keyword();
          var keyword21 = new Keyword();
          var keyword31 = new Keyword();
          var keyword41 = new Keyword();
          var keyword51 = new Keyword();
          var keyword61 = new Keyword();
          TDN.data = data2;
          TDN.data.keyword1 = keyword11;
          TDN.data.keyword2 = keyword21;
          TDN.data.keyword3 = keyword31;




          TDN.touser = model.OpenId;
          TDN.template_id = SysTem.TemIdAccept;
          TDN.form_id = Fom.FormIds;
          TDN.data.keyword1.value = model.OrderNo;
          TDN.data.keyword2.value = "已接单";
          TDN.data.keyword3.value = Addrs.ContactPhone;

          Text.GetAppToken(TDN);

          var Time1 = DateTime.Now.AddDays(-7);
          var y = _context.FormIds.Where(x => x.CreatDate < Time1).ToList();
          _context.FormIds.RemoveRange(y);
          _context.Remove(Fom);

          _context.SaveChanges();
          return Ok("修改成功");

        }
        else if (model.PayState == PayState.已发货)
        {
          var SysTem = _context.SysUsers.Where(x => x.SysUserId == OrderInfo.SysUserId).FirstOrDefault();
          var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
          AppToken Text = new AppToken(_context);

          var Fom = _context.FormIds.Where(x => x.UserId == model.UserId).FirstOrDefault();


          var TDN = new TemDto();
          var data2 = new Data1();
          var first1 = new Keyword();
          var keyword11 = new Keyword();
          var keyword21 = new Keyword();
          var keyword31 = new Keyword();
          var keyword41 = new Keyword();
          var keyword51 = new Keyword();
          var keyword61 = new Keyword();
          TDN.data = data2;
          TDN.data.keyword1 = keyword11;
          TDN.data.keyword2 = keyword21;
          TDN.data.keyword3 = keyword31;




          TDN.touser = model.OpenId;
          TDN.template_id = SysTem.TemIdSend;
          TDN.form_id = Fom.FormIds;
          TDN.data.keyword1.value = model.OrderNo;
          TDN.data.keyword2.value = "已发货";
          TDN.data.keyword3.value = model.Price.ToString();

          Text.GetAppToken(TDN);

          var Time1 = DateTime.Now.AddDays(-7);
          var y = _context.FormIds.Where(x => x.CreatDate < Time1).ToList();
          _context.FormIds.RemoveRange(y);
          _context.Remove(Fom);

          _context.SaveChanges();
          return Ok("修改成功");

        }

        else if (model.PayState == PayState.已拒绝)
        {
          var SysTem = _context.SysUsers.Where(x => x.SysUserId == OrderInfo.SysUserId).FirstOrDefault();
          var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
          AppToken Text = new AppToken(_context);

          var Fom = _context.FormIds.Where(x => x.UserId == model.UserId).FirstOrDefault();


          var TDN = new TemDto();
          var data2 = new Data1();
          var first1 = new Keyword();
          var keyword11 = new Keyword();
          var keyword21 = new Keyword();
          var keyword31 = new Keyword();
          var keyword41 = new Keyword();
          var keyword51 = new Keyword();
          var keyword61 = new Keyword();
          TDN.data = data2;
          TDN.data.keyword1 = keyword11;
          TDN.data.keyword2 = keyword21;
          TDN.data.keyword3 = keyword31;




          TDN.touser = model.OpenId;
          TDN.template_id = SysTem.TemIdRefuse;
          TDN.form_id = Fom.FormIds;
          TDN.data.keyword1.value = model.OrderNo;
          TDN.data.keyword2.value = "超出配送范围";
          // TDN.data.keyword3.value = model.Price.ToString();

          Text.GetAppToken(TDN);

          var Time1 = DateTime.Now.AddDays(-7);
          var y = _context.FormIds.Where(x => x.CreatDate < Time1).ToList();
          _context.FormIds.RemoveRange(y);
          _context.Remove(Fom);

          _context.SaveChanges();
          return Ok("修改成功");



        }

        return Ok("");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 查询当前商户订单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult GetAppOrder([FromBody] PageDto pageDto)
    {
      GetPageDto<List<OrderDto>> returnData = new GetPageDto<List<OrderDto>>();
      returnData.TotalCount = _context.Orders.Where(x => x.SysUserId == pageDto.Cid).Count();
      if (pageDto.Page >= 1)
      {
        var OrderList = (from a in _context.Orders
                         where a.SysUserId == pageDto.Cid
                         from b in _context.Addresss
                         where a.AddressId == b.AddressId
                         select new OrderDto()
                         {
                           OrderId = a.OrderId,
                           OrderNo = a.OrderNo,
                           Price = a.Price,
                           PayType = a.PayType,
                           PayState = a.PayState,
                           PayTypestr = a.PayState.ToString(),
                           CreatDate = a.CreatDate,
                           CreatDatestr = a.CreatDate.ToString("yyyy-MM-dd HH:mm:ss"),
                           UserId = a.UserId,
                           Addr = b.Addr,
                           ContactName = b.ContactName,
                           ContactPhone = b.ContactPhone,
                           GodDtos = (from d in _context.OrderGoods
                                      where d.OrderId == a.OrderId
                                      from e in _context.Goodss
                                      where e.GoodsId == d.GoodsId
                                      select new GodDto()
                                      {
                                        GodImg = e.Img,
                                        GodName = e.Name,
                                        NewPrice = e.NewPrice,
                                        Num = d.Num
                                      }).ToList()
                         }).OrderByDescending(d => d.CreatDate).Skip((pageDto.Page - 1) * 15).Take(15).ToList();
        returnData.BigField = OrderList;
      }
      return Ok(returnData);
    }
    /// <summary>
    /// 修改App订单状态
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult ModifyAppOrder(long OrderId)
    {
      try
      {

        var OrderInfo = _context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
        OrderInfo.PayState = PayState.已接单;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 修改App订单状态
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult ModifyApp(long OrderId)
    {
      try
      {

        var OrderInfo = _context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
        OrderInfo.PayState = PayState.已拒绝;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 修改App订单状态
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpGet]
    public IActionResult Modify(long OrderId)
    {
      try
      {

        var OrderInfo = _context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
        OrderInfo.PayState = PayState.交易完成;
        _context.SaveChanges();
        return Ok("修改成功");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    /// <summary>
    /// 新增在线支付订单
    /// </summary>
    /// <returns>返回结果</returns>
    [HttpPost]
    public IActionResult CreateOnlineOrder(OrderDto model)
    {
      try
      {
        Token Fun = new Token(_context);
        var Addrs = _context.Addresss.Where(x => x.AddressId == model.AddressId).FirstOrDefault();
        var UserId = _context.User.Where(i => i.OpenId == model.OpenId).FirstOrDefault().UserId;
        if (model.PayType == PayType.在线支付)
        {
          var Order = new Order()
          {
            OrderNo = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "" + UserId,
            Price = model.Price,
            PayType = model.PayType,
            PayState = PayState.已付款,
            CreatDate = DateTime.Now,
            UserId = UserId,
            SysUserId = model.SysUserId,
            AddressId = model.AddressId,
            Comments = model.Comments
          };
          var goodsID = model.GoodsId.Replace("[", "").Replace("]", "").Split(',');
          _context.Orders.Add(Order);
          foreach (var p in goodsID)
          {
            var num = 1;
            if (model.ordertype == "wu")
            {
              num = _context.AppCarts.Where(i => i.GoodsId == Convert.ToInt64(p) && i.UserId == UserId).FirstOrDefault().Num;
            }
            //if(_context.AppCarts.Where(i => i.UserId == UserId).ToList().Contains(Convert.ToInt64(p)))

            var ordergoods = new OrderGoods()
            {
              OrderId = Order.OrderId,
              GoodsId = Convert.ToInt64(p),
              Num = num

            };
            _context.OrderGoods.Add(ordergoods);

          }
          var CartInfo = _context.AppCarts.Where(x => x.UserId == UserId).ToList();
          if (CartInfo.Count != 0)
          {
            _context.AppCarts.RemoveRange(CartInfo);
            _context.SaveChanges();
          }
          else
          {
            _context.SaveChanges();
          }


          var Tem = new TemDto();
          var sysUserinfo = _context.SysUsers.Where(x => x.SysUserId == model.SysUserId).FirstOrDefault();

          var Good = _context.OrderGoods.Where(x => x.OrderId == Order.OrderId).ToList();
          var NT = "";
          foreach (var s in Good)
          {
            var Goodss = _context.Goodss.Where(x => x.GoodsId == s.GoodsId).FirstOrDefault();
            NT = NT + Goodss.Name + "*" + s.Num + "; ";
          }
          Tem.touser = sysUserinfo.Popenid;
          Tem.template_id = "xEh7eyNbqUA4REhnVT-aCCH0-NOwqrHFh4IyqFnkM-g";

          var data1 = new Data1();
          var keyword1 = new Keyword();
          var keyword2 = new Keyword();
          var keyword3 = new Keyword();
          var keyword4 = new Keyword();
          var keyword5 = new Keyword();
          var first = new Keyword();
          var remark = new Keyword();
          Tem.data = data1;
          Tem.data.keyword1 = keyword1;
          Tem.data.keyword2 = keyword2;
          Tem.data.keyword3 = keyword3;
          Tem.data.keyword4 = keyword4;
          Tem.data.keyword5 = keyword5;
          Tem.data.first = first;
          Tem.data.remark = remark;
          Tem.data.first.value = Order.OrderNo;
          Tem.data.keyword1.value = NT;
          Tem.data.keyword2.value = model.Price.ToString();
          Tem.data.keyword3.value = Addrs.ContactName + "   " + Addrs.ContactPhone;
          Tem.data.keyword4.value = Addrs.Addr;
          Tem.data.keyword5.value = model.Comments;
          Tem.data.remark.value = "在线支付";

          var Tnc = new TemDto();
          var data2 = new Data1();
          var first1 = new Keyword();
          var keyword11 = new Keyword();
          var keyword21 = new Keyword();
          var keyword31 = new Keyword();
          var keyword41 = new Keyword();
          var keyword51 = new Keyword();
          var keyword61 = new Keyword();
          Tnc.data = data2;
          Tnc.data.keyword1 = keyword11;
          Tnc.data.keyword2 = keyword21;
          Tnc.data.keyword3 = keyword31;
          Tnc.data.keyword4 = keyword41;
          Tnc.data.keyword5 = keyword51;
          Tnc.data.keyword6 = keyword61;

          Tnc.touser = model.OpenId;
          Tnc.template_id = "IOxfg-a_xjT3GSY8RHgF6qybqmV-4MlSM1IOpgwpWWg";
          Tnc.form_id = model.FormId;



          Tnc.data.keyword1.value = Order.OrderNo;
          Tnc.data.keyword2.value = model.Price.ToString();
          Tnc.data.keyword3.value = NT;
          Tnc.data.keyword4.value = DateTime.Now.ToString();
          Tnc.data.keyword5.value = Addrs.Addr;
          Tnc.data.keyword6.value = Addrs.ContactPhone;

          Fun.GetToken(Tem);
          AppToken Text = new AppToken(_context);
          Text.GetAppToken(Tnc);
          Jpush jpush = new Jpush();
          jpush.SendOrder(model.SysUserId,sysUserinfo.Name);


        }
        return Ok("666！！");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }

}

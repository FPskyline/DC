using Supermarket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Supermarket.Models.InitModels;
using EcommerceSys.Utility;

namespace Supermarket.Data
{
  /// <summary>
  /// 数据库初始化
  /// </summary>
  public class DbInitializer
  {
    private SupermarketDbContext _context;
    /// <summary>
    /// 初始化方法
    /// </summary>
    /// <param name="context"></param>
    public DbInitializer(SupermarketDbContext context)
    {
      _context = context;
      //context.Database.EnsureCreated();//如果数据库不存在，那么它会创建，但不做任何事
    }
    /// <summary>
    /// 异步调用初始化数据
    /// </summary>
    /// <returns></returns>
    public async Task InitializeDataAsync()
    {
      await SeedDate();
    }
    /// <summary>
    /// 异步建立数据值
    /// </summary>
    /// <returns></returns>
    private async Task SeedDate()
    {
      if (!_context.SysUsers.Any())
      {
        PwdTransition pwdTransition = new PwdTransition();
        var Salt = Guid.NewGuid().ToString();
        var Hashpwd = pwdTransition.ToHash("123aaa", Salt);
        var sysuser = new SysUser { Account = "admin", Pwd = Hashpwd, Salt = Salt, Name = "超管", CreatDate = DateTime.Now };
        await _context.SysUsers.AddAsync(sysuser);
        _context.SaveChanges();
      }

    }
  }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Models
{
  public class InitModels
  {
    /// <summary>
    /// 定义项目上下文
    /// </summary>
    public class SupermarketDbContext : DbContext
    {
      /// <summary>
      /// 构造项目上下文
      /// </summary>
      /// <param name="options"></param>
      public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }
      /// <summary>
      /// 重构项目上下文
      /// </summary>
      public SupermarketDbContext() { }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="builder"></param>
      protected override void OnModelCreating(ModelBuilder builder)
      {

        builder.Entity<OrderGoods>()
           .HasKey(c => new { c.OrderId, c.GoodsId });

      }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="optionsBuilder"></param>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        if (optionsBuilder.IsConfigured) return;


      }
      /// <summary>
      /// 系统用户表(商家)
      /// </summary>
      public DbSet<SysUser> SysUsers { get; set; }
      /// <summary>
      /// 用户表
      /// </summary>
      public DbSet<User> User { get; set; }
      /// <summary>
      /// 地址管理表
      /// </summary>
      public DbSet<Address> Addresss { get; set; }
      /// <summary>
      /// 商品表
      /// </summary>
      public DbSet<Goods> Goodss { get; set; }
      /// <summary>
      /// 商品种类表
      /// </summary>
      public DbSet<GoodsKind> GoodsKinds { get; set; }
      /// <summary>
      /// 商品种类表
      /// </summary>
      public DbSet<GoodsBigKind> GoodsBigKinds { get; set; }
      /// <summary>
      /// 订单表
      /// </summary>
      public DbSet<Order> Orders { get; set; }
      /// <summary>
      /// 购物车表
      /// </summary>
      public DbSet<AppCart> AppCarts { get; set; }
      /// <summary>
      /// 订单、商品多对多表
      /// </summary>

      public DbSet<OrderGoods> OrderGoods { get; set; }
      /// <summary>
      /// 参数表
      /// </summary>
      public DbSet<Paras> Parass { get; set; }
      /// <summary>
      /// FromId表
      /// </summary>
      public DbSet<FormId> FormIds { get; set; }
      /// <summary>
      /// FromId表
      /// </summary>
      public DbSet<Lottery> Lotterys { get; set; }
      /// <summary>
      /// 中奖名单表
      /// </summary>
      public DbSet<Winner> Winners { get; set; }
    }
  }
}

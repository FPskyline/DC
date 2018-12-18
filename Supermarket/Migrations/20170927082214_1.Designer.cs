using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Supermarket.Models;

namespace Supermarket.Migrations
{
    [DbContext(typeof(InitModels.SupermarketDbContext))]
    [Migration("20170927082214_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Supermarket.Models.Address", b =>
                {
                    b.Property<long>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Addr");

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<string>("ContactName");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreatDate");

                    b.Property<bool>("IsDefault");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime>("UpDate");

                    b.Property<long>("UserId");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresss");
                });

            modelBuilder.Entity("Supermarket.Models.AppCart", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<long>("GoodsId");

                    b.Property<int>("Num");

                    b.Property<bool>("Seclect");

                    b.Property<DateTime>("UpDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("UserId");

                    b.ToTable("AppCarts");
                });

            modelBuilder.Entity("Supermarket.Models.FormId", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("FormIds");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FormIds");
                });

            modelBuilder.Entity("Supermarket.Models.Goods", b =>
                {
                    b.Property<long>("GoodsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<long>("GoodsKindId");

                    b.Property<string>("Img");

                    b.Property<string>("Name");

                    b.Property<double>("NewPrice");

                    b.Property<string>("Notice");

                    b.Property<int>("OldPrice");

                    b.Property<long>("SysUserId");

                    b.Property<DateTime>("UpDate");

                    b.HasKey("GoodsId");

                    b.HasIndex("GoodsKindId");

                    b.HasIndex("SysUserId");

                    b.ToTable("Goodss");
                });

            modelBuilder.Entity("Supermarket.Models.GoodsBigKind", b =>
                {
                    b.Property<long>("GoodsBigKindId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("Img");

                    b.Property<string>("Index");

                    b.Property<string>("Name");

                    b.Property<long>("SysUserId");

                    b.Property<DateTime>("UpDate");

                    b.HasKey("GoodsBigKindId");

                    b.HasIndex("SysUserId");

                    b.ToTable("GoodsBigKinds");
                });

            modelBuilder.Entity("Supermarket.Models.GoodsKind", b =>
                {
                    b.Property<long>("GoodsKindId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<long>("GoodsBigKindId");

                    b.Property<string>("Img");

                    b.Property<string>("Index");

                    b.Property<string>("Name");

                    b.Property<long>("SysUserId");

                    b.Property<DateTime>("UpDate");

                    b.HasKey("GoodsKindId");

                    b.HasIndex("GoodsBigKindId");

                    b.HasIndex("SysUserId");

                    b.ToTable("GoodsKinds");
                });

            modelBuilder.Entity("Supermarket.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressId");

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("Name");

                    b.Property<string>("OrderNo");

                    b.Property<int>("PayState");

                    b.Property<int>("PayType");

                    b.Property<double>("Price");

                    b.Property<string>("RefuseReason");

                    b.Property<long>("SysUserId");

                    b.Property<long>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("SysUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Supermarket.Models.OrderGoods", b =>
                {
                    b.Property<long>("OrderId");

                    b.Property<long>("GoodsId");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<int>("Num");

                    b.HasKey("OrderId", "GoodsId");

                    b.HasAlternateKey("GoodsId", "OrderId");

                    b.ToTable("OrderGoods");
                });

            modelBuilder.Entity("Supermarket.Models.Paras", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("Describe");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpDate");

                    b.HasKey("Id");

                    b.ToTable("Parass");
                });

            modelBuilder.Entity("Supermarket.Models.SysUser", b =>
                {
                    b.Property<long>("SysUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account");

                    b.Property<string>("Addr");

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("Ip");

                    b.Property<string>("IsOpen");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Popenid");

                    b.Property<double>("Price");

                    b.Property<string>("Pwd");

                    b.Property<string>("Salt");

                    b.Property<string>("TelPhone");

                    b.Property<string>("TemIdAccept");

                    b.Property<string>("TemIdOrder");

                    b.Property<string>("TemIdRefuse");

                    b.Property<string>("TemIdSend");

                    b.Property<string>("Token");

                    b.HasKey("SysUserId");

                    b.ToTable("SysUsers");
                });

            modelBuilder.Entity("Supermarket.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("Comment1");

                    b.Property<string>("Comment2");

                    b.Property<string>("Comment3");

                    b.Property<DateTime>("CreatDate");

                    b.Property<string>("Ip");

                    b.Property<string>("Name");

                    b.Property<string>("OpenId");

                    b.Property<DateTime>("UpDate");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Supermarket.Models.Address", b =>
                {
                    b.HasOne("Supermarket.Models.User", "Users")
                        .WithMany("Addresss")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.AppCart", b =>
                {
                    b.HasOne("Supermarket.Models.Goods", "Goods")
                        .WithMany()
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.FormId", b =>
                {
                    b.HasOne("Supermarket.Models.User", "Users")
                        .WithMany("FormIds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.Goods", b =>
                {
                    b.HasOne("Supermarket.Models.GoodsKind", "GoodsKinds")
                        .WithMany("Goodss")
                        .HasForeignKey("GoodsKindId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.SysUser", "SysUsers")
                        .WithMany("Goodss")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.GoodsBigKind", b =>
                {
                    b.HasOne("Supermarket.Models.SysUser", "SysUsers")
                        .WithMany()
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.GoodsKind", b =>
                {
                    b.HasOne("Supermarket.Models.GoodsBigKind", "GoodsBigKinds")
                        .WithMany("GoodsKinds")
                        .HasForeignKey("GoodsBigKindId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.SysUser", "SysUsers")
                        .WithMany("GoodsKinds")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.Order", b =>
                {
                    b.HasOne("Supermarket.Models.Address", "Addresss")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.SysUser", "SysUsers")
                        .WithMany("Orders")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.User", "Users")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Supermarket.Models.OrderGoods", b =>
                {
                    b.HasOne("Supermarket.Models.Goods", "Goods")
                        .WithMany("OrderGoodss")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supermarket.Models.Order", "Order")
                        .WithMany("OrderGoodss")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

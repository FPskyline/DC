using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Supermarket.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUsers",
                columns: table => new
                {
                    SysUserId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Account = table.Column<string>(nullable: true),
                    Addr = table.Column<string>(nullable: true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    IsOpen = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    MobilePhone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Popenid = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Pwd = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    TelPhone = table.Column<string>(nullable: true),
                    TemIdAccept = table.Column<string>(nullable: true),
                    TemIdOrder = table.Column<string>(nullable: true),
                    TemIdRefuse = table.Column<string>(nullable: true),
                    TemIdSend = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUsers", x => x.SysUserId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Avatar = table.Column<string>(nullable: true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsBigKinds",
                columns: table => new
                {
                    GoodsBigKindId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    Img = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SysUserId = table.Column<long>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsBigKinds", x => x.GoodsBigKindId);
                    table.ForeignKey(
                        name: "FK_GoodsBigKinds_SysUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUsers",
                        principalColumn: "SysUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresss",
                columns: table => new
                {
                    AddressId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Addr = table.Column<string>(nullable: true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresss", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresss_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormIds",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    FormIds = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormIds_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsKinds",
                columns: table => new
                {
                    GoodsKindId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    GoodsBigKindId = table.Column<long>(nullable: false),
                    Img = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SysUserId = table.Column<long>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsKinds", x => x.GoodsKindId);
                    table.ForeignKey(
                        name: "FK_GoodsKinds_GoodsBigKinds_GoodsBigKindId",
                        column: x => x.GoodsBigKindId,
                        principalTable: "GoodsBigKinds",
                        principalColumn: "GoodsBigKindId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsKinds_SysUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUsers",
                        principalColumn: "SysUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AddressId = table.Column<long>(nullable: false),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    PayState = table.Column<int>(nullable: false),
                    PayType = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    RefuseReason = table.Column<string>(nullable: true),
                    SysUserId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Addresss_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresss",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_SysUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUsers",
                        principalColumn: "SysUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goodss",
                columns: table => new
                {
                    GoodsId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment1 = table.Column<string>(nullable: true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    GoodsKindId = table.Column<long>(nullable: false),
                    Img = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NewPrice = table.Column<double>(nullable: false),
                    Notice = table.Column<string>(nullable: true),
                    OldPrice = table.Column<int>(nullable: false),
                    SysUserId = table.Column<long>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goodss", x => x.GoodsId);
                    table.ForeignKey(
                        name: "FK_Goodss_GoodsKinds_GoodsKindId",
                        column: x => x.GoodsKindId,
                        principalTable: "GoodsKinds",
                        principalColumn: "GoodsKindId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goodss_SysUsers_SysUserId",
                        column: x => x.SysUserId,
                        principalTable: "SysUsers",
                        principalColumn: "SysUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCarts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: false),
                    GoodsId = table.Column<long>(nullable: false),
                    Num = table.Column<int>(nullable: false),
                    Seclect = table.Column<bool>(nullable: false),
                    UpDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCarts_Goodss_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goodss",
                        principalColumn: "GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCarts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGoods",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false),
                    GoodsId = table.Column<long>(nullable: false),
                    Comment2 = table.Column<string>(nullable: true),
                    Comment3 = table.Column<string>(nullable: true),
                    Num = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGoods", x => new { x.OrderId, x.GoodsId });
                    table.UniqueConstraint("AK_OrderGoods_GoodsId_OrderId", x => new { x.GoodsId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderGoods_Goodss_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goodss",
                        principalColumn: "GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGoods_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresss_UserId",
                table: "Addresss",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCarts_GoodsId",
                table: "AppCarts",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCarts_UserId",
                table: "AppCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormIds_UserId",
                table: "FormIds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goodss_GoodsKindId",
                table: "Goodss",
                column: "GoodsKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Goodss_SysUserId",
                table: "Goodss",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsBigKinds_SysUserId",
                table: "GoodsBigKinds",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsKinds_GoodsBigKindId",
                table: "GoodsKinds",
                column: "GoodsBigKindId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsKinds_SysUserId",
                table: "GoodsKinds",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SysUserId",
                table: "Orders",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCarts");

            migrationBuilder.DropTable(
                name: "FormIds");

            migrationBuilder.DropTable(
                name: "OrderGoods");

            migrationBuilder.DropTable(
                name: "Parass");

            migrationBuilder.DropTable(
                name: "Goodss");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "GoodsKinds");

            migrationBuilder.DropTable(
                name: "Addresss");

            migrationBuilder.DropTable(
                name: "GoodsBigKinds");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SysUsers");
        }
    }
}

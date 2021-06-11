using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace WP.NetCore.Repository.EFCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleClass",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ClassName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Component = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    IsButton = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    text = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    _ts = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ClassId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_ArticleClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ArticleClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuRole_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<long>(nullable: true),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<long>(nullable: true),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ArticleClass",
                columns: new[] { "Id", "ClassName", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime" },
                values: new object[,]
                {
                    { 15327861808481299L, ".NetCore", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(7799), null, false, null, null },
                    { 15327861808481300L, "Vue", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(8272), null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 1005L, "nested/menu1/menu1-2/menu1-2-1/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6188), null, "lightning", false, false, false, null, null, 1004L, 4, "子级22", null },
                    { 1004L, "nested/menu1/menu1-2/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6186), null, "lightning", false, false, false, null, null, 1002L, 3, "子级22", null },
                    { 1003L, "nested/menu2/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6184), null, "lightning", false, false, false, null, null, 1001L, 2, "子级22", null },
                    { 1002L, "nested/menu1/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6182), null, "lightning", false, false, false, null, null, 1001L, 1, "子级11", null },
                    { 1001L, "nested", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6179), null, "nested", false, false, false, null, null, 0L, 999, "多级", null },
                    { 8L, "serverlog/request", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6177), null, "el-icon-moon", false, false, false, null, null, 0L, 5, "审计日志", null },
                    { 15327861808481298L, "deleteArticle", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6174), null, null, true, false, false, null, null, 7L, 0, "删除", "article/delete" },
                    { 15327861808481297L, "editArticle", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6171), null, null, true, false, false, null, null, 7L, 0, "编辑", "article/put" },
                    { 15327861808481296L, "addArticle", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6168), null, null, true, false, false, null, null, 7L, 0, "新增", "article/post" },
                    { 7L, "article/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6166), null, "el-icon-cloudy", false, false, false, null, null, 0L, 4, "文章列表", null },
                    { 15327861808481295L, "deleteMenu", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6163), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 15327861808481294L, "editMenu", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6159), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 15327861808481293L, "getMenu", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6155), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15327861808481292L, "addMenu", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6152), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" },
                    { 15327861808481291L, "getMenu", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6149), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6147), null, "el-icon-cloudy-and-sunny", false, false, false, null, null, 0L, 3, "菜单管理", null },
                    { 15327861808481290L, "getPermission", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6143), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 15327861808481289L, "setPermission", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6141), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" },
                    { 15327861808481288L, "deleteRole", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6137), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15327861808481287L, "editRole", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6131), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15327861808481286L, "addRole", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6065), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 15327861808481285L, "getRole", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6062), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 2L, "role/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6060), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15327861808481284L, "deleteUser", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6051), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 15327861808481283L, "editUser", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6047), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15327861808481282L, "addUser", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(6035), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15327861808481281L, "getUser", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(4610), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 1L, "user/index", null, new DateTime(2021, 6, 11, 23, 47, 21, 479, DateTimeKind.Local).AddTicks(2629), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 6, 11, 23, 47, 21, 478, DateTimeKind.Local).AddTicks(4260), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 6, 11, 23, 47, 21, 475, DateTimeKind.Local).AddTicks(2038), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15327861808481280L, null, new DateTime(2021, 6, 11, 23, 47, 21, 478, DateTimeKind.Local).AddTicks(5831), null, false, null, null, 999999999L, 999999999L });

            migrationBuilder.CreateIndex(
                name: "IX_Article_ClassId",
                table: "Article",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRole_MenuId",
                table: "MenuRole",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRole_RoleId",
                table: "MenuRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "MenuRole");

            migrationBuilder.DropTable(
                name: "RequestLog");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "ArticleClass");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

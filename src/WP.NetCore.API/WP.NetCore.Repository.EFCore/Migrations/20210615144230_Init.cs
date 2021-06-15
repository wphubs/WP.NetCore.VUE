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
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    _ts = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLog", x => x.id);
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
                    { 15333460353893396L, ".NetCore", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(9277), null, false, null, null },
                    { 15333460353893397L, "Vue", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(9731), null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 1005L, "nested/menu1/menu1-2/menu1-2-1/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7706), null, "lightning", false, false, false, null, null, 1004L, 4, "子级22", null },
                    { 1004L, "nested/menu1/menu1-2/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7704), null, "lightning", false, false, false, null, null, 1002L, 3, "子级22", null },
                    { 1003L, "nested/menu2/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7702), null, "lightning", false, false, false, null, null, 1001L, 2, "子级22", null },
                    { 1002L, "nested/menu1/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7700), null, "lightning", false, false, false, null, null, 1001L, 1, "子级11", null },
                    { 1001L, "nested", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7699), null, "nested", false, false, false, null, null, 0L, 999, "多级", null },
                    { 8L, "serverlog/request", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7697), null, "el-icon-moon", false, false, false, null, null, 0L, 5, "审计日志", null },
                    { 15333460353893395L, "deleteArticle", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7694), null, null, true, false, false, null, null, 7L, 0, "删除", "article/delete" },
                    { 15333460353893394L, "editArticle", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7691), null, null, true, false, false, null, null, 7L, 0, "编辑", "article/put" },
                    { 15333460353893393L, "addArticle", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7688), null, null, true, false, false, null, null, 7L, 0, "新增", "article/post" },
                    { 15333460353893392L, "getArticle", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7685), null, null, true, false, false, null, null, 7L, 0, "查看", "article/get" },
                    { 7L, "article/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7683), null, "el-icon-cloudy", false, false, false, null, null, 0L, 4, "文章列表", null },
                    { 15333460353893391L, "deleteMenu", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7680), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 15333460353893390L, "editMenu", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7675), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 15333460353893389L, "getMenu", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7673), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15333460353893388L, "addMenu", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7670), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" },
                    { 15333460353893387L, "getMenu", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7666), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7665), null, "el-icon-cloudy-and-sunny", false, false, false, null, null, 0L, 3, "菜单管理", null },
                    { 15333460353893386L, "getPermission", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7661), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 15333460353893385L, "setPermission", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7621), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" },
                    { 15333460353893384L, "deleteRole", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7617), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15333460353893383L, "editRole", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7613), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15333460353893382L, "addRole", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7610), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 15333460353893381L, "getRole", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7607), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 2L, "role/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7605), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15333460353893380L, "deleteUser", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7593), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 15333460353893379L, "editUser", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7589), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15333460353893378L, "addUser", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(7578), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15333460353893377L, "getUser", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(6315), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 1L, "user/index", null, new DateTime(2021, 6, 15, 22, 42, 29, 573, DateTimeKind.Local).AddTicks(4637), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 6, 15, 22, 42, 29, 572, DateTimeKind.Local).AddTicks(7744), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 6, 15, 22, 42, 29, 570, DateTimeKind.Local).AddTicks(1270), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15333460353893376L, null, new DateTime(2021, 6, 15, 22, 42, 29, 572, DateTimeKind.Local).AddTicks(8942), null, false, null, null, 999999999L, 999999999L });

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

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
                    { 15337002233070592L, ".NetCore", null, new DateTime(2021, 6, 18, 10, 45, 28, 722, DateTimeKind.Local).AddTicks(320), null, false, null, null },
                    { 15337002233070593L, "Vue", null, new DateTime(2021, 6, 18, 10, 45, 28, 722, DateTimeKind.Local).AddTicks(552), null, false, null, null },
                    { 15337002233070594L, "Docker", null, new DateTime(2021, 6, 18, 10, 45, 28, 722, DateTimeKind.Local).AddTicks(559), null, false, null, null },
                    { 15337002233070595L, "Other", null, new DateTime(2021, 6, 18, 10, 45, 28, 722, DateTimeKind.Local).AddTicks(559), null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 15337002233054223L, "deleteMenu", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9282), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 7L, "article/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9282), null, "el-icon-cloudy", false, false, false, null, null, 0L, 4, "文章列表", null },
                    { 15337002233054224L, "getArticle", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9284), null, null, true, false, false, null, null, 7L, 0, "查看", "article/get" },
                    { 15337002233054225L, "addArticle", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9284), null, null, true, false, false, null, null, 7L, 0, "新增", "article/post" },
                    { 15337002233054226L, "editArticle", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9287), null, null, true, false, false, null, null, 7L, 0, "编辑", "article/put" },
                    { 15337002233054227L, "deleteArticle", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9287), null, null, true, false, false, null, null, 7L, 0, "删除", "article/delete" },
                    { 1001L, "nested", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9289), null, "nested", false, false, false, null, null, 0L, 999, "多级", null },
                    { 15337002233054222L, "editMenu", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9279), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 1002L, "nested/menu1/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9292), null, "lightning", false, false, false, null, null, 1001L, 1, "子级11", null },
                    { 1003L, "nested/menu2/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9292), null, "lightning", false, false, false, null, null, 1001L, 2, "子级22", null },
                    { 1004L, "nested/menu1/menu1-2/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9294), null, "lightning", false, false, false, null, null, 1002L, 3, "子级22", null },
                    { 1005L, "nested/menu1/menu1-2/menu1-2-1/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9294), null, "lightning", false, false, false, null, null, 1004L, 4, "子级22", null },
                    { 8L, "serverlog/request", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9289), null, "el-icon-moon", false, false, false, null, null, 0L, 5, "接口日志", null },
                    { 15337002233054221L, "getMenu", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9277), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15337002233054219L, "getMenu", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9272), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9272), null, "el-icon-cloudy-and-sunny", false, false, false, null, null, 0L, 3, "菜单管理", null },
                    { 15337002233054218L, "getPermission", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9270), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 15337002233054217L, "setPermission", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9270), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" },
                    { 15337002233054216L, "deleteRole", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9267), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15337002233054215L, "editRole", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9265), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15337002233054214L, "addRole", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9265), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 15337002233054213L, "getRole", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9262), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 2L, "role/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9262), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15337002233054212L, "deleteUser", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9255), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 15337002233054211L, "editUser", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9253), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15337002233054210L, "addUser", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9194), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15337002233054209L, "getUser", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(8570), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 1L, "user/index", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(7741), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null },
                    { 15337002233054220L, "addMenu", null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(9274), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(3459), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 6, 18, 10, 45, 28, 719, DateTimeKind.Local).AddTicks(6639), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15337002233054208L, null, new DateTime(2021, 6, 18, 10, 45, 28, 721, DateTimeKind.Local).AddTicks(4171), null, false, null, null, 999999999L, 999999999L });

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

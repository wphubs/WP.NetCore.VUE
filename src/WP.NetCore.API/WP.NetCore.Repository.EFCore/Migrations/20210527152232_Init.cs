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
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 1L, "user/index", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(6411), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null },
                    { 5L, "nested/menu2/index", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9555), null, "lightning", false, false, false, null, null, 3L, 2, "子级22", null },
                    { 4L, "nested/menu1/index", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9553), null, "lightning", false, false, false, null, null, 3L, 1, "子级11", null },
                    { 3L, "nested", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9551), null, "nested", false, false, false, null, null, 0L, 3, "多级", null },
                    { 15306603741414415L, "deleteMenu", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9548), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 15306603741414414L, "editMenu", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9543), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 15306603741414413L, "getMenu", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9539), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15306603741414412L, "addMenu", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9536), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" },
                    { 15306603741414411L, "getMenu", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9533), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9531), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "菜单管理", null },
                    { 15306603741414410L, "getPermission", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9528), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 15306603741414408L, "deleteRole", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9521), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15306603741414407L, "editRole", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9516), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15306603741414406L, "addRole", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9513), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 15306603741414405L, "getRole", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9510), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 2L, "role/index", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9508), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15306603741414404L, "deleteUser", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9499), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 15306603741414403L, "editUser", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9495), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15306603741414402L, "addUser", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9483), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15306603741414401L, "getUser", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(8162), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 15306603741414409L, "setPermission", null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(9525), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 5, 27, 23, 22, 32, 33, DateTimeKind.Local).AddTicks(9001), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 5, 27, 23, 22, 32, 31, DateTimeKind.Local).AddTicks(857), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15306603741414400L, null, new DateTime(2021, 5, 27, 23, 22, 32, 34, DateTimeKind.Local).AddTicks(269), null, false, null, null, 999999999L, 999999999L });

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
                name: "MenuRole");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

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
                name: "ScheduleJob",
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
                    JobName = table.Column<string>(maxLength: 100, nullable: true),
                    JobGroup = table.Column<string>(maxLength: 100, nullable: true),
                    JobType = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Cron = table.Column<string>(nullable: true),
                    SimpleTimes = table.Column<int>(nullable: true),
                    ExecTimes = table.Column<int>(nullable: false),
                    IntervalSecond = table.Column<int>(nullable: true),
                    TriggerType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsStart = table.Column<bool>(nullable: false),
                    RequestUrl = table.Column<string>(maxLength: 100, nullable: true),
                    RequestParameters = table.Column<string>(maxLength: 100, nullable: true),
                    Headers = table.Column<string>(maxLength: 100, nullable: true),
                    RequestType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleJob", x => x.Id);
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
                    Content = table.Column<string>(nullable: true),
                    Browse = table.Column<int>(nullable: false),
                    Comment = table.Column<int>(nullable: false)
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
                    { 15362543018492953L, ".NetCore", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(2712), null, false, null, null },
                    { 15362543018492954L, "Vue", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(2943), null, false, null, null },
                    { 15362543018492955L, "Docker", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(2948), null, false, null, null },
                    { 15362543018492956L, "Other", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(2951), null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 15362543018492945L, "editArticle", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1744), null, null, true, false, false, null, null, 7L, 0, "编辑", "article/put" },
                    { 15362543018492946L, "deleteArticle", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1746), null, null, true, false, false, null, null, 7L, 0, "删除", "article/delete" },
                    { 9L, "job/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1746), null, "el-icon-partly-cloudy", false, false, false, null, null, 0L, 5, "任务计划", null },
                    { 15362543018492947L, "getJobList", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1746), null, null, true, false, false, null, null, 9L, 0, "查看", "ScheduleJob/get" },
                    { 15362543018492948L, "addJob", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1749), null, null, true, false, false, null, null, 9L, 0, "新增", "ScheduleJob/post" },
                    { 15362543018492949L, "resumeJob", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1751), null, null, true, false, false, null, null, 9L, 0, "恢复", "ScheduleJob/ResumeJob" },
                    { 15362543018492950L, "pauseJob", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1754), null, null, true, false, false, null, null, 9L, 0, "停止", "ScheduleJob/PauseJob" },
                    { 15362543018492951L, "deleteJob", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1754), null, null, true, false, false, null, null, 9L, 0, "删除", "ScheduleJob/Delete" },
                    { 8L, "serverlog/request", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1756), null, "el-icon-moon", false, false, false, null, null, 0L, 6, "接口日志", null },
                    { 15362543018492944L, "addArticle", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1741), null, null, true, false, false, null, null, 7L, 0, "新增", "article/post" },
                    { 10L, "serverlog/job", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1759), null, "el-icon-moon-night", false, false, false, null, null, 0L, 7, "任务日志", null },
                    { 1001L, "nested", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1759), null, "nested", false, false, false, null, null, 0L, 999, "多级", null },
                    { 1002L, "nested/menu1/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1761), null, "lightning", false, false, false, null, null, 1001L, 1, "子级11", null },
                    { 1003L, "nested/menu2/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1761), null, "lightning", false, false, false, null, null, 1001L, 2, "子级22", null },
                    { 1004L, "nested/menu1/menu1-2/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1763), null, "lightning", false, false, false, null, null, 1002L, 3, "子级22", null },
                    { 1005L, "nested/menu1/menu1-2/menu1-2-1/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1763), null, "lightning", false, false, false, null, null, 1004L, 4, "子级22", null },
                    { 15362543018492952L, "editJob", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1756), null, null, true, false, false, null, null, 9L, 0, "修改", "ScheduleJob/put" },
                    { 15362543018492943L, "getArticle", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1741), null, null, true, false, false, null, null, 7L, 0, "查看", "article/get" },
                    { 15362543018492942L, "deleteMenu", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1739), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 15362543018492934L, "editRole", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1688), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15362543018492929L, "addUser", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1666), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15362543018492930L, "editUser", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1673), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15362543018492931L, "deleteUser", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1676), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 2L, "role/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1683), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15362543018492932L, "getRole", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1683), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 15362543018492933L, "addRole", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1685), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 7L, "article/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1739), null, "el-icon-cloudy", false, false, false, null, null, 0L, 4, "文章列表", null },
                    { 15362543018492935L, "deleteRole", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1690), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15362543018492936L, "setPermission", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1693), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" },
                    { 15362543018492937L, "getPermission", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1693), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1695), null, "el-icon-cloudy-and-sunny", false, false, false, null, null, 0L, 3, "菜单管理", null },
                    { 15362543018492938L, "getMenu", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1695), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 15362543018492939L, "addMenu", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1732), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" },
                    { 15362543018492940L, "getMenu", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1734), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15362543018492941L, "editMenu", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1737), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 15362543018492928L, "getUser", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(1051), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 1L, "user/index", null, new DateTime(2021, 7, 6, 11, 46, 54, 551, DateTimeKind.Local).AddTicks(232), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 7, 6, 11, 46, 54, 550, DateTimeKind.Local).AddTicks(6063), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 7, 6, 11, 46, 54, 549, DateTimeKind.Local).AddTicks(578), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15362543018476544L, null, new DateTime(2021, 7, 6, 11, 46, 54, 550, DateTimeKind.Local).AddTicks(6760), null, false, null, null, 999999999L, 999999999L });

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
                name: "ScheduleJob");

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

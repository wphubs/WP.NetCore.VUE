using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WP.NetCore.Repository.EFCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleClass", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Component = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsHidden = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Icon = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsButton = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ScheduleJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    JobName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobGroup = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobType = table.Column<int>(type: "int", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cron = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SimpleTimes = table.Column<int>(type: "int", nullable: true),
                    ExecTimes = table.Column<int>(type: "int", nullable: false),
                    IntervalSecond = table.Column<int>(type: "int", nullable: true),
                    TriggerType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsStart = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RequestUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestParameters = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Headers = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleJob", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Browse = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyBy = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ArticleClass",
                columns: new[] { "Id", "ClassName", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime" },
                values: new object[,]
                {
                    { 15371324859515928L, ".NetCore", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(1763), null, false, null, null },
                    { 15371324859515929L, "Vue", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(2075), null, false, null, null },
                    { 15371324859515930L, "Docker", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(2080), null, false, null, null },
                    { 15371324859515931L, "Other", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(2083), null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Component", "CreateBy", "CreateTime", "DeleteTime", "Icon", "IsButton", "IsDelete", "IsHidden", "ModifyBy", "ModifyTime", "ParentId", "Sort", "Title", "Url" },
                values: new object[,]
                {
                    { 15371324859515920L, "editArticle", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(659), null, null, true, false, false, null, null, 7L, 0, "编辑", "article/put" },
                    { 15371324859515921L, "deleteArticle", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(659), null, null, true, false, false, null, null, 7L, 0, "删除", "article/delete" },
                    { 9L, "job/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(661), null, "el-icon-partly-cloudy", false, false, false, null, null, 0L, 5, "任务计划", null },
                    { 15371324859515922L, "getJobList", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(661), null, null, true, false, false, null, null, 9L, 0, "查看", "ScheduleJob/get" },
                    { 15371324859515923L, "addJob", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(664), null, null, true, false, false, null, null, 9L, 0, "新增", "ScheduleJob/post" },
                    { 15371324859515924L, "resumeJob", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(666), null, null, true, false, false, null, null, 9L, 0, "恢复", "ScheduleJob/ResumeJob" },
                    { 15371324859515925L, "pauseJob", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(666), null, null, true, false, false, null, null, 9L, 0, "停止", "ScheduleJob/PauseJob" },
                    { 15371324859515926L, "deleteJob", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(668), null, null, true, false, false, null, null, 9L, 0, "删除", "ScheduleJob/Delete" },
                    { 8L, "serverlog/request", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(671), null, "el-icon-moon", false, false, false, null, null, 0L, 7, "审计日志", null },
                    { 15371324859515919L, "addArticle", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(656), null, null, true, false, false, null, null, 7L, 0, "新增", "article/post" },
                    { 10L, "serverlog/job", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(673), null, "el-icon-moon-night", false, false, false, null, null, 0L, 6, "任务日志", null },
                    { 1001L, "nested", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(673), null, "nested", false, false, false, null, null, 0L, 999, "多级测试", null },
                    { 1002L, "nested/menu1/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(676), null, "lightning", false, false, false, null, null, 1001L, 1, "子级11", null },
                    { 1003L, "nested/menu2/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(676), null, "lightning", false, false, false, null, null, 1001L, 2, "子级22", null },
                    { 1004L, "nested/menu1/menu1-2/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(729), null, "lightning", false, false, false, null, null, 1002L, 3, "子级22", null },
                    { 1005L, "nested/menu1/menu1-2/menu1-2-1/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(729), null, "lightning", false, false, false, null, null, 1004L, 4, "子级22", null },
                    { 15371324859515927L, "editJob", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(671), null, null, true, false, false, null, null, 9L, 0, "修改", "ScheduleJob/put" },
                    { 15371324859515918L, "getArticle", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(654), null, null, true, false, false, null, null, 7L, 0, "查看", "article/get" },
                    { 15371324859515917L, "deleteMenu", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(651), null, null, true, false, false, null, null, 6L, 0, "删除", "menu/delete" },
                    { 15371324859515909L, "editRole", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(634), null, null, true, false, false, null, null, 2L, 0, "编辑", "role/put" },
                    { 15371324859515904L, "addUser", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(615), null, null, true, false, false, null, null, 1L, 0, "新增", "user/post" },
                    { 15371324859515905L, "editUser", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(620), null, null, true, false, false, null, null, 1L, 0, "编辑", "user/put" },
                    { 15371324859515906L, "deleteUser", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(622), null, null, true, false, false, null, null, 1L, 0, "删除", "user/delete" },
                    { 2L, "role/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(629), null, "el-icon-heavy-rain", false, false, false, null, null, 0L, 2, "角色管理", null },
                    { 15371324859515907L, "getRole", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(629), null, null, true, false, false, null, null, 2L, 0, "查看", "role/get" },
                    { 15371324859515908L, "addRole", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(632), null, null, true, false, false, null, null, 2L, 0, "新增", "role/post" },
                    { 7L, "article/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(654), null, "el-icon-cloudy", false, false, false, null, null, 0L, 4, "文章列表", null },
                    { 15371324859515910L, "deleteRole", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(637), null, null, true, false, false, null, null, 2L, 0, "删除", "role/delete" },
                    { 15371324859515911L, "setPermission", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(637), null, null, true, false, false, null, null, 2L, 0, "设置权限", "role/setPermission/post" },
                    { 15371324859515912L, "getPermission", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(639), null, null, true, false, false, null, null, 2L, 0, "查看权限", "role/getPermission/get" },
                    { 6L, "menu/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(642), null, "el-icon-cloudy-and-sunny", false, false, false, null, null, 0L, 3, "菜单管理", null },
                    { 15371324859515913L, "getMenu", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(642), null, null, true, false, false, null, null, 6L, 0, "查看菜单树", "menu/getMenuTree/get" },
                    { 15371324859515914L, "addMenu", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(644), null, null, true, false, false, null, null, 6L, 0, "新增", "menu/post" },
                    { 15371324859515915L, "getMenu", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(644), null, null, true, false, false, null, null, 6L, 0, "查看所有", "menu/get" },
                    { 15371324859515916L, "editMenu", null, new DateTime(2021, 7, 12, 16, 40, 15, 590, DateTimeKind.Local).AddTicks(647), null, null, true, false, false, null, null, 6L, 0, "编辑", "menu/put" },
                    { 15371324859499521L, "getUser", null, new DateTime(2021, 7, 12, 16, 40, 15, 589, DateTimeKind.Local).AddTicks(9981), null, null, true, false, false, null, null, 1L, 0, "查看", "user/get" },
                    { 1L, "user/index", null, new DateTime(2021, 7, 12, 16, 40, 15, 589, DateTimeKind.Local).AddTicks(9147), null, "el-icon-lightning", false, false, false, null, null, 0L, 1, "用户管理", null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleName" },
                values: new object[] { 999999999L, null, new DateTime(2021, 7, 12, 16, 40, 15, 589, DateTimeKind.Local).AddTicks(4690), null, false, null, null, "系统管理员" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "IsEnable", "ModifyBy", "ModifyTime", "Name", "Password", "Sex", "UserName" },
                values: new object[] { 999999999L, null, null, new DateTime(2021, 7, 12, 16, 40, 15, 587, DateTimeKind.Local).AddTicks(7653), null, false, true, null, null, "系统管理员", "670b14728ad9902aecba32e22fa4f6bd", 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreateTime", "DeleteTime", "IsDelete", "ModifyBy", "ModifyTime", "RoleId", "UserId" },
                values: new object[] { 15371324859499520L, null, new DateTime(2021, 7, 12, 16, 40, 15, 589, DateTimeKind.Local).AddTicks(5541), null, false, null, null, 999999999L, 999999999L });

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

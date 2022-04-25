using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WP.NetCore.Common.Helper;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.Repository.EFCore
{
    public class WPDbContext : DbContext
    {


        public WPDbContext(DbContextOptions<WPDbContext> options) : base(options)
        {

        }


        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<MenuRole> MenuRole { get; set; }

        public DbSet<Article> Article { get; set; }

        public DbSet<Nothing> Nothing { get; set; }

        public DbSet<ArticleClass> ArticleClass { get; set; }
        public DbSet<ScheduleJob> ScheduleJob { get; set; }

        public DbSet<JobLog> JobLog { get; set; }

        public DbSet<RequestLog> RequestLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestLog>(eb =>
             {
                 eb.HasNoKey();
                 eb.ToView("RequestLog");
             });

            modelBuilder.Entity<JobLog>(eb =>
            {
                eb.HasNoKey();
                eb.ToView("JobLog");
            });


            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            var idWork = new Snowflake();
            var roleId = 999999999;//系统管理员
            var userId = 999999999;//系统管理员
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = userId,
                UserName = "admin",
                Name = "系统管理员",
                Password = "670b14728ad9902aecba32e22fa4f6bd",
                Sex = 1,

            });
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                RoleName = "系统管理员",
                Id = roleId,
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                Id = idWork.NextId(),
                UserId = userId,
                RoleId = roleId

            });
            modelBuilder.Entity<Menu>().HasData(new List<Menu>()
            {

                #region 用户
                new Menu()
                {
                    Id=1,Title="用户管理",Component="user/index",Icon="el-icon-lightning",Sort=1,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="查看",Url="user/get",Component="getUser",ParentId=1,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="新增",Url="user/post",Component="addUser",ParentId=1,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="编辑",Url="user/put",Component="editUser",ParentId=1,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="删除",Url="user/delete",Component="deleteUser",ParentId=1,IsButton=true,
                },
                #endregion

                #region 角色
                new Menu()
                {
                    Id=2,Title="角色管理",Component="role/index",Icon="el-icon-heavy-rain",Sort=2,
                },
                  new Menu()
                {
                    Id=idWork.NextId(),Title="查看",Url="role/get",Component="getRole",ParentId=2,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="新增",Url="role/post",Component="addRole",ParentId=2,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="编辑",Url="role/put",Component="editRole",ParentId=2,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="删除",Url="role/delete",Component="deleteRole",ParentId=2,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="设置权限",Url="role/setPermission/post",Component="setPermission",ParentId=2,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="查看权限",Url="role/getPermission/get",Component="getPermission",ParentId=2,IsButton=true,
                },
                #endregion

                #region 菜单
                new Menu()
                {
                    Id=6,Title="菜单管理",Component="menu/index",Icon="el-icon-cloudy-and-sunny",Sort=3,
                },

                new Menu()
                {
                    Id=idWork.NextId(),Title="查看菜单树",Url="menu/getMenuTree/get",Component="getMenu",ParentId=6,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="新增",Url="menu/post",Component="addMenu",ParentId=6,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="查看所有",Url="menu/get",Component="getMenu",ParentId=6,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="编辑",Url="menu/put",Component="editMenu",ParentId=6,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="删除",Url="menu/delete",Component="deleteMenu",ParentId=6,IsButton=true,
                },
                #endregion

                #region 文章
                new Menu()
                {
                    Id=7,Title="文章列表",Component="article/index",Icon="el-icon-cloudy",Sort=4,
                },

                new Menu()
                {
                    Id=idWork.NextId(),Title="查看",Url="article/get",Component="getArticle",ParentId=7,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="新增",Url="article/post",Component="addArticle",ParentId=7,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="编辑",Url="article/put",Component="editArticle",ParentId=7,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="删除",Url="article/delete",Component="deleteArticle",ParentId=7,IsButton=true,
                },
                #endregion

                #region 任务

                new Menu()
                {
                    Id=9,Title="任务计划",Component="job/index",Icon="el-icon-partly-cloudy",Sort=5,
                },

                new Menu()
                {
                    Id=idWork.NextId(),Title="查看",Url="ScheduleJob/get",Component="getJobList",ParentId=9,IsButton=true,
                },
                 new Menu()
                {
                    Id=idWork.NextId(),Title="新增",Url="ScheduleJob/post",Component="addJob",ParentId=9,IsButton=true,
                },
                  new Menu()
                {
                    Id=idWork.NextId(),Title="恢复",Url="ScheduleJob/ResumeJob",Component="resumeJob",ParentId=9,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="停止",Url="ScheduleJob/PauseJob",Component="pauseJob",ParentId=9,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="删除",Url="ScheduleJob/Delete",Component="deleteJob",ParentId=9,IsButton=true,
                },
                new Menu()
                {
                    Id=idWork.NextId(),Title="修改",Url="ScheduleJob/put",Component="editJob",ParentId=9,IsButton=true,
                },

                #endregion

                #region 日志
                new Menu()
                {
                    Id=8,Title="审计日志",Component="serverlog/request",Icon="el-icon-moon",Sort=7,
                },

                new Menu()
                {
                    Id=10,Title="任务日志",Component="serverlog/job",Icon="el-icon-moon-night",Sort=6,
                },
                #endregion

                #region 多级菜单
                new Menu()
                {
                    Id=1001,Title="多级测试",Component="nested",ParentId=0,Icon="nested",Sort=999,
                },
                new Menu()
                {
                    Id=1002,Title="子级11",Component="nested/menu1/index",Icon="lightning",ParentId=1001,Sort=1,
                },
                new Menu()
                {
                    Id=1003,Title="子级22",Component="nested/menu2/index",Icon="lightning",ParentId=1001,Sort=2,
                }
                ,
                new Menu()
                {
                    Id=1004,Title="子级22",Component="nested/menu1/menu1-2/index",Icon="lightning",ParentId=1002,Sort=3,
                },
                new Menu()
                {
                    Id=1005,Title="子级22",Component="nested/menu1/menu1-2/menu1-2-1/index",Icon="lightning",ParentId=1004,Sort=4,
                } 
	            #endregion
            });


            modelBuilder.Entity<ArticleClass>().HasData(new List<ArticleClass>()
            {
                    new ArticleClass()
                    {
                        Id = idWork.NextId(),
                        ClassName = ".NetCore",
                    },
                     new ArticleClass()
                    {
                        Id = idWork.NextId(),
                        ClassName = "Vue",
                    }
                     ,
                     new ArticleClass()
                    {
                        Id = idWork.NextId(),
                        ClassName = "Docker",
                    }
                    ,
                    new ArticleClass()
                    {
                        Id = idWork.NextId(),
                        ClassName = "Other",
                    }
            });


        }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }


}

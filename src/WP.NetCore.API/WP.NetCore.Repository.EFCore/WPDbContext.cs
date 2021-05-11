﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);


            //modelBuilder.Entity<UserRole>().HasData(new List<UserRole>()
            //{
            //    new UserRole()
            //    {
            //        Id= new Snowflake().GetId(),
            //        User=new User()
            //        {
            //            Id = new Snowflake().GetId(),
            //            UserName = "admin",
            //            Name = "系统管理员",
            //            Password = "670b14728ad9902aecba32e22fa4f6bd",
            //            Sex = 1,
            //        },
            //        Role = new Role() { Id = new Snowflake().GetId(), RoleName = "系统管理员" },
            //    },

            //    new UserRole()
            //    {
            //              Id= new Snowflake().GetId(),
            //          User=new User()
            //        {
            //            Id = new Snowflake().GetId(),
            //            UserName = "admin",
            //            Name = "系统管理员",
            //            Password = "670b14728ad9902aecba32e22fa4f6bd",
            //            Sex = 1,
            //        },
            //        Role = new Role() { Id = new Snowflake().GetId(), RoleName = "系统管理员" },
            //    }
            //});

            //modelBuilder.Entity<Role>().HasData(new List<Role>()
            //{
            //    new Role(){Id= new Snowflake().GetId(),RoleName="系统管理员" },
            //    new Role(){Id= new Snowflake().GetId(),RoleName="测试测试" }
            //});
            var idWork=new Snowflake();
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = idWork.NextId(),
                UserName = "admin",
                Name = "系统管理员",
                Password = "670b14728ad9902aecba32e22fa4f6bd",
                Sex = 1,
            });

            modelBuilder.Entity<Role>().HasData(new Role() 
            {
                RoleName="系统管理员",
                Id = idWork.NextId(),
            });

            modelBuilder.Entity<Menu>().HasData(new List<Menu>()
            {
                new Menu()
                {
                    Id=1,Title="用户管理",Component="user/index",Icon="el-icon-lightning",Sort=1,
                },
                 new Menu()
                {
                    Id=9,Title="新增",Component="addUser",ParentId=1,IsButton=true,
                },
                     new Menu()
                {
                    Id=7,Title="修改",Component="editUser",ParentId=1,IsButton=true,
                },
                         new Menu()
                {
                    Id=8,Title="删除",Component="deleteUser",ParentId=1,IsButton=true,
                },
                new Menu()
                {
                    Id=2,Title="角色管理",Component="role/index",Icon="el-icon-heavy-rain",Sort=2,
                },
                new Menu()
                {
                    Id=6,Title="菜单管理",Component="menu/index",Icon="el-icon-heavy-rain",Sort=2,
                },
                new Menu()
                {
                    Id=3,Title="多级",Component="nested",ParentId=0,Icon="nested",Sort=3,
                },
                new Menu()
                {
                    Id=4,Title="子级11",Component="nested/menu1/index",Icon="lightning",ParentId=3,Sort=1,
                },
                new Menu()
                {
                    Id=5,Title="子级22",Component="nested/menu2/index",Icon="lightning",ParentId=3,Sort=2,
                }
            });
        }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }


}

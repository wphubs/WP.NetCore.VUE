using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WP.NetCore.Common.Helper;
using WP.NetCore.Model.Model;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<User>().HasData(new User() { 
                Id= new Snowflake().GetId(),
                UserName ="admin",
                Name="系统管理员",
                Password= "670b14728ad9902aecba32e22fa4f6bd",
                Sex=1,
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
          
        }
    }

  
}

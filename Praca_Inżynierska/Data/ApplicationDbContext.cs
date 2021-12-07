using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Praca_Inżynierska.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Praca_Inżynierska.ViewModels;

namespace Praca_Inżynierska.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                builder.Entity<Order>().HasOne<ApplicationUser>(x => x.ApplicationUser)
                   .WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
                //builder.Entity<OrderDetail>().HasOne<Order>(x => x.Order)
                //.WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId);
        }


        


        


        
    }
   


}


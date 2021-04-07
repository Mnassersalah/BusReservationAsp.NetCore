using BusSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationUser>.HasData(new ApplicationUser { Id = "4da77c21-83a3-4d39-ab9f-322c3b7f2cbc", ClientName = "Admin", UserName = "Admin@ByBus.com" });
        //}

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seeding Admin User & Admin Role
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = ADMIN_ID;
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Admin",
                NormalizedName = "admin"
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                ClientName = "Admin",
                UserName = "admin@bybus.com",
                NormalizedUserName = "admin@bybus.com",
                Email = "admin@bybus.com",
                NormalizedEmail = "admin@bybus.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin_123456"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

    }
}

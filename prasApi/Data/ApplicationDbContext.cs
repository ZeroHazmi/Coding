using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prasApi.Models;

namespace prasApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Police", NormalizedName = "POLICE" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };



            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Gender).HasConversion(
                    v => v.ToString(),
                    v => (Gender)Enum.Parse(typeof(Gender), v)
                );
            });

            builder.Entity<Report>(entity =>
            {
                entity.Property(e => e.Status).HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));
                entity.Property(e => e.Priority).HasConversion(
                    v => v.ToString(),
                    v => (Priority)Enum.Parse(typeof(Priority), v));
            });

            builder.Entity<ReportType>(entity =>
            {
                entity.Property(e => e.TemplateStructure).HasColumnType("jsonb");
            });

            builder.Entity<ReportDetail>(entity =>
            {
                entity.Property(e => e.FieldValue).HasColumnType("jsonb");
            });
        }

    }

    
}


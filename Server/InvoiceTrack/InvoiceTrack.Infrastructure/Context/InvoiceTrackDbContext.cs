using InvoiceTrack.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTrack.Infrastructure.Context
{
    public class InvoiceTrackDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public InvoiceTrackDbContext(DbContextOptions<InvoiceTrackDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            string email = _configuration["Admin:Email"];
            string password = _configuration["Admin:Password"];
            base.OnModelCreating(builder);
            var roles = new IdentityRole[]
            {
                new IdentityRole()
                {
                    Id = "1",
                    ConcurrencyStamp = "462f2a91-f7b8-4dbd-a17c-4d3d279c566d",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
                new IdentityRole()
                {
                    Id = "2",
                    ConcurrencyStamp = "462f2a91-f7b8-4dbd-a17c-4d3d279c566d",
                    Name = "BUHead",
                    NormalizedName = "BUHEAD"
                },
                new IdentityRole()
                {
                    Id = "3",
                    ConcurrencyStamp = "462f2a91-f7b8-4dbd-a17c-4d3d279c566d",
                    Name = "ProjectManager",
                    NormalizedName = "PROJECTMANAGER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = Guid.NewGuid().ToString(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, password)
                }
            );
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "2",
                    UserName = Guid.NewGuid().ToString(),
                    Email = "fathima.as@experionglobal.com",
                    NormalizedEmail = "FATHIMA.AS@EXPERIONGLOBAL.COM"
                    //PasswordHash = hasher.HashPassword(null, password)
                }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                 new IdentityUserRole<string>
                 {
                     RoleId = "1",
                     UserId = "1"
                 }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                 new IdentityUserRole<string>
                 {
                     RoleId = "2",
                     UserId = "2"
                 }
            );
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}

using System;
using MemberEntry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemberEntry.Data
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
            
        public DbSet<MemberBasicInfoModel> Members { get; set; }
        public DbSet<PassportType> PassportTypes { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding a 'SystemAdmin' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "SystemAdmin",
                NormalizedName = "SYSTEMADMIN".ToUpper()
            });

            // Seeding a user to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "e1ae1f42-75b2-4604-97ec-10f844b1962f", // primary key
                UserName = "tareq@yahoo.com",
                NormalizedUserName = "TAREQ@YAHOO.COM",
                Email = "tareq@yahoo.com",
                NormalizedEmail = "TAREQ@YAHOO.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "123456"),
                PhoneNumber = "01861268168",
                PhoneNumberConfirmed = true,
                FirstName = "Tareq",
                LastName="Hossian"
            });

            // Seeding relation between the user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "e1ae1f42-75b2-4604-97ec-10f844b1962f"
                }
            );

            // Seeding a claim to AspNetUserClaims table
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                    ClaimType = "FirstName",
                    ClaimValue = "Tareq"
                }
            );


          
            modelBuilder.Entity<MemberBasicInfoModel>(entity =>
            {
                entity.HasOne(p => p.PassportType)
                      .WithMany(pt => pt.Members)
                      .HasForeignKey(p => p.PassportTypeId)
                      .IsRequired(false);
            });
        }

    }
}

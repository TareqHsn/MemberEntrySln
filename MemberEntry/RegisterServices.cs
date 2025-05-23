using MemberEntry.Data;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using MemberEntry.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MemberEntry
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IPassprtTypeRepository, PassportTypeRepository>();

        }
    }
}

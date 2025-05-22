using MemberEntry.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemberEntry.Data
{

    //public class AppDbContext : DbContext
    //{
    //    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    //    {

    //    }

    //    public DbSet<MemberBasicInfoModel> Members { get; set; }
    //}
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
            
        public DbSet<MemberBasicInfoModel> Members { get; set; }
    }
}

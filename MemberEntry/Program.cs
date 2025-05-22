using MemberEntry.Data;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using MemberEntry.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser and Role support
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Register custom repository
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

// Additional services
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor

// QuestPDF license
QuestPDF.Settings.License = LicenseType.Community;

// Razor Pages and Authorization
builder.Services.AddRazorPages();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/Identity/Account/Login");
    await Task.CompletedTask;
});

app.Run();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "Admin");
        // Assign default permissions to admin
        await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Permission", Permissions.CanEditProfile));
        await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Permission", Permissions.CanDeleteUsers));
        await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Permission", Permissions.CanViewReports));
    }
}
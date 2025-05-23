using MemberEntry;
using MemberEntry.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity ApplicationUser and Role 
builder.ConfigureServices();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor(); 

// QuestPDF license
QuestPDF.Settings.License = LicenseType.Community;

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


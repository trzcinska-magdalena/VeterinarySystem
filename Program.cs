using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Repository;
using VeterinarySystem.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using VeterinarySystem.Service;
using VeterinarySystem.Service.IService;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<VeterinarySystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));



builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<VeterinarySystemContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});


builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog(new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger());
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IBaseManagementService, BaseManagementService>();
builder.Services.AddScoped<IGlobalService, GlobalService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Employee}/{controller=Home}/{action=Index}/{id?}");


app.Run();



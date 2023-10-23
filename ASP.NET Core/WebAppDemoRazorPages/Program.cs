using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using WebAppDemoRazorPages.Data;
using UAParser;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Use(async (context, next) =>
{
    await next.Invoke();
    
    string requestDate = DateTime.Now.ToString();
    string requestPath = context.Request.Path;
    string ipAddress = context.Connection.RemoteIpAddress.ToString();
    string userAgent = context.Request.Headers["User-Agent"];
    var uaParser = Parser.GetDefault();
    ClientInfo c = uaParser.Parse(userAgent);
    string browser = c.UA.Major.ToString();
    string os = c.OS.Family.ToString();
    string logMessage = $"Date:{requestDate} -Path: {requestPath} -Ip: {ipAddress} - Browser: {browser}, OS: {os}\n";
    // Укажите путь к вашему файлу журнала
    string logFilePath = "log.txt";

    // Добавьте информацию в файл
    File.AppendAllText(logFilePath, logMessage);
});
app.Run();

using CustomLog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Data;
using SchoolJournal.Data.Repos;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("SchoolJournalDBContext");
builder.Services.AddDbContext<SchoolJournalDBContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IStreamRepository, StreamRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPupilRepository, PupilRepository>();
builder.Services.AddSingleton<IJournalRecordRepository, JournalRecordRepository>();

builder.Services.AddAutoMapper(typeof(SchoolJournal.MappingProfile));

builder.Configuration.AddJsonFile("CustomLogOptions.json");
builder.Services.Configure<CustomLogOptions>(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

// установка конфигурации подключения
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Подключаем логирование с использованием ПО промежуточного слоя
app.UseMiddleware<CustomLog.CustomLog>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

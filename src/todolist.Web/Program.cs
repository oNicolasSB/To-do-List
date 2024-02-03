using todolist.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using todolist.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//serviços:
//mvc e razor runtime compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//autenticação
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.SlidingExpiration = true;
        option.LoginPath = "/Usuario/Login";
        option.LogoutPath = "/Usuario/Logout";
    }
);
builder.Services.AddHttpContextAccessor();

string host = builder.Configuration["DBHOST"] ?? "localhost";
string port = builder.Configuration["DBPORT"] ?? "3306";
string password = builder.Configuration["DBPASSWORD"] ?? "root";

//adição do serviço do banco de dados
string mySqlConnection = $"Server={host};Port={port};Database=todolistdb;User=root;Password={password};";
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
    // options.UseSqlite("Data source=data.db");
});

var app = builder.Build();


//pipeline:
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.InitializeData();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();

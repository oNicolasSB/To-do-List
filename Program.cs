using todolist.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//serviços:
//mvc e razor runtime compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//autenticação
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option=>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.SlidingExpiration = true;
        option.LoginPath = "/Usuario/Login";
        option.LogoutPath = "/Usuario/Logout";
    }
);
builder.Services.AddHttpContextAccessor();

//adição do serviço do banco de dados

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    // options.UseSqlite("Data source=data.db");
});

var app = builder.Build();


//pipeline:
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();

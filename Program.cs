using todolist.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//serviços:
//mvc e razor runtime compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

 //adição do serviço do banco de dados
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data source=data.db");
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

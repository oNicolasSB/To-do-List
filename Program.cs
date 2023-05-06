var builder = WebApplication.CreateBuilder(args);

//serviços:
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
app.MapDefaultControllerRoute(); //define a utilização da rota padrão
app.Run();

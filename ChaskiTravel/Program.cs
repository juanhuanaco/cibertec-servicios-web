using ChaskiTravel.DAO;
using ChaskiTravel.Models.DI;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddSingleton<ITour, tourDAO>();
builder.Services.AddSingleton<IHotel, hotelDAO>();
builder.Services.AddSingleton<IDestino, destinoDAO>();
builder.Services.AddSingleton<ICategoria, categoriaDAO>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        //CONFIGURACION
        //especificar la apgina de logueo
        option.LoginPath = "/Acceso/Logueo";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.AccessDeniedPath = "/Home/Privacy";

    });
var app = builder.Build();
app.UseSession(); //habilitar el estado del Session

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Logueo}/{id?}");

app.Run();
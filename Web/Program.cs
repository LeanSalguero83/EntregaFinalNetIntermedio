
using Data;
using Data.Manager;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Interfaces;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(option =>
{
	option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
{
	config.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		context.Response.Redirect("https://localhost:7218");
		return Task.CompletedTask;
	};
}).AddGoogle(GoogleDefaults.AuthenticationScheme, option =>
{
    option.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    option.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    option.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
});

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext"); // OJO QUE PUEDE SER ConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("ADMINISTRADORES", policy =>
    {
        policy.RequireRole("Administrador");
    });
    option.AddPolicy("USUARIOS", policy =>
    {
        policy.RequireRole("Usuario");
    });
});
builder.Services.AddHttpClient("useApi", config =>
{
	config.BaseAddress = new Uri(builder.Configuration["Url:Api"]);
});

builder.Services.AddScoped<UsuariosManager>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddSession();
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
app.UseSession();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Login}/{id?}");
app.MapHub<ChatHub>("/Chat");

app.Run();
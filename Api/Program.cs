using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.Services;
using Data;
using Data.Manager;
using Api.Interfaces;
using Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Origin = "*";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Origin, policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();

    });
});
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductosService, ProductosService>();
builder.Services.AddScoped<ProductosManager>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<UsuariosManager>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<RolesManager>();
builder.Services.AddScoped<IServiciosService, ServiciosService>();
builder.Services.AddScoped<ServiciosManager>();
builder.Services.AddScoped<IRecuperarCuentaService, RecuperarCuentaService>();
builder.Services.AddScoped<RecuperarCuentaManager>();






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
option.AddSecurityDefinition("Bearer",
    new OpenApiSecurityScheme
    {
        Description = "Autorizacion",
        Name = "Autorizacion",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Firma"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(Origin);
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
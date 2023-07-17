global using TecnicalSupportAppV1.Data.DatabaseContext;
global using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using TecnicalSupportAppV1.Bussiness.Services;
using TecnicalSupportAppV1.Api.Interfaces.Authertification;
using TecnicalSupportAppV1.Bussiness.Authentification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TecnicalSupportAppV1.Bussiness.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Data.Dao;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Transient);

//builder.Services.AddTransient<DataContext>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Dependency injection 
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAdminDao, AdminDao>();
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IRolesDao, RolesDao>();
builder.Services.AddScoped<IAuthFacade, AuthFacade>();
builder.Services.AddScoped<ILoginFacade, LoginFacade>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientDao, ClientDao>();

builder.Services.AddTransient<IOfficeService, OfficeService>();
builder.Services.AddTransient<IOfficeDao, OfficeDao>();

builder.Services.AddScoped<ITechnicianService, TechnicianService>();
builder.Services.AddScoped<ITechnicianDao, TechnicianDao>();

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemDao, ItemDao>();

builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockDao, StockDao>();

builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();
builder.Services.AddScoped<IServiceOrderDao, ServiceOrderDao>();

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


app.MapControllers();


app.Run();

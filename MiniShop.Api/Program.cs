
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using MiniShop.Application.Interfaces;
using MiniShop.Infrastructure.Repositories;
using MiniShop.Application.Services;
using MiniShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using MiniShop.Infrastructure.Security;
using MiniShop.Domain.Pricing;
using Serilog.Sinks.Grafana.Loki;
using Serilog;
using Prometheus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MiniShop.Api.GraphQL;
using MiniShop.Application.Mapping;
using MiniShop.Api.Mapping;

var builder = WebApplication.CreateBuilder(args);

// =====================

// Add services ----v4

// =====================

builder.Services.AddControllers();

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure()
    ));

// DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPriceStrategy, NoDiscountStrategy>();
builder.Services.AddScoped<IPriceStrategy, PercentageDiscountStrategy>();
builder.Services.AddScoped<IPriceStrategy, FixedAmountDiscountStrategy>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<EmbeddingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<SearchService>();



var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

//// Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki("http://localhost:3100")
    .CreateLogger();

// Connect serilogprovider to Ilogger az provider (we have this implementation in this package Serilog.AspNetCore - ILogger -> ILoggerfactory -> IloggerProvider -> SerilogLogger)
builder.Host.UseSerilog();

//graphQl
builder.Services
    .AddGraphQLServer()
    .AddQueryType<MiniShop.Api.GraphQL.Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

// =====================
// Middleware pipeline
// =====================

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
   //  app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    db.Database.Migrate();
}

//graph QL


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.UseHttpMetrics();
app.MapMetrics();

app.Run();

using DotnetExample.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using SharedService.DBContext;
using SharedService.LogProvider.Implement;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using System.Globalization;
using System.Text;
using WebApiServices.BussinessLogic;
using WebApiServices.Middleware;
using WebApiServices.Services.Implement;
using WebApiServices.Services.Interface;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;

// Add Scoped
AddScopedConfig(builder);

// Add Singleton
AddSingletion(builder);
 
//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container. 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AppSettings
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

//JSON Serializer
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
    .Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());

// DBContext
builder.Services.AddDbContext<DatasContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddDbContext<DatasContext>(o => o.UseSqlite("Data source=books.db"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    string jwt = Configuration["JWTConfig:Key"] ?? string.Empty;
    var key = Encoding.ASCII.GetBytes(jwt);
    var issuer = Configuration["JWTConfig:Issuer"];
    var audience = Configuration["JWTConfig:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
    };
});

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

builder.Services.AddIoc(Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add Photo (Employee)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();
 
void AddScopedConfig(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<DepartmentLogic>();
    builder.Services.AddScoped<EmployeeLogic>();
    builder.Services.AddScoped<ProductsLogic>();
    builder.Services.AddScoped<MasterDetailsOrdersLogic>();

    builder.Services.AddScoped<ILoggerService, LoggerServiceImpl>(); 
    builder.Services.AddScoped<IBookRepository, BookRepository>();
}
 
void AddSingletion(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
}
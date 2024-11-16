using Newtonsoft.Json.Serialization;
using Serilog;
using SharedService.LogProvider.Implement;
using SharedService.LogProvider.Interface;
using SharedService.Models;
using WebApiServices.BussinessLogic;
using WebApiServices.Middleware;

var builder = WebApplication.CreateBuilder(args);

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
 
builder.Services.Configure<AppSettings>(builder.Configuration);

//JSON Serializer
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
    .Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
 
void AddScopedConfig(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<DepartmentLogic>();
    builder.Services.AddScoped<ILoggerService, LoggerServiceImpl>();
}
 
void AddSingletion(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
}
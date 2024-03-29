using System.Net;
using System.Reflection;
using API;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Models;
using Serilog;
using Utilities;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Constants.CurrentFolder,
});


Logger.InitLogger();

ILogger log = Logger.ContextLog<Program>();

builder.Host.UseSerilog(log);

log.Debug("Serilog initialized");

log.Debug("Loading AD Information");
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = MonitoringFacade.Instance.Authentication.ValidationParams;
//});

//builder.Services.AddAuthorization();

builder.Services.AddOptions();
log.Debug("Loading Host Information");
HostSettings hostsettings = builder.Configuration.GetSection("Host").Get<HostSettings>();


log.Debug("Loading Controllers");
MonitoringFacade.Instance.Init();


builder.Services.AddControllers().AddNewtonsoftJson().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SIRA",
        Version = "v1",
        Description = "UED-APPD Project by Marcel Puchas, Marie-Therese Uhl, Edith Hörtnagel, Peter Feldgrill"
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddCors(p =>
    p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

log.Debug("Starting Service...");

builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, hostsettings.Port, listenOptions =>
    {
        try
        {
            if (hostsettings.Protocol.Equals("https"))
            {
                log.Information("Starting over HTTPS");
            }
            else
            {
                log.Information("Starting over HTTP on Port " + hostsettings.Port);
            }
        }
        catch (Exception ex)
        {
            log.Fatal("Could not start Webserver on " + hostsettings.Port, ex);
        }
    });
});


var app = builder.Build();

app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHsts();
app.MapControllers();
app.Run();
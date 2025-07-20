using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RealtimeNotifications.Business;
using RealtimeNotifications.Hubs;
using RealtimeNotifications.Interfaces;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RealtimeNotifications;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Middleware;
using Serilog;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<NotificationContext>();
builder.Services.AddDbContext<NotificationContext>(option => option.UseSqlite("Data Source=app.db"));
builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
        ValidIssuer = "My-api",
        ValidAudience = "My-Client",
        NameClaimType=ClaimTypes.NameIdentifier
    };
    
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = (context) =>
        {
            var accesstoken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accesstoken)&& path.StartsWithSegments("/Client"))
            {
                context.Token = accesstoken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthentication();

builder.Services.AddCors(options =>
{
    
    
        options.AddPolicy("AllowAngularDev", policy =>
        {
            policy.WithOrigins("http://localhost:4200","https://my-signalr-client.onrender.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(Int32.Parse(port));
//});
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NotificationContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseCors("AllowAngularDev");

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.UseWebSockets();
app.MapHub<NotificationHub>("/Client");
app.UseHttpsRedirection();
app.Run();

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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NotificationContext>();
builder.Services.AddSignalR();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

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
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDev");

app.MapControllers();
app.UseWebSockets();
app.MapHub<NotificationHub>("/Client");
app.UseHttpsRedirection();
app.Run();

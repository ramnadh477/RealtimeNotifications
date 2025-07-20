using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealtimeNotifications.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealtimeNotifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly NotificationContext _context;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IConfiguration configuration,NotificationContext context,ILogger<LoginController> logger)
        {
            Configuration = configuration;
            _context = context;
            _logger = logger;
        }
        [HttpPost(Name = "Login")]
        public IActionResult Post(Login login)
        {
            var user = _context.Users.Where(n => n.UserName == login.UserName).FirstOrDefault();
            if (user is not null)
            {
                _logger.LogInformation($"User {user.UserName} logged in successfully.");
                var token = getValidToken(user.UserName,user.UserId);
                return Ok(new { Token = token ,User=user});
            }else
                return NotFound();
        }
        [HttpPut]
        public IActionResult Put(Login login)
        {
            var loginObj = new User
            {
                UserName = login.UserName,
                Password = login.Password
            };
            _context.Users.Add(loginObj);
            _context.SaveChanges();
            return Ok("Login API is working");
        }
        private string getValidToken(string? userName,int userID)
        {
            var claim = new List<Claim> { 
                new Claim(ClaimTypes.Name, userID.ToString()),
             new Claim(ClaimTypes.NameIdentifier, userID.ToString())};
            var keystring=!string.IsNullOrEmpty(Configuration["Jwt:Key"])? Configuration["Jwt:Key"]:string.Empty;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s:keystring));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "My-api", audience: "My-Client", claims: claim, expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

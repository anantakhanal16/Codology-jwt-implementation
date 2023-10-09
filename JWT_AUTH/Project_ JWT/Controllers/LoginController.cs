using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project__JWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project__JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Crediatail manually check gareko 
        /// database lai mock  garera
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Users AuthenticateUser(Users user)
        {
            Users _user = null;

            if (user.UserName == "admin" && user.Password == "12378")
            {
                _user = new Users { UserName ="Manoj Deshwal"};
            }

            return _user;
        }
        /// <summary>
        /// _congig ko value app setting.json bata 
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private string GenerateToken(Users users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"], 
                        _config["Jwt:Audience"],
                         null,
                         expires:DateTime.Now.AddMinutes(10),
                         signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Api bata login garda 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = AuthenticateUser(user);
            if (user_ != null) 
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });

            }
            return response;

        }

    }
}

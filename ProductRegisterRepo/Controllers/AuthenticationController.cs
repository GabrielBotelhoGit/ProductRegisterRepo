using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductRegister.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRegister.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthenticationController(IConfiguration configuration)
        {
            _config = configuration;
        }
        [HttpPost]
        public IActionResult Login([FromBody]User loginDetails)
        {
            bool resultado = ValidateUser(loginDetails);
            if (resultado)
            {
                var tokenString = GenerateToken();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidateUser(User loginDetails)
        {
            if (loginDetails.Login == "AdminMaster" && loginDetails.Password == "AllPowerful")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GenerateToken()
        {            
            var expiry = DateTime.Now.AddMinutes(60);
            var secretKey = _config["Auth:Key"];
            var issuer = _config["Auth:Issuer"];
            var audience = _config["Auth:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));            
            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: expiry,
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}

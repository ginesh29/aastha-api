using AASTHA2.Common;
using AASTHA2.Common.Helpers;
using AASTHA2.Models;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private static UserService _UserService;
        public AuthController(IConfiguration configuration, ServicesWrapper ServicesWrapper)
        {
            Configuration = configuration;
            _UserService = ServicesWrapper.UserService;
        }
        // GET api/values          
        [HttpPost]
        public ActionResult GenerateToken(LoginModel loginModel)
        {
            loginModel.Password = PasswordHash.GenerateHash(loginModel.Password);
            var user = _UserService.VerifyUser(loginModel);

            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
                claims.Add(new Claim("UserId", user.id.ToString()));
                claims.Add(new Claim("Role", user.isSuperAdmin ? ((int)Role.Admin).ToString() : ((int)Role.Assistant).ToString()));
                var tokenOptions = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Audience"],
                    claims: claims,
                    expires: loginModel.RememberMe ? DateTime.Now.AddMonths(1) : DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
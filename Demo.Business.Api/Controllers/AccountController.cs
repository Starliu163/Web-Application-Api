using Demo.Business.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Dmo.Entity.Class;

namespace Demo.Business.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        // 构造函数，初始化 AuthService 和 IConfiguration
        public AccountController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        // 登录方法，处理 POST 请求
        [HttpPost("login")]
        
        public IActionResult Login([FromBody] User user)
        {
            // 创建 User 对象
            User u = new User { Account = user.Account, Pwd = user.Pwd };

            // 验证用户
            if (_authService.ValidateUser(u))
            {
                // 生成 JWT 令牌
                var token = GenerateJwtToken(u);
                //var handler = new JwtSecurityTokenHandler();
                //var tokenS = handler.ReadToken(token) as JwtSecurityToken;

                //Console.WriteLine("过期时间"+tokenS.ValidTo);
                return Ok(new { token }); // 返回令牌
            }
            return Unauthorized(); // 验证失败返回 401
        }
        [HttpGet("login")]
        public IActionResult Login(string Account)
        {
            // 创建 User 对象
                return Ok(new { Account }); // 返回令牌
        }

        // 生成 JWT 令牌的方法
        private string GenerateJwtToken(User user)
        {
            // 获取密钥并创建加密密钥
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // 创建签名凭证

            // 创建声明，包含用户账号和唯一标识符
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Account.ToString()), // 用户账号
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // 唯一标识符
            };

            // 创建 JWT 令牌
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // 发行者
                audience: _configuration["Jwt:Audience"], // 受众
                claims: claims, // 声明
                expires: DateTime.UtcNow.AddDays(1),// 令牌过期时间
                signingCredentials: credentials); // 签名凭证

            Console.WriteLine($"Token generated at: {DateTime.UtcNow}");
            Console.WriteLine($"Token expires at: {token.ValidTo}");
            Console.WriteLine($"Server local time: {DateTime.Now}");
            Console.WriteLine($"Server UTC time: {DateTime.UtcNow}");
            // 返回生成的令牌
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

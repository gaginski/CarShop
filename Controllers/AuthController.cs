using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarShop.Data;
using CarShop.Models;
using CarShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext db, TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public record LoginRequest(string UserName, string Password);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request is null ||
                string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Requisição inválida.");
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == request.UserName &&
                    u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var roles = new List<string>();

              if (user.Role == 0)
                roles.Add("Admin");
            else
                roles.Add("User");

            var token = _tokenService.GenerateToken(user, roles);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });

            return Ok("Login efetuado.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return Ok("Logout efetuado.");
        }
    }
}

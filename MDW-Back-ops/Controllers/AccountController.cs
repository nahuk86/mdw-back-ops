using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MDW_Back_ops.Data;
using MDW_Back_ops.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;
using MDW_Back_ops.Helpers;

namespace MDW_Back_ops.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet("test-db")]
        public IActionResult TestDatabase()
        {
            try
            {
                var users = _context.Users.ToList(); // Realiza una consulta simple
                return Ok(new { Count = users.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al acceder a la base de datos: {ex.Message}");
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                    return BadRequest(new { message = "El correo ya está registrado." }); // Respuesta JSON

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = PasswordHasher.HashPassword(model.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuario registrado con éxito." }); // Respuesta JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al conectar con la base de datos: {ex.Message}" }); // Respuesta JSON
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized("Correo o contraseña incorrectos.");

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: null,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using challenge_moto_connect.Application.DTOs;
using challenge_moto_connect.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace challenge_moto_connect.Api.Controllers
{
    /// <summary>
    /// Controller para autenticação de usuários e geração de tokens JWT.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do controller de autenticação.
        /// </summary>
        /// <param name="userService">Serviço de usuários.</param>
        /// <param name="configuration">Configuração da aplicação.</param>
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT.
        /// </summary>
        /// <param name="loginDto">Dados de login (Email e Senha).</param>
        /// <returns>Token JWT.</returns>
        /// <response code="200">Retorna o token JWT.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            // Simulação de autenticação (a lógica real deve ser implementada no UserService)
            // Por enquanto, apenas verifica se o email e senha não estão vazios.
            // O ideal seria verificar no banco de dados.
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            // Simulação de busca de usuário e tipo de usuário
            // A busca real deve ser feita no banco de dados
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password) // **ATENÇÃO**: Senha em texto plano, deve ser HASHED
            {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }

            var token = GenerateJwtToken(user);

            return Ok(new { token = token });
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userType", user.Type.ToString()), // Adiciona o tipo de usuário
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    /// DTO para dados de login.
    /// </summary>
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

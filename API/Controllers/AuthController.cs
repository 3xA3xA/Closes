using Domain.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Регистрирует нового пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для регистрации: имя, email и пароль.</param>
        /// <returns>Информация о пользователе и JWT токен.</returns>
        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Регистрация пользователя",
            Description = "Регистрирует пользователя с указанными данными, создает учетную запись и возвращает JWT токен."
        )]
        [SwaggerResponse(200, "Пользователь успешно зарегистрирован", typeof(RegisterDto))]
        [SwaggerResponse(400, "Ошибка в данных регистрации")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                // После регистрации сразу генерируем JWT-токен
                var token = GenerateJwtToken(user);
                return Ok(new { user.Id, user.Name, user.Email, user.AvatarUrl, Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Аутентифицирует пользователя и выдает JWT токен.
        /// </summary>
        /// <param name="dto">Данные для входа: email и пароль.</param>
        /// <returns>Данные пользователя и JWT токен.</returns>
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Аутентификация пользователя",
            Description = "Принимает учетные данные и, если они корректны, возвращает JWT токен для доступа к защищенным ресурсам."
        )]
        [SwaggerResponse(200, "Пользователь успешно аутентифицирован", typeof(LoginDto))]
        [SwaggerResponse(400, "Неверные учетные данные")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var user = await _userService.AuthenticateAsync(dto);
                var token = GenerateJwtToken(user);
                return Ok(new { user.Id, user.Name, user.Email, user.AvatarUrl, Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Метод для генерации JWT-токена
        private string GenerateJwtToken(Domain.Entities.User user)
        {
            // Получаем настройки JWT
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60");

            // Задаем набор претензий (claims), которые вы хотите добавить в токен
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Создаем ключ и подписываем данные
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Создаем описание токена
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials);

            // Создаем токен
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenDescriptor);
        }
    }
}

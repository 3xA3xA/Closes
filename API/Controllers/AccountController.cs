using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Все методы этого контроллера требуют аутентификации
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Обновление данных аккаунта пользователя: изменение email, пароля и аватарки.
        /// Для смены пароля необходимо предоставить старый пароль.
        /// </summary>
        /// <param name="updateDto">DTO с новыми данными аккаунта.</param>
        /// <returns>Обновленные данные пользователя.</returns>
        /// <response code="200">Данные успешно обновлены.</response>
        /// <response code="400">Ошибка обновления аккаунта (например, неверный старый пароль или email уже используется).</response>
        /// <response code="401">Пользователь не аутентифицирован.</response>
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateUserAccountDto updateDto)
        {
            // Получаем идентификатор пользователя из claims. JWT-токен содержит идентификатор в claim "sub".
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Идентификатор пользователя отсутствует или имеет неверный формат.");
            }

            try
            {
                var updatedUser = await _userService.UpdateAccountAsync(userId, updateDto);
                return Ok(new
                {
                    updatedUser.Id,
                    updatedUser.Name,
                    updatedUser.Email,
                    updatedUser.AvatarUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

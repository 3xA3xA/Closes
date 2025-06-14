using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env; // Для получения wwwroot пути

        public AccountController(IUserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
        }

        /// <summary>
        /// Обновляет данные аккаунта пользователя, включая возможность загрузки нового аватара.
        /// Для смены пароля требуется указать старый пароль.
        /// </summary>
        /// <param name="dto">Данные формы для обновления аккаунта.</param>
        /// <returns>Обновленные данные пользователя.</returns>
        [HttpPut("updateAccount")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(
            Summary = "Обновление аккаунта пользователя",
            Description = "Обновляет данные аккаунта, такие как имя, email и пароль"
        )]
        [SwaggerResponse(200, "Данные аккаунта успешно обновлены", typeof(UpdateUserAccountDto))]
        [SwaggerResponse(400, "Ошибка обновления аккаунта")]
        [SwaggerResponse(401, "Пользователь не аутентифицирован")]
        public async Task<IActionResult> UpdateAccount([FromForm] UpdateUserAccountDto updateDto)
        {
            //// Код получения идентификатора пользователя из claim "sub"
            //var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            //if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            //{
            //    return Unauthorized("Идентификатор пользователя отсутствует или имеет неверный формат.");
            //}

            try
            {
                //var updatedUser = await _userService.UpdateAccountAsync(userId, updateDto);
                var updatedUser = await _userService.UpdateAccountAsync(updateDto);
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

        /// <summary>
        /// Загружает аватар для пользователя. Файл сохраняется в папке "wwwroot/uploads/avatars",
        /// а в базе данных сохраняется относительный путь к файлу.
        /// </summary>
        /// <param name="uploadDto">DTO, содержащий файл аватарки.</param>
        /// <returns>Относительный путь к сохраненному файлу аватарки.</returns>
        [HttpPost("avatar")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(
            Summary = "Обновление аватара пользователя",
            Description = "Обновляет аватар пользователя"
        )]
        [SwaggerResponse(200, "Данные аккаунта успешно обновлены", typeof(UpdateUserAccountDto))]
        [SwaggerResponse(400, "Ошибка обновления аккаунта")]
        [SwaggerResponse(401, "Пользователь не аутентифицирован")]
        public async Task<IActionResult> UploadAvatar([FromForm] UploadAvatarDto uploadDto)
        {
            if (uploadDto?.Avatar == null || uploadDto.Avatar.Length == 0)
            {
                return BadRequest("Файл не выбран или пуст.");
            }

            try
            {
                // 1. Определяем корневую папку для загрузки
                string contentRootPath = _env.ContentRootPath; // Используем ContentRoot вместо WebRoot
                string uploadsFolder = Path.Combine(contentRootPath, "Uploads", "Avatars");

                // 2. Создаем папку, если не существует
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // 3. Генерируем уникальное имя файла
                string fileExtension = Path.GetExtension(uploadDto.Avatar.FileName);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);

                // 4. Сохраняем файл
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await uploadDto.Avatar.CopyToAsync(stream);
                }

                // 5. Формируем относительный путь для БД
                string relativePath = Path.Combine("Uploads", "Avatars", uniqueFileName)
                    .Replace("\\", "/");

                // 6. Обновляем пользователя
                var updatedUser = await _userService.UpdateAvatarAsync(uploadDto.Id, relativePath);

                return Ok(new
                {
                    AvatarUrl = relativePath,
                    FullPath = fullPath // Для отладки (в продакшене удалить)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при загрузке файла");
            }
        }
    }
}

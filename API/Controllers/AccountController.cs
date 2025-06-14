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
    [Authorize] // Все методы этого контроллера требуют аутентификации
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
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateUserAccountDto updateDto)
        {
            // Код получения идентификатора пользователя из claim "sub"
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
            if (uploadDto == null || uploadDto.Avatar == null || uploadDto.Avatar.Length == 0)
            {
                return BadRequest("Файл не выбран или пуст.");
            }

            // Получаем идентификатор пользователя из JWT (claim "sub")
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Идентификатор пользователя отсутствует или имеет неверный формат.");
            }

            // Определяем папку для загрузки: wwwroot/uploads/avatars
            string folderPath = Path.Combine("uploads", "avatars");
            string webRootPath = _env.WebRootPath;  // Получаем путь к wwwroot
            string fullFolderPath = Path.Combine(webRootPath, folderPath);

            // Если папка не существует, создаем её
            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            // Генерируем уникальное имя файла с сохранением оригинального расширения
            string fileExtension = Path.GetExtension(uploadDto.Avatar.FileName);
            string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            string fullFilePath = Path.Combine(fullFolderPath, uniqueFileName);

            // Сохраняем файл
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await uploadDto.Avatar.CopyToAsync(stream);
            }

            // Формируем относительный путь, который будет сохранен в базе (например, "uploads/avatars/uniqueFileName.jpg")
            string relativePath = Path.Combine(folderPath, uniqueFileName).Replace("\\", "/");

            // Обновляем поле AvatarUrl для пользователя через сервис
            var updatedUser = await _userService.UpdateAvatarAsync(userId, relativePath);

            return Ok(new { AvatarUrl = updatedUser.AvatarUrl });
        }
    }
}

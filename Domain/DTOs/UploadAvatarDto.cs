using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для загрузки файла аватарки.
    /// </summary>
    public class UploadAvatarDto
    {
        /// <summary>
        /// Файл аватарки в формате multipart/form-data.
        /// </summary>
        [Required]
        public IFormFile Avatar { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}

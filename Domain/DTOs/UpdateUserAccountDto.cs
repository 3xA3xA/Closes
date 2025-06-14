using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для обновления данных аккаунта пользователя.
    /// Позволяет изменить имя, email (логин), пароль и аватар.
    /// </summary>
    public class UpdateUserAccountDto
    {
        /// <summary>
        /// Новое имя пользователя. Если не задано, останется прежним.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Новый email пользователя (логин). Должен быть уникален, если указан.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Старый пароль. Обязателен, если указывается новый пароль.
        /// </summary>
        public string? OldPassword { get; set; }

        /// <summary>
        /// Новый пароль пользователя.
        /// </summary>
        public string? NewPassword { get; set; }

        /// <summary>
        /// URL аватарки пользователя.
        /// </summary>
        public string? AvatarUrl { get; set; }
    }
}

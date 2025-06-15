using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для создания нового списка пожеланий.
    /// </summary>
    public class CreateWishlistDto
    {
        /// <summary>
        /// Идентификатор пользователя, которому принадлежит список.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Заголовок списка пожеланий.
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для создания группы.
    /// </summary>
    public class CreateGroupDto
    {
        /// <summary>
        /// Название группы.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Тип группы (например, Пара, Семья, Друзья).
        /// </summary>
        public GroupType Type { get; set; }

        /// <summary>
        /// Идентификатор владельца группы.
        /// Обычно это Id пользователя, создавшего группу.
        /// </summary>
        public Guid OwnerId { get; set; }
    }
}

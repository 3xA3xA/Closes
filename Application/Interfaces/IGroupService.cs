using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с группами.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Создает новую группу.
        /// </summary>
        /// <param name="dto">Данные для создания группы.</param>
        /// <returns>Объект созданной группы.</returns>
        Task<Group> CreateGroupAsync(CreateGroupDto dto);

        /// <summary>
        /// Получает группу по её уникальному идентификатору.
        /// </summary>
        /// <param name="groupId">Уникальный идентификатор группы.</param>
        /// <returns>Найденная группа или null, если группа не найдена.</returns>
        Task<Group> GetGroupByIdAsync(Guid groupId);

        /// <summary>
        /// Получает группу по её уникальному коду.
        /// </summary>
        /// <param name="groupCode">Уникальный код группы (например, 5-символьная строка).</param>
        /// <returns>Найденная группа или null, если группа не найдена.</returns>
        Task<Group> GetGroupByCodeAsync(string groupCode);

        /// <summary>
        /// Получает все группы, в которых участвует пользователь.
        /// Пользователь может быть владельцем или участником.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>Список групп.</returns>
        Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid userId);
    }
}

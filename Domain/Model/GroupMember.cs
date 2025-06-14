using Domain.Enum;

namespace Domain.Model
{
    /// <summary>
    /// Представляет участника группы.
    /// </summary>
    public class GroupMember
    {
        /// <summary>
        /// Уникальный идентификатор записи участия.
        /// </summary>
        public Guid MemberId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор пользователя, который является участником.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор группы, в которой состоит пользователь.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Дата присоединения пользователя к группе.
        /// </summary>
        public DateTime JoinedAt { get; set; }

        /// <summary>
        /// Роль участника в группе, например "администратор" или "обычный участник".
        /// </summary>
        public GroupRoleType Role { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Пользователь, связанный с этой записью.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Группа, связанная с этой записью.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}

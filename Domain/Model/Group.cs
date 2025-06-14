using Domain.Enum;

namespace Domain.Model
{
    public class Group
    {
        /// <summary>
        /// Уникальный идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название группы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип группы - Family, Couple или Friends.
        /// </summary>
        public GroupType Type { get; set; }

        /// <summary>
        /// Идентификатор владельца группы (внешний ключ к User).
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// URL изображения, представляющего группу. Хранить лишь ссылку на внешний ресурс.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Описание группы.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка для приглашения в группу.
        /// </summary>
        public string ShareLink { get; set; }

        /// <summary>
        /// Дата создания группы.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата последнего обновления данных группы.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Коллекция участников группы.
        /// </summary>
        public virtual ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();

        /// <summary>
        /// Связанные элементы активности, если группа участвует в активностях.
        /// </summary>
        public virtual ICollection<ActivityItem> ActivityItems { get; set; } = new List<ActivityItem>();
    }
}

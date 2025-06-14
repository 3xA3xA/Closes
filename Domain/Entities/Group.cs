using Domain.Enums;

namespace Domain.Entities
{
    // Группа (Пара, Семья, Друзья)
    public class Group
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public GroupType Type { get; set; }
        public Guid OwnerId { get; set; }  // Связь с User.Id, владелец группы
        public string Code { get; set; } = string.Empty; // Уникальный код (5 символов)
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public virtual ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}

using Domain.Enums;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    // Активность (событие, викторина, прочее) — принадлежит конкретной группе
    public class Activity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public ActivityStatus Status { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        // Привязка к группе
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        // Участники активности
        public virtual ICollection<ActivityMember> ActivityMembers { get; set; } = new List<ActivityMember>();
    }
}

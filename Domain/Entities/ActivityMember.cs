using System.Text.Json.Serialization;

namespace Domain.Entities
{
    // Промежуточная таблица для связи активностей и пользователей
    public class ActivityMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ActivityId { get; set; }
        public Guid GroupMemberId { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public virtual Activity Activity { get; set; }
        public virtual GroupMember GroupMember { get; set; }
        public virtual ICollection<ActivityMember> ActivityMembers { get; set; } = new List<ActivityMember>();
    }
}

using Domain.Enums;

namespace Domain.Entities
{
    // Членство в группе
    public class GroupMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime JoinedAt { get; set; }
        public GroupRole Role { get; set; }
        public string UniqueColor { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}

namespace Domain.Entities
{
    // Промежуточная таблица для связи активностей и пользователей
    public class ActivityMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ActivityId { get; set; }
        public Guid UserId { get; set; }

        // Навигационные свойства
        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}

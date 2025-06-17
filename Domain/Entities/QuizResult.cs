namespace Domain.Entities
{
    // Результат завершённой викторины (общий результат)
    public class QuizResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizId { get; set; }
        public Guid GroupMemberId { get; set; }
        public DateTime CompletedAt { get; set; }
        public int Score { get; set; }

        // Навигационные свойства
        public virtual Quiz Quiz { get; set; }
        public virtual GroupMember GroupMember { get; set; }
    }
}

namespace Domain.Entities
{
    // Результат завершённой викторины (общий результат)
    public class QuizResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CompletedAt { get; set; }
        public int Score { get; set; }

        // Навигационные свойства
        public virtual Quiz Quiz { get; set; }
        public virtual User User { get; set; }
    }
}

namespace Domain.Entities
{
    // Ответ пользователя на вопрос викторины
    public class QuizAnswer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizItemId { get; set; }
        public Guid UserId { get; set; }
        public string Answer { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual QuizItem QuizItem { get; set; }
        public virtual User User { get; set; }
    }
}

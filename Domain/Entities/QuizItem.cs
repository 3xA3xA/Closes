namespace Domain.Entities
{
    // Элемент викторины (вопрос)
    public class QuizItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizId { get; set; }
        public string Text { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
    }
}

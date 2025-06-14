namespace Domain.Entities
{
    // Викторина
    public class Quiz
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Список вопросов (элементы викторины)
        public virtual ICollection<QuizItem> QuizItems { get; set; } = new List<QuizItem>();
        // Результаты прохождения викторины
        public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}

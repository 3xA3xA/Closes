using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }

        // Навигационное свойство к пользователю (создателю квиза)
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<QuizItem> QuizItems { get; set; } = new List<QuizItem>();
        // Результаты прохождения викторины
        [JsonIgnore]
        public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}

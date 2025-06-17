namespace Domain.Entities
{
    // Ответ участника группы на вопрос викторины
    public class QuizAnswer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuizItemId { get; set; }
        public Guid GroupMemberId { get; set; }
        public string Answer { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual QuizItem QuizItem { get; set; }
        public virtual GroupMember GroupMember { get; set; }
    }
}

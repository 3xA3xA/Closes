using Domain.Enums;
using System.Text.Json.Serialization;

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

        // Коллекция созданных элементов вишлиста от данного участника
        [JsonIgnore]
        public virtual ICollection<WishlistItem> CreatedWishlistItems { get; set; } = new List<WishlistItem>();

        // Если у участника группы есть ответы на викторины, добавляем навигационное свойство.
        [JsonIgnore]
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();

        // Если у участника группы имеются результаты викторин, добавляем навигационное свойство.
        [JsonIgnore]
        public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}

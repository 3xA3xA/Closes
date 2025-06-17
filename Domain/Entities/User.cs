using System.Text.Json.Serialization;

namespace Domain.Entities
{
    // Пользователь
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<WishlistItem> CreatedWishlistItems { get; set; } = new List<WishlistItem>();
        public virtual ICollection<ActivityMember> ActivityMembers { get; set; } = new List<ActivityMember>();
        [JsonIgnore]
        public virtual ICollection<Quiz> CreatedQuizzes { get; set; } = new List<Quiz>();


    }
}
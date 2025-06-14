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
        public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<WishlistItem> CreatedWishlistItems { get; set; } = new List<WishlistItem>();
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
        public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
        public virtual ICollection<ActivityMember> ActivityMembers { get; set; } = new List<ActivityMember>();
        
    }
}
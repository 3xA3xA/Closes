namespace Domain.Entities
{
    // Элемент Wish List (пожелание)
    // Элемент Wish List (пожелание)
    public class WishlistItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WishlistId { get; set; }

        public Guid GroupMemberId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public virtual Wishlist Wishlist { get; set; }
        // Вместо User теперь будет ссылка на участника группы
        public virtual GroupMember GroupMember { get; set; }
    }
}

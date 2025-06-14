namespace Domain.Entities
{
    // Элемент Wish List (пожелание)
    public class WishlistItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WishlistId { get; set; }
        public Guid UserId { get; set; } // Используется для определения персонального цвета/приоритета
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty; // Можно заменить на Enum (например, Low, Medium, High)
        public string ImageUrl { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public virtual Wishlist Wishlist { get; set; }
        public virtual User User { get; set; }
    }
}

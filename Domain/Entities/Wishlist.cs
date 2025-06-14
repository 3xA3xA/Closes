namespace Domain.Entities
{
    // Wish List (список пожеланий)
    public class Wishlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства
        public virtual User User { get; set; }
        public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}

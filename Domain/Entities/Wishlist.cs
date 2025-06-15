namespace Domain.Entities
{
    // Wish List (список пожеланий)
    public class Wishlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GroupId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства

        // Если у вас уже есть сущность Group, можно добавить ссылку на неё:
        public virtual Group Group { get; set; }

        public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}

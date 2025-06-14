using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    /// <summary>
    /// Элемент wish-листа, представляющий товар или услугу.
    /// </summary>
    public class WishlistItem
    {
        /// <summary>
        /// Уникальный идентификатор элемента wish-листа.
        /// </summary>
        public Guid WishlistItemId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Внешний ключ, связывающий элемент с конкретным wish-листом.
        /// </summary>
        public Guid WishlistId { get; set; }

        /// <summary>
        /// Название элемента (например, название желаемого подарка).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость элемента.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// URL изображения элемента. Храним лишь путь или ссылку на внешний ресурс, а не сам объект.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Подробное описание элемента.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Флаг, указывающий, что элемент уже выполнен (например, подарок куплен).
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Дата создания элемента.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата последнего обновления элемента.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        // Навигационное свойство

        /// <summary>
        /// Связанный wish-лист, которому принадлежит элемент.
        /// </summary>
        public virtual Wishlist Wishlist { get; set; }
    }
}

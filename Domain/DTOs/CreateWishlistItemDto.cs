using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для создания нового элемента вишлиста.
    /// </summary>
    public class CreateWishlistItemDto
    {
        public Guid WishlistId { get; set; }
        public Guid GroupMemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}

using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с вишлистами.
    /// </summary>
    public interface IWishlistService
    {
        Task<Wishlist> CreateWishlistAsync(CreateWishlistDto dto);
        Task<Wishlist> GetWishlistByIdAsync(Guid wishlistId);
        Task<IEnumerable<Wishlist>> GetWishlistsByUserIdAsync(Guid userId);
        Task<IEnumerable<Wishlist>> GetWishlistsByGroupIdAsync(Guid groupId);
        Task<WishlistItem> AddWishlistItemAsync(CreateWishlistItemDto dto);
        Task<IEnumerable<WishlistItemDto>> GetWishlistItemsAsync(Guid groupId);
        Task DeleteWishlistItemAsync(Guid wishlistItemId);
    }
}

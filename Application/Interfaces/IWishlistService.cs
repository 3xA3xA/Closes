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

        /// <summary>
        /// Получает все элементы вишлиста и, если alternate=true, возвращает их в чередующемся порядке (round‑robin) между добавившими их пользователями.
        /// </summary>
        /// <param name="wishlistId">Уникальный идентификатор вишлиста.</param>
        /// <param name="alternate">Если true, порядок будет организован по чередованию между разными пользователями.</param>
        Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(Guid wishlistId, bool alternate = false);
    }
}

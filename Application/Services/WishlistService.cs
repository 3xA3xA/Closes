using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Сервис для работы с вишлистом группы – общий список для конкретной группы,
    /// куда разные участники могут добавлять свои предметы.
    /// Дополнительно метод GetWishlistItemsAsync позволяет опционально вернуть предметы в чередующемся порядке.
    /// </summary>
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _dbContext;

        public WishlistService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Создаёт новый вишлист для группы.
        /// Ожидается, что CreateWishlistDto содержит поле GroupId.
        /// </summary>
        public async Task<Wishlist> CreateWishlistAsync(CreateWishlistDto dto)
        {
            var wishlist = new Wishlist
            {
                // Вместо UserId теперь используем GroupId, так как вишлист общий для группы.
                GroupId = dto.GroupId,
                Title = dto.Title,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Wishlists.Add(wishlist);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Wishlists
                .Include(w => w.Items)
                .FirstOrDefaultAsync(w => w.Id == wishlist.Id);
        }

        /// <summary>
        /// Получает вишлист по его уникальному идентификатору с подгрузкой элементов.
        /// </summary>
        public async Task<Wishlist> GetWishlistByIdAsync(Guid wishlistId)
        {
            return await _dbContext.Wishlists
                .Include(w => w.Items)
                    .ThenInclude(i => i.GroupMember)
                        .ThenInclude(gm => gm.User)
                .FirstOrDefaultAsync(w => w.Id == wishlistId);
        }

        /// <summary>
        /// Получает все вишлисты для пользователя, основываясь на том, что пользователь состоит в группе,
        /// для которой создан общий вишлист.
        /// </summary>
        public async Task<IEnumerable<Wishlist>> GetWishlistsByUserIdAsync(Guid userId)
        {
            return await _dbContext.Wishlists
                .Include(w => w.Items)
                    .ThenInclude(i => i.GroupMember)
                        .ThenInclude(gm => gm.User)
                .Where(w => _dbContext.GroupMembers.Any(m => m.GroupId == w.GroupId && m.UserId == userId))
                .ToListAsync();
        }

        /// <summary>
        /// Получает вишлисты для указанной группы.
        /// </summary>
        public async Task<IEnumerable<Wishlist>> GetWishlistsByGroupIdAsync(Guid groupId)
        {
            return await _dbContext.Wishlists
                .Include(w => w.Items)
                    .ThenInclude(i => i.GroupMember)
                        .ThenInclude(gm => gm.User)
                .Where(w => w.GroupId == groupId)
                .ToListAsync();
        }

        /// <summary>
        /// Добавляет новый элемент в вишлист.
        /// </summary>
        public async Task<WishlistItem> AddWishlistItemAsync(CreateWishlistItemDto dto)
        {
            // Проверка существования вишлиста
            var wishlist = await _dbContext.Wishlists.FindAsync(dto.WishlistId);
            if (wishlist == null)
                throw new Exception("Вишлист не найден.");

            // Проверка существования участника группы
            var groupMember = await _dbContext.GroupMembers.FindAsync(dto.GroupMemberId);
            if (groupMember == null)
                throw new Exception("Участник группы не найден.");

            var wishlistItem = new WishlistItem
            {
                WishlistId = dto.WishlistId,
                GroupMemberId = dto.GroupMemberId,
                Name = dto.Name,
                Description = dto.Description,
                Priority = dto.Priority,
                ImageUrl = dto.ImageUrl,
                Completed = dto.Completed,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.WishlistItems.Add(wishlistItem);
            await _dbContext.SaveChangesAsync();

            return wishlistItem;
        }

        /// <summary>
        /// Удаляет товар из вишлиста по его идентификатору.
        /// </summary>
        /// <param name="wishlistItemId">Уникальный идентификатор товара вишлиста, который необходимо удалить.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task DeleteWishlistItemAsync(Guid wishlistItemId)
        {
            // Находим элемент вишлиста по его Id
            var item = await _dbContext.WishlistItems.FindAsync(wishlistItemId);
            if (item == null)
            {
                throw new Exception("Элемент вишлиста не найден.");
            }

            _dbContext.WishlistItems.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Получает все элементы вишлиста, упорядоченные по CreatedAt.
        /// </summary>
        public async Task<IEnumerable<WishlistItemDto>> GetWishlistItemsAsync(Guid groupId)
        {
            // Получаем вишлист, принадлежащий группе
            var wishlist = await _dbContext.Wishlists
                .FirstOrDefaultAsync(w => w.GroupId == groupId);

            // Если вишлист не найден, возвращаем пустой список или обрабатываем соответствующим образом
            if (wishlist == null)
            {
                return new List<WishlistItemDto>();
            }

            // Выбираем товары вишлиста по его Id
            var items = await _dbContext.WishlistItems
                 .Where(i => i.WishlistId == wishlist.Id)
                 .OrderBy(i => i.CreatedAt)
                 .Select(i => new WishlistItemDto
                 {
                     WishlistItemId = i.Id,
                     //WishlistId = i.WishlistId,
                     GroupMemberId = i.GroupMemberId,
                     Name = i.Name,
                     Description = i.Description,
                     Priority = i.Priority,
                     ImageUrl = i.ImageUrl,
                     Completed = i.Completed,
                     CreatedAt = i.CreatedAt
                 })
                 .ToListAsync();

            return items;
        }
    }
}

using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
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
    /// Сервис для работы с группами.
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _dbContext;

        public GroupService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Group> CreateGroupAsync(CreateGroupDto dto)
        {
            // Создаем группу с указанными данными;
            // владелец задается через поле OwnerId, но его запись в таблице GroupMembers нужно создать отдельно.
            var group = new Group
            {
                Name = dto.Name,
                Type = dto.Type,
                OwnerId = dto.OwnerId,
                CreatedAt = DateTime.UtcNow,
                Code = await GenerateUniqueGroupCodeAsync()
            };

            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync();

            // Добавляем владельца группы в качестве участника с ролью Owner.
            var ownerMember = new GroupMember
            {
                UserId = dto.OwnerId,
                GroupId = group.Id,
                JoinedAt = DateTime.UtcNow,
                Role = GroupRole.Owner,  // Если у вас нет специального значения для владельца, можно использовать GroupRole.Member.
                UniqueColor = GenerateRandomColor()
            };

            _dbContext.GroupMembers.Add(ownerMember);
            await _dbContext.SaveChangesAsync();

            // Возвращаем группу с подгруженными участниками.
            return await _dbContext.Groups
                .Include(g => g.Members)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(g => g.Id == group.Id);
        }

        /// <summary>
        /// Удаляет группу по её идентификатору.
        /// Удалять группу может только её владелец.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя, пытающегося удалить группу.</param>
        public async Task DeleteGroupAsync(Guid groupId, Guid userId)
        {
            // Получаем группу вместе с участниками.
            var group = await _dbContext.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
            {
                throw new Exception("Группа не найдена.");
            }

            // Проверяем, что пользователь является владельцем группы.
            if (group.OwnerId != userId)
            {
                throw new Exception("Удалить группу может только владелец.");
            }

            // Получаем идентификаторы участников группы.
            var memberIds = group.Members.Select(m => m.Id).ToList();

            // Находим все товары вишлиста, принадлежащие этим участникам.
            var wishlistItems = await _dbContext.WishlistItems
                .Where(wi => memberIds.Contains(wi.GroupMemberId))
                .ToListAsync();

            // Удаляем товары вишлиста.
            _dbContext.WishlistItems.RemoveRange(wishlistItems);

            // Удаляем участников группы.
            _dbContext.GroupMembers.RemoveRange(group.Members);

            // Удаляем саму группу.
            _dbContext.Groups.Remove(group);

            await _dbContext.SaveChangesAsync();
        }


        public async Task<Group> GetGroupByIdAsync(Guid groupId)
        {
            return await _dbContext.Groups
                .Include(g => g.Members)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<Group> GetGroupByCodeAsync(string groupCode)
        {
            return await _dbContext.Groups
                .Include(g => g.Members)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(g => g.Code == groupCode);
        }

        public async Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid userId)
        {
            return await _dbContext.Groups
                .Include(g => g.Members)
                    .ThenInclude(m => m.User)
                .Where(g => g.OwnerId == userId || g.Members.Any(m => m.UserId == userId))
                .ToListAsync();
        }

        public async Task<GroupMember> JoinGroupAsync(string groupCode, Guid userId)
        {
            // Находим группу по уникальному коду.
            var group = await _dbContext.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Code == groupCode);
            if (group == null)
                throw new Exception("Группа не найдена.");

            // Если пользователь уже является владельцем или участником, выдаем ошибку.
            if (group.OwnerId == userId || group.Members.Any(m => m.UserId == userId))
                throw new Exception("Пользователь уже состоит в группе.");

            var member = new GroupMember
            {
                UserId = userId,
                GroupId = group.Id,
                JoinedAt = DateTime.UtcNow,
                Role = GroupRole.Member,
                UniqueColor = GenerateRandomColor()
            };

            _dbContext.GroupMembers.Add(member);
            await _dbContext.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// Позволяет пользователю выйти из группы.
        /// Владелец группы не может покинуть её, его нужно либо удалить, либо передать владение.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя, который хочет покинуть группу.</param>
        public async Task LeaveGroupAsync(Guid groupId, Guid userId)
        {
            // Находим запись участника в группе.
            var member = await _dbContext.GroupMembers
                .FirstOrDefaultAsync(m => m.GroupId == groupId && m.UserId == userId);

            if (member == null)
            {
                throw new Exception("Вы не состоите в данной группе.");
            }

            // Проверяем, является ли пользователь владельцем.
            var group = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group.OwnerId == userId)
            {
                throw new Exception("Владелец группы не может покинуть группу. Удалите группу или передайте владение другому пользователю.");
            }

            _dbContext.GroupMembers.Remove(member);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GroupMember> GetGroupMemberAsync(Guid userId, Guid groupId)
        {
            var member = await _dbContext.GroupMembers
                .FirstOrDefaultAsync(m => m.UserId == userId && m.GroupId == groupId);

            if (member == null)
            {
                throw new Exception("Пользователь не найден в указанной группе.");
            }

            return member;
        }

        /// <summary>
        /// Генерирует уникальный код группы из 5 символов.
        /// </summary>
        /// <returns>Уникальный код группы.</returns>
        private async Task<string> GenerateUniqueGroupCodeAsync()
        {
            string code;
            do
            {
                code = GenerateRandomCode(5);
            }
            while (await _dbContext.Groups.AnyAsync(g => g.Code == code));
            return code;
        }

        /// <summary>
        /// Генерирует случайный код указанной длины.
        /// </summary>
        /// <param name="length">Длина кода.</param>
        /// <returns>Случайный код.</returns>
        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            // Для простоты используем System.Random; для продакшна можно рассмотреть более криптостойкий генератор.
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Генерирует случайный HEX-цвет в формате "#RRGGBB".
        /// </summary>
        /// <returns>Строка с уникальным цветом.</returns>
        private string GenerateRandomColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}";
        }
    }
}

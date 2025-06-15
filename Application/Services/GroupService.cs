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
    /// Сервис для работы с группами.
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _dbContext;

        public GroupService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Group> CreateGroupAsync(CreateGroupDto dto)
        {
            // Создаем объект группы и заполняем поля
            var group = new Group
            {
                Name = dto.Name,
                Type = dto.Type,
                OwnerId = dto.OwnerId,
                CreatedAt = DateTime.UtcNow,
                // Генерируем уникальный код из 5 символов
                Code = await GenerateUniqueGroupCodeAsync()
            };

            _dbContext.Groups.Add(group);
            await _dbContext.SaveChangesAsync();
            return group;
        }

        /// <inheritdoc />
        public async Task<Group> GetGroupByIdAsync(Guid groupId)
        {
            return await _dbContext.Groups
                .Include(g => g.Members)  // если нужно подтянуть участников
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }

        /// <inheritdoc />
        public async Task<Group> GetGroupByCodeAsync(string groupCode)
        {
            // Ищем группу, у которой поле Code совпадает с переданным значением (без учета регистра можно добавить настройку, если требуется)
            return await _dbContext.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Code == groupCode);
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
            // Для простоты используем System.Random; для продакшна можно рассмотреть более криптостойкий генератор
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

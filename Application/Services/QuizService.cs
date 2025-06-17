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
    public class QuizService : IQuizService
    {
        private readonly AppDbContext _dbContext;

        public QuizService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Quiz> CreateQuizAsync(CreateQuizDto dto)
        {
            // Создаем новый объект квиза с заданными параметрами
            var quiz = new Quiz
            {
                Name = dto.Name,
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId
            };

            // Добавляем квиз в контекст и сохраняем изменения
            _dbContext.Quizzes.Add(quiz);
            await _dbContext.SaveChangesAsync();

            // Можно при необходимости загрузить дополнительные навигационные свойства

            return quiz;
        }

        public async Task<QuizItem> CreateQuizItemAsync(CreateQuizItemDto dto)
        {
            // Создаем новый объект вопроса квиза.
            var quizItem = new QuizItem
            {
                QuizId = dto.QuizId,
                Text = dto.Text
            };

            _dbContext.QuizItems.Add(quizItem);
            await _dbContext.SaveChangesAsync();

            // Можно добавить Include навигационных свойств, если необходимо.
            return quizItem;
        }

        public async Task<QuizItem?> GetQuizItemByIdAsync(Guid id)
        {
            // Можно добавить Include навигационных свойств, если это необходимо.
            return await _dbContext.QuizItems.FirstOrDefaultAsync(qi => qi.Id == id);
        }

        public async Task<IEnumerable<QuizItem>> GetQuizItemsByQuizIdAsync(Guid quizId)
        {
            return await _dbContext.QuizItems
                .Where(qi => qi.QuizId == quizId)
                .ToListAsync();
        }

        public async Task<bool> DeleteQuizItemByIdAsync(Guid id)
        {
            var quizItem = await _dbContext.QuizItems.FirstOrDefaultAsync(qi => qi.Id == id);
            if (quizItem == null)
            {
                return false; // Вопрос не найден
            }

            _dbContext.QuizItems.Remove(quizItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

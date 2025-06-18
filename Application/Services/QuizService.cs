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
            // Создаем объект квиза и заполняем его основными свойствами
            var quiz = new Quiz
            {
                Name = dto.Name,
                Description = dto.Description, // новое поле
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId
            };

            // Для каждого вопроса, пришедшего из фронта, создаём объект QuizItem
            foreach (var question in dto.Questions)
            {
                quiz.QuizItems.Add(new QuizItem
                {
                    Text = question.Text
                });
            }

            _dbContext.Quizzes.Add(quiz);
            await _dbContext.SaveChangesAsync();

            return quiz;
        }

        public async Task<QuizItem?> GetQuizItemByIdAsync(Guid id)
        {
            return await _dbContext.QuizItems.FirstOrDefaultAsync(qi => qi.Id == id);
        }

        public async Task<Quiz?> GetQuizWithItemsByQuizIdAsync(Guid quizId)
        {
            return await _dbContext.Quizzes
                        .Include(q => q.QuizItems)
                        .FirstOrDefaultAsync(q => q.Id == quizId);
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

        public async Task<bool> DeleteQuizByIdAsync(Guid quizId)
        {
            // Загружаем квиз вместе с вопросами
            var quiz = await _dbContext.Quizzes
                        .Include(q => q.QuizItems)
                        .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
                return false;

            _dbContext.Quizzes.Remove(quiz);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

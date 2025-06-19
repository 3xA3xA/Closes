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
            var quiz = new Quiz
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId
            };

            // Задаём порядок для каждого вопроса по индексу
            int order = 1;
            foreach (var question in dto.Questions)
            {
                quiz.QuizItems.Add(new QuizItem
                {
                    Text = question.Text,
                    Order = order++
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
            var quiz = await _dbContext.Quizzes
                        .Include(q => q.QuizItems) 
                        .FirstOrDefaultAsync(q => q.Id == quizId);

            // Если квиз найден, упорядочим вопросы по Order
            if (quiz != null)
            {
                // Приводим коллекцию к списку с нужным порядком
                quiz.QuizItems = quiz.QuizItems.OrderBy(qi => qi.Order).ToList();
            }

            return quiz;
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

        public async Task<IEnumerable<QuizAnswer>> SubmitQuizAnswersByQuizAsync(SubmitQuizAnswersDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.Answers == null || !dto.Answers.Any())
                throw new ArgumentException("Список ответов пуст", nameof(dto.Answers));

            // Загружаем квиз с его вопросами
            var quiz = await _dbContext.Quizzes
                        .Include(q => q.QuizItems)
                        .FirstOrDefaultAsync(q => q.Id == dto.QuizId);
            if (quiz == null)
                throw new Exception("Квиз не найден");

            // Сортируем вопросы по полю Order
            var quizItems = quiz.QuizItems.OrderBy(qi => qi.Order).ToList();

            if (quizItems.Count != dto.Answers.Count)
                throw new Exception($"Количество вопросов квиза ({quizItems.Count}) не совпадает с количеством переданных ответов ({dto.Answers.Count}).");

            // Создаем ответы, сопоставляя каждый вопрос с соответствующим ответом
            var quizAnswers = new List<QuizAnswer>();
            for (int i = 0; i < quizItems.Count; i++)
            {
                var answer = new QuizAnswer
                {
                    QuizItemId = quizItems[i].Id,
                    GroupMemberId = dto.GroupMemberId,
                    Answer = dto.Answers[i]
                };

                quizAnswers.Add(answer);
            }

            await _dbContext.QuizAnswers.AddRangeAsync(quizAnswers);
            await _dbContext.SaveChangesAsync();

            return quizAnswers;
        }

        public async Task<QuizWithAnswersDto?> GetQuizWithItemsAndAnswersForMemberByQuizIdAsync(Guid quizId, Guid groupMemberId)
        {
            var quiz = await _dbContext.Quizzes
                .Include(q => q.QuizItems)
                    .ThenInclude(qi => qi.QuizAnswers)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
                return null;

            // Сортируем вопросы по Order
            var sortedQuizItems = quiz.QuizItems.OrderBy(qi => qi.Order).ToList();

            // Формируем DTO с выбором единственного ответа для указанного участника
            var quizDto = new QuizWithAnswersDto
            {
                Id = quiz.Id,
                Name = quiz.Name,
                Category = quiz.Category,
                Description = quiz.Description,
                CreatedAt = quiz.CreatedAt,
                UserId = quiz.UserId,
                QuizItems = sortedQuizItems.Select(qi => new QuizItemDto
                {
                    Id = qi.Id,
                    QuizId = qi.QuizId,
                    Text = qi.Text,
                    Order = qi.Order,
                    Answer = qi.QuizAnswers.FirstOrDefault(qa => qa.GroupMemberId == groupMemberId)?.Answer
                })
            };

            return quizDto;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            // Можно добавить сортировку или фильтрацию при необходимости
            return await _dbContext.Quizzes.ToListAsync();
        }
    }
}

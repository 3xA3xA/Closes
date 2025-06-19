using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuizAsync(CreateQuizDto dto);
        Task<QuizItem?> GetQuizItemByIdAsync(Guid id);
        Task<Quiz?> GetQuizWithItemsByQuizIdAsync(Guid quizId);
        Task<bool> DeleteQuizItemByIdAsync(Guid id);
        Task<bool> DeleteQuizByIdAsync(Guid quizId);
        Task<IEnumerable<QuizAnswer>> SubmitQuizAnswersByQuizAsync(SubmitQuizAnswersDto dto);
        Task<QuizWithAnswersDto?> GetQuizWithItemsAndAnswersForMemberByQuizIdAsync(Guid quizId, Guid groupMemberId);
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
    }
}

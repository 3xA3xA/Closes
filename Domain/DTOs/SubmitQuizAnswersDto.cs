using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{

    public class SubmitQuizAnswersDto
    {
        public Guid GroupMemberId { get; set; }
        public Guid QuizId { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }
    //public class SubmitQuizAnswersDto
    //{
    //    // Идентификатор участника группы, который отвечает на вопросы
    //    public Guid GroupMemberId { get; set; }

    //    // Список ответов
    //    public List<QuizAnswerDto> Answers { get; set; } = new List<QuizAnswerDto>();
    //}
}

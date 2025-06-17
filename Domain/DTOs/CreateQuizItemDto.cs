using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateQuizItemDto
    {
        // Идентификатор квиза, к которому принадлежит вопрос.
        public Guid QuizId { get; set; }

        // Текст вопроса.
        public string Text { get; set; } = string.Empty;
    }
}

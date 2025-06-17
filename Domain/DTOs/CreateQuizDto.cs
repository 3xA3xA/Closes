using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateQuizDto
    {
        public string Name { get; set; } = string.Empty;

        // Новое поле – описание квиза
        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
        public Guid UserId { get; set; }

        // Список вопросов квиза
        public List<CreateQuizQuestionDto> Questions { get; set; } = new List<CreateQuizQuestionDto>();
    }
}

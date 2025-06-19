using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class QuizItemDto
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Answer { get; set; }
    }
}

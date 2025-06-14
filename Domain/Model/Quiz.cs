using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    internal class Quiz
    {
        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        public Guid QuizId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Вопрос, который задаётся участникам.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Категория игры. Можно задать фиксированный перечень значений через enum, если понадобится.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Дата создания игры.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата последнего обновления игры.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Коллекция ответов участников на этот вопрос.
        /// </summary>
        public virtual ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }
}

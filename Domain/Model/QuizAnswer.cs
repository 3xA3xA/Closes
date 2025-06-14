using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    /// <summary>
    /// Представляет ответ пользователя на вопрос из игры.
    /// После того как пользователь отвечает, может быть инициировано уведомление другим участникам группы.
    /// </summary>
    internal class QuizAnswer
    {
        /// <summary>
        /// Уникальный идентификатор ответа.
        /// </summary>
        public Guid QuizAnswerId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Внешний ключ к вопросу игры, на который даётся ответ.
        /// </summary>
        public Guid GameId { get; set; }

        /// <summary>
        /// Внешний ключ к пользователю, который дал ответ.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Текст ответа пользователя.
        /// </summary>
        public string UserAnswer { get; set; }

        /// <summary>
        /// Дата и время, когда пользователь оставил ответ.
        /// </summary>
        public DateTime AnsweredAt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Игра, к которой относится ответ.
        /// </summary>
        public virtual Quiz Quiz { get; set; }

        /// <summary>
        /// Пользователь, который дал ответ.
        /// </summary>
        public virtual User User { get; set; }
    }
}

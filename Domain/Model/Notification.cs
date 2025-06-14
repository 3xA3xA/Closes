using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    /// <summary>
    /// Представляет уведомление, отправляемое участникам группы.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Уникальный идентификатор уведомления.
        /// </summary>
        public Guid NotificationId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор пользователя, которому предназначено уведомление.
        /// </summary>
        public Guid RecipientUserId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который инициировал уведомление (например, тот, кто дал ответ).
        /// </summary>
        public Guid SenderUserId { get; set; }

        /// <summary>
        /// Идентификатор ответа (GameAnswer) или события, на основании которого сформировано уведомление.
        /// Этот параметр связывает уведомление с конкретным ответом.
        /// </summary>
        public Guid QuizAnswerId { get; set; }

        /// <summary>
        /// Текст уведомления, который будет отображён пользователю.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Флаг, показывающий, было ли уведомление прочитано.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Дата и время создания уведомления.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Пользователь, получивший уведомление.
        /// </summary>
        public virtual User Recipient { get; set; }

        /// <summary>
        /// Пользователь, инициировавший уведомление.
        /// </summary>
        public virtual User Sender { get; set; }

        /// <summary>
        /// Ответ на игру, связанный с уведомлением.
        /// </summary>
        public virtual QuizAnswer QuizAnswer { get; set; }
    }
}

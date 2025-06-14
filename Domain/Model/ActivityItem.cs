namespace Domain.Model
{
    /// <summary>
    /// Представляет связь активности с конкретной группой.
    /// Например, активность добавлена для планирования в определённой группе.
    /// </summary>
    public class ActivityItem
    {
        /// <summary>
        /// Уникальный идентификатор записи активности для группы.
        /// </summary>
        public Guid ItemId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Внешний ключ к активности.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Внешний ключ к группе, для которой запланирована активность.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Дата и время, когда активность была добавлена группе.
        /// </summary>
        public DateTime AddedAt { get; set; }

        // Навигационные свойства

        /// <summary>
        /// Активность, связанная с этой записью.
        /// </summary>
        public virtual Activity Activity { get; set; }

        /// <summary>
        /// Группа, связанная с этой записью.
        /// </summary>
        public virtual Group Group { get; set; }
    }
}

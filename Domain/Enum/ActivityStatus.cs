using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    /// <summary>
    /// Перечисление статусов активности.
    /// </summary>
    public enum ActivityStatus
    {
        /// <summary>
        /// Активность завершена.
        /// </summary>
        Completed,
        /// <summary>
        /// Активность запланирована.
        /// </summary>
        Planning,
        /// <summary>
        /// Активность ещё не выполнена.
        /// </summary>
        NonCompleted
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для регистрации участника на активности.
    /// </summary>
    public class JoinActivityDto
    {
        /// <summary>
        /// Идентификатор активности.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Идентификатор участника группы.
        /// </summary>
        public Guid GroupMemberId { get; set; }
    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// DTO для представления данных участника группы.
    /// </summary>
    public class GroupMemberDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime JoinedAt { get; set; }
        public GroupRole Role { get; set; }
        public string UniqueColor { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}

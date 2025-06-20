using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateActivityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ActivityType Type { get; set; }
        public ActivityStatus Status { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public Guid GroupId { get; set; }
    }
}

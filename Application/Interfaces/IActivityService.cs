using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IActivityService
    {
        Task<Activity> CreateActivityAsync(CreateActivityDto dto);
        Task<IEnumerable<Activity>> GetActivitiesByGroupIdAsync(Guid groupId);
        Task<ActivityMember> JoinActivityAsync(JoinActivityDto dto);
    }
}

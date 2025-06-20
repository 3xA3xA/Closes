using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _dbContext;

        public ActivityService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> CreateActivityAsync(CreateActivityDto dto)
        {
            var activity = new Activity
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                Status = dto.Status,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                GroupId = dto.GroupId
            };

            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();
            return activity;
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByGroupIdAsync(Guid groupId)
        {
            return await _dbContext.Activities
                .Where(a => a.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<ActivityMember> JoinActivityAsync(JoinActivityDto dto)
        {
            var exists = await _dbContext.ActivityMembers.AnyAsync(am =>
                 am.ActivityId == dto.ActivityId && am.GroupMemberId == dto.GroupMemberId);
            if (exists)
                throw new Exception("Участник уже зарегистрирован на эту активность.");

            var activityMember = new ActivityMember
            {
                ActivityId = dto.ActivityId,
                GroupMemberId = dto.GroupMemberId
            };

            _dbContext.ActivityMembers.Add(activityMember);
            await _dbContext.SaveChangesAsync();

            return activityMember;
        }
    }
}

using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGroupService
    {
        Task<Group> CreateGroupAsync(CreateGroupDto dto);
        Task DeleteGroupAsync(Guid groupId, Guid userId);
        Task<Group> GetGroupByIdAsync(Guid groupId);
        Task<Group> GetGroupByCodeAsync(string groupCode);
        Task<IEnumerable<Group>> GetGroupsByUserIdAsync(Guid userId);
        Task<GroupMember> JoinGroupAsync(string groupCode, Guid userId);
        Task LeaveGroupAsync(Guid groupId, Guid userId);
        Task<GroupMember> GetGroupMemberAsync(Guid userId, Guid groupId);        
    }
}

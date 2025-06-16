using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для управления группами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Создает новую группу.
        /// При создании владелец автоматически добавляется как участник с ролью Owner.
        /// </summary>
        /// <param name="dto">Данные для создания группы.</param>
        /// <returns>HTTP-ответ с DTO созданной группы с участниками.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создание новой группы",
            Description = "Принимает данные группы, создает новую группу и возвращает объект с уникальным кодом, включая список участников (владельца, добавленного автоматически)."
        )]
        [SwaggerResponse(201, "Группа успешно создана", typeof(GroupDto))]
        [SwaggerResponse(400, "Неверные входные данные")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto dto)
        {
            try
            {
                Group createdGroup = await _groupService.CreateGroupAsync(dto);
                var response = MapToGroupDto(createdGroup);
                return CreatedAtAction(nameof(GetGroupById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Удаляет группу.
        /// Удалить группу может только владелец.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя, пытающегося удалить группу.</param>
        [HttpDelete("delete/{groupId:guid}")]
        [SwaggerOperation(
            Summary = "Удаление группы",
            Description = "Удаляет группу, если вызывающий пользователь является владельцем."
        )]
        [SwaggerResponse(200, "Группа успешно удалена")]
        [SwaggerResponse(400, "Ошибка при удалении группы")]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid groupId, [FromQuery] Guid userId)
        {
            try
            {
                await _groupService.DeleteGroupAsync(groupId, userId);
                return Ok(new { message = "Группа успешно удалена." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Присоединяет пользователя к группе по её уникальному коду.
        /// Для участия пользователь передаётся как параметр userId.
        /// </summary>
        /// <param name="groupCode">Уникальный код группы.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Данные о созданной записи GroupMember.</returns>
        [HttpPost("join/{groupCode}")]
        [SwaggerOperation(
            Summary = "Присоединение к группе по коду",
            Description = "Присоединяет пользователя к группе, найденной по её уникальному коду, и создает запись GroupMember."
        )]
        [SwaggerResponse(200, "Пользователь успешно присоединился к группе", typeof(GroupMemberDto))]
        [SwaggerResponse(400, "Ошибка при присоединении к группе")]
        public async Task<IActionResult> JoinGroup([FromRoute] string groupCode, [FromQuery] Guid userId)
        {
            try
            {
                GroupMember member = await _groupService.JoinGroupAsync(groupCode, userId);
                var response = new GroupMemberDto
                {
                    Id = member.Id,
                    GroupId = member.GroupId,
                    UserId = member.UserId,
                    JoinedAt = member.JoinedAt,
                    Role = member.Role,
                    UniqueColor = member.UniqueColor
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Позволяет пользователю выйти из группы.
        /// Владелец группы не может покинуть группу.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <param name="userId">Идентификатор пользователя, который хочет выйти из группы.</param>
        [HttpDelete("leave/{groupId:guid}")]
        [SwaggerOperation(
            Summary = "Выход из группы",
            Description = "Позволяет пользователю выйти из группы, если он не является владельцем."
        )]
        [SwaggerResponse(200, "Пользователь успешно вышел из группы")]
        [SwaggerResponse(400, "Ошибка при выходе из группы")]
        public async Task<IActionResult> LeaveGroup([FromRoute] Guid groupId, [FromQuery] Guid userId)
        {
            try
            {
                await _groupService.LeaveGroupAsync(groupId, userId);
                return Ok(new { message = "Вы успешно покинули группу." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получает группу по её уникальному идентификатору, включая список участников.
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы.</param>
        /// <returns>Объект группы с участниками.</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Получение группы по ID",
            Description = "Возвращает данные группы по её уникальному идентификатору, включая список участников."
        )]
        [SwaggerResponse(200, "Группа успешно найдена", typeof(GroupDto))]
        [SwaggerResponse(404, "Группа не найдена")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            Group group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound(new { message = "Группа не найдена" });

            return Ok(MapToGroupDto(group));
        }

        /// <summary>
        /// Поиск группы по уникальному коду.
        /// Возвращает объект группы с участниками.
        /// </summary>
        /// <param name="code">Уникальный код группы (например, 5-символьная строка).</param>
        /// <returns>Объект группы с участниками.</returns>
        [HttpGet("code/{code}")]
        [SwaggerOperation(
            Summary = "Поиск группы по коду",
            Description = "Находит группу по уникальному коду, сгенерированному при создании, и возвращает её данные с участниками."
        )]
        [SwaggerResponse(200, "Группа успешно найдена", typeof(GroupDto))]
        [SwaggerResponse(404, "Группа с указанным кодом не найдена")]
        public async Task<IActionResult> GetGroupByCode([FromRoute] string code)
        {
            Group group = await _groupService.GetGroupByCodeAsync(code);
            if (group == null)
                return NotFound(new { message = "Группа с указанным кодом не найдена" });
            return Ok(MapToGroupDto(group));
        }

        /// <summary>
        /// Получает все группы, в которых участвует заданный пользователь, включая список участников каждой группы.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>Список групп с участниками.</returns>
        [HttpGet("user/{userId:guid}")]
        [SwaggerOperation(
            Summary = "Получение групп пользователя",
            Description = "Возвращает все группы, где пользователь является владельцем или участником, включая список участников каждой группы."
        )]
        [SwaggerResponse(200, "Список групп успешно получен", typeof(IEnumerable<GroupDto>))]
        [SwaggerResponse(404, "Группы для указанного пользователя не найдены")]
        public async Task<IActionResult> GetGroupsByUserId(Guid userId)
        {
            IEnumerable<Group> groups = await _groupService.GetGroupsByUserIdAsync(userId);
            if (groups == null || !groups.Any())
                return NotFound(new { message = "Группы для указанного пользователя не найдены" });

            var response = groups.Select(g => MapToGroupDto(g));
            return Ok(response);
        }

        [HttpGet("member")]
        [SwaggerOperation(
    Summary = "Получение участника группы по userId и groupId",
    Description = "Возвращает идентификатор участника (GroupMemberId) для заданных userId и groupId."
)]
        [SwaggerResponse(200, "Участник найден", typeof(Guid))]
        [SwaggerResponse(404, "Участник не найден")]
        public async Task<IActionResult> GetGroupMember([FromQuery] Guid userId, [FromQuery] Guid groupId)
        {
            // Вызываем метод сервиса, который возвращает Guid идентификатор участника группы.
            GroupMember member = await _groupService.GetGroupMemberAsync(userId, groupId);

            if (member.Id == Guid.Empty)
            {
                return NotFound(new { message = "Участник не найден." });
            }

            return Ok(member);
        }


        #region Mapping Helpers

        private GroupDto MapToGroupDto(Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Type = group.Type,
                Code = group.Code,
                CreatedAt = group.CreatedAt,
                Members = group.Members?.Select(m => new GroupMemberDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    GroupId = m.GroupId,
                    JoinedAt = m.JoinedAt,
                    Role = m.Role,
                    UniqueColor = m.UniqueColor,
                    UserName = m.User != null ? m.User.Name : string.Empty
                }).ToList() ?? new List<GroupMemberDto>()
            };
        }

        #endregion
    }
}

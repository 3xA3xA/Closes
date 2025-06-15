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
        /// </summary>
        /// <param name="dto">Данные для создания группы.</param>
        /// <returns>HTTP-ответ с DTO созданной группы.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создание новой группы",
            Description = "Принимает данные группы, валидирует их и создает новую группу. Возвращает созданный объект с уникальным идентификатором и сгенерированным кодом."
        )]
        [SwaggerResponse(201, "Группа успешно создана", typeof(GroupDto))]
        [SwaggerResponse(400, "Неверные входные данные")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto dto)
        {
            try
            {
                Group createdGroup = await _groupService.CreateGroupAsync(dto);

                // Формируем DTO для ответа
                var response = new GroupDto
                {
                    Id = createdGroup.Id,
                    Name = createdGroup.Name,
                    Type = createdGroup.Type,
                    Code = createdGroup.Code,
                    CreatedAt = createdGroup.CreatedAt
                };

                return CreatedAtAction(nameof(GetGroupById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Присоединяет текущего аутентифицированного пользователя к группе по её уникальному коду.
        /// Создает запись GroupMember.
        /// </summary>
        /// <param name="groupCode">Уникальный код группы.</param>
        /// <returns>Данные созданного участника группы.</returns>
        [HttpPost("join/{groupCode}")]
        [SwaggerOperation(
            Summary = "Присоединение к группе по коду",
            Description = "Присоединяет текущего аутентифицированного пользователя к группе, найденной по её уникальному коду, и создает запись GroupMember."
        )]
        [SwaggerResponse(200, "Пользователь успешно присоединился к группе", typeof(GroupMemberDto))]
        [SwaggerResponse(400, "Ошибка при присоединении к группе")]
        [SwaggerResponse(401, "Пользователь не аутентифицирован")]
        public async Task<IActionResult> JoinGroup([FromRoute] string groupCode, Guid userId)
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
        /// Получает группу по ее уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы.</param>
        /// <returns>Объект группы.</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Получение группы по ID",
            Description = "Возвращает данные группы по её уникальному идентификатору."
        )]
        [SwaggerResponse(200, "Группа успешно найдена", typeof(GroupDto))]
        [SwaggerResponse(404, "Группа не найдена")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            Group group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound(new { message = "Группа не найдена" });
            }

            var response = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Type = group.Type,
                Code = group.Code,
                CreatedAt = group.CreatedAt
            };
            return Ok(response);
        }

        /// <summary>
        /// Поиск группы по уникальному коду.
        /// </summary>
        /// <param name="code">Уникальный код группы (например, 5-символьная строка).</param>
        /// <returns>Объект группы, если группа найдена, иначе 404.</returns>
        [HttpGet("code/{code}")]
        [SwaggerOperation(
            Summary = "Поиск группы по коду",
            Description = "Находит группу по уникальному коду, который был сгенерирован при создании группы."
        )]
        [SwaggerResponse(200, "Группа успешно найдена", typeof(GroupDto))]
        [SwaggerResponse(404, "Группа с указанным кодом не найдена")]
        public async Task<IActionResult> GetGroupByCode([FromRoute] string code)
        {
            Group group = await _groupService.GetGroupByCodeAsync(code);
            if (group == null)
            {
                return NotFound(new { message = "Группа с указанным кодом не найдена" });
            }
            var response = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Type = group.Type,
                Code = group.Code,
                CreatedAt = group.CreatedAt
            };

            return Ok(response);
        }

        /// <summary>
        /// Получает все группы, в которых участвует заданный пользователь.
        /// Пользователь может быть владельцем или участником группы.
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>Список найденных групп.</returns>
        [HttpGet("user/{userId:guid}")]
        [SwaggerOperation(
            Summary = "Получение групп пользователя",
            Description = "Возвращает все группы, в которых пользователь является владельцем или участником."
        )]
        [SwaggerResponse(200, "Список групп успешно получен", typeof(IEnumerable<GroupDto>))]
        [SwaggerResponse(404, "Группы для указанного пользователя не найдены")]
        public async Task<IActionResult> GetGroupsByUserId([FromRoute] Guid userId)
        {
            IEnumerable<Group> groups = await _groupService.GetGroupsByUserIdAsync(userId);
            if (groups == null || !groups.Any())
            {
                return NotFound(new { message = "Группы для указанного пользователя не найдены" });
            }

            var response = groups.Select(group => new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Type = group.Type,
                Code = group.Code,
                CreatedAt = group.CreatedAt
            });

            return Ok(response);
        }
    }
}

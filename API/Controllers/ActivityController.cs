using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        /// <summary>
        /// Создание новой активности.
        /// </summary>
        /// <param name="dto">Данные для создания активности (имя, описание, тип, статус, время начала, время окончания, id группы).</param>
        /// <returns>Созданная активность.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создание новой активности",
            Description = "Создает новую активность с указанными данными."
        )]
        [SwaggerResponse(201, "Активность успешно создана", typeof(Activity))]
        [SwaggerResponse(400, "Ошибка при создании активности")]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto dto)
        {
            try
            {
                var activity = await _activityService.CreateActivityAsync(dto);
                return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получение активности по ее уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор активности.</param>
        /// <returns>Активность.</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Не трогать! Говнокод.",
            Description = "Возвращает данные активности по ее уникальному идентификатору."
        )]
        [SwaggerResponse(200, "Активность найдена", typeof(Activity))]
        [SwaggerResponse(404, "Активность не найдена")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {
            // Здесь можно реализовать получение активности по Id.
            // Для примера возвращается NotFound.
            return NotFound();
        }

        /// <summary>
        /// Получение всех активностей по groupId.
        /// </summary>
        /// <param name="groupId">Уникальный идентификатор группы.</param>
        /// <returns>Список активностей для указанной группы.</returns>
        [HttpGet("{groupId:guid}")]
        [SwaggerOperation(
            Summary = "Получение активностей по groupId",
            Description = "Возвращает список активностей для заданной группы по её уникальному идентификатору."
        )]
        [SwaggerResponse(200, "Активности успешно получены", typeof(IEnumerable<Activity>))]
        public async Task<IActionResult> GetActivitiesByGroupId(Guid groupId)
        {
            var activities = await _activityService.GetActivitiesByGroupIdAsync(groupId);
            return Ok(activities);
        }
    }
}

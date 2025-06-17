using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        /// <summary>
        /// Создание нового квиза с вопросами.
        /// </summary>
        /// <param name="dto">Данные для создания квиза (название, описание, категория, идентификатор создателя и список вопросов).</param>
        /// <returns>Созданный квиз со всеми вопросами.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создание нового квиза",
            Description = "Создает новый квиз с указанными данными, включая описание и вопросы. Поле UserId используется для определения создателя."
        )]
        [SwaggerResponse(201, "Квиз успешно создан", typeof(Quiz))]
        [SwaggerResponse(400, "Ошибка при создании квиза")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizDto dto)
        {
            try
            {
                var quiz = await _quizService.CreateQuizAsync(dto);
                // Если у вас реализован эндпоинт получения квиза по Id, можно использовать CreatedAtAction.
                return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получение квиза по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор квиза.</param>
        /// <returns>Квиз.</returns>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Получение квиза по Id",
            Description = "Возвращает данные квиза по его уникальному идентификатору."
        )]
        [SwaggerResponse(200, "Квиз найден", typeof(Quiz))]
        [SwaggerResponse(404, "Квиз не найден")]
        public async Task<IActionResult> GetQuizById(Guid id)
        {
            // В данном примере метод получения квиза не реализован,
            // поэтому возвращаем NotFound. Реализуйте его при необходимости.
            return NotFound();
        }

        /// <summary>
        /// Получение вопроса квиза по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор вопроса.</param>
        /// <returns>Объект вопроса квиза.</returns>
        [HttpGet("item/{id:guid}")]
        [SwaggerOperation(
            Summary = "Получение вопроса квиза по Id",
            Description = "Возвращает данные вопроса квиза по его уникальному идентификатору."
        )]
        [SwaggerResponse(200, "Вопрос найден", typeof(QuizItem))]
        [SwaggerResponse(404, "Вопрос не найден")]
        public async Task<IActionResult> GetQuizItemById(Guid id)
        {
            var quizItem = await _quizService.GetQuizItemByIdAsync(id);
            if (quizItem == null)
                return NotFound(new { message = "Вопрос не найден" });
            return Ok(quizItem);
        }

        /// <summary>
        /// Получение всех вопросов для указанного квиза.
        /// </summary>
        /// <param name="quizId">Уникальный идентификатор квиза.</param>
        /// <returns>Список вопросов квиза.</returns>
        [HttpGet("item/quiz/{quizId:guid}")]
        [SwaggerOperation(
            Summary = "Получение всех вопросов квиза по Id квиза",
            Description = "Возвращает список всех вопросов для указанного квиза."
        )]
        [SwaggerResponse(200, "Вопросы квиза успешно получены", typeof(IEnumerable<QuizItem>))]
        [SwaggerResponse(404, "Вопросы квиза не найдены")]
        public async Task<IActionResult> GetQuizItemsByQuizId(Guid quizId)
        {
            var quizItems = await _quizService.GetQuizItemsByQuizIdAsync(quizId);
            if (quizItems == null || !quizItems.Any())
                return NotFound(new { message = "Вопросы квиза не найдены" });
            return Ok(quizItems);
        }

        /// <summary>
        /// Удаление вопроса квиза по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор вопроса.</param>
        /// <returns>Статус успешного удаления.</returns>
        [HttpDelete("item/{quizItemId:guid}")]
        [SwaggerOperation(
            Summary = "Удаление вопроса квиза по Id",
            Description = "Удаляет вопрос квиза по его уникальному идентификатору."
        )]
        [SwaggerResponse(204, "Вопрос квиза успешно удален")]
        [SwaggerResponse(404, "Вопрос квиза не найден")]
        public async Task<IActionResult> DeleteQuizItem(Guid quizItemId)
        {
            var deleted = await _quizService.DeleteQuizItemByIdAsync(quizItemId);
            if (!deleted)
                return NotFound(new { message = "Вопрос квиза не найден" });
            return NoContent();
        }
    }
}

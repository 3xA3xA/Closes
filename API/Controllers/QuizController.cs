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
            Summary = "НЕ ИСПОЛЬЗУЙ!!!!",
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
            Summary = "НЕ ИСПОЛЬЗУЙ!!!!",
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
        /// Получение квиза с вопросами по его уникальному идентификатору.
        /// </summary>
        /// <param name="quizId">Уникальный идентификатор квиза.</param>
        /// <returns>Квиз со списком вопросов.</returns>
        [HttpGet("with-items/{quizId:guid}")]
        [SwaggerOperation(
            Summary = "Получение квиза с вопросами по Id квиза",
            Description = "Возвращает квиз и все связанные вопросы для указанного квиза."
        )]
        [SwaggerResponse(200, "Квиз с вопросами успешно получен", typeof(Quiz))]
        [SwaggerResponse(404, "Квиз не найден")]
        public async Task<IActionResult> GetQuizWithItemsByQuizId(Guid quizId)
        {
            var quiz = await _quizService.GetQuizWithItemsByQuizIdAsync(quizId);
            if (quiz == null)
            {
                return NotFound(new { message = "Квиз не найден" });
            }
            return Ok(quiz);
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

        /// <summary>
        /// Удаление квиза по его уникальному идентификатору, включая все связанные вопросы.
        /// </summary>
        /// <param name="quizId">Уникальный идентификатор квиза.</param>
        /// <returns>Статус успешного удаления.</returns>
        [HttpDelete("{quizId:guid}")]
        [SwaggerOperation(
            Summary = "Удаление квиза по Id",
            Description = "Удаляет квиз и все связанные вопросы по его уникальному идентификатору."
        )]
        [SwaggerResponse(204, "Квиз успешно удален")]
        [SwaggerResponse(404, "Квиз не найден")]
        public async Task<IActionResult> DeleteQuiz(Guid quizId)
        {
            var deleted = await _quizService.DeleteQuizByIdAsync(quizId);
            if (!deleted)
                return NotFound(new { message = "Квиз не найден" });
            return NoContent();
        }

        /// <summary>
        /// Сохранение ответов участника группы на вопросы квиза по id квиза.
        /// Передаются GroupMemberId, QuizId и массив ответов (строк) – порядок ответов должен соответствовать порядку вопросов квиза.
        /// </summary>
        /// <param name="dto">DTO, содержащий GroupMemberId, QuizId и массив ответов.</param>
        /// <returns>Коллекция сохраненных объектов QuizAnswer.</returns>
        [HttpPost("submit")]
        [SwaggerOperation(
            Summary = "Сохранение ответов участника группы по квизу",
            Description = "Принимает GroupMemberId, QuizId и массив ответов. После загрузки квиза с вопросами ответы сопоставляются с вопросами по порядку."
        )]
        [SwaggerResponse(201, "Ответы успешно сохранены", typeof(IEnumerable<QuizAnswer>))]
        [SwaggerResponse(400, "Ошибка при сохранении ответов")]
        public async Task<IActionResult> SubmitQuizAnswersByQuiz([FromBody] SubmitQuizAnswersDto dto)
        {
            try
            {
                var savedAnswers = await _quizService.SubmitQuizAnswersByQuizAsync(dto);
                return Created(string.Empty, savedAnswers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("with-answers/{quizId:guid}/{groupMemberId:guid}")]
        [SwaggerOperation(
            Summary = "Получение квиза с вопросами и ответами участника",
            Description = "Возвращает квиз с его вопросами, в каждом из которых остаются только ответы указанного участника группы."
        )]
        [SwaggerResponse(200, "Квиз с вопросами и ответами успешно получен", typeof(QuizWithAnswersDto))]
        [SwaggerResponse(404, "Квиз не найден")]
        public async Task<IActionResult> GetQuizWithItemsAndAnswersForMember(Guid quizId, Guid groupMemberId)
        {
            var quizDto = await _quizService.GetQuizWithItemsAndAnswersForMemberByQuizIdAsync(quizId, groupMemberId);
            if (quizDto == null)
            {
                return NotFound(new { message = "Квиз не найден" });
            }
            return Ok(quizDto);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Получение всех квизов",
            Description = "Возвращает список всех квизов из базы данных."
        )]
        [SwaggerResponse(200, "Список квизов успешно получен", typeof(IEnumerable<Quiz>))]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpGet("for-member/{groupMemberId:guid}")]
        [SwaggerOperation(
            Summary = "Получение квизов, пройденных участником",
            Description = "Возвращает список квизов, для которых указанный участник группы оставил ответы."
        )]
        [SwaggerResponse(200, "Квиз успешно получены", typeof(IEnumerable<Quiz>))]
        public async Task<IActionResult> GetQuizzesForMember(Guid groupMemberId)
        {
            var quizzes = await _quizService.GetQuizzesForMemberAsync(groupMemberId);
            return Ok(quizzes);
        }

    }
}

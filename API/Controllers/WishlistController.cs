using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для работы с вишлистом.
    /// В этом контроллере вишлист общий для группы, и разные участники могут добавлять свои предметы.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        /// <summary>
        /// Создаёт новый вишлист для группы.
        /// Ожидается, что в CreateWishlistDto передаётся GroupId.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Создание вишлиста для группы",
            Description = "Создаёт новый вишлист для конкретной группы и возвращает созданный объект."
        )]
        [SwaggerResponse(201, "Вишлист создан", typeof(Wishlist))]
        [SwaggerResponse(400, "Ошибка при создании вишлиста")]
        public async Task<IActionResult> CreateWishlist([FromBody] CreateWishlistDto dto)
        {
            try
            {
                var wishlist = await _wishlistService.CreateWishlistAsync(dto);
                return CreatedAtAction(nameof(GetWishlistById), new { id = wishlist.Id }, wishlist);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получает вишлист по его GroupId, включая элементы.
        /// </summary>
        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Получение вишлиста по GroupId",
            Description = "Возвращает данные вишлиста с его элементами."
        )]
        [SwaggerResponse(200, "Вишлист получен", typeof(Wishlist))]
        [SwaggerResponse(404, "Вишлист не найден")]
        public async Task<IActionResult> GetWishlistByGroupId(Guid id)
        {
            var wishlist = await _wishlistService.GetWishlistsByGroupIdAsync(id);
            if (wishlist == null)
                return NotFound(new { message = "Вишлист не найден." });
            return Ok(wishlist);
        }

        /// <summary>
        /// Добавляет новый элемент в вишлист.
        /// Используйте CreateWishlistItemDto, в котором передаётся GroupMemberId той записи участника, от имени которого создаётся элемент.
        /// </summary>
        [HttpPost("item")]
        [SwaggerOperation(
            Summary = "Добавление элемента в вишлист",
            Description = "Добавляет новый элемент в выбранный вишлист."
        )]
        [SwaggerResponse(200, "Элемент добавлен", typeof(WishlistItem))]
        [SwaggerResponse(400, "Ошибка при добавлении элемента")]
        public async Task<IActionResult> AddWishlistItem([FromBody] CreateWishlistItemDto dto)
        {
            try
            {
                var item = await _wishlistService.AddWishlistItemAsync(dto);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получает все элементы вишлиста.
        /// Если alternate = true, элементы возвращаются по принципу round-robin, чередуя предметы между участниками.
        /// </summary>
        [HttpGet("{wishlistId:guid}/items")]
        [SwaggerOperation(
            Summary = "Получение элементов вишлиста",
            Description = "Возвращает все элементы вишлиста, опционально в чередующем порядке (round-robin) по разным участникам."
        )]
        [SwaggerResponse(200, "Элементы получены", typeof(IEnumerable<WishlistItem>))]
        public async Task<IActionResult> GetWishlistItems(Guid wishlistId, [FromQuery] bool alternate = false)
        {
            var items = await _wishlistService.GetWishlistItemsAsync(wishlistId, alternate);
            return Ok(items);
        }
        private async Task<IActionResult> GetWishlistById(Guid id)
        {
            var wishlist = await _wishlistService.GetWishlistByIdAsync(id);
            if (wishlist == null)
                return NotFound(new { message = "Вишлист не найден." });
            return Ok(wishlist);
        }
    }
}

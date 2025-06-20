﻿using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
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
                    // Используем имя маршрута "GetWishlistById", чтобы сгенерировать значение URL
                    return CreatedAtRoute("GetWishlistById", new { id = wishlist.Id }, wishlist);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            /// <summary>
            /// Получает вишлист по его Id, включая элементы.
            /// </summary>
            [HttpGet("{id:guid}", Name = "GetWishlistById")]
            [SwaggerOperation(
                Summary = "Получение вишлиста по Id",
                Description = "Возвращает данные вишлиста с его элементами."
            )]
            [SwaggerResponse(200, "Вишлист получен", typeof(Wishlist))]
            [SwaggerResponse(404, "Вишлист не найден")]
            public async Task<IActionResult> GetWishlistById(Guid id)
            {
                var wishlist = await _wishlistService.GetWishlistByIdAsync(id);
                if (wishlist == null)
                    return NotFound(new { message = "Вишлист не найден." });
                return Ok(wishlist);
            }

            /// <summary>
            /// Получает вишлист для указанной группы.
            /// </summary>
            [HttpGet("group/{groupId:guid}")]
            [SwaggerOperation(
                Summary = "Получение вишлиста по GroupId",
                Description = "Возвращает вишлист для указанной группы."
            )]
            [SwaggerResponse(200, "Вишлист получен", typeof(Wishlist))]
            [SwaggerResponse(404, "Вишлист не найден")]
            public async Task<IActionResult> GetWishlistByGroupId(Guid groupId)
            {
                var wishlist = await _wishlistService.GetWishlistByGroupIdAsync(groupId);
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
            /// Удаляет элемент вишлиста по его уникальному идентификатору.
            /// </summary>
            [HttpDelete("item/{wishlistItemId:guid}")]
            [SwaggerOperation(
                Summary = "Удаление элемента вишлиста",
                Description = "Удаляет элемент вишлиста по его уникальному идентификатору."
            )]
            [SwaggerResponse(200, "Элемент успешно удален")]
            [SwaggerResponse(404, "Элемент не найден")]
            public async Task<IActionResult> DeleteWishlistItem(Guid wishlistItemId)
            {
                try
                {
                    await _wishlistService.DeleteWishlistItemAsync(wishlistItemId);
                    return Ok(new { message = "Элемент вишлиста успешно удален." });
                }
                catch (Exception ex)
                {
                    return NotFound(new { message = ex.Message });
                }
            }

            /// <summary>
            /// Получает все элементы вишлиста.
            /// </summary>
            [HttpGet("{groupId:guid}/items")]
            [SwaggerOperation(
                Summary = "Получение элементов вишлиста",
                Description = "Возвращает все элементы вишлиста."
            )]
            [SwaggerResponse(200, "Элементы получены", typeof(IEnumerable<WishlistItem>))]
            public async Task<IActionResult> GetWishlistItems(Guid groupId)
            {
                var items = await _wishlistService.GetWishlistItemsAsync(groupId);
                return Ok(items);
            }
        }
    }
}
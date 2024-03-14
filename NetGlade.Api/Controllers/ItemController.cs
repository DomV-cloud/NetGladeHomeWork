using Microsoft.AspNetCore.Mvc;
using NetGlade.Api.Filters;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Application.PaginationFilter;
using NetGlade.Contracts.Items;
using NetGlade.Domain.Entities;

namespace NetGlade.Api.Controllers
{
    [ApiController]
    [Route("item")]
    [ErrorHandlingFilter]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        [HttpPost("create", Name = "createItem")]
        public async Task<IActionResult> CreateItem(ItemRequest request)
        {
            try
            {
                if (request is null)
                {
                    return NotFound("Failed to create item. Section not found.");
                }

                var response = await _itemRepository.AddItem(request.item);

                if (response is null)
                {
                    return NotFound("Failed to create item. Section not found.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating item");
                return BadRequest("Failed to create item: " + ex.Message);
            }
        }

        [HttpGet("get/item/{itemName}", Name = "get/item")]
        public async Task<IActionResult> GetAllItemsByName([FromQuery] BasePaginationFilter filter, [FromRoute] string itemName)
        {
            try
            {
                if (filter is null)
                {
                    return NotFound("Failed to load items.");
                }

                if (String.IsNullOrEmpty(itemName))
                {
                    return NotFound("Failed to load items. Name of the item is empty or null.");
                }

                var response = await _itemRepository.GetItemsByName(filter, itemName);

                if (response is null)
                {
                    return NotFound("Failed to load items. Items not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting items");
                return BadRequest("Failed to while getting items: " + ex.Message);
            }
        }

        [HttpGet("get/item/section/{sectionId}", Name = "get/item/section")]
        public async Task<IActionResult> GetAllItemsBySection([FromQuery] BasePaginationFilter filter, [FromRoute] Guid sectionId)
        {
            try
            {
                if (filter is null)
                {
                    return NotFound("Failed to load items.");
                }

                if (String.IsNullOrEmpty(sectionId.ToString()))
                {
                    return NotFound("Failed to load items. Id of the Section is empty.");
                }

                var response = await _itemRepository.GetItemsBySection(filter, sectionId);

                if (response is null)
                {
                    return NotFound("Failed to load items. Items not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting items");
                return BadRequest("Failed to while getting items: " + ex.Message);
            }
        }
    }
}

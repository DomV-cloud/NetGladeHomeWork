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

        // TODO: Controllers should not retrieve sensitive data like token or Id

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
                    _logger.LogError("Failed to save item. Item was not deliverd correctly");
                    return NotFound("Failed to save item. Item was not deliverd correctly");
                }

                _logger.LogInformation("Saving item...");
                var response = await _itemRepository.AddItem(request.Item);

                if (response is null)
                {
                    _logger.LogError("Failed to save Item {0}", request.Item);
                    return NotFound("Failed to save Item.");
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
                    _logger.LogWarning("Failed to load items by filter.");
                    return NotFound("Failed to load items by filter.");
                }

                if (String.IsNullOrEmpty(itemName))
                {
                    _logger.LogWarning("Failed to load items. Name of the item is empty or null.");
                    return NotFound("Failed to load items. Name of the item is empty or null.");
                }

                _logger.LogInformation("Getting Item with name {0}", itemName);
                var response = await _itemRepository.GetItemsByName(filter, itemName);

                if (response is null)
                {
                    _logger.LogWarning("Failed to return items. Items not found");
                    return NotFound("Failed to return items. Items not found");
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
                    _logger.LogWarning("Failed to load items.");
                    return NotFound("Failed to load items.");
                }

                if (sectionId == Guid.Empty)
                {
                    _logger.LogWarning("Failed to load items. Id of the Section is empty.");
                    return NotFound("Failed to load items. Id of the Section is empty.");
                }

                _logger.LogInformation("Getting item by section...");
                var response = await _itemRepository.GetItemsBySection(filter, sectionId);

                if (response is null)
                {
                    _logger.LogWarning("Failed to load items. Items not found");
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

        [HttpGet("get/item/category/{categoryId}", Name = "get/item/category")]
        public async Task<IActionResult> GetAllItemsByCategory([FromQuery] BasePaginationFilter filter, [FromRoute] Guid categoryId)
        {
            try
            {
                if (filter is null)
                {
                    _logger.LogWarning("Failed to load items by filter.");
                    return NotFound("Failed to load items by filter.");
                }

                if (String.IsNullOrEmpty(categoryId.ToString()))
                {
                    _logger.LogWarning("Failed to load items. Id of the Category is empty.");
                    return NotFound("Failed to load items. Id of the Category is empty.");
                }

                _logger.LogInformation("Getting item by category...");
                var response = await _itemRepository.GetItemsByCategory(filter, categoryId);

                if (response is null)
                {
                    _logger.LogWarning("Failed to load items. Category not found");
                    return NotFound("Failed to load items. Category not found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting items");
                return BadRequest("Failed to while getting items: " + ex.Message);
            }
        }

        [HttpPut("update/item/{itemId}", Name = "update/item")]
        public async Task<IActionResult> UpdateItem([FromBody] ItemRequest request, [FromRoute] Guid itemId)
        {
            try
            {
                if (request == null)
                {
                    _logger.LogWarning("Failed to update items. Request body is null.");
                    return BadRequest("Failed to update items. Request body is null.");
                }

                if (itemId == Guid.Empty)
                {
                    _logger.LogWarning("Failed to update items. Id of the Item is empty.");
                    return BadRequest("Failed to update items. Id of the Item is empty.");
                }

                _logger.LogInformation("Getting item by Id...");
                var existingItem = await _itemRepository.GetItemById(itemId);

                if (existingItem == null)
                {
                    _logger.LogWarning("Failed to update items. Item not found.");
                    return NotFound("Failed to update items. Item not found.");
                }

                _logger.LogWarning("Updating item....");
                var response = await _itemRepository.UpdateItem(request.Item, itemId);

                if (!response)
                {
                    _logger.LogWarning("Failed to update items. Update operation failed.");
                    return NotFound("Failed to update items. Update operation failed.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating item");
                return BadRequest("Failed to update item: " + ex.Message);
            }
        }

        [HttpDelete("delete/item/{eanCodeId}", Name = "delete/item")]
        public async Task<IActionResult> DeleteItemByEanCode([FromRoute] Guid eanCodeId)
        {
            try
            {
                if (eanCodeId == Guid.Empty)
                {
                    _logger.LogWarning("Code is empty");
                    return BadRequest("Code is empty");
                }

                _logger.LogInformation("Deleting item...");
                var response = await _itemRepository.DeleteItemByEanCode(eanCodeId);

                if (!response)
                {
                    _logger.LogWarning("Deleting item failed");
                    return BadRequest("Deleting item failed");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting item");
                return BadRequest("Failed to delete item: " + ex.Message);
            }
        }
    }
}

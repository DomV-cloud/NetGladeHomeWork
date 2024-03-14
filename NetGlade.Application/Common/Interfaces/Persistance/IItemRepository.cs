using NetGlade.Application.Pagination;
using NetGlade.Application.PaginationFilter;
using NetGlade.Domain.Entities;

namespace NetGlade.Application.Common.Interfaces.Persistance
{
    public interface IItemRepository
    {
        Task<BaseResponse<Item?>> AddItem(Item item);

        Task<bool> DeleteItemByEanCode(Guid id);

        Task<bool> UpdateItem(Item newItem, Guid Id);
        
        Task<Item?>? GetItemById(Guid Id);

        Task<PagedResponse<List<Item>>?>? GetItemsByName(BasePaginationFilter filter, string itemName);

        Task<PagedResponse<List<Item>>?>? GetItemsBySection(BasePaginationFilter filter, Guid sectionId);

        Task<PagedResponse<List<Item>>?>? GetItemsByCategory(BasePaginationFilter filter, Guid categoryId);

    }
}

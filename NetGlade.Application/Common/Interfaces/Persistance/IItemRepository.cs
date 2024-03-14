using NetGlade.Application.Pagination;
using NetGlade.Application.PaginationFilter;
using NetGlade.Domain.Entities;

namespace NetGlade.Application.Common.Interfaces.Persistance
{
    public interface IItemRepository
    {
        Task<BaseResponse<Item?>> AddItem(Item item);

        Task<bool> DeleteItemByEanCode(EANCode code);

        Task<bool> UpdateItem(Item newItem, string nameOfItemToUpdate);
        
        Task<Item?>? GetItemByName(string itemName);

        Task<PagedResponse<List<Item>>?>? GetItemsByName(BasePaginationFilter filter, string itemName);

        Task<PagedResponse<List<Item>>?>? GetItemsBySection(BasePaginationFilter filter, Guid sectionId);

    }
}

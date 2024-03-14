using Microsoft.EntityFrameworkCore;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Application.DatabaseContext;
using NetGlade.Application.Pagination;
using NetGlade.Application.PaginationFilter;
using NetGlade.Domain.Entities;

namespace NetGlade.Infrastructure.Persistance
{
    public class ItemRepository : IItemRepository
    {
        private readonly NetGladeContext _context;

        public ItemRepository(NetGladeContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Item?>> AddItem(Item item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await _context.Items.AddAsync(item);

            await _context.SaveChangesAsync();

            return new BaseResponse<Item?>(item);
        }

        public async Task<bool> DeleteItemByEanCode(EANCode code)
        {
            if (code is null)
            {
                return false;
            }

            var deletedItemByCode = await _context.Items.FirstOrDefaultAsync(i => i.ItemEanCode.Id == code.Id);

            if (deletedItemByCode == null)
            {
                return false;
            }

            _context.Items.Remove(deletedItemByCode);

            int isRemoved = await _context.SaveChangesAsync();

            return isRemoved > 0;
        }

        public async Task<PagedResponse<List<Item>>?>? GetItemsByName(BasePaginationFilter filter, string itemName)
        {
            if (String.IsNullOrEmpty(itemName))
            {
                return null;
            }

            var validFilter = new BasePaginationFilter(filter.PageNumber, filter.PageSize);

            var pagedData = await _context.Items
               .Where(i => i.ItemName.Equals(itemName))
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Items.CountAsync();

            return new PagedResponse<List<Item>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }

        public async Task<PagedResponse<List<Item>>?> GetItemsBySection(BasePaginationFilter filter, Guid sectionId)
        {
            if (sectionId == Guid.Empty)
            {
                return null;
            }

            var validFilter = new BasePaginationFilter(filter.PageNumber, filter.PageSize);

            var pagedData = await _context.Items
                .Where(item => item.SectionId == sectionId)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var totalRecords = await _context.Items
                .Where(item => item.SectionId == sectionId)
                .CountAsync();

            return new PagedResponse<List<Item>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }

        public async Task<Item?>? GetItemByName(string itemName)
        {
            if (String.IsNullOrEmpty(itemName))
            {
                return null;
            }

            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemName == itemName);

            return item;
        }

        public async Task<bool> UpdateItem(Item newItem, string nameOfItemToUpdate)
        {
            if (newItem is null)
            {
                return false;
            }

            if (String.IsNullOrEmpty(nameOfItemToUpdate))
            {
                return false;
            }

            var itemToUpdate = await GetItemByName(nameOfItemToUpdate);

            if (itemToUpdate is null)
            {
                return false;
            }

            itemToUpdate.ItemName = newItem.ItemName;

            _context.Items.Update(itemToUpdate);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}

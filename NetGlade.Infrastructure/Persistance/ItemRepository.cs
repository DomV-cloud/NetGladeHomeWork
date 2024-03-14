using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<bool> DeleteItemByEanCode(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return false;
            }

            var deletedItemByCode = await _context.Items.FirstOrDefaultAsync(i => i.ItemEanCode.Id == Id);

            if (deletedItemByCode is null)
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

        public async Task<Item?>? GetItemById(Guid Id)
        {
            if (String.IsNullOrEmpty(Id.ToString()))
            {
                return null;
            }

            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == Id);

            if (item is null)
            {
                return null;
            }

            return item;
        }

        public async Task<bool> UpdateItem(Item newItem, Guid itemId)
        {
            if (newItem == null)
            {
                return false;
            }

            var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);

            if (existingItem == null)
            {
                return false;
            }
            
            existingItem.ItemName = newItem.ItemName;
            existingItem.ItemEanCode = newItem.ItemEanCode;
            existingItem.CategoryId = newItem.CategoryId;
            existingItem.SectionId = newItem.SectionId;

            _context.Items.Update(existingItem);

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<PagedResponse<List<Item>>?>? GetItemsByCategory(BasePaginationFilter filter, Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return null;
            }

            var validFilter = new BasePaginationFilter(filter.PageNumber, filter.PageSize);

            var pagedData = await _context.Items
                .Where(item => item.CategoryId == categoryId)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var totalRecords = await _context.Items
                .Where(item => item.CategoryId == categoryId)
                .CountAsync();

            return new PagedResponse<List<Item>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }
    }
}

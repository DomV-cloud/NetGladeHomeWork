using Microsoft.EntityFrameworkCore;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Application.DatabaseContext;
using NetGlade.Application.Pagination;
using NetGlade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace NetGlade.Infrastructure.Persistance
{
    public class SectionRepository : ISectionRepository
    {
        private readonly NetGladeContext _context;

        public SectionRepository(NetGladeContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<Section?>> AddSection(Section section)
        {
            using (var context = _context)
            {
                if (section is null)
                {
                    throw new ArgumentNullException(nameof(section));
                }
                await _context.Sections.AddAsync(section);
                
                await _context.SaveChangesAsync();

                return new BaseResponse<Section?>(section);
            }
        }

        public async Task<bool> DeleteSection(string sectionName)
        {
            var sectionToDelete = await GetSectionByName(sectionName);

            if (sectionToDelete is null)
            {
                return false;
            }

            _context.Sections.Remove(sectionToDelete);

            int isRemoved = await _context.SaveChangesAsync();

            return isRemoved > 0;
        }

        public async Task<Section?> GetSectionByName(string sectionName)
        {
            if (String.IsNullOrEmpty(sectionName))
            {
                return null;
            }

            var section = await _context.Sections.FirstOrDefaultAsync(s => s.SectionName == sectionName);

            return section;
        }

        public async Task<bool> UpdateSection(Section newSection, string nameOfSectionToUpdate)
        {
            var sectionToUpdate = await GetSectionByName(newSection.SectionName);

            sectionToUpdate = newSection;

            using (var context = _context)
            {
                _context.Sections.Update(sectionToUpdate);

                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
        }
    }
}

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

        public async Task<bool> DeleteSection(Section section)
        {
            using (var context = _context)
            {
                _context.Sections.Remove(section);
                int result = await _context.SaveChangesAsync();

                return result > 0;
            }
        }

        public async Task<Section?> GetSectionByName(string sectionName)
        {
            using (var context = _context)
            {
                if (String.IsNullOrEmpty(sectionName))
                {
                    throw new Exception("Name of the section is empty");
                }

                var section = await _context.Sections.FirstOrDefaultAsync(s => s.SectionName == sectionName);

                await _context.SaveChangesAsync();

                return section;
            }
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

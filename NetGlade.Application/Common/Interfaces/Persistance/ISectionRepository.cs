using NetGlade.Application.Pagination;
using NetGlade.Domain.Entities;

namespace NetGlade.Application.Common.Interfaces.Persistance
{
    public interface ISectionRepository
    {
        Task<BaseResponse<Section?>> AddSection(Section section);

        Task<Section?> GetSectionByName(string sectionName);

        Task<bool> UpdateSection(Section newSection, string nameOfSectionToUpdate);

        Task<bool> DeleteSection(string sectionName);

    }
}

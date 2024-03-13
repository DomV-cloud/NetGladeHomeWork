using Microsoft.AspNetCore.Mvc;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Contracts.Sections;

namespace NetGlade.Api.Controllers
{
    [ApiController]
    [Route("section")]
    public class SectionController : Controller
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        [HttpPost("create", Name = "create")]
        public async Task<IActionResult> CreateSection(SectionRequest request)
        {
            try
            {
                var response = await _sectionRepository.AddSection(request.Section);

                return Ok(response);
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

        [HttpGet("get/{sectionName}", Name = "get")]
        public async Task<IActionResult> GetSection([FromRoute] string sectionName)
        {
            try
            {
                var response = await _sectionRepository.GetSectionByName(sectionName);

                return Ok(response);
            }
            catch (Exception)
            {
                //log
                throw;
            }
        }

    }
}

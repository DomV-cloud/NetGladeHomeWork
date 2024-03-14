using Microsoft.AspNetCore.Mvc;
using NetGlade.Api.Filters;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Contracts.Sections;

namespace NetGlade.Api.Controllers
{
    [ApiController]
    [Route("section")]
    [ErrorHandlingFilter]
    public class SectionController : Controller
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<SectionController> _logger;

        public SectionController(ISectionRepository sectionRepository, ILogger<SectionController> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        [HttpPost("create", Name = "create")]
        public async Task<IActionResult> CreateSection(SectionRequest request)
        {
            try
            {
                if (request.Section is null)
                {
                    return NotFound("Failed to create section.");
                }

                var response = await _sectionRepository.AddSection(request.Section);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed to create section.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating section");
                return BadRequest("Failed to create section: " + ex.Message);
            }
        }


        [HttpGet("get/{sectionName}", Name = "get")]
        public async Task<IActionResult> GetSection([FromRoute] string sectionName)
        {
            try
            {
                var response = await _sectionRepository.GetSectionByName(sectionName);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed to get section. Section not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting section");
                return BadRequest("Failed to get section: " + ex.Message);
            }
        }

        [HttpDelete("delete/{sectionName}", Name = "delete")]
        public async Task<IActionResult> DeleteSection([FromRoute] string sectionName)
        {
            try
            {
                var response = await _sectionRepository.DeleteSection(sectionName);

                if (response)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed to delete. Section not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting section");
                return BadRequest("Failed to delete: " + ex.Message);
            }
        }

        [HttpPut("update/{sectionName}", Name = "update")]
        public async Task<IActionResult> UpdateSection([FromQuery] SectionRequest request, [FromRoute] string sectionName)
        {
            try
            {
                var response = await _sectionRepository.UpdateSection(request.Section, sectionName);

                if (response)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed to update. Section not found.");
                }
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error occurred while updating section");

                return BadRequest("Failed to update section: " + ex.Message);
            }
        }
    }
}

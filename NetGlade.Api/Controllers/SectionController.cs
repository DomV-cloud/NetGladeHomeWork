using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Contracts.Sections;
using NetGlade.Domain.Entities;

namespace NetGlade.Api.Controllers
{
    [ApiController]
    [Route("section")]
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
                var response = await _sectionRepository.AddSection(request.Section);

                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound("Failed to create section. Section not found.");
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
                    return NotFound("Failed to get section. Section not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting section");
                return BadRequest("Failed to get section: " + ex.Message);
            }
        }
    }
}

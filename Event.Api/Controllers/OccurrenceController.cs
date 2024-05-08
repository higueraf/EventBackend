using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Event.Application.Dtos.Occurrence.Request;
using Event.Application.Interfaces;
using Event.Infraestructure.Commons.Bases.Request;

namespace EVENT.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OccurrenceController : ControllerBase
    {
        private readonly IOccurrenceApplication _occurrenceApplication;

        public OccurrenceController(IOccurrenceApplication occurrenceApplication = null)
        {
            _occurrenceApplication = occurrenceApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListOccurrences([FromBody] BaseFilterRequest filters)
        {
            var response = await _occurrenceApplication.ListOccurrences(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectOccurrences()
        {
            var response = await _occurrenceApplication.ListSelectOccurrences();
            return Ok(response);
        }
        [HttpGet("{occurrenceId:int}")]
        public async Task<IActionResult> OccurrenceById(int occurrenceId)
        {
            var response = await _occurrenceApplication.OccurrenceById(occurrenceId);
            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOccurrence([FromBody] OccurrenceRequestDto requestDto)
        {
            var response = await _occurrenceApplication.CreateOccurrence(requestDto);
            return Ok(response);
        }
        [HttpPut("Update/{occurrenceId:int}")]
        public async Task<IActionResult> UpdateOccurrence(int occurrenceId, [FromBody] OccurrenceRequestDto requestDto)
        {
            var response = await _occurrenceApplication.UpdateOccurrence(occurrenceId, requestDto);
            return Ok(response);
        }

        [HttpDelete("Delete/{occurrenceId:int}")]
        public async Task<IActionResult> DeleteOccurrence(int occurrenceId)
        {
            var response = await _occurrenceApplication.DeleteOccurrence(occurrenceId);
            return Ok(response);
        }

    }

}

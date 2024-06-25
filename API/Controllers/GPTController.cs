using Domain.Enumerations;
using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GPTController(IGPTService gptService) : ControllerBase
    {
        [HttpGet("batch/get")]
        public async Task<IActionResult> Get(string fileid)
            => Ok(await gptService.GetBatchAsync(fileid));

        [HttpGet("batch/upload/{type}")]
        public async Task<IActionResult> Upload(enMotivationType type)
            => Ok(await gptService.UploadBatchAsync(type));

        [HttpGet("batch/status")]
        public async Task<IActionResult> Status(string batchId)
            => Ok(await gptService.CheckBatchStatusAsync(batchId));
    }
}

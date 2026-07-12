using Microsoft.AspNetCore.Mvc;
using ResumeAnalyzerAPI.Models;
using ResumeAnalyzerAPI.Services;

namespace ResumeAnalyzerAPI.Controllers
{
    [Route("/api/[controller]")]
    public class AnalysisController : Controller
    {
        private readonly ILogger<AnalysisController> _logger;
        private readonly IAnalysisService _analysisService;

        public AnalysisController(ILogger<AnalysisController> logger, IAnalysisService analysisService)
        {
            _logger = logger;
            _analysisService = analysisService;
        }

        [HttpGet("GetHistory")]
        public async Task<IActionResult> Get()
        {
            var result = await _analysisService.GetHistoryAsync();
            return Ok(result); 
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Post([FromBody] AnalysisRequest request)
        {
            if(request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if(string.IsNullOrWhiteSpace(request.JobDescription))
            {
                return BadRequest("Job description cannot be empty.");
            }
            if(string.IsNullOrEmpty(request.Skills))
            {
                return BadRequest("Skills list cannot be empty.");
            }
            // Process the analysis request
            var result = await _analysisService.AnalyzeAsync(request.JobDescription, request.Skills);
            return Ok(result);
        }

        [HttpGet("GetAnalysisById/{id}")]
        public async Task<IActionResult> GetAnalysisById(int id)
        {
            var result = await _analysisService.GetAnalysisByIdAsync(id);
            if (result == null)
            {
                return NotFound($"No analysis found with ID {id}.");
            }
            return Ok(result);
        }
    }
}
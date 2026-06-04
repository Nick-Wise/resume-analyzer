using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResumeAnalyzerAPI.Models;
using ResumeAnalyzerAPI.Services;

namespace ResumeAnalyzerAPI.Controllers
{
    [Route("[controller]")]
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

        [HttpPost]
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
            if(request.Skills == null || request.Skills.Count == 0)
            {
                return BadRequest("Skills list cannot be empty.");
            }
            // Process the analysis request
            var result = await _analysisService.AnalyzeAsync(request.JobDescription, request.Skills);
            return Ok(result);
        }
    }
}
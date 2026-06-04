using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResumeAnalyzerAPI.Models;

namespace ResumeAnalyzerAPI.Services
{
    public interface IAnalysisService
    {
        public Task<AnalysisResponse> AnalyzeAsync(string jobDescription, List<string> skills);
        public Task<List<AnalysisHistoryDto>> GetHistoryAsync();
    }
}
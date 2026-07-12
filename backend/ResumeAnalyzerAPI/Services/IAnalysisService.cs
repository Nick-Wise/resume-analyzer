using ResumeAnalyzerAPI.Models;

namespace ResumeAnalyzerAPI.Services
{
    public interface IAnalysisService
    {
        public Task<AnalysisResponse> AnalyzeAsync(string jobDescription, string skills);
        public Task<List<AnalysisHistoryDto>> GetHistoryAsync();
        public Task<AnalysisResponse?> GetAnalysisByIdAsync(int id);
    }
}
using System.ComponentModel.DataAnnotations;


namespace ResumeAnalyzerAPI.Models
{
    public class AnalysisResponse
    {
        [Required]
        public required List<string> MatchedSkills { get; set; }
        [Required]
        public required List<string> UnmatchedSkills { get; set; }
        [Required]
        public required decimal MatchPercentage { get; set; }
    }
}
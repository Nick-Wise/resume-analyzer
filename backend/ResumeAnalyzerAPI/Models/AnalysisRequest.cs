using System.ComponentModel.DataAnnotations;

namespace ResumeAnalyzerAPI.Models
{
    public class AnalysisRequest
    {
        [Required]
        public required string JobDescription { get; set; }
        [Required]
        public required string Skills { get; set; }
    }
}
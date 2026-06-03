using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeAnalyzerAPI.Models
{
    public class AnalysisResponse
    {
        [Required]
        public required List<string> matchedSkills { get; set; }
        [Required]
        public required List<string> unmatchedSkills { get; set; }
        [Required]
        public required decimal matchPercentage { get; set; }
    }
}
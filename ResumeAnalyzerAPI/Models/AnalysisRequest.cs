using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeAnalyzerAPI.Models
{
    public class AnalysisRequest
    {
        [Required]
        public required string jobDescription { get; set; }
        [Required]
        public required List<string> skills { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
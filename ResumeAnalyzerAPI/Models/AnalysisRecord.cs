using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeAnalyzerAPI.Models
{
    public class AnalysisRecord
    {
        [Key]
        public int id {get; set;}
        [Required]
        public string jobDescription {get; set;}
        [Required]
        public List<string> skills {get; set;}
        public List<string> matchedSkills {get; set;}
        public List<string> unmatchedSkills {get; set;}
        public decimal matchedSkillsPercentage {get; set;}
        public DateTime CreatedAt {get; set;}
    }
}
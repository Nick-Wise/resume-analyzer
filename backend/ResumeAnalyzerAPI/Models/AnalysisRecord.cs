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
        public int Id {get; set;}
        [Required]
        public string JobDescription {get; set;} = string.Empty;
        [Required]
        public List<string> Skills {get; set;} = new List<string>();
        public List<string> MatchedSkills {get; set;} = new List<string>();
        public List<string> UnmatchedSkills {get; set;} = new List<string>();
        public decimal MatchPercentage {get; set;}
        public DateTime RunDate {get; set;}
    }
}
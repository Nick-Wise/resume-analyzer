using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ResumeAnalyzerAPI.Models;
using ResumeAnalyzerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ResumeAnalyzerAPI.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly ResumeAnalyzerDbContext _context;

        public AnalysisService(ResumeAnalyzerDbContext context)
        {
            _context = context;
        }

        public async Task<AnalysisResponse> AnalyzeAsync(string jobDescription, List<string> skills)
        {
            var _matchedSkills = new List<string>();
            var _unmatchedSkills = new List<string>();
            var normalizedDescription = NormalizeText(jobDescription);

            foreach (var skill in skills)
            {
                if (string.IsNullOrWhiteSpace(skill))
                {
                    _unmatchedSkills.Add(skill ?? string.Empty);
                    continue;
                }

                var pattern = BuildSkillPattern(skill);
                if (Regex.IsMatch(normalizedDescription, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    _matchedSkills.Add(skill);
                }
                else
                {
                    _unmatchedSkills.Add(skill);
                }
                Console.WriteLine($"Skill: {skill}");
                Console.WriteLine($"Pattern: {BuildSkillPattern(skill)}");
                Console.WriteLine($"Match: {Regex.IsMatch(normalizedDescription, BuildSkillPattern(skill), RegexOptions.IgnoreCase)}");
            }

            var _matchPercentage = skills.Count == 0
                ? 0m
                : (decimal)_matchedSkills.Count / skills.Count * 100;
            var response = new AnalysisResponse
            {
                MatchedSkills = _matchedSkills,
                UnmatchedSkills = _unmatchedSkills,
                MatchPercentage = _matchPercentage
            };

            var analysisRecord = new AnalysisRecord
            {
                JobDescription = jobDescription,
                Skills = skills,
                MatchedSkills = _matchedSkills,
                UnmatchedSkills = _unmatchedSkills,
                MatchPercentage = _matchPercentage,
                RunDate = DateTime.UtcNow
            };
            await _context.AnalysisRecord.AddAsync(analysisRecord);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<List<AnalysisHistoryDto>> GetHistoryAsync(){
            var records = await _context.AnalysisRecord
                .OrderByDescending(p => p.RunDate)
                .Select(p => new AnalysisHistoryDto
                {
                    Id = p.Id,
                    JobDescription = p.JobDescription,
                    Skills = p.Skills,
                    MatchedSkills = p.MatchedSkills,
                    UnmatchedSkills = p.UnmatchedSkills,
                    MatchPercentage = Math.Round(p.MatchPercentage,2),
                    RunDate = p.RunDate
                })
                .ToListAsync();

            return records;
        }

        private static string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            return Regex.Replace(text.Trim(), @"\s+", " ");
        }

        private static string BuildSkillPattern(string skill)
        {
            var escapedSkill = Regex.Escape(skill.Trim());
            escapedSkill = Regex.Replace(escapedSkill, @"\s+", "[\\s\\-]+");
            return $"(?<![\\p{{L}}\\p{{N}}]){escapedSkill}(?![\\p{{L}}\\p{{N}}])";
        }
    }
}
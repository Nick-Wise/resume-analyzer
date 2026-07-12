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

        public async Task<AnalysisResponse> AnalyzeAsync(string jobDescription, string skills)
        {

            var skillsList = skills.Split(",").ToList();


            var _matchedSkills = new List<string>();
            var _unmatchedSkills = new List<string>();
            var normalizedDescription = NormalizeText(jobDescription);

            foreach (var skill in skillsList)
            {
                var trimmedSkill = skill.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedSkill))
                {
                    var pattern = BuildSkillPattern(trimmedSkill);
                    if (Regex.IsMatch(normalizedDescription, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    {
                        _matchedSkills.Add(trimmedSkill);
                    }
                    else
                    {
                        _unmatchedSkills.Add(trimmedSkill);
                    }

                }
            }
            var trimmedSkills = _matchedSkills.Concat(_unmatchedSkills).ToList();
            var totalSkillsCount = trimmedSkills.Count();
            var _matchPercentage = totalSkillsCount == 0
                ? 0m
                : (decimal)_matchedSkills.Count / totalSkillsCount * 100;

            var analysisRecord = new AnalysisRecord
            {
                JobDescription = jobDescription,
                Skills = trimmedSkills,
                MatchedSkills = _matchedSkills,
                UnmatchedSkills = _unmatchedSkills,
                MatchPercentage = _matchPercentage,
                RunDate = DateTime.UtcNow
            };
            await _context.AnalysisRecord.AddAsync(analysisRecord);
            await _context.SaveChangesAsync();

            var response = new AnalysisResponse
            {
                Id = analysisRecord.Id,
                MatchedSkills = _matchedSkills,
                UnmatchedSkills = _unmatchedSkills,
                MatchPercentage = _matchPercentage
            };
            return response;
        }

        public async Task<AnalysisResponse?> GetAnalysisByIdAsync(int id)
        {
            var record = await _context.AnalysisRecord.FindAsync(id);
            if (record == null)
            {
                return null;
            }

            return new AnalysisResponse
            {
                Id = record.Id,
                MatchedSkills = record.MatchedSkills,
                UnmatchedSkills = record.UnmatchedSkills,
                MatchPercentage = record.MatchPercentage
            };
        }

        public async Task<List<AnalysisHistoryDto>> GetHistoryAsync()
        {
            var records = await _context.AnalysisRecord
                .OrderByDescending(p => p.RunDate)
                .Select(p => new AnalysisHistoryDto
                {
                    Id = p.Id,
                    JobDescription = p.JobDescription,
                    Skills = p.Skills,
                    MatchedSkills = p.MatchedSkills,
                    UnmatchedSkills = p.UnmatchedSkills,
                    MatchPercentage = Math.Round(p.MatchPercentage, 2),
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
            var word = skill.Split(' ');
            var escapedSkill = word.Select(word => Regex.Escape(word));
            var result = string.Join(@"[\s\-]+", escapedSkill);
            return $"(?<![\\p{{L}}\\p{{N}}]){result}(?![\\p{{L}}\\p{{N}}])";
        }
    }
}
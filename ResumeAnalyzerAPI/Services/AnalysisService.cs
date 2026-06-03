using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ResumeAnalyzerAPI.Models;

namespace ResumeAnalyzerAPI.Services
{
    public class AnalysisService : IAnalysisService
    {
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
            }

            var _matchPercentage = skills.Count == 0
                ? 0m
                : (decimal)_matchedSkills.Count / skills.Count * 100;
            var response = new AnalysisResponse
            {
                matchedSkills = _matchedSkills,
                unmatchedSkills = _unmatchedSkills,
                matchPercentage = _matchPercentage
            };
            return await Task.FromResult(response);
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
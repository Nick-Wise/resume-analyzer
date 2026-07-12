export interface AnalysisResponse{
  id : number;
  matchedSkills : string[];
  unmatchedSkills : string[];
  matchPercentage : number;
}
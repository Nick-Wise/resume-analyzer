export interface AnalysisHistoryDto{
  id : number;
  jobDescription : string;
  skills : string[];
  matchedSkills : string[];
  unMatchedSkills : string[];
  matchPercentage : number;
  runTime : Date;
}
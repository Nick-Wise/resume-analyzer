import type { AnalysisResponse } from "../types/AnalysisResponse";
import { useParams} from "react-router-dom";

export default function Result({cache}: {cache : Record<string,AnalysisResponse> }){

  type RouteParams = {resultId : string};
  const {resultId} = useParams<RouteParams>();

  if(resultId === undefined){
    return(
      <p>Result not Found</p>
    )
  }

  const response = cache[resultId];

  

  return(
          <div className="flex flex-col flex-1 h-full w-full items-center" >
            <div className="grid grid-rows-[auto_1fr] justify-center w-full h-full bg-white p-4 rounded">
              <h1 className="text-xl text-center pb-20">Results</h1>
              <div className="flex flex-col gap-2">
                <p className="text-xl">Matched Skills:</p>
                <div className="flex flex-col items-center">
                  <ul className="list-disc">
                    {response.matchedSkills.map((skill) => (
                      <li key={skill}>{skill}</li>
                    ))}
                  </ul>
                </div>

                <p className="text-xl">Unmatched Skills:</p>
                <div className="flex flex-col items-center">
                  <ul className="list-disc">
                    {response.unmatchedSkills.map((skill) => (
                      <li key={skill}>{skill}</li>
                    ))}
                  </ul>
                </div>

                <p className="text-xl">Match Percentage: {response.matchPercentage.toFixed(2)}%</p>
              </div>
            </div>
          </div>
  )
}
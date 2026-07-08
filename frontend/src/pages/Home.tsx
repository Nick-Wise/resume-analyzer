import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import type { AnalysisRequest } from "../types/AnalysisRequest";
import type { AnalysisResponse } from "../types/AnalysisResponse";
import { useState } from "react";

export default function Home() {
  //add loading state
  const [result, setResult] = useState<AnalysisResponse | null>(null);

  const AnalysisRequestSchema = z.object({
    jobDescription: z.string().min(1, "Job Description is Required"),
    skills: z.string().refine((val) => {
      const parts = val.split(",");
      return parts.every(part => part.trim() !== "");
    }, "Must be comma seperated values")
  });

  type AnalysisRequestForm = z.infer<typeof AnalysisRequestSchema>;

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AnalysisRequestForm>({
    resolver: zodResolver(AnalysisRequestSchema),
  })

  async function OnSubmit(data: AnalysisRequestForm) {
    const request: AnalysisRequest = {
      jobDescription: data.jobDescription,
      skills: data.skills,
    };

    const response = await fetch("http://localhost:5185/api/Analysis/analyze", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(request),
    });

    const responseResult = await response.json() as AnalysisResponse;

    setResult(responseResult);

  }


  return (
    <>
      <section>
        <form onSubmit={handleSubmit(OnSubmit)}>

          <div>
            <label>Job Description</label>
            <textarea className="border" {...register("jobDescription")} />
          </div>

          <div>
            <label>Skills</label>
            <input {...register("skills")} />
          </div>
          <button>Submit</button>
        </form>
      </section>
    {result && 
      <section>
        <p>Matched Skills:</p>
        <ul>
          {result.matchedSkills.map((skill)=>(
            <li key={skill}>{skill}</li>
          ))}
        </ul>
        <p>Unmatched Skills:</p>
        <ul>
          {result.unmatchedSkills.map((skill)=>(
            <li key={skill}>{skill}</li>
          ))}
        </ul>
        <p>Match Percentage: {result.matchPercentage.toFixed(2)}%</p>
      </section>
    } 
    </>
  );

}
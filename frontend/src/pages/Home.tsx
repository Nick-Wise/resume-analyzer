import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import type { AnalysisRequest } from "../types/AnalysisRequest";
import type { AnalysisResponse } from "../types/AnalysisResponse";
import type { Dispatch} from "react";
import type { SetStateAction} from "react";

export default function Home({setCache} : {setCache : Dispatch<SetStateAction<Record<string,AnalysisResponse>>>}) {

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

    setCache(prev => ({...prev, [responseResult.id]: responseResult}));

    console.log("Submitted and cached")
  }


  return (
    <main className="h-screen flex items-center p-16 ">
      <section className="flex flex-1 items-center gap-2 h-full w-full p-4 bg-sky-100 rounded ">
        <div className="flex flex-1 items-center h-full w-full bg-white">
          <form className="flex flex-col flex-1 gap-3 items-center w-full h-full  p-4 rounded" onSubmit={handleSubmit(OnSubmit)}>
            <div className="flex flex-col gap-2  w-full">
              <label className="text-xl">Skills:</label>
              <input className="border rounded w-full" {...register("skills")} />
            </div>

            <div className="flex flex-col gap-2 flex-1 w-full">
              <label className="text-xl" >Job Description:</label>
              <textarea className="border rounded h-full" {...register("jobDescription")} />
            </div>
            <button className="text-xl text-white border rounded bg-sky-500/50 hover:bg-sky-700/50 p-2 w-1/2" >Submit</button>
          </form>
        </div>
      </section>
    </main>
  );

}
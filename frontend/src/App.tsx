import {BrowserRouter,Routes,Route} from "react-router-dom"
import {useState} from "react";
import Home from "./pages/Home"
import Result from "./pages/Result"
import type { AnalysisResponse } from "./types/AnalysisResponse";
function App() {
  const [analysisCache, setAnalysisCache] = useState<Record<string,AnalysisResponse>>({});

  return(
    <BrowserRouter>
      <Routes>
        <Route path ="/" element = {<Home setCache = {setAnalysisCache} />}/>
        <Route path ="/results/:id" element = {<Result/>}/>
      </Routes>
    </BrowserRouter>
  )
  
}

export default App

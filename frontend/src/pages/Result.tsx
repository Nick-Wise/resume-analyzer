import { useParams} from "react-router-dom";

export default function Result(){

  type RouteParams = {id : string};
  const {id} = useParams<RouteParams>();

  if(id === undefined){
    return(
      <p>Result not Found</p>
    )
  }

  const resultId = parseInt(id)

  return(
    <>
    <p>This is the results page</p>
    <p>ResultId: {resultId}</p>
    </>
  )
}
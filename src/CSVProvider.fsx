#r @"..\packages\FSharp.Data\lib\net40\Fsharp.Data.dll"
open FSharp.Data

let [<Literal>] csvPath = ".\goodreads_library_export.csv"
type File = CsvProvider<csvPath>

let excel = new File()
excel.Rows 
|> Seq.sortByDescending (fun book -> book.``Average Rating``)
|> Seq.take 10 
|> Seq.map (fun book -> book.``Average Rating``, book.Title) 
|> Seq.iter (printfn "%A")
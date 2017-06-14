#r @"..\packages\FSharp.Data\lib\net40\Fsharp.Data.dll"
open FSharp.Data

type File = CsvProvider<"C:\Users\ext_jvy\Desktop\goodreads_library_export.csv">
let excel = new File()
excel.Rows |> Seq.map (fun book -> (sprintf "{\"createdAt\" : ISODate(\"2017-04-07T23:03:52.692+02:00\"),
    \"updatedAt\" : ISODate(\"2017-06-09T13:44:31.728+02:00\"), \"ISBN\" : %s, \"title\" : \"%s\", \"author\" : {\"lastName\" : \"%s\", \"firstName\" : \"\"}}" book.ISBN13 book.Title book.Author)) |> Seq.take 50 |> Seq.iter (printfn "%A")

#r @"..\packages\FSharp.Data\lib\net40\Fsharp.Data.dll"
open FSharp.Data

type BookTypes = JsonProvider<"sample-books.json", RootName="books">
type GoodReadsExport = CsvProvider<"goodreads_library_export.csv">

let toAuthor (lastNameFirstName : string) = 
    match lastNameFirstName.Split([|", "|], System.StringSplitOptions.RemoveEmptyEntries) with
    | [| lastName; firstName |] -> BookTypes.Author(lastName, firstName)
    | _ -> failwithf "Let's not deal with errors today."

let export = new GoodReadsExport()
export.Rows
|> Seq.sortByDescending(fun r -> r.``My Rating``)
|> Seq.take 20
|> Seq.map (fun row -> 
    BookTypes.Book(
        row.Title, 
        row.``Author l-f`` |> toAuthor, 
        row.ISBN13, 
        createdAt = row.``Date Added``, 
        readAt = row.``Date Read``))
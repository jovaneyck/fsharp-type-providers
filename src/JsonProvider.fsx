#r @"..\packages\FSharp.Data\lib\net40\Fsharp.Data.dll"
open FSharp.Data
open System

let [<Literal>] URL = "http://swapi.co/api/"
let [<Literal>] PeopleUrl = URL + "people/1"
let [<Literal>] PagePeopleUrl = URL + "people/"
let [<Literal>] FilmUrl = URL + "films/1"

type SWAPI = JsonProvider<URL>
type PagePeople = JsonProvider<PagePeopleUrl>
type People = JsonProvider<PeopleUrl>
type Film = JsonProvider<FilmUrl>

let root = SWAPI.GetSample()

let buildUrl url id = sprintf "%s%s" url id

//Let's fetch person number 13:
let thirteen = People.Load(buildUrl root.People "13")
printfn "%s" thirteen.Name

let loadPeople (pageUrl) = 
    let rec iter (pageUrl : string) =
        seq{
            if (pageUrl <> "") then
                let currentPage = PagePeople.Load(pageUrl)
                yield! currentPage.Results |> Array.toSeq
                yield! iter currentPage.Next
        }
    iter pageUrl

let allCharacters = loadPeople root.People
let withFilms = allCharacters |> Seq.map (fun p -> p.Name, p.Films |> Seq.map Film.Load)

let (_,filmsStarringR2) = withFilms |> Seq.find (fun (p,films) -> p.Contains("Darth Vader"))
filmsStarringR2 |> Seq.map (fun f -> f.Title) |> Seq.toList

type BookAPI = JsonProvider<"http://localhost:4100/api/books">
let books = BookAPI.Load("http://localhost:4100/api/books")
books |> Seq.map (fun b -> b.Title) |> Seq.iter (printfn "%A")

#r @"C:\nuget_local\Fsharp.Data.2.3.2\lib\net40\Fsharp.Data.dll"
open FSharp.Data
[<Literal>]
let sample = @"{
    ""title"" : ""Management 4.2"",
    ""author"" : {
        ""firstName"" : ""Jurgen"",
        ""lastName"" : ""Appelo""
    },
    ""ISBN"" : ""978-0321123479"",
    ""createdAt"" : ""2017-04-07T23:03:52.692+02:00"",
    ""updatedAt"" : ""2017-06-09T13:44:31.728+02:00""
}"
type lAEbrary = JsonProvider<sample, RootName="book">
let book = lAEbrary.Book("title",lAEbrary.Author("first name", "last name"),"isbn", System.DateTime.Now, System.DateTime.Now)

book
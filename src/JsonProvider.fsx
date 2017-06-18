#r @"..\packages\FSharp.Data\lib\net40\Fsharp.Data.dll"
open FSharp.Data
open System

//Easily talk to a REST API.
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
let withFilms = allCharacters |> Seq.map (fun p -> p, p.Films |> Seq.map Film.Load)
let (_,filmsStarringDarthVader) = withFilms |> Seq.find (fun (p,films) -> p.Name.Contains("Darth Vader"))
filmsStarringDarthVader |> Seq.map (fun f -> f.Title) |> Seq.toList

//Or just generate some types based on sample JSON.
#r @"..\packages\Fsharp.Data\lib\net40\Fsharp.Data.dll"
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
type BookTypes = JsonProvider<sample, RootName="book">
let book = BookTypes.Book("title",BookTypes.Author("first name", "last name"),"isbn", System.DateTime.Now, System.DateTime.Now)
printfn "%A" book
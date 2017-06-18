#r @"..\packages\FSharp.Data.TypeProviders\lib\net40\FSharp.Data.TypeProviders.dll"
#r @"System.ServiceModel.dll"
#r @"System.Runtime.Serialization.dll" //Needed to see the generated contract types in you IDE.
open FSharp.Data.TypeProviders
open System.ServiceModel

type Service = WsdlService<"http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?WSDL">
let svc = Service.GetCountryInfoServiceSoap12()

let response = svc.FullCountryInfo("NG")

printf "Languages of %s: " response.sName
response.Languages
|> Seq.map(fun lang -> lang.sName) 
|> String.concat ","
|> printfn "%s"
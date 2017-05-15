#r @"..\packages\FSharp.Data.TypeProviders\lib\net40\FSharp.Data.TypeProviders.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.ServiceModel.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\System.Runtime.Serialization.dll"
open FSharp.Data.TypeProviders
open System.ServiceModel

type SDSService = WsdlService<"http://servicedomainservice.do.dev.euc/DataRetrievalService.svc">
let svc = SDSService.GetBasicHttpBinding_IDataRetrievalService()
let response = 
    svc.GetServiceScope(
        new SDSService.ServiceTypes.Euroconsumers.ServiceDomainService.Contract.MessageContracts.GetServiceScopeRequest(
            Id=1,
            CultureCode="nl-BE"
        ))
printfn "%A" (response.ServiceScope.Id, response.ServiceScope.Name, response.ServiceScope.CssClassCode)
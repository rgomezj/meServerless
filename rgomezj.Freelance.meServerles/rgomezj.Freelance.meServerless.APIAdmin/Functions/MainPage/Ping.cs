using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using rgomezj.Freelance.meServerless.API.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.meServerless.API.Functions;
using Microsoft.Extensions.Configuration;
using rgomezj.Freelance.MeServerless.Data;
using Indigo.Functions.Unity;

namespace rgomezj.Freelance.meServerless.API
{
    public static class Ping
    {
        [FunctionName("Ping")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, [Inject]IGeneralInfoRepository generalRepository, TraceWriter log)
        {  
            log.Info("C# HTTP trigger function processed a request.");
            FreelanceInfoViewModel info = new FreelanceInfoViewModel() {
                GeneralInfo = await generalRepository.Get()
            };

            return  (ActionResult)new OkObjectResult(info);
        }
    }
}

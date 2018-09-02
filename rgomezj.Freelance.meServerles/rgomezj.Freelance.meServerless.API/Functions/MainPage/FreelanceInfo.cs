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
using rgomezj.Freelance.meServerless.Services.Abstract;

namespace rgomezj.Freelance.meServerless.API
{
    public static class FreelanceInfo
    {
        private static IGeneralInfoRepository _generalRepository;
        private static IAptitudeRepository _aptitudeRepository;
        private static ICompanyRepository _companyRepository;
        private static IReferenceRepository _referenceRepository;
        private static ISkillRepository _skillRepository;
        private static ITechnologyRepository _technologyRepository;

        private static IEmailService _emailService;
        private static ICaptchaValidationService _captchaValidationService;

        [FunctionName("FreelanceInfo")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            TraceWriter log)
        {
            Util.InitializeInstances(ref _generalRepository, ref _aptitudeRepository, ref _companyRepository, ref _referenceRepository, ref _skillRepository, ref _technologyRepository, ref _emailService, ref _captchaValidationService);

            log.Info("C# HTTP trigger function processed a request.");
            FreelanceInfoViewModel info = new FreelanceInfoViewModel() {
                GeneralInfo = await _generalRepository.Get(),
                Aptitudes = await _aptitudeRepository.GetAll(),
                Companies = await _companyRepository.GetAll(),
                References = await _referenceRepository.GetAll(),
                Skills = await _skillRepository.GetAll(),
                Technologies = await _technologyRepository.GetAll()
            };

            return  (ActionResult)new OkObjectResult(info);
        }
    }
}

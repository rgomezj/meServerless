
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.meServerless.API.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using rgomezj.Freelance.meServerless.Services;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Linq;
using rgomezj.Freelance.meServerless.API.Functions;
using rgomezj.Freelance.MeServerless.Data;
using Indigo.Functions.Unity;
using rgomezj.Freelance.meServerless.Services.Implementation;
using rgomezj.Freelance.meServerless.Services.Abstract;

namespace rgomezj.Freelance.meServerless.API
{
    public static class SendEmail
    {
        private static IGeneralInfoRepository _generalRepository;
        private static IAptitudeRepository _aptitudeRepository;
        private static ICompanyRepository _companyRepository;
        private static IReferenceRepository _referenceRepository;
        private static ISkillRepository _skillRepository;
        private static ITechnologyRepository _technologyRepository;

        private static IEmailService _emailService;
        private static ICaptchaValidationService _captchaValidationService;

        [FunctionName("SendEmail")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            Util.InitializeInstances(ref _generalRepository, ref _aptitudeRepository, ref _companyRepository, ref _referenceRepository, ref _skillRepository, ref _technologyRepository, ref _emailService, ref _captchaValidationService);

            #region Getting Request values and configuration

            string requestFormValues = new StreamReader(req.Body).ReadToEnd();
            string message = Util.GetSerializedBody(requestFormValues);

            EmailMessage emailMessage = Util.Deserialize<EmailMessage>(message);
            string captchaResponse = Util.GetFormValue(requestFormValues, _captchaValidationService.GetSettings().CaptchaResponseKey);
            
            #endregion

            var validationResult = await _captchaValidationService.IsValidCaptcha(_captchaValidationService.GetSettings().SecretKey, captchaResponse);
            string errorMessage = string.Empty;
            bool success = true;

            if (validationResult)
            {
                string name = System.Net.WebUtility.HtmlEncode(emailMessage.FromName);
                GeneralInfo generalInfo = await _generalRepository.Get();
                emailMessage.To = generalInfo.EmailAddress;
                emailMessage.ToName = generalInfo.Name;
                emailMessage.Message = emailMessage.Message + Environment.NewLine + emailMessage.FromName + Environment.NewLine + emailMessage.From;
                try
                {
                    await _emailService.SendEmail(emailMessage);
                }
                catch
                {
                    errorMessage = $"Sorry {name}, it seems that my mail server is not responding. Please try again later!";
                    success = false;
                }
            }
            else
            {
                errorMessage = "Captcha validation didn't pass, please try again";
                success = false;
            }
            return new JsonResult(new { success, errorMessage });
        }
    }
}

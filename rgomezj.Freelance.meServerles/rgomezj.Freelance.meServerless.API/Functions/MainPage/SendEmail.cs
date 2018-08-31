
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

namespace rgomezj.Freelance.meServerless.API
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, [Inject]IGeneralInfoRepository generalRepository, TraceWriter log, ExecutionContext context)
        {
            #region Getting Request values and configuration

            var config = Util.InitConfiguration(context);

            string requestFormValues = new StreamReader(req.Body).ReadToEnd();
            string message = Util.GetSerializedBody(requestFormValues);

            EmailMessage emailMessage = Util.Deserialize<EmailMessage>(message);

            string captchaSettings = Util.GetConfigVariable("captchaSettings", config);
            string emailSettings = Util.GetConfigVariable("emailSettings", config);
            CaptchaSettings captchaSettingsConfig = Util.Deserialize<CaptchaSettings>(captchaSettings);
            EmailSettings emailSettingsConfig = Util.Deserialize<EmailSettings>(emailSettings);

            CaptchaValidationService _captchaValidationService = new CaptchaValidationService(captchaSettingsConfig);
            string captchaResponse = Util.GetFormValue(requestFormValues, captchaSettingsConfig.CaptchaResponseKey);
            
            SendGridEmailService _emailService = new SendGridEmailService(emailSettingsConfig);

            #endregion

            var validationResult = await _captchaValidationService.IsValidCaptcha(captchaSettingsConfig.SecretKey, captchaResponse);
            string errorMessage = string.Empty;
            bool success = true;

            if (validationResult)
            {
                string name = System.Net.WebUtility.HtmlEncode(emailMessage.FromName);
                GeneralInfo generalInfo = await FreelanceInfo.GetGeneralInfo(context);
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

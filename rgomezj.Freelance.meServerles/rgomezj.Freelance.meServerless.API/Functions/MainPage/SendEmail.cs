
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

namespace rgomezj.Freelance.meServerless.API
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var config = InitConfiguration(context);

            string requestFormValues = new StreamReader(req.Body).ReadToEnd();
            string message = GetSerializedBody(requestFormValues);

            EmailMessage emailMessage = Deserialize<EmailMessage>(message);

            string captchaSettings = GetConfigVariable("captchaSettings", config);
            string emailSettings = GetConfigVariable("emailSettings", config);
            CaptchaSettings captchaSettingsConfig = Deserialize<CaptchaSettings>(captchaSettings);
            EmailSettings emailSettingsConfig = Deserialize<EmailSettings>(emailSettings);

            string errorMessage = string.Empty;
            bool success = true;
            GeneralInfo generalInfo = FreelanceInfo.GetGeneralInfo();

            CaptchaValidationService _captchaValidationService = new CaptchaValidationService(captchaSettingsConfig);
            string captchaResponse = GetFormValue(requestFormValues, captchaSettingsConfig.CaptchaResponseKey);
            
            string name = System.Net.WebUtility.HtmlEncode(emailMessage.FromName);
            log.Info("Captcha response:" + captchaResponse);
            SendGridEmailService _emailService = new SendGridEmailService(emailSettingsConfig);
            var validationResult = await _captchaValidationService.IsValidCaptcha(captchaSettingsConfig.SecretKey, captchaResponse);

            if (validationResult)
            {
                log.Info("Email Address:" + generalInfo.EmailAddress);
                log.Info("Email settings:" + emailSettingsConfig.ApiKey);
                log.Info("Email settings user:" + emailSettingsConfig.UserName);
                log.Info("Email settings SMTP:" + emailSettingsConfig.SmtpServer);
                log.Info("Email settings Password:" + emailSettingsConfig.Password);
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

        public static string GetConfigVariable(string configKey, IConfiguration configuration)
        {
            string result = Environment.GetEnvironmentVariable(configKey);
            if (string.IsNullOrEmpty(result) && configuration != null)
            {
                result = configuration.GetSection(configKey).Value;
            }
            return result;
        }

        /// <summary>
        /// Allows to deserialize an object of the given type. In case the serialization method changes, would be changed only here
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static IConfiguration InitConfiguration(ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(context.FunctionAppDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            return config;
        }

        public static string GetSerializedBody(string data)
        {
            var dataNvc = HttpUtility.ParseQueryString(data);
            var dataCollection = dataNvc.AllKeys.ToDictionary(o => o, o => dataNvc[o]);
            var jsonString = JsonConvert.SerializeObject(dataCollection);
            return jsonString;
        }

        public static string GetFormValue(string data, string key)
        {
            var dataNvc = HttpUtility.ParseQueryString(data);
            var value = dataNvc[key];
            return value;
        }
    }
}

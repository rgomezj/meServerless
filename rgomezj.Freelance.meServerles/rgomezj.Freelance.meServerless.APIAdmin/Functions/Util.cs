using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using rgomezj.Freelance.Me.Data.Implementation.JSON;
using rgomezj.Freelance.Me.Data.Implementation.JSON.Config;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage.Config;
using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.meServerless.Services.Abstract;
using rgomezj.Freelance.meServerless.Services.Implementation;
using rgomezj.Freelance.MeServerless.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace rgomezj.Freelance.meServerless.API.Functions
{
    public class Util
    {
        const string STORAGECONNECTIONKEY = "StorageConnection";
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
        public static T Deserialize<T>(string json)
        {
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

        public static void InitializeInstances(ref IGeneralInfoRepository generalInfoRepository,
                                               ref IAptitudeRepository aptitudeRepository,
                                               ref ICompanyRepository companyRepository,
                                               ref IReferenceRepository referenceRepository,
                                               ref ISkillRepository skillRepository,
                                               ref ITechnologyRepository technologyRepository,
                                               ref IEmailService _emailService,
                                               ref ICaptchaValidationService _captchaValidationService)
        {
            string storageConnectionKey = Util.GetConfigVariable(STORAGECONNECTIONKEY, null);

            generalInfoRepository = new TableGeneralInfoRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            aptitudeRepository = new TableAptitudeRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            companyRepository = new TableCompanyRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            referenceRepository = new TableReferenceRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            skillRepository = new TableSkillRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            technologyRepository = new TableTechologyRepository(new TableStorageConfig() { ConnectionString = storageConnectionKey });
            
            // generalInfoRepository = new JSONGeneralInfoRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });
            //aptitudeRepository = new JSONAptitudeRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });
            //companyRepository = new JSONCompanyRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });
            //referenceRepository = new JSONReferenceRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });
            //skillRepository = new JSONSkillRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });
            //technologyRepository = new JSONTechnologyRepository(new JSONDatabaseConfig() { ConnectionString = storageConnectionKey });

            _emailService = new SendGridEmailService(Util.Deserialize<EmailSettings>(Util.GetConfigVariable("emailSettings", null)));
            _captchaValidationService = new CaptchaValidationService(Util.Deserialize<CaptchaSettings>(Util.GetConfigVariable("captchaSettings", null)));
        }
    }
}

using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace rgomezj.Freelance.meServerless.API.Functions
{
    public class Util
    {
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
    }
}

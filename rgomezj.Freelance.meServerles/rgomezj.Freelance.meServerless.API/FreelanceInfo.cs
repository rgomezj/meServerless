
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using rgomezj.Freelance.meServerless.API.Profile;
using rgomezj.Freelance.meServerless.API.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace rgomezj.Freelance.meServerless.API
{
    public static class FreelanceInfo
    {
        [FunctionName("FreelanceInfo")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            FreelanceInfoViewModel info = new FreelanceInfoViewModel() {
                GeneralInfo = GetGeneralInfo(),
                Aptitudes = GetAptitudes()
            };

            return  (ActionResult)new OkObjectResult(info);
        }

        public static GeneralInfo GetGeneralInfo()
        {
            var result = JsonConvert.DeserializeObject<GeneralInfo>(@"{
              'ClientsFreelanceCount': '2',
              'DynamicsCRMProjectsDelivered': '10',
              'YearsTechnicalLead': '3',
              'YearsWorkingSoftware': '11',
              'Name': 'Roger Gomez',
              'Title': 'Software Developer',
              'Phone': '+57 3006318287',
              'EmailAddress': 'rogergomez780@gmail.com',
              'Location': 'Medellin, Colombia',
              'Subtitle': 'Detail Oriented, Resourceful, Fast Learner',
              'Summary': 'Expertise in software development (back end and front end), patterns, practices, and continuous integration. &#9658; 10+ years of experience working with enterprise databases including SQL Server and Oracle. &#9658; Worked within all aspects of the software development lifecycle. &#9658; A long record of working with customers and developers to successfully deliver solutions that meet business needs. &#9658; Extremely experienced using the Microsoft Stack.'
            }
            ");
            return result;
        }

        public static List<Aptitude> GetAptitudes()
        {
            var result = JsonConvert.DeserializeObject < List<Aptitude>>(@"[
  {
    'SortOrder': 1,
    'Title': 'Resourceful',
    'Description': 'Even though I have my preferences in technology. I have used a wide variety of technologies/tools to meet business needs',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/network.png'
  },
  {
    'SortOrder': 2,
    'Title': 'Fast Learner',
    'Description': 'As technology evolves, adapting to changes and learning quickly allows me to bring solutions to reality',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/optimization-clock.png'
  },
  {
    'SortOrder': 3,
    'Title': 'Configuration Management',
    'Description': 'Not only involved in Development activities but in deployments and configuration management for the solutions delivered',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/settings.png'
  },
  {
    'SortOrder': 4,
    'Title': 'Detail Oriented',
    'Description': 'Not just coding, I understand sometimes solutions go beyond the code itself, so paying special attention to requirements and proposing different ways to do things (which may or not involve code)',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/monitoring.png'
  },
  {
    'SortOrder': 5,
    'Title': 'Communication',
    'Description': 'Good communication skills, meeting with clients for management of the projects or production support. Providing technical and functional training',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/operator.png'
  },
  {
    'SortOrder': 6,
    'Title': 'Work with love',
    'Description': 'I enjoy what I do, being a software developer and bringing solutions through the knowledge I have gained, is one of my passions',
    'IconPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/icons/like.png'
  }
]
");
            return result;
        }
    }
}

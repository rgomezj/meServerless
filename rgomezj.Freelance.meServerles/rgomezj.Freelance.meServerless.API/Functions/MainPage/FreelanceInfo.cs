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

namespace rgomezj.Freelance.meServerless.API
{
    public static class FreelanceInfo
    {
        [FunctionName("FreelanceInfo")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log, ExecutionContext context)
        {
            IConfiguration config = Util.InitConfiguration(context);
            Me.Data.Implementation.JSON.JSONGeneralInfoRepository generalRepository = new Me.Data.Implementation.JSON.JSONGeneralInfoRepository(new Me.Data.Implementation.JSON.Config.JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", config) });
            GeneralInfo generalInfo = await generalRepository.GetEntity<GeneralInfo>();

            log.Info("C# HTTP trigger function processed a request.");
            FreelanceInfoViewModel info = new FreelanceInfoViewModel() {
                GeneralInfo = GetGeneralInfo(),
                Aptitudes = GetAptitudes(),
                Companies = GetCompanies(),
                References = GetReferences(),
                Skills = GetSkills(),
                Technologies = GetTechnologies()
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

        public static List<Company> GetCompanies()
        {
            var result = JsonConvert.DeserializeObject<List<Company>>(@"[
  {
    'SortOrder': 1,
    'CompanyName': 'Intergrupo',
    'Position': 'Software Developer',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/1.jpg'
  },
  {
    'SortOrder': 2,
    'CompanyName': 'Edatel',
    'Position': 'Software Developer-Analyst',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/2.jpg'
  },
  {
    'SortOrder': 3,
    'CompanyName': 'Compuredes',
    'Position': 'Software Developer',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/3.png'
  },
  {
    'SortOrder': 4,
    'CompanyName': 'Pavliks.com',
    'Position': 'Technical Leader',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/5.jpg'
  },
  {
    'SortOrder': 5,
    'CompanyName': 'Upwork',
    'Position': 'Freelance Software Developer',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/4.jpg'
  },
  {
    'SortOrder': 6,
    'CompanyName': 'Wyware Solutions',
    'Position': 'Freelance Software Developer',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/works/6.png'
  }
]
");
            return result;
        }

        public static List<Reference> GetReferences()
        {
            var result = JsonConvert.DeserializeObject<List<Reference>>(@"[
  {
    'SortOrder': 1,
    'ReferenceName': 'Giovanny Alzate',
    'ReferenceCompanyPosition': 'UruIT - Sr. Software Developer',
    'ReferenceDescription': 'I know Roger for almost 15 years and I have had the opportunity to work with him in a couple of opportunities. Roger is a person who loves what he does, he is the kind of people who make the things happen, his commitment talks by himself and when things go wrong he is the one to propose solutions, he is the “fixer”. Roger has leadership skills, he enjoys learning new stuff participating actively in .Net communities, in short Roger is the one I would want to work with in my team.',
    'WebPage': 'https://www.uruit.com/'
  },
  {
    'SortOrder': 2,
    'ReferenceName': 'Juan David Lara',
    'ReferenceCompanyPosition': 'Globant - Sr. Software Developer',
    'ReferenceDescription': 'Excellent engineer, direct person, clear, committed, responsible, collaborator, team player, great technical knowledge and resourceful',
    'WebPage': 'https://www.globant.com/'
  },
  {
    'SortOrder': 3,
    'ReferenceName': 'Frank Wyton',
    'ReferenceCompanyPosition': 'Owner - www.wyware.com',
    'ReferenceDescription': 'Roger is a critical part of our team. He is very strong technically and has the general business acumen to quickly comprehend what is needed to develop a suitable solution. I can trust that the work I give him will be done quickly and at the highest level of quality. It is extremely rare to find someone with such a high level of workmanship and an excellent mentality.',
    'WebPage': 'https://www.wyware.com/'
  },
  {
    'SortOrder': 4,
    'ReferenceName': 'Salim Adamon',
    'ReferenceCompanyPosition': 'CEO - https://www.sadax-technology.com',
    'ReferenceDescription': 'I have been extremely please by my experience working with Roger. Not only does he have very strong technical skills with Dynamics CRM and related development technologies, he also has the ability learn new tools and features and become proficient in no time. His understanding of business needs allowed us to delegate work to him with confidence that the necessary thought process would be applied when building application features. My team really enjoys working with him.',
    'WebPage': 'https://www.sadax-technology.com/'
  }
]
");
            return result;
        }

        public static List<Skill> GetSkills()
        {
            var result = JsonConvert.DeserializeObject<List<Skill>>(@"[
  {
    'SortOrder': 1,
    'SkillName': 'C#',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 2,
    'SkillName': 'ASP.Net',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 3,
    'SkillName': 'Javascript',
    'PercentageExpertise': '80'
  },
  {
    'SortOrder': 4,
    'SkillName': 'HTML/CSS',
    'PercentageExpertise': '75'
  },
  {
    'SortOrder': 5,
    'SkillName': 'SQL Server (Transact SQL)',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 6,
    'SkillName': 'Oracle (PL/SQL)',
    'PercentageExpertise': '85'
  },
  {
    'SortOrder': 7,
    'SkillName': '.Net Core - Learning.......',
    'PercentageExpertise': '30'
  },
  {
    'SortOrder': 8,
    'SkillName': 'React JS - Learning.......',
    'PercentageExpertise': '30'
  },
  {
    'SortOrder': 9,
    'SkillName': 'Azure - Learning.......',
    'PercentageExpertise': '30'
  },
  {
    'SortOrder': 10,
    'SkillName': 'Microsoft Dynamics CRM',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 11,
    'SkillName': 'Patterns and Practices',
    'PercentageExpertise': '80'
  },
  {
    'SortOrder': 12,
    'SkillName': 'Requirements Gathering',
    'PercentageExpertise': '80'
  },
  {
    'SortOrder': 13,
    'SkillName': 'Software Design',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 14,
    'SkillName': 'Problem Solving',
    'PercentageExpertise': '90'
  },
  {
    'SortOrder': 15,
    'SkillName': 'Team Leadership',
    'PercentageExpertise': '90'
  }
]
");
            return result;
        }

        public static List<Technology> GetTechnologies()
        {
            var result = JsonConvert.DeserializeObject<List<Technology>>(@"[
  {
                'SortOrder': 1,
    'TechnologyName': 'Solid',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/1.jpeg'
  },
  {
                'SortOrder': 2,
    'TechnologyName': '.Net',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/2.png'
  },
  {
                'SortOrder': 3,
    'TechnologyName': 'Dynamics CRM',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/3.jpg'
  },
  {
                'SortOrder': 4,
    'TechnologyName': 'Oracle',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/4.png'
  },
  {
                'SortOrder': 5,
    'TechnologyName': 'JS',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/5.png'
  },
  {
                'SortOrder': 6,
    'TechnologyName': 'ASPNET',
    'LogoPath': 'https://rogergomez.z13.web.core.windows.net/template/assets/img/client-logo/6.png'
  }
]");
            return result;
        }
    }
}

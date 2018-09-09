using Indigo.Functions.Unity;
using Microsoft.Extensions.DependencyInjection;
using rgomezj.Freelance.Me.Data.Implementation.JSON;
using rgomezj.Freelance.Me.Data.Implementation.JSON.Config;
using rgomezj.Freelance.meServerless.API.Functions;
using rgomezj.Freelance.MeServerless.Data;
using System;
using Unity;
using Unity.Injection;

namespace rgomezj.Freelance.meServerless.API
{
    public class DependencyInjectionConfig : IDependencyConfig
    {
        public void RegisterComponents(UnityContainer container)
        {
            container.RegisterType<IGeneralInfoRepository, JSONGeneralInfoRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
            container.RegisterType<IAptitudeRepository, JSONAptitudeRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
            container.RegisterType<ICompanyRepository, JSONCompanyRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
            container.RegisterType<IReferenceRepository, JSONReferenceRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
            container.RegisterType<ISkillRepository, JSONSkillRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
            container.RegisterType<ITechnologyRepository, JSONTechnologyRepository>(new InjectionConstructor((new InjectionParameter<JSONDatabaseConfig>(new JSONDatabaseConfig() { ConnectionString = Util.GetConfigVariable("StorageConnection", null) }))));
        }
    }
}

using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.MeServerless.Data;
using rgomezj.Freelance.Me.Data.Implementation.JSON.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace rgomezj.Freelance.Me.Data.Implementation.JSON
{
    public class JSONGeneralInfoRepository : JSONConfigContext, IGeneralInfoRepository 
    {
        public JSONGeneralInfoRepository(JSONDatabaseConfig config) : base(config, "GeneralInfo.json")
        {
        }

        public async Task<GeneralInfo> Get()
        {
            GeneralInfo generalInfo = await this.GetEntity<GeneralInfo>();
            return generalInfo;
        }
    }
}

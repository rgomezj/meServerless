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
    public class JSONAptitudeRepository : JSONConfigContext, IAptitudeRepository
    {
        public JSONAptitudeRepository(JSONDatabaseConfig config) : base(config, "Aptitudes.json")
        {
        }

        public async Task<List<Aptitude>> GetAll()
        {
            List<Aptitude> aptitudes = await this.GetEntity<List<Aptitude>>();
            return aptitudes;
        }
    }
}

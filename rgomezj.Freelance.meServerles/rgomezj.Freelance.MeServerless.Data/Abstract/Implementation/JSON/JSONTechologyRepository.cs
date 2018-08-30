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
    public class JSONTechnologyRepository : JSONConfigContext, ITechnologyRepository
    {
        public JSONTechnologyRepository(JSONDatabaseConfig config) : base(config, "Technologies.json")
        {
        }

        public async Task<List<Technology>> GetAll()
        {
            List<Technology> technologies = await this.GetEntity<List<Technology>>();
            return technologies;
        }
    }
}

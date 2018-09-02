using rgomezj.Freelance.meServerless.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage.Config;
using rgomezj.Freelance.MeServerless.Data;

namespace rgomezj.Freelance.Me.Data.Implementation.TableStorage
{
    public class TableTechologyRepository : TableStorageContext, ITechnologyRepository
    {
        public TableTechologyRepository(TableStorageConfig config) : base(config, "Technologies")
        {
        }

        public async Task<List<Technology>> GetAll()
        {
            List<Technology> technologies = await this.GetEntity<List<Technology>>();
            return technologies;
        }
    }
}

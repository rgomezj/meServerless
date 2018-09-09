using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.MeServerless.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage.Config;

namespace rgomezj.Freelance.Me.Data.Implementation.TableStorage
{
    public class TableAptitudeRepository : TableStorageContext, IAptitudeRepository
    {
        public TableAptitudeRepository(TableStorageConfig config) : base(config, "Aptitudes")
        {
        }

        public async Task<List<Aptitude>> GetAll()
        {
            List<Aptitude> aptitudes = await this.GetEntity<List<Aptitude>>();
            return aptitudes;
        }
    }
}

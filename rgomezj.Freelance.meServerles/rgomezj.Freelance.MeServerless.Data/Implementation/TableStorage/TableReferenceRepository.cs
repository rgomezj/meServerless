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
    public class TableReferenceRepository : TableStorageContext, IReferenceRepository
    {
        public TableReferenceRepository(TableStorageConfig config) : base(config, "References")
        {
        }

        public async Task<List<Reference>> GetAll()
        {
            List<Reference> references = await this.GetEntity<List<Reference>>();
            return references;
        }
    }
}

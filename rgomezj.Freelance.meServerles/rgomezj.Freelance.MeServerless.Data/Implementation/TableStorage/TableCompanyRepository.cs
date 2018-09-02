using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.MeServerless.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage.Config;

namespace rgomezj.Freelance.Me.Data.Implementation.TableStorage
{
    public class TableCompanyRepository : TableStorageContext, ICompanyRepository
    {
        public TableCompanyRepository(TableStorageConfig config) : base(config, "Companies")
        {
        }

        public async Task<List<Company>> GetAll()
        {
            var companies = await this.GetEntity<List<Company>>();
            return companies;
        }
    }
}

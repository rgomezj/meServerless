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
    public class TableGeneralInfoRepository : TableStorageContext, IGeneralInfoRepository 
    {
        public TableGeneralInfoRepository(TableStorageConfig config) : base(config, "GeneralInfo")
        {
        }

        public async Task<GeneralInfo> Get()
        {
            GeneralInfo generalInfo = await this.GetEntity<GeneralInfo>();
            return generalInfo;
        }
    }
}

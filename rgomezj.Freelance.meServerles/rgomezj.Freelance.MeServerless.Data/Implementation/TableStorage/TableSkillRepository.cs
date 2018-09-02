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
    public class TableSkillRepository : TableStorageContext, ISkillRepository
    {
        public TableSkillRepository(TableStorageConfig config) : base(config, "Skills")
        {
        }

        public async Task<List<Skill>> GetAll()
        {
            List<Skill> skills = await this.GetEntity<List<Skill>>();
            return skills;
        }
    }
}

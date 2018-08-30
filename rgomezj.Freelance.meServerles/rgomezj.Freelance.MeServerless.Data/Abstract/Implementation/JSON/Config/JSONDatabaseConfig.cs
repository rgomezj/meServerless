using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.MeServerless.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace rgomezj.Freelance.Me.Data.Implementation.JSON.Config
{
    public class JSONDatabaseConfig
    {
        public string ConnectionString { get; set; }

        public JSONDBFiles Files { get; set; }
    }
}

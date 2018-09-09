using rgomezj.Freelance.meServerless.Core;
using System;
using System.Collections.Generic;

namespace rgomezj.Freelance.meServerless.API.TableStorage
{
    public class GeneralInfoTableEntity : GeneralInfo
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }
    }
}

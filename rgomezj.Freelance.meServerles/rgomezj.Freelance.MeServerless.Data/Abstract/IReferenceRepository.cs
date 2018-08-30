using rgomezj.Freelance.meServerless.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rgomezj.Freelance.MeServerless.Data
{
    public interface IReferenceRepository
    {
        Task<List<Reference>> GetAll();
    }
}

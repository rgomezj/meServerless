using rgomezj.Freelance.meServerless.API.Profile;
using System;
using System.Collections.Generic;

namespace rgomezj.Freelance.meServerless.API.ViewModels
{
    public class FreelanceInfoViewModel
    {
        public GeneralInfo GeneralInfo { get; set; }

        public List<Aptitude> Aptitudes { get; set; }

        public List<Company> Companies { get; set; }

        public List<Reference> References { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Technology> Technologies { get; set; }
    }
}

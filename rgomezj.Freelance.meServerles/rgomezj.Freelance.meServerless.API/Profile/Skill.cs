using System;

namespace rgomezj.Freelance.meServerless.API.Profile
{
    public class Skill
    {
        public int SortOrder { get; set; }

        public string SkillName { get; set; }

        public int PercentageExpertise { get; set; }

        public string PercentageExpertiseWidth
        {
            get
            {
                return this.PercentageExpertise + "%";
            }
        }
    }
}

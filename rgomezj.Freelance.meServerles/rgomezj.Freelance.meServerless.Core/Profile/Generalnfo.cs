using System;

namespace rgomezj.Freelance.meServerless.Core
{
    public class GeneralInfo
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Summary { get; set; }

        public string Phone { get; set; }

        public string EmailAddress { get; set; }

        public string Location { get; set; }

        public string NameAndTitle
        {
            get
            {
                return this.Name + " - " + this.Title;
            }
        }

        public int YearsWorkingSoftware { get; set; }

        public int YearsTechnicalLead { get; set; }

        public int ClientsFreelanceCount { get; set; }

        public int DynamicsCRMProjectsDelivered { get; set; }
    }
}

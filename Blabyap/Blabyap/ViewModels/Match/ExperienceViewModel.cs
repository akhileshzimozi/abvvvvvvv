using Blabyap.Common.Model;
using System;
using System.Collections.Generic;

namespace Blabyap.ViewModels.Match
{
    public class ExperienceViewModel
    {


        public string AI { get; set; }
        public string Startups { get; set; }
        public string IoT { get; set; }
        public string Blockchain { get; set; }
        public string Consulting { get; set; }
        public string Coaching { get; set; }
        public string Technology { get; set; }
        public string Enterpreneur { get; set; }

        private List<FamilyData> expertiseCollection;
        public List<FamilyData> ExpertiseCollection
        {
            get { return expertiseCollection; }
            set { expertiseCollection = value; }

        }
    }
}

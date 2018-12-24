using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels.Profile
{
    public class WorkViewModel
    {
        public string Heading { get; set; }

        public string CurrentCompany { get; set; }
        public string JobTitle { get; set; }
        private List<FamilyData> industry;
        public List<FamilyData> Industry
        {
            get { return industry; }
            set { industry = value; }
        }
        public string IndustryText { get; set; }



        private ObservableCollection<string> seniority;
        public ObservableCollection<string> Seniority
        {
            get { return seniority; }
            set { seniority = value; }
        }
        public string SeniorityText { get; set; }


    }
}

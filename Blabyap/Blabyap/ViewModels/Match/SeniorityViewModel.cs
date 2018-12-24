using System;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels.Match
{
    public class SeniorityViewModel
    {
        public string Heading { get; set; }

        private ObservableCollection<string> seniority;
        public ObservableCollection<string> Seniority
        {
            get { return seniority; }
            set { seniority = value; }
        }

        //public string Student { get; set; }
        //public string Operational { get; set; }
        //public string Supervisory { get; set; }
        //public string MiddleMgt { get; set; }
        //public string SeniorMgt { get; set; }
        //public string CSuite { get; set; }

    }
}

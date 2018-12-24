using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels.Profile
{
    public class OtherViewModel
    {
        public string Heading { get; set; }

       

        private List<FamilyData> nationality;
        public List<FamilyData> Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        public string NationalityText { get; set; }
        public string Education { get; set; }
        public string Enterpreneur { get; set; }
        public string Investor { get; set; }

    }
}

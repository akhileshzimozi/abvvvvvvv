using Blabyap.Common.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Blabyap.ViewModels.Profile
{
    public class ExpertiseViewModel : BindableBase
    {
        public string Heading { get; set; }


        private List<FamilyData> expertise;

      

        public List<FamilyData> Expertise
        {
            get { return expertise; }
            set { expertise = value;  }



        }

       

        //public string Leadership { get; set; }
        //public string Marketing { get; set; }
        //public string AI { get; set; }
        //public string Startups { get; set; }
        //public string IoT { get; set; }
        //public string Blockchain { get; set; }
        //public string Education { get; set; }
        //public string Finance { get; set; }
        //public string Consulting { get; set; }
        //public string Coaching { get; set; }
        //public string Technology { get; set; }
        //public string Enterpreneur { get; set; }

    }
}

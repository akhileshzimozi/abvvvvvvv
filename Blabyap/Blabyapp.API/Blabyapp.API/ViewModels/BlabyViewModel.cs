using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blabyapp.API.DataModels;
using Blabyap.Common.Model;

namespace Blabyapp.API.ViewModels
{

    public class CandidateViewModel
    {
        public string CandidateID { get; set; }
        public string ImageURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CurrentDesignation { get; set; }
        public string Age { get; set; }
        public string CurrentLocationKM { get; set; }
        public List<CustomCaption> LookUpList { get; set; }
        public List<CustomCaption> SectionList { get; set; }
        
        public CandidateViewModel()
        {
            LookUpList = new List<CustomCaption>();
            SectionList = new List<CustomCaption>();
        }

        public List<CandidateViewModel> GetDemoDataCandidateViewModelList()
        {
            return new List<CandidateViewModel>
            {
                new CandidateViewModel
                {
                    CandidateID = "1",
                    Age = "36",
                    CurrentDesignation = "Chief Complaince OFfice",
                    CurrentLocationKM = "1.2 K",
                    FullName ="Peter",
                    LookUpList = new List<CustomCaption>
                    {
                        new CustomCaption{ Code ="Consulting",Translation="Consulting",ISSelected = true},
                        new CustomCaption{ Code ="Coaching",Translation="Consulting",ISSelected = false},
                        new CustomCaption{ Code ="Consulting",Translation="Consulting",ISSelected = false}

                    },
                    SectionList = new List<CustomCaption>
                    {
                        new CustomCaption{Code="Industry" , Translation="Insurance"},
                        new CustomCaption{Code="Organisation" , Translation="PF"},
                        new CustomCaption{Code="Education", Translation="S MBA"},
                    }
                },
                new CandidateViewModel
                {
                    CandidateID = "2",
                    Age = "33",
                    CurrentDesignation = "Director",
                    CurrentLocationKM = "1.2 K",
                    FullName ="Rattana",
                    LookUpList = new List<CustomCaption>
                    {
                        new CustomCaption{ Code ="Consulting",Translation="Consulting",ISSelected = true},
                        new CustomCaption{ Code ="IT",Translation="Consulting",ISSelected = false},
                        new CustomCaption{ Code ="Consulting",Translation="Consulting",ISSelected = false}

                    },
                    SectionList = new List<CustomCaption>
                    {
                        new CustomCaption{Code="Industry" , Translation="Automobile"},
                        new CustomCaption{Code="Organisation" , Translation="PF"},
                        new CustomCaption{Code="Education", Translation="S MBA"},
                    }
                }

            };
        }
    }

    
}
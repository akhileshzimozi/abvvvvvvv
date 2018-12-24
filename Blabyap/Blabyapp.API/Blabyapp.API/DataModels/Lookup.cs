using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blabyapp.API.DataModels
{
    public class Lookup
    {
        public string LookupId { get; set; }
        public string Code { get; set; }//m/F    // 
        public string Family { get; set; }   //gender  // Expertise
        public string FamilyType { get; set; }//choice
        public string Translation { get; set; } // Male/female   //
    }
}
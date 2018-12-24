using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blabyapp.API.DataModels
{
    public class Seniority
    {
        
        public string SeniorityID { get; set; }
        public string SeniorityInfo { get; set; }
        public string SeniorityType { get; set; }

        //public string Student { get; set; }
        //public string Operational { get; set; }
        //public string Supervisory { get; set; }
        //public string MiddleManagement { get; set; }
        //public string SeniorManagement { get; set; }
        //public string CSuite { get; set; }
        //public string Senior { get; set; }
    }
}
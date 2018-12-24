using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blabyapp.API.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Blabyapp.API.DataModels
{
    public class ExpertiseArea
    {
        [Key]
        public int ExperAreaID { get; set; }
        public string ExpertInfo { get; set; }
        public string ExpertType { get; set; }
        
    }
}
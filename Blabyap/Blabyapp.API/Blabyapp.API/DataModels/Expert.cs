using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blabyapp.API.DataModels
{
    public class Expert
    {
        [Key]
        public int ExperID { get; set; }
        public string ExpertInfo { get; set; }
        public string ExpertType { get; set; }
    }
}

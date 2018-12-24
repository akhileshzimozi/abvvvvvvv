using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationAPI.Models
{
    public class Guide
    {
        public int GuideID { get; set; }
        public string GuideName { get; set; }
        public string GuideAddress { get; set; }
    }
}
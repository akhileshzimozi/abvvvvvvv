using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blabyapp.API.DataModels
{
    public class MeetNow
    {
        public string MeetNowID { get; set; }
        public string Percentage { get; set; }
        public string UserName { get; set; }
        public string CurrentDesignation { get; set; }
        public string CurrentCompany { get; set; }
    }
}
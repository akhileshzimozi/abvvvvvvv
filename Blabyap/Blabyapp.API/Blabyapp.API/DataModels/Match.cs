using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blabyap.Common.Model;

namespace Blabyapp.API.DataModels
{
    public class Match
    {
        [Key]
        public string MatchID { get; set; }
        public string RecruiterEmail { get; set; }
        public string CandidateEmail { get; set; }

        public static implicit operator Match(CVMatch cv)
        {
            return new Match()
            {
                MatchID = cv.MatchID,
                RecruiterEmail = cv.RecruiterEmail,
                CandidateEmail = cv.CandidateEmail
            };
        }

        public static implicit operator CVMatch(Match Mtch)
        {
            return new CVMatch()
            {
                MatchID = Mtch.MatchID,
                RecruiterEmail = Mtch.RecruiterEmail,
                CandidateEmail = Mtch.CandidateEmail
            };
        }
    }
    
}
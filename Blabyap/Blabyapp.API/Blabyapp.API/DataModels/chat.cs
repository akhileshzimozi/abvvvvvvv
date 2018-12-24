using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blabyap.Common.Model;

namespace Blabyapp.API.DataModels
{
    public class chat
    {
        [Key]
        public int chatID { get; set; }
        public string RecruiterEmail { get; set; }
        public string CandidateEmail { get; set; }
        public string ChatMsg { get; set; }
        public DateTime? CreatedDate { get; set; }

        public static implicit operator ChatInfo(chat cs)
        {
            return new ChatInfo()
            {
                chatID = cs.chatID.ToString(),
                RecruiterEmail = cs.RecruiterEmail,
                CandidateEmail = cs.CandidateEmail,
                ChatMsg=cs.ChatMsg,
                CreatedDate = cs.CreatedDate
            };
        }
        
        }
}
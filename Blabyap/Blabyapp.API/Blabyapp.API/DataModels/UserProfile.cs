using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Blabyapp.API.DataModels
{
    public class UserProfile
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }


        public UserProfile GetDemoUserProfile(int id)
        {
            return new UserProfile
            {
                UserID = 1,
                FullName = "rattana sharma",
                DisplayName = "Rattana",
                EmailAddress = "rattana.sharma@zimozi.co",
                Password = "P@ssw0rd",
                Birthday = new DateTime(1980, 06, 30),
                ImageUrl = "d.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                DeletedDate = DateTime.Now
            };
         }

        

    
}
    }
using Blabyap.Common.Model;
using Blabyapp.API.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Blabyapp.API.DataModels
{
    //public class ExpertiseInfo
    //{
    //    public string Code { get; set; }
    //    public string Translation { get; set; }
    //    public bool ISSelected { get; set; }
    //}

    public class MyCV
    {
        [Key]
        public int MyCVID { get; set; }
        public string cvID { get; set; }
        public string UserMail { get; set; }
        public string CurrentCompany { get; set; }
        public string JobTitle { get; set; }
        public string Industry { get; set; }
        public string Seniority { get; set; }
        public string Organization1 { get; set; }
        public string Organization2 { get; set; }
        public string Organization3 { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public string Startups { get; set; }
        public string AboutYou { get; set; }
        public string Expertise { get; set; }

        public static implicit operator CVData(MyCV mycv)
        {
          // List<CustomCaption> expertList  = new List<CustomCaption>();
            List<string> expertList = new List<string>();

            string phrase = mycv.Expertise;
            string[] words = phrase.Split(',');

            //foreach (string word in words)
            //{
            //    CustomCaption skill = <CustomCaption> JsonConvert.DeserializeObject(word);
            //    //string skill = JsonConvert.DeserializeObject<string>(word);
                
            //    expertList.Add(skill);
            //}
          

            return new CVData
            {
                cvID = mycv.cvID,
                CurrentCompany = mycv.CurrentCompany,
                JobTitle = mycv.JobTitle,
                Industry = mycv.Industry,
                Seniority = mycv.Seniority,
               MyExpertise = expertList,
                Organization1 = mycv.Organization1,
                Organization2 = mycv.Organization2,
                Organization3 = mycv.Organization3,
                Nationality = mycv.Nationality,
                Education = mycv.Education,
                Startups = mycv.Startups,
                AboutYou = mycv.AboutYou
            };
            
        }

        public static implicit operator MyCV(CVData cv)
        {
            string expertise = ""; 

            //foreach (CustomCaption exp in cv.MyExpertise)
            //{
            //    if(expertise != "")
            //        expertise += ",";

            //    expertise = expertise + JsonConvert.SerializeObject(exp.Translation);
            //}
            
            return new MyCV
            {
                cvID = cv.cvID,
                CurrentCompany = cv.CurrentCompany,
                JobTitle = cv.JobTitle,
                Industry = cv.Industry,
                Seniority = cv.Seniority,
                Expertise = expertise,
                Organization1 = cv.Organization1,
                Organization2 = cv.Organization2,
                Organization3 = cv.Organization3,
                Nationality = cv.Nationality,
                Education = cv.Education,
                Startups = cv.Startups,
                AboutYou = cv.AboutYou
            };
        }
        // public MyCV GetDemoUserCV(string id)
        //{
        //    return new MyCV
        //    {
        //        cvID = id,
        //        CurrentCompany = "Zimozi",
        //        JobTitle = "Trainee",
        //        UserMail = "rattana.sharma@zimozi.co",
        //        Industry = "Automobile",
        //        Seniority = "Beginner",
        //        Organization1 = "zimozi",
        //        Nationality = "Indian",
        //        Education = "MBA",
        //        Startups = "1",
        //        Expertise = new List<SkillSetCV>
        //        {
        //            new SkillSetCV { id = new Guid().ToString(),skillInfo ="Leadership", skillType="Expertise"},
        //           new SkillSetCV { id = new Guid().ToString(), skillInfo = "Marketing", skillType = "Expertise" }
        //        },
        //        AboutYou = "I believe in myself"
        //    };
        //}
    }

    
}
using System;
using Blabyap.ViewModels.Profile;

namespace Blabyap.ViewModels.Match
{
    public class MatchProfileItem
    {
        public string Type { get; set; }
        public PersonalViewModel PersonalViewModelItem { get; set; }
        public ExperienceViewModel ExperienceViewModelItem { get; set; }
        public MatchAboutYouViewModel MatchAboutYouViewModelItem { get; set; }
        public OtherPersonalInfoViewModel OtherPersonalInfoViewModelItem { get; set; }
    }
}

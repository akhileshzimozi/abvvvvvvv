using System;
namespace Blabyap.ViewModels.Profile
{
    public  class MyCVItem
    {
        public string Type { get; set; }

        public WorkViewModel WorkViewModelItem { get; set; }
        public OrganizationViewModel OrganizationViewModelItem { get; set; }
        public OtherViewModel OtherViewModelItem { get; set; }
        public ExpertiseViewModel ExpertiseViewModelItem { get; set; }
        public AboutYouViewModel AboutYouViewModelItem { get; set; }


    }
}

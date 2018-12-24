using System;
using Blabyap.ViewModels.Profile;

namespace Blabyap.ViewModels.Match
{
    public class DiscoveryItem
    {
        public string Type { get; set; }

        public SeniorityViewModel SeniorityViewModelItem { get; set; }
        public IndustriesViewModel IndustriesViewModelItem { get; set; }
        public ExpertiseViewModel ExpertiseViewModelItem { get; set; }
    }
}

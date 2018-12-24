using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels.Match
{
    public class IndustriesViewModel
    {
        public string Heading { get; set; }

        private List<FamilyData> industry;
        public List<FamilyData> Industry
        {
            get { return industry; }
            set { industry = value; }
        }
    }
}

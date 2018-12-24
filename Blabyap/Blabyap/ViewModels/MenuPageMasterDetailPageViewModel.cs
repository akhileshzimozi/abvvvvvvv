using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Prism.Mvvm;

namespace Blabyap.ViewModels
{
    public class MenuPageMasterDetailPageViewModel : BindableBase
    {
        public string Title
        {
            get;
            set;
        }
        protected MenuPageMasterDetailPageViewModel() : base()
        {
            Title = "Menu";
        }
    }
}

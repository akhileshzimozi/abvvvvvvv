using Blabyap.Models;
using Blabyap.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blabyap.ViewModels
{


    public class MainPageViewModel : BindableBase
    {
        private ObservableCollection<UserDetails> expertiseCollection;
        public ObservableCollection<UserDetails> ExpertiseCollection
        {
            get { return expertiseCollection; }
            set { expertiseCollection = value; }
        }


       // IRestServices restServices;
        public MainPageViewModel(IRestServices services)
        {
         //   restServices = services;

            expertiseCollection = new ObservableCollection<UserDetails>
            {
                new UserDetails() { Email = "Sales",FullName="Akhilesh" }
            };
            
        }


        //public Task<List<UserDetails>> GetTasksAsync()
        //{
        //    return restServices.GetUserDetails();

        //}
    }
}

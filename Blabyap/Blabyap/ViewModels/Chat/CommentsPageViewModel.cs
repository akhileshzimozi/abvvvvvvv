using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels
{
    public class CommentsPageViewModel : ViewModelBase
    {
        public ObservableCollection<CommentsModel> CommentsModelsList { get; set; }

        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand SendMsgCommand { get; set; }

        public CommentsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=>CanNavigate);
            SendMsgCommand = new DelegateCommand(SendMsgCommandAction).ObservesCanExecute(() => CanNavigate);

            CommentsModelsList = new ObservableCollection<CommentsModel>
            {
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic1",Name="Chris",RatingsImg="RatingStar",Date="17 August 2017",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",
                    },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic2", Name="Tracy",RatingsImg="RatingStar",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",Date="16 August 2017",
                    },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic3",Name="Alessandro",RatingsImg="RatingStar",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",Date="15 August 2017",
                },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic4",Name="Rhonda",RatingsImg="RatingStar",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",Date="14 August 2017",
                },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic2",Name="Sue Brention",RatingsImg="RatingStar",Date="13 August 2017",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",
               },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic1",Name="Steve",RatingsImg="RatingStar",Date="12 August 2017",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",
                },
                new CommentsModel(){Title="Title Here", ImageUrl="ChatPic3",Name="Tom",RatingsImg="RatingStar",Date="11 August 2017",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",
                },
                  new CommentsModel(){Title="Title Here", ImageUrl="ChatPic3",Name="Jerry",RatingsImg="RatingStar",Date="10 August 2017",
                    Description="Lorem ipsum dolor sit amet, et earm illud mazim pertinax. " +
                        "Ea tempor laoreet neglegentur duo. vis ex fastidii suscipiantur, earn et aeque.",
                }


            };
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }

        void SendMsgCommandAction()
        {
            CanNavigate = false;

            CanNavigate = true;

        }
    }

    public class CommentsModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string RatingsImg { get; set; }
    }
}

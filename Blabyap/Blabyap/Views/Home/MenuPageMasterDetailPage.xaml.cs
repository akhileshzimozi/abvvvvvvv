using Prism.Navigation;
using Xamarin.Forms;

namespace Blabyap.Views.Home
{
    public partial class MenuPageMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public MenuPageMasterDetailPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }
    }
}

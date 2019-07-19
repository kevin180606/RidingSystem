using FirstFloor.ModernUI.Windows.Navigation;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidingSystem.ViewModels
{
    public class SplashPageViewModel : ModernViewModelBase
    {
        public SplashPageViewModel(IModernNavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}

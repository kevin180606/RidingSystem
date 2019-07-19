using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;
using HCFaceApi;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace RidingSystem.ViewModels
{
    public class MainPageViewModel : ModernViewModelBase
    {
        public ICommand ExitCommand { get; }

        public MainPageViewModel(IModernNavigationService navigationService) : base(navigationService)
        {
            ExitCommand = new RelayCommand((obj) =>
              {
                  Application.Current.Shutdown();
              });
        }
    }
}

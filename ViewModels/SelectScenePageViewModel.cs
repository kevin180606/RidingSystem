using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using RidingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RidingSystem.ViewModels
{
    public class SelectScenePageViewModel : ModernViewModelBase
    {
        protected ProgramInfo _programInfo;
        public ProgramInfo ProgramInfo
        {
            get { return _programInfo; }
            set { Set(ref _programInfo, value); }
        }

        public ICommand SelectSceneCommand { get; }

        public SelectScenePageViewModel(IModernNavigationService navigationService) : base(navigationService)
        {
            SelectSceneCommand = new RelayCommand((obj) =>
              {
                  navigationService.NavigateTo("ScenePage", obj);
              });
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.NavigationType!= NavigationType.Back)
            {
                ProgramInfo = e.Parameter as ProgramInfo;
            }
        }
    }
}

using FirstFloor.ModernUI.Windows.Navigation;
using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using RidingSystem.Common.Controls;
using RidingSystem.Models;
using RidingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RidingSystem.Views
{
    /// <summary>
    /// SelectScenePage.xaml 的交互逻辑
    /// </summary>
    public partial class SelectScenePage : NavigationPage
    {
        public SelectScenePage()
        {
            InitializeComponent();
        }

        public override void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationType != NavigationType.Back)
            {
                ButtonContainer.Children.Clear();

                if (e.Parameter is ProgramInfo programInfo)
                {
                    foreach (var scene in programInfo.SceneInfoCollection)
                    {
                        var btn = new SoundImageButton() { Stretch = Stretch.Uniform };
                        if (scene.IsEnabled)
                        {
                            btn.NormalImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/SelectScenePage/Button_{scene.Scene}_Normal.png", UriKind.RelativeOrAbsolute));
                            btn.PressedImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/SelectScenePage/Button_{scene.Scene}_Pressed.png", UriKind.RelativeOrAbsolute));
                            btn.SetBinding(ButtonBase.CommandProperty, new Binding("SelectSceneCommand"));
                            btn.CommandParameter = new Tuple<string, string, int>(programInfo.Program, scene.Scene, scene.RoadTypeCount);
                        }
                        else
                        {
                            btn.IsEnabled = false;
                            btn.DisabledImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/SelectScenePage/Button_{scene.Scene}_Disabled.png", UriKind.RelativeOrAbsolute));
                        }

                        ButtonContainer.Children.Add(btn);
                    }
                }
            }
            else
            {
                MainWindow.Current.PlayBackgroundMusic();
            }
        }

        public override void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if(e.NavigationType== NavigationType.New)
            {
                MainWindow.Current.PauseBackgroundMusic();
            }
        }
    }
}

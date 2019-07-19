using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using RidingSystem.Common.Controls;
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
    /// ProgramPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProgramPage : NavigationPage
    {
        public ProgramPage()
        {
            InitializeComponent();

            foreach (var programInfo in ((ProgramPageViewModel)DataContext).ProgramInfoCollection)
            {
                var btn = new SoundImageButton() { Stretch = Stretch.Uniform };
                if (programInfo.IsEnabled)
                {
                    btn.NormalImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/ProgramPage/Button_{programInfo.Program}_Normal.png", UriKind.RelativeOrAbsolute));
                    btn.PressedImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/ProgramPage/Button_{programInfo.Program}_Pressed.png", UriKind.RelativeOrAbsolute));
                    btn.SetBinding(ButtonBase.CommandProperty, new Binding("SelectProgramCommand"));
                    btn.CommandParameter = programInfo;
                }
                else
                {
                    btn.IsEnabled = false;
                    btn.DisabledImage = new BitmapImage(new Uri($"/RidingSystem.Assets;component/Assets/Image/ProgramPage/Button_{programInfo.Program}_Disabled.png", UriKind.RelativeOrAbsolute));
                }

                ButtonContainer.Children.Add(btn);
            }
        }
    }
}

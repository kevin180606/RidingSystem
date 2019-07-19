using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RidingSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Current { get; set; }

        public MainWindow()
        {
            Current = this;

            InitializeComponent();

//            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new System.EventHandler(displaySettingsChanged);


//#if DEBUG
//            Width = 1080;
//            Height = 1920;
//            WindowState = WindowState.Normal;
//#endif
        }

        private void displaySettingsChanged(object sender, EventArgs e)
        {
            if (SystemParameters.PrimaryScreenHeight > SystemParameters.PrimaryScreenWidth)
            {
                Width = SystemParameters.PrimaryScreenWidth;

                Height = SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                Width = SystemParameters.PrimaryScreenHeight;

                Height = SystemParameters.PrimaryScreenWidth;
            }
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement.Position = TimeSpan.Zero;
            MediaElement.Play();
        }

        public void PlayBackgroundMusic()
        {
            MediaElement.Play();
        }

        public void PauseBackgroundMusic()
        {
            MediaElement.Pause();
        }
    }
}

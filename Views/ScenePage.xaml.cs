using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using HC.Device.Bike.Codec;
using LinnerToolkit.Core.IO;
using LinnerToolkit.Core.IO.Serial;
using LinnerToolkit.Desktop.IO;
using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using PsySportsPark.Common.Desktop.Scenes;
using PsySportsPark.Common.Scenes;
using RidingSystem.Helpers;
using RidingSystem.Scenes.Common.Scenes;
using RidingSystem.ViewModels;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace RidingSystem.Views
{
    /// <summary>
    /// ScenePage.xaml 的交互逻辑
    /// </summary>
    public partial class ScenePage : NavigationPage
    {
        protected RidingSceneBase _scene;
        
        protected bool _simulationData = true;

        public ScenePage()
        {
            InitializeComponent();
        }

        public override void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Tuple<string, string, int> parameter)
            {
                // 动态生成场景
                _scene = SceneHelper.CreateScene(parameter.Item1, parameter.Item2, parameter.Item3);
            }
            if (_scene == null)
            {
                ModernDialog.ShowMessage("创建场景失败,请确认配置正确.", "错误", MessageBoxButton.OK);
                ((ScenePageViewModel)DataContext).GoBackCommand.Execute(null);
                return;
            }

            Root.Children.Insert(0, _scene);
            _scene.HorizontalAlignment = HorizontalAlignment.Stretch;
            _scene.VerticalAlignment = VerticalAlignment.Stretch;
            _scene.Exit += _scene_Exit;

            // 将Root的DataContext设置为游戏场景
            Root.DataContext = _scene;

            // 准备开始游戏
            _scene.Show();

            if (_simulationData)
            {
                MouseRightButtonDown += ScenePage_MouseRightButtonDown;
            }

            if (DataProducerHelper.Current.DataProducer != null)
            {
                DataProducerHelper.Current.DataProducer.DataReceived += DataProducer_DataReceived;
              //  DataProducerHelper.Current.DataProducer.Open();
            }
        }

        private void ScenePage_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_scene.Status == SceneStatus.Playing)
                _scene.AddRound(new DecodeData { Interval = 1000, IsPositive = true });
            //TextTest.Text += TextTest.Text.Length % 2 == 0 ? "0" : "1";
        }

        private void DataProducer_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data is DecodeData decodeData && _scene?.Status == SceneStatus.Playing)
                _scene.AddRound(decodeData);
            //Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    TextTest.Text += TextTest.Text.Length % 2 == 0 ? "0" : "1";
            //}));
        }

        private void _scene_Exit(object sender, EventArgs e)
        {
            ((ScenePageViewModel)DataContext).GoBackCommand.Execute(null);
        }

        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // 退出场景页面时停止并移除游戏场景
            if (_scene != null)
            {
                if (_scene.Status == PsySportsPark.Common.Scenes.SceneStatus.Playing)
                    _scene.Stop();

                Root.Children.Remove(_scene);
                _scene = null;
            }

            if (DataProducerHelper.Current.DataProducer != null)
            {
                DataProducerHelper.Current.DataProducer.DataReceived -= DataProducer_DataReceived;
               // DataProducerHelper.Current.DataProducer.Close();
            }

            MouseRightButtonDown -= ScenePage_MouseRightButtonDown;
        }
    }
}

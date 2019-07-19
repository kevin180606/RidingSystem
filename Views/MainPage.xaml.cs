using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using HCFaceApi;
using LinnerToolkit.Desktop.ModernUI.Windows.Controls;
using RidingSystem.Helpers;
using RidingSystem.ViewModels;
using System;
using System.Configuration;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Threading;

namespace RidingSystem.Views
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : NavigationPage
    {
        private FaceApi faceApi;
        private DispatcherTimer dispatcherTimer;
        private SpeechSynthesizer reader;                         //ttf语音阅读器

        protected bool _faceLogin;

        public MainPage()
        {
            InitializeComponent();

            _faceLogin = bool.Parse(ConfigurationManager.AppSettings["FaceLogin"]);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {

        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.NavigationType== NavigationType.New)
            {
                MainWindow.Current.PlayBackgroundMusic();
            }
        }

        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if(e.NavigationType== NavigationType.New)
            {
                if (_faceLogin)
                {
                    ImagePhoto.Visibility = Visibility.Collapsed;
                    btnSingle.IsEnabled = true;
                    faceApi.GetImageEvent -= FaceApi_GetImageEvent;
                    faceApi.FaceRecogFinishedEvent -= FaceApi_FaceRecogFinishedEvent;
                    faceApi.Stop();
                    dispatcherTimer.Stop();
                }
            }
        }

        private void SoundImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_faceLogin)
            {
                btnSingle.IsEnabled = false;
                
                try
                {
                    if (faceApi == null)
                    {
                        dispatcherTimer = new DispatcherTimer();
                        dispatcherTimer.Tick += DispatcherTimer_Tick;
                        dispatcherTimer.Interval = TimeSpan.FromMinutes(1);

                        faceApi = new FaceApi();
                        ConfigHelper.GetSysConfig();
                        DBHelper.SetDBSource();
                        faceApi.Init(ConfigHelper.CameraIndex, ConfigHelper.DetectStyle, ConfigHelper.RecogStyle, ConfigHelper.Similarity, ConfigHelper.Diversity);

                        reader = new SpeechSynthesizer();
                        reader.SelectVoice(ConfigHelper.Voice);
                    }

                    faceApi.LstFaceInformation = DBHelper.GetLstFaceInformation();
                    if (faceApi.LstFaceInformation == null)
                    {
                        ModernDialog.ShowMessage("访问数据库异常！", "提示", MessageBoxButton.OK);
                        return;
                    }

                    faceApi.GetImageEvent += FaceApi_GetImageEvent;
                    faceApi.FaceRecogFinishedEvent += FaceApi_FaceRecogFinishedEvent;
                    faceApi.Start();
                    dispatcherTimer.Start();

                    ImagePhoto.Visibility = Visibility.Visible;
                }
                catch (Exception ee)
                {
                    ModernDialog.ShowMessage("人脸识别始化失败：" + ee.Message, "提示", MessageBoxButton.OK);

                    if (faceApi != null)
                        faceApi.Dispose();
                    faceApi = null;
                    btnSingle.IsEnabled = true;
                    ImagePhoto.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                ((MainPageViewModel)DataContext).NavigateCommand.Execute("ProgramPage");
            }
        }

        private void FaceApi_FaceRecogFinishedEvent(object sender, FaceRecogEventArgs e)
        {
            faceApi.GetImageEvent -= FaceApi_GetImageEvent;
            faceApi.FaceRecogFinishedEvent -= FaceApi_FaceRecogFinishedEvent;
            PalyVoice($"{e.FaceRecogResult.FaceInformation.Name},欢迎使用骑行反馈训练系统！");
            Settings.Instance.UserName = e.FaceRecogResult.FaceInformation.Name;
            Dispatcher.Invoke(new Action(() =>
            {
                ((MainPageViewModel)DataContext).NavigateCommand.Execute("ProgramPage");
            }));
        }

        private void FaceApi_GetImageEvent(object sender, GetImageEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => { ImagePhoto.Source = BitmapSourceConvert.ToBitmapSource(e.colorFrame); }));
        }

        /// <summary>
        /// 播放语音
        /// </summary>
        /// <param name="voiceText"></param>
        private void PalyVoice(string voiceText)
        {
            reader?.SpeakAsync(voiceText);
        }
    }
}

using LinnerToolkit.Desktop.ModernUI.Windows;
using RidingSystem.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RidingSystem
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : ModernApplication
    {
        public App()
        {
            // 以指定uri作为程序入口
            RunApp("MainWindow/MainRegion/MainPage");
        }

        protected override void RegisterTypes()
        {
            // 注册窗体和页面
            RegisterForNavigation<MainWindow>();
            RegisterForNavigation<SplashPage>();
            RegisterForNavigation<MainPage>();
            RegisterForNavigation<ProgramPage>();
            RegisterForNavigation<SelectScenePage>();
            RegisterForNavigation<ScenePage>();
        }
    }
}

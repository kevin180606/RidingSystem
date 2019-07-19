using FirstFloor.ModernUI.Presentation;
using LinnerToolkit.Desktop.ModernUI.Mvvm;
using LinnerToolkit.Desktop.ModernUI.Navigation;
using Newtonsoft.Json;
using RidingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RidingSystem.ViewModels
{
    public class ProgramPageViewModel : ModernViewModelBase
    {
        public List<ProgramInfo> ProgramInfoCollection { get; }

        public ICommand SelectProgramCommand { get; }

        public ProgramPageViewModel(IModernNavigationService navigationService) : base(navigationService)
        {
            SelectProgramCommand = new RelayCommand((obj) =>
              {
                  navigationService.NavigateTo("SelectScenePage", obj);
              });

            string fileName = "ProgramInfo.config";
            if (File.Exists(fileName))
            {
                ProgramInfoCollection = JsonConvert.DeserializeObject<List<ProgramInfo>>(File.ReadAllText(fileName));
            }
            else
            {
                ProgramInfoCollection = new List<ProgramInfo>
                {
                new ProgramInfo
                {
                    Program = "YDXX",
                    IsEnabled=true,
                    SceneInfoCollection =new List<SceneInfo>
                    {
                       new SceneInfo{ Scene="XY",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="GL",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="SD",IsEnabled=false},
                       new SceneInfo{Scene="SL",IsEnabled=false},
                       new SceneInfo{Scene="SM",IsEnabled=false},
                       new SceneInfo{Scene="CY",IsEnabled=false},
                    }
                },
                new ProgramInfo
                {
                    Program = "JLYG",
                    IsEnabled=true,
                    SceneInfoCollection =new List<SceneInfo>
                    {
                       new SceneInfo{ Scene="XY",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="GL",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="SD",IsEnabled=false},
                       new SceneInfo{Scene="SL",IsEnabled=false},
                       new SceneInfo{Scene="SM",IsEnabled=false},
                       new SceneInfo{Scene="CY",IsEnabled=false},
                    }
                },new ProgramInfo
                {
                    Program = "SJYG",
                    IsEnabled=true,
                    SceneInfoCollection =new List<SceneInfo>
                    {
                       new SceneInfo{ Scene="XY",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="GL",IsEnabled=true,RoadTypeCount=4,},
                       new SceneInfo{Scene="SD",IsEnabled=false},
                       new SceneInfo{Scene="SL",IsEnabled=false},
                       new SceneInfo{Scene="SM",IsEnabled=false},
                       new SceneInfo{Scene="CY",IsEnabled=false},
                    }
                },new ProgramInfo
                {
                    Program = "SXZZ",
                    IsEnabled=false,
                },new ProgramInfo
                {
                    Program = "ZADB",
                    IsEnabled=true,

                    SceneInfoCollection = new List<SceneInfo>
                    {
                       new SceneInfo{ Scene="CHDD",IsEnabled=true,RoadTypeCount=2,},
                       new SceneInfo{Scene="CLTX",IsEnabled=true,RoadTypeCount=2,},
                    }
                },
                };

                File.WriteAllText(fileName, JsonConvert.SerializeObject(ProgramInfoCollection));
            }
        }
    }
}

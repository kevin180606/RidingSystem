using PsySportsPark.Common.Desktop.Scenes;
using RidingSystem.Scenes.Common.Scenes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RidingSystem.Helpers
{
    /// <summary>
    /// 场景帮助类
    /// </summary>
    public static class SceneHelper
    {
        /// <summary>
        /// 创建场景
        /// </summary>
        /// <param name="programType">方案类型</param>
        /// <param name="sceneType">场景类型</param>
        /// <param name="roadTypeCount">路中间段的数量</param>
        /// <returns></returns>
        public static RidingSceneBase CreateScene(string programType,string sceneType,int roadTypeCount)
        {
            try
            {
                string[] split = programType.Split('_');
                if (split.Length <= 0)
                    return null;

                // 方案类型中如果带有等级标识，则将该标识分离出来
                string type = split[0];
                int level = 0;
                if (split.Length > 1)
                    int.TryParse(split[1], out level);
                
                string assembly = $"RidingSystem.Scenes.{programType}";
                string file = Path.Combine(Directory.GetCurrentDirectory(), $"{assembly}.dll");
                // 若场景所在库不存在，返回null
                if (!File.Exists(file))
                    return null;

                // 利用反射生成场景
                string sceneName = $"{assembly}.{programType}Scene";
                var scene = Assembly.LoadFile(file).CreateInstance(sceneName, true, BindingFlags.Default, null, new object[] {programType, sceneType,roadTypeCount }, null, null) as RidingSceneBase;
                // 重置首个等级
                scene.ResetFirstLevel(level);
                return scene;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidingSystem.Models
{
    /// <summary>
    /// 场景信息
    /// </summary>
    public class SceneInfo
    {
        /// <summary>
        /// 场景名称
        /// </summary>
        public string Scene { get; set; }

        /// <summary>
        /// 路的类型数量
        /// </summary>
        public int RoadTypeCount { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        public SceneInfo()
        {
            RoadTypeCount = 1;
            IsEnabled = false;
        }
    }
}

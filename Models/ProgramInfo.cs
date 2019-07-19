using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidingSystem.Models
{
    /// <summary>
    /// 方案信息
    /// </summary>
    public class ProgramInfo
    {
        /// <summary>
        /// 方案
        /// </summary>
        public string Program { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        public List<SceneInfo> SceneInfoCollection { get; set; }

        public ProgramInfo()
        {
            SceneInfoCollection = new List<SceneInfo>();
        }
    }
}

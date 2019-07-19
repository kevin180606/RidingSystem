using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidingSystem.Helpers
{
    public class Settings
    {
        protected static Settings _instance;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Settings();
                return _instance;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        protected Settings()
        {

        }
    }
}

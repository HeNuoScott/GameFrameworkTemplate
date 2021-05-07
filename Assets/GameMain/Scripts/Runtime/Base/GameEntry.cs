//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityGameFramework.Runtime;
using UnityEngine;

namespace Sirius.Runtime
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static GameEntry Instance 
        {
            get;
            private set;
        }

        private void Start()
        {
            Log.Info("<color=green>GameEntry Start</color>");
            Instance = this;
            InitBuiltinComponents();
            InitCustomComponents();
        }

        public static void ApplicationQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }
    }
}

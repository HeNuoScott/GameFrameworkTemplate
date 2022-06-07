//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace GameMain
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static Transform Customs
        {
            get;
            private set;
        }

        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
        }

        /// <summary>
        /// 添加组件
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架组件类型。</typeparam>
        /// <returns>要获取的游戏框架组件。</returns>
        public static T AddComponent<T>() where T : Component
        {
            GameObject gameObject = new GameObject(typeof(T).Name);
            gameObject.transform.SetParent(Customs);
            return gameObject.AddComponent<T>();
        }

        public Transform GetTransform(string tranName)
        {
            Transform[] allTransform = this.transform.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < allTransform.Length; i++)
            {
                Transform item = allTransform[i];
                if (item.name == tranName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}

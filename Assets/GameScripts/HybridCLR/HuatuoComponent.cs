using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

namespace GameFrame.Main
{

    public class HuatuoComponent : GameFrameworkComponent
    {
        private Assembly gameAss;

        public void LoadGameDll(Dictionary<string, byte[]> loadedHotifx)
        {
#if UNITY_EDITOR
            gameAss = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "GameFrame.Hotfix");
#else
            foreach (var assembly in loadedHotifx)
            {
                Assembly gameAssembly = Assembly.Load(assembly.Value);
                if (assembly.Key == "GameFrame.Hotfix.dll") gameAss = gameAssembly;
            }
#endif
            if (gameAss == null)
            {
                Debug.LogError("dll未加载");
                return;
            }

            StartHotfixLogic();
        }

        private void StartHotfixLogic()
        {
            var hotfixEntry = gameAss.GetType("GameFrame.Hotfix.GameHotfixEntry");
            var startMethod = hotfixEntry.GetMethod("Awake");
            startMethod?.Invoke(null, null);
        }
    }
}
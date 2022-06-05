using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine;
using System.Linq;
using System;

namespace Sirius
{
    public class PrefabData
    {
        public string prefabName;
    }
    public class HuatuoComponent : GameFrameworkComponent
    {
        private System.Reflection.Assembly gameAss;
        private Type gameRun = null;

        public void LoadGameDll(Dictionary<string, byte[]> loadedHotifx)
        {
#if !UNITY_EDITOR
            foreach (var assembly in loadedHotifx)
            {
                System.Reflection.Assembly gameAssembly = System.Reflection.Assembly.Load(assembly.Value);
                if (assembly.Key == "HotFixLogic.dll") gameAss = gameAssembly;
            }
#else
            gameAss = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "HotFixLogic");
#endif

            if (gameAss == null)
            {
                UnityEngine.Debug.LogError("dll未加载");
                return;
            }
            gameRun = gameAss.GetType("GameRun");
        }

        public void RunMain()
        {
            var mainMethod = gameRun.GetMethod("Run");
            mainMethod.Invoke(null, null);
        }

        public void Init()
        {
            var mainMethod = gameRun.GetMethod("Init");
            mainMethod.Invoke(null, null);
        }


        public void LoadPreFab(string perfabName)
        {
            PrefabData prefabData = new PrefabData() { prefabName = perfabName };
            string assetName = AssetUtility.GetPerfabsAsset(perfabName);
            GameEntry.Resource.LoadAsset(assetName, new LoadAssetCallbacks(OnLoadPerfabAssetSucceed, OnLoadPerfabAssetFailured), prefabData);
        }

        private void OnLoadPerfabAssetSucceed(string assetName, object asset, float duration, object userData)
        {
            GameObject formPrefab = asset as GameObject;
            GameObject instance = GameObject.Instantiate(formPrefab, GameEntry.Customs);
            formPrefab = null;
        }

        private void OnLoadPerfabAssetFailured(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("Can not load {0} from '{1}' with error message '{2}'.", assetName, "PerfabsAsset", errorMessage);
        }
    }
}
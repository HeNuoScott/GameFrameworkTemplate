using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Resource;
using GameEntry = GameMain.GameEntry;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework;
using UnityEngine;

namespace GameHotFix
{
    public class HotFixEntry 
    {
        public const string HPBarName = "HP Bar";

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }

        public static void Start()
        {
            // 重置流程组件，初始化热更新流程。
            GameEntry.Fsm.DestroyFsm<IProcedureManager>();
            var procedureManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            ProcedureBase[] procedures =
            {
                new ProcedureChangeScene(),
                new ProcedureMain(),
                new ProcedureMenu(),
                new ProcedurePreload(),
            };
            procedureManager.Initialize(GameFrameworkEntry.GetModule<IFsmManager>(), procedures);
            procedureManager.StartProcedure<ProcedurePreload>();

            LoadPreFab(HPBarName);

            Debug.Log("HotFixEntry Started !!");
        }

        public static void LoadPreFab(string perfabName)
        {
            string assetName = AssetUtility.GetPerfabsAsset(perfabName);
            GameEntry.Resource.LoadAsset(assetName, new LoadAssetCallbacks(OnLoadPerfabAssetSucceed, OnLoadPerfabAssetFailured));
        }

        private static void OnLoadPerfabAssetSucceed(string assetName, object asset, float duration, object userData)
        {
            GameObject formPrefab = asset as GameObject;
            GameObject instance = GameObject.Instantiate(formPrefab, GameEntry.Customs);
            HPBar = instance.GetComponent<HPBarComponent>();
            formPrefab = null;
        }

        private static void OnLoadPerfabAssetFailured(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("Can not load {0} from '{1}' with error message '{2}'.", assetName, "PerfabsAsset", errorMessage);
        }
    }
}
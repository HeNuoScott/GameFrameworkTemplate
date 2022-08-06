// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/2   18:20:0
// -----------------------------------------------
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameEntry = GameFrame.Main.GameEntry;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Resource;
using GameFrame.Main;
using UnityEngine;
using System;

namespace GameFrame.Hotfix
{
    public class PrefabData
    {
        public string prefabName;
    }

    public class ProcedureHuatuoLaunch : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        private bool isLoadSuccess = false;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_LoadedFlag.Clear();
            isLoadSuccess = false;

#if !UNITY_EDITOR
            LoadMetadataForAOTAssembly();
#endif
            // 测试补充元数据后使用 AOT泛型
            TestAOTGeneric();

            LoadPreFab("HP Bar");
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (isLoadSuccess) return;

            foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
            {
                if (!loadedFlag.Value)
                {
                    return;
                }
            }

            isLoadSuccess = true;
            GameHotfixEntry.Start();
            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        /// <summary>
        /// 测试 aot泛型
        /// </summary>
        private void TestAOTGeneric()
        {
            var arr = new List<MyValue>();
            arr.Add(new MyValue() { x = 1, y = 10, s = "abc" });
            Debug.Log("AOT泛型补充元数据机制测试正常");
        }

        private void LoadPreFab(string perfabName)
        {
            m_LoadedFlag.Add(perfabName, false);
            PrefabData prefabData = new PrefabData() { prefabName = perfabName };
            string assetName = AssetUtility.GetPerfabsAsset(perfabName);
            GameEntry.Resource.LoadAsset(assetName, new LoadAssetCallbacks(OnLoadPerfabAssetSucceed, OnLoadPerfabAssetFailured), prefabData);
        }

        private unsafe void LoadAOTDll(string aotName)
        {
            m_LoadedFlag.Add(aotName, false);
            PrefabData prefabData = new PrefabData() { prefabName = aotName };
            string assetName = AssetUtility.GetAOTDllAsset(aotName);
            GameEntry.Resource.LoadAsset(assetName, new LoadAssetCallbacks(OnLoadAOTDllSuccess,OnLoadPerfabAssetFailured), prefabData);
        }

        private void OnLoadPerfabAssetSucceed(string assetName, object asset, float duration, object userData)
        {
            GameObject formPrefab = asset as GameObject;
            GameObject.Instantiate(formPrefab, GameEntry.Customs);
            PrefabData prefabData = userData as PrefabData;
            m_LoadedFlag[prefabData.prefabName] = true;
            formPrefab = null;
        }

        private unsafe void OnLoadAOTDllSuccess(string assetName, object asset, float duration, object userData)
        {
            PrefabData prefabData = userData as PrefabData;
            m_LoadedFlag[prefabData.prefabName] = true;

            byte[] dllBytes = ((TextAsset)asset).bytes;
            fixed (byte* ptr = dllBytes)
            {
                // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                int err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly((IntPtr)ptr, dllBytes.Length);
                Log.Info(string.Format("LoadMetadataForAOTAssembly:{0}. ret:{1}", assetName, err));
            }
        }

        private void OnLoadPerfabAssetFailured(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("Can not load {0} from '{1}' with error message '{2}'.", assetName, "PerfabsAsset", errorMessage);
        }

        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        public unsafe void LoadMetadataForAOTAssembly()
        {
            // 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用原始dll。
            // 我们在Huatuo_BuildProcessor_xxx里添加了处理代码，这些裁剪后的dll在打包时自动被复制到 {项目目录}/HuatuoData/AssembliesPostIl2CppStrip/{Target} 目录。

            /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
            /// 
            //List<string> aotDllList = new List<string>
            //{
            //    "mscorlib.dll",
            //    "System.dll",
            //    "System.Core.dll", // 如果使用了Linq，需要这个
            //    // "Newtonsoft.Json.dll",
            //    // "protobuf-net.dll",
            //    // "Google.Protobuf.dll",
            //    // "MongoDB.Bson.dll",
            //    // "DOTween.Modules.dll",
            //    // "UniTask.dll",
            //};

            foreach (var aotDllName in GameHotfixEntry.AOTMetaDlls)
            {
                LoadAOTDll(aotDllName);
            }
        }
    }
}
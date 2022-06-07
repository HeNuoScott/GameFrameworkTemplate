//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Resource;
using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

namespace GameMain
{
    public class ProcedureCodeInit : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        private Dictionary<string, byte[]> m_loadedHotifx = new Dictionary<string, byte[]>();

        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_LoadedFlag.Clear();
            m_loadedHotifx.Clear();

            //加载热更资源
            LoadHotfix("HotFixLogic.dll");
            LoadHotfix("HotFixMono.dll");
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
            {
                if (!loadedFlag.Value)
                {
                    return;
                }
            }

            LoadGameDll(m_loadedHotifx);
        }

        //加载热更新脚本
        private void LoadHotfix(string hotfixName)
        {
            m_LoadedFlag.Add(hotfixName, false);
            GameEntry.Resource.LoadAsset(GetHotDllAsset(hotfixName),
                new LoadAssetCallbacks(
                    //加载成功的回调
                    (assetName, asset, duration, userData) =>
                    {
                        m_LoadedFlag[hotfixName] = true;
                        m_loadedHotifx.Add(hotfixName, ((TextAsset)asset).bytes);
                        Log.Info("Load hotfix '{0}' OK.", hotfixName);
                    },
                    //加载失败的回调
                    (assetName, status, errorMessage, userData) =>
                    {
                        Log.Error("Can not load hotfix '{0}' from '{1}' with error message '{2}'.", hotfixName, assetName, errorMessage);
                    }
            ));
        }

        private string GetHotDllAsset(string assetName)
        {
            return String.Format("Assets/GameMain/HotFixDll/{0}.bytes", assetName);
        }

        private void LoadGameDll(Dictionary<string, byte[]> loadedHotifx)
        {
            Assembly gameAss = null;
#if !UNITY_EDITOR
            foreach (var assembly in loadedHotifx)
            {
                System.Reflection.Assembly gameAssembly = System.Reflection.Assembly.Load(assembly.Value);
                if (assembly.Key == "HotFixLogic.dll") gameAss = gameAssembly;
            }
#else
            if (GameEntry.Base.EditorResourceMode)
            {
                gameAss = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "HotFixLogic");
            }
            else
            {
                foreach (var assembly in loadedHotifx)
                {
                    Assembly gameAssembly = Assembly.Load(assembly.Value);
                    if (assembly.Key == "HotFixLogic.dll") gameAss = gameAssembly;
                }
            }
#endif

            if (gameAss == null)
            {
                Debug.LogError("dll未加载");
                return;
            }

            var hotfixEntry = gameAss.GetType("HotFix");
            var entryMethod = hotfixEntry.GetMethod("GameEntry");
            entryMethod.Invoke(null, null);
        }
    }
}
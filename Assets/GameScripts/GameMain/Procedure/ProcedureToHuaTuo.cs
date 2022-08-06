//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameEntry = GameFrame.Main.GameEntry;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Resource;

using GameFramework.Event;
using UnityEngine;

namespace GameFrame.Main
{
    public class ProcedureToHuaTuo : ProcedureBase
    {
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        //热更新程序
        private Dictionary<string, byte[]> m_loadedHotifx = new Dictionary<string, byte[]>();

        private bool isLoadSuccess = false;
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

            Log.Info("<color=green> 进入huatuo热更逻辑 </color>");

            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            m_LoadedFlag.Clear();
            isLoadSuccess = false;

            //加载热更资源
            LoadHotfix("GameFrame.Hotfix.dll");
            LoadHotfix("GameFrame.ThirdParty.dll");
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            base.OnLeave(procedureOwner, isShutdown);
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

            EntryHuatuoLogic();
        }

        //加载热更新脚本
        private void LoadHotfix(string hotfixName)
        {
            m_LoadedFlag.Add(hotfixName, false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetHotDllAsset(hotfixName), Constant.AssetPriority.DataTableAsset,
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

        // 进入huatuo热更逻辑
        private void EntryHuatuoLogic()
        {
            isLoadSuccess = true;
            GameEntry.Huatuo.LoadGameDll(m_loadedHotifx);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[ne.DataTableAssetName] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }
    }
}

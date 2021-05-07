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
using Sirius.Runtime;
using GameFramework.Event;
using System.Reflection;
using GameFramework;
using UnityEngine;
using System;

namespace Sirius.Runtime
{
    public class ProcedurePreload : ProcedureBase
    {
        public static readonly string[] DataTableNames = new string[]
        {
            "Aircraft",
            "Armor",
            "Asteroid",
            "Entity",
            "Music",
            "Scene",
            "Sound",
            "Thruster",
            "UIForm",
            "UISound",
            "Weapon",
        };

        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        //热更新程序
        private Dictionary<string, byte[]> m_loadedHotifx = new Dictionary<string, byte[]>();

        private bool m_IsFinish = false;  //是否加载完的标志位
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

            GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

            m_LoadedFlag.Clear();
            m_IsFinish = false;
            PreloadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            //完成全部预加载，并且切换到热更新模式后，等待热更新模式的流程开启
            if (m_IsFinish) return;

            foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
            {
                if (!loadedFlag.Value)
                {
                    return;
                }
            }

            //进入热更新
            m_IsFinish = true;

            string hotfixDllName = Utility.Text.Format("Hotfix.{0}", AssetUtility.HotfixDllName);
            string hotfixPdbName = Utility.Text.Format("Hotfix.{0}", AssetUtility.HotfixPdbName);
            byte[] dllBytes, pdbBytes;
            m_loadedHotifx.TryGetValue(hotfixDllName, out dllBytes);
            m_loadedHotifx.TryGetValue(hotfixPdbName, out pdbBytes);
            GameEntry.Hotfix.LoadHotfixAssembly(dllBytes, pdbBytes); //启动热更新

            //设置新的流程数据，开启新的流程
            //HotProcedure[] newProcedures = GetAllProcedures().ToArray();
            //GameEntry.Procedure.StartInitProcedure(newProcedures, newProcedures[0]);
            ChangeState<HotProcedureEntry>(procedureOwner);   //这里不能这样操作
            GC.Collect();
        }

        //获取所有的流程
        private static List<HotProcedure> GetAllProcedures()
        {
            List<HotProcedure> results = new List<HotProcedure>();
            string enterProcedureTypeName = typeof(HotProcedureEntry).FullName;  //流程入口

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();  //所有的类型
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                //抽象类或者非类
                if (type.IsAbstract || !type.IsClass) continue;

                if (typeof(HotProcedure).IsAssignableFrom(type))
                {
                    object obj = Activator.CreateInstance(type);
                    //流程入口放入第一个
                    if (enterProcedureTypeName == type.FullName) results.Insert(0, obj as HotProcedure);
                    else results.Add(obj as HotProcedure);
                }
            }

            return results;
        }

        private void PreloadResources()
        {
            // Preload configs
            LoadConfig("DefaultConfig");

            // Preload data tables
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }

            // Preload dictionaries
            LoadDictionary("Default");

            // Preload fonts
            LoadFont("MainFont");

            //加载热更资源
            LoadHotfix(AssetUtility.HotfixDllName);
            LoadHotfix(AssetUtility.HotfixPdbName);
        }

        private void LoadConfig(string configName)
        {
            string configAssetName = AssetUtility.GetConfigAsset(configName, false);
            m_LoadedFlag.Add(configAssetName, false);
            GameEntry.Config.ReadData(configAssetName, this);
        }

        private void LoadDataTable(string dataTableName)
        {
            string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
            m_LoadedFlag.Add(dataTableAssetName, false);
            GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
        }

        private void LoadDictionary(string dictionaryName)
        {
            string dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
            m_LoadedFlag.Add(dictionaryAssetName, false);
            GameEntry.Localization.ReadData(dictionaryAssetName, this);
        }

        private void LoadFont(string fontName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Font.{0}", fontName), false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Font.{0}", fontName)] = true;
                    UGUIForm.SetMainFont((Font)asset);
                    Log.Info("Load font '{0}' OK.", fontName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
                }));
        }
        //加载热更新脚本
        private void LoadHotfix(string hotfixName)
        {
            string name = Utility.Text.Format("Hotfix.{0}", hotfixName);
            m_LoadedFlag.Add(name, false);
            GameEntry.Resource.LoadAsset(AssetUtility.GetHotfixAsset(hotfixName), Constant.AssetPriority.FontAsset,
                new LoadAssetCallbacks(
                    //加载成功的回调
                    (assetName, asset, duration, userData) =>
                    {
                        m_LoadedFlag[name] = true;
                        m_loadedHotifx.Add(name, ((TextAsset)asset).bytes);
                        Log.Info("Load hotfix '{0}' OK.", hotfixName);
                    },
                    //加载失败的回调
                    (assetName, status, errorMessage, userData) =>
                    {
                        Log.Error("Can not load hotfix '{0}' from '{1}' with error message '{2}'.", hotfixName, assetName, errorMessage);
                    }
            ));

        }
        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[ne.ConfigAssetName] = true;
            Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
        }

        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName, ne.ConfigAssetName, ne.ErrorMessage);
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

        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[ne.DictionaryAssetName] = true;
            Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
        }

        private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
        {
            LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName, ne.DictionaryAssetName, ne.ErrorMessage);
        }
    }
}

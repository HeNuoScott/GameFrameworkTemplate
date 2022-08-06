// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/3   14:57:35
// -----------------------------------------------
using GameEntry = GameFrame.Main.GameEntry;
using ProcedureBase = GameFrame.Main.ProcedureBase;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFrame.Main;
using GameFramework;

namespace GameFrame.Hotfix
{

    public static class GameHotfixEntry
    {
        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// 
        /// 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用原始dll。
        /// 我们在Huatuo_BuildProcessor_xxx里添加了处理代码，这些裁剪后的dll在打包时自动被复制到 {项目目录}/HuatuoData/AssembliesPostIl2CppStrip/{Target} 目录。
        /// 
        /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        /// </summary>
        public static List<string> AOTMetaDlls = new List<string>
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll", // 如果使用了Linq，需要这个
            // "Newtonsoft.Json.dll",
            // "protobuf-net.dll",
            // "Google.Protobuf.dll",
            // "MongoDB.Bson.dll",
            // "DOTween.Modules.dll",
            // "UniTask.dll",
        };

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }

        public static void Awake()
        {
            Log.Info("<color=green> GameHotfixEntry.Awake </color>");
            // 重置流程组件，初始化热更新流程。
            GameEntry.Fsm.DestroyFsm<IProcedureManager>();
            var procedureManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            ProcedureBase[] procedures =
            {
                new ProcedureChangeScene(),
                new ProcedureMain(),
                new ProcedureMenu(),
                new ProcedurePreload(),
                new ProcedureHuatuoLaunch(),
            };
            procedureManager.Initialize(GameFrameworkEntry.GetModule<IFsmManager>(), procedures);
            procedureManager.StartProcedure<ProcedurePreload>();
        }

        public static void Start()
        {
            Log.Info("<color=green> GameHotfixEntry.Start </color>");
            HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
        }
    }
}

// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:44:26
// -----------------------------------------------
using UnityGameFramework.Editor;
using UnityEditor;

namespace Sirius.Editor
{
    public sealed class ToolsMenu
    {
        //生成数据表
        [MenuItem("Tools/DataTables/Generate DataTables")]
        private static void GenerateDataTables()
        {
            DataTableCodeBinary.GenerateDataTables();
        }

        //配置Configs转Bytes
        [MenuItem("Tools/DataTables/ConfigsToBinary")]
        private static void SelectionConfigTextToBinary()
        {
            DataTableCodeBinary.ConfigTextToBinary();
        }

        //生成自动绑定脚本
        [MenuItem("Tools/ILRuntime/Generate CLR Binding Code by Analysis")]
        private static void GenerateCLRBindingByAnalysis()
        {
            ILRuntimeCLRBinding.GenerateCLRBindingByAnalysis();
        }

        //设置热更新的类型ILRuntime
        [MenuItem("Tools/ILRuntime/Set Hotfix Type ILRuntime")]
        private static void SetHotfixTypeILRuntime()
        {
            ScriptingDefineSymbols.AddScriptingDefineSymbol("ILRuntime");
        }

        //创建热更新程序文件
        [MenuItem("Tools/Build Hotfix Bytes")]
        private static void BuildHotfixBytes()
        {
            HitfixProjectAssets.BuildHotfixBytes();
        }

        //打开热更新的工程
        [MenuItem("Tools/Open Hotfix C# Project")]
        private static void OpenHitfixProject()
        {
            HitfixProjectAssets.OpenHitfixProject();
        }
    }
}
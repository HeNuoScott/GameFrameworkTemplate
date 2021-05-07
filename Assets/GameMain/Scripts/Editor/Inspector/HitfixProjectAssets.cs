// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:44:26
// -----------------------------------------------
using System.Diagnostics;
using Sirius.Runtime;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Sirius.Editor
{
    public static class HitfixProjectAssets
    {
        //创建热更新程序文件
        public static void BuildHotfixBytes()
        {
            string HotfixDll = Directory.GetParent(Application.dataPath).FullName + "\\Library\\ScriptAssemblies\\Project.Hotfix.dll";
            string HotfixPdb = Directory.GetParent(Application.dataPath).FullName + "\\Library\\ScriptAssemblies\\Project.Hotfix.pdb";

            File.Copy(HotfixDll, Path.Combine(AssetUtility.HotfixPath, AssetUtility.HotfixDllName), true);
            File.Copy(HotfixPdb, Path.Combine(AssetUtility.HotfixPath, AssetUtility.HotfixPdbName), true);
            UnityEngine.Debug.Log($"Build Hotfix.dll, Hotfix.pdb 到{AssetUtility.HotfixPath} 完成");
            AssetDatabase.Refresh();
        }

        //打开热更新的工程
        public static void OpenHitfixProject()
        {
            string path = Application.dataPath + "/../Project.Hotfix/Project.Hotfix.sln";
            if (!File.Exists(path))
            {
                UnityEngine.Debug.Log("路径不存在 -> " + path);
                return;
            }
            Process.Start(path);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Function()
        {
            string HotfixDll = Directory.GetParent(Application.dataPath).FullName + "\\Library\\ScriptAssemblies\\Project.Hotfix.dll";
            string HotfixPdb = Directory.GetParent(Application.dataPath).FullName + "\\Library\\ScriptAssemblies\\Project.Hotfix.pdb";

            if (File.Exists(HotfixDll))
            {
                File.Copy(HotfixDll, Path.Combine(AssetUtility.HotfixPath, AssetUtility.HotfixDllName), true);
                File.Copy(HotfixPdb, Path.Combine(AssetUtility.HotfixPath, AssetUtility.HotfixPdbName), true);
                UnityEngine.Debug.LogWarning("热更代码 自动导入项目 完成!");
                AssetDatabase.Refresh();
            }
            else
            {
                UnityEngine.Debug.LogWarning("热更代码 自动导入项目 失败!");
            }
        }
    }
}
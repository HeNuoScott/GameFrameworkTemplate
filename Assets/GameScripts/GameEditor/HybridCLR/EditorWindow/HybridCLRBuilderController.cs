using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using GameFramework;
using HybridCLR.Generators;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityGameFramework.Editor.ResourceTools;
using Debug = UnityEngine.Debug;
using GameFrame.Editor;

namespace HybridCLR.Builder
{
    public class HybridCLRBuilderController 
    {
        public void CompileHotfixDll(int platformIndex)
        {
            BuildTarget buildTarget = GetBuildTargetByPlatformIndex(platformIndex);
            string buildPath = BuildConfig.GetHotFixDllsOutputDirByTarget(buildTarget);//dll 输出路径
            Debug.Log($"编译目标平台: {buildTarget};编译后的DLL输出路径 : {buildPath}");

            // Build Hotfix Dll
            CompileDllHelper.CompileDll(buildPath, buildTarget);


            //AssetBundleBuildHelper.BuildAssetBundles()
            //补齐下面方法

            // Copy Hotfix Dll
            string HotfixDllPath = $"{Application.dataPath}/GameMain/HotFixDll";
            IOUtility.CreateDirectoryIfNotExists(HotfixDllPath);
            foreach (var hotDllName in BuildConfig.MonoHotUpdateDllNames)
            {
                string oriFileName = string.Format("{0}/{1}", buildPath, hotDllName);
                string desFileName = $"{HotfixDllPath}/{hotDllName}.bytes";
                File.Copy(oriFileName, desFileName, true);
            }

            // Copy AOT Dll
            string aotDllPath = string.Format("{0}/{1}", BuildConfig.AssembliesPostIl2CppStripDir, buildTarget);
            IOUtility.CreateDirectoryIfNotExists(aotDllPath);
            foreach (var dllName in BuildConfig.AOTMetaDlls)
            {
                Debug.LogFormat("Copy AOT assemblies:{0}", dllName);
                string oriFileName = string.Format("{0}/{1}", aotDllPath, dllName);
                string desFileName = $"{Application.dataPath}/GameMain/HotFixDll/AOT/{dllName}.bytes";
                if (!File.Exists(oriFileName))
                {
                    Debug.LogError($"AOT 补充元数据 dll: {oriFileName} 文件不存在。需要构建一次主包后才能生成裁剪后的 AOT dll.");
                    continue;
                }
                File.Copy(oriFileName, desFileName, true);
            }

            Debug.Log("Hotfix dll build complete.");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private BuildTarget GetBuildTargetByPlatformIndex(int platformIndex)
        {
            string[] PlatformNames = Enum.GetNames(typeof(Platform));
            Platform platform = (Platform)Enum.Parse(typeof(Platform), PlatformNames[platformIndex]);
            return GetBuildTarget(platform);
        }

        private BuildTarget GetBuildTarget(Platform platform)
        {
            switch (platform)
            {
                case Platform.Windows:
                    return BuildTarget.StandaloneWindows;

                case Platform.Windows64:
                    return BuildTarget.StandaloneWindows64;

                case Platform.MacOS:
#if UNITY_2017_3_OR_NEWER
                    return BuildTarget.StandaloneOSX;
#else
                    return BuildTarget.StandaloneOSXUniversal;
#endif
                case Platform.Linux:
                    return BuildTarget.StandaloneLinux64;

                case Platform.IOS:
                    return BuildTarget.iOS;

                case Platform.Android:
                    return BuildTarget.Android;

                case Platform.WindowsStore:
                    return BuildTarget.WSAPlayer;

                case Platform.WebGL:
                    return BuildTarget.WebGL;

                default:
                    throw new GameFrameworkException("Platform is invalid.");
            }
        }



    }
}
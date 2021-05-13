// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:44:26
// -----------------------------------------------
using System.Diagnostics;
using Sirius.Runtime;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;

namespace Sirius.Editor
{
    public static class HitfixProjectAssets
    {
        const string k_WindowsNewline = "\r\n";
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
            string HotfixProjectPath = Application.dataPath + "/../Project.Hotfix/";
            string HotfixPath = Application.dataPath + "/../Project.Hotfix/Hotfix/";
            string csproj = Application.dataPath + "/../Project.Hotfix/Project.Hotfix.csproj";
            StringBuilder CompileInclude = new StringBuilder();
            StringBuilder projectBuilder = new StringBuilder();

            if (!File.Exists(path) || !File.Exists(csproj))
            {
                UnityEngine.Debug.Log("路径不存在 -> " + csproj);
                UnityEngine.Debug.Log("路径不存在 -> " + path);
                return;
            }
            if (Directory.Exists(HotfixPath))
            {
                DirectoryInfo direction = new DirectoryInfo(HotfixPath);
                DirectoryInfo Project = new DirectoryInfo(HotfixProjectPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    //string filepath = files[i].FullName.Substring(38);
                    string filepath = files[i].FullName.Replace(Project.FullName, "");
                    CompileInclude.Append("    <Compile Include=\"").Append(filepath).Append("\" />").Append(k_WindowsNewline);
                }
                CompileInclude.Append("    <Compile Include=\"").Append("Properties\\AssemblyInfo.cs").Append("\" />");
            }
            if (File.Exists(csproj))
            {
                string[] content = File.ReadAllLines(csproj);
                int skip = 0;
                for (int i = 0; i < content.Length; i++)
                {
                    if (content[i].Trim() == "<ItemGroup>")
                    {
                        projectBuilder.Append(content[i]).Append(k_WindowsNewline);
                        skip++;
                        if (skip == 1) projectBuilder.Append(CompileInclude.ToString()).Append(k_WindowsNewline);
                    }
                    else if (content[i].Trim() == "</ItemGroup>")
                    {
                        projectBuilder.Append(content[i]).Append(k_WindowsNewline);
                        skip++;
                    }
                    else if (skip != 1) projectBuilder.Append(content[i]).Append(k_WindowsNewline);
                }

                File.WriteAllText(csproj, projectBuilder.ToString());
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
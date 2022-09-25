// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/3   14:49:55
// -----------------------------------------------
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;

namespace HybridCLR.Builder
{
    public class HybridCLRBuilder : EditorWindow
    {
        private HybridCLRBuilderController m_Controller;
        private int m_VersionIndex;
        private int m_HotfixPlatformIndex;
        
        private int VersionIndex
        {
            get
            {
                return EditorPrefs.GetInt("HybridCLRVersion", 0);
            }
            set
            {
                m_VersionIndex = value;
                EditorPrefs.SetInt("HybridCLRVersion", m_VersionIndex);
            }
        }

        private int HotfixPlatformIndex
        {
            get
            {
                return EditorPrefs.GetInt("HybridCLRPlatform", 2);
            }
            set
            {
                m_HotfixPlatformIndex = value;
                EditorPrefs.SetInt("HybridCLRPlatform", m_HotfixPlatformIndex);
            }
        }

        private string[] PlatformNames;

        [MenuItem("HybridCLR/HybridCLR Builder", false, 0)]
        private static void Open()
        {
            HybridCLRBuilder window = GetWindow<HybridCLRBuilder>("HybridCLR Builder", true);
            window.minSize = new Vector2(800f, 500f);
            window.m_Controller = new HybridCLRBuilderController();
        }

        private void OnEnable()
        {
            m_VersionIndex = VersionIndex;
            m_HotfixPlatformIndex = HotfixPlatformIndex;
            PlatformNames = Enum.GetNames(typeof(Platform));
        }

        private void OnGUI()
        {
            GUILayout.Space(5f);
            EditorGUILayout.LabelField("Build", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            GUIItem("由于ab包依赖裁剪后的dll，在编译hotfix.dl前需要build工程。", "EditorBuild", BuildPlayerWindow.ShowBuildPlayerWindow);
            int hotfixPlatformIndex = EditorGUILayout.Popup("选择hotfix平台。", m_HotfixPlatformIndex, PlatformNames);
            if (hotfixPlatformIndex != m_HotfixPlatformIndex)
            {
                HotfixPlatformIndex = hotfixPlatformIndex;
            }
            GUIItem("编译hotfix.dll。", "Compile", CompileHotfixDll);
            GUIResourcesTool();
            EditorGUILayout.EndVertical();
        }

        private void GUIItem(string content, string button, Action onClick)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(content);
            if (GUILayout.Button(button, GUILayout.Width(100)))
            {
                onClick?.Invoke();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void GUISelectUnityDirectory(string content, string selectButton)
        {
            EditorGUILayout.BeginHorizontal();
            //m_Controller.UnityInstallDirectory = EditorGUILayout.TextField(content, m_Controller.UnityInstallDirectory);
            //if (GUILayout.Button(selectButton, GUILayout.Width(100)))
            //{
            //    string temp = EditorUtility.OpenFolderPanel(content, m_Controller.UnityInstallDirectory, string.Empty);
            //    if (!string.IsNullOrEmpty(temp))
            //    {
            //        m_Controller.UnityInstallDirectory = temp;
            //    }
            //}
            EditorGUILayout.EndHorizontal();
        }

        private void GUIResourcesTool()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("编辑hotfix.dll等资源，并打包。");
            if (GUILayout.Button("ResourceEditor", GUILayout.Width(150)))
            {
                EditorWindow window = GetWindow(Type.GetType("UnityGameFramework.Editor.ResourceTools.ResourceEditor,UnityGameFramework.Editor"));
                window.Show();
            }
            if (GUILayout.Button("ResourceBuilder", GUILayout.Width(150)))
            {
                EditorWindow window = GetWindow(Type.GetType("UnityGameFramework.Editor.ResourceTools.ResourceBuilder,UnityGameFramework.Editor"));
                window.Show();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CompileHotfixDll()
        {
            m_Controller.CompileHotfixDll(m_HotfixPlatformIndex);
        }
    }
}

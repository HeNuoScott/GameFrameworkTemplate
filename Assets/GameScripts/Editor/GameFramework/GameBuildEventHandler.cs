//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;
using System.Collections.Generic;
using System.Text;

namespace StarForce.Editor
{
    public sealed class GameBuildEventHandler : IBuildEventHandler
    {
        public const string URL = "Http://127.0.0.1:8880/";
        private static Dictionary<Platform, string> WaitForOutputFullPath;
        private static string ApplicableGameVersion;
        private static int InternalResourceVersion;

        public bool ContinueOnFailure
        {
            get
            {
                return false;
            }
        }
        // 所有平台生成开始前的预处理事件。
        public void OnPreprocessAllPlatforms(string productName, string companyName, string gameIdentifier, string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName, bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName, string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions,
            string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
            string streamingAssetsPath = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "StreamingAssets"));
            string[] fileNames = Directory.GetFiles(streamingAssetsPath, "*", SearchOption.AllDirectories);
            foreach (string fileName in fileNames)
            {
                if (fileName.Contains(".gitkeep"))
                {
                    continue;
                }

                File.Delete(fileName);
            }

            Utility.Path.RemoveEmptyDirectory(streamingAssetsPath);
            WaitForOutputFullPath = new Dictionary<Platform, string>();
            ApplicableGameVersion = applicableGameVersion;
            InternalResourceVersion = internalResourceVersion;
        }

        public void OnPostprocessAllPlatforms(string productName, string companyName, string gameIdentifier, string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName, bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName, string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions,
            string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
        }

        public void OnPreprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath)
        {
            if (outputFullSelected)
            {
                WaitForOutputFullPath.Add(platform, outputFullPath);
            }
        }

        public void OnBuildAssetBundlesComplete(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, AssetBundleManifest assetBundleManifest)
        {
        }

        public void OnOutputUpdatableVersionListData(Platform platform, string versionListPath, int versionListLength, int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode)
        {
            string path = WaitForOutputFullPath[platform] + "/version.txt";
            const string WindowsNewline = "\r\n\r\n         ";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{").Append("\r\n         ");
            stringBuilder.Append("  \"ForceUpdateGame\": true,").Append(WindowsNewline);
            stringBuilder.Append("  \"LatestGameVersion\": \"").Append(ApplicableGameVersion).Append("\",").Append(WindowsNewline);
            stringBuilder.Append("  \"InternalGameVersion\": ").Append(InternalResourceVersion).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"InternalResourceVersion\": ").Append(InternalResourceVersion).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"UpdatePrefixUri\": \"").Append(GameBuildEventHandler.URL + WaitForOutputFullPath[platform]).Append("\",").Append(WindowsNewline);
            stringBuilder.Append("  \"VersionListLength\": ").Append(versionListLength).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"VersionListHashCode\": ").Append(versionListHashCode).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"VersionListCompressedLength\": ").Append(versionListCompressedLength).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"VersionListCompressedHashCode\": ").Append(versionListCompressedHashCode).Append(",").Append(WindowsNewline);
            stringBuilder.Append("  \"END_OF_JSON\": \"\"").Append("\r\n");
            stringBuilder.Append("}");
            File.WriteAllText(path, stringBuilder.ToString());
        }

        public void OnPostprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, bool isSuccess)
        {
            if (!outputPackageSelected)
            {
                Debug.Log($"放弃拷贝Bundle 原因: outputPackageSelected:{outputPackageSelected}");
                return;
            }

            //if (platform != Platform.Windows)
            //{
            //    Debug.Log(platform);
            //    return;
            //}

            string streamingAssetsPath = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "StreamingAssets"));
            string[] fileNames = Directory.GetFiles(outputPackagePath, "*", SearchOption.AllDirectories);
            foreach (string fileName in fileNames)
            {
                string destFileName = Utility.Path.GetRegularPath(Path.Combine(streamingAssetsPath, fileName.Substring(outputPackagePath.Length)));
                FileInfo destFileInfo = new FileInfo(destFileName);
                if (!destFileInfo.Directory.Exists)
                {
                    destFileInfo.Directory.Create();
                }

                Debug.Log($"拷贝Bundle :{fileName}");
                File.Copy(fileName, destFileName, true);
            }
        }
    }
}

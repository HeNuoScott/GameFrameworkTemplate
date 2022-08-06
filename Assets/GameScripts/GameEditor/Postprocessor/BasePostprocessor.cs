using UnityEngine;
using UnityEditor;

namespace GameFrame.Editor
{
    public class BasePostprocessor : AssetPostprocessor
    {
        // 当Assets资源发生改变时调用
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            //bool miniGameChanged = false;
            //对比更改
            //foreach (string assetPath in importedAssets)
            //{
            //    if (assetPath == "Assets/GameMain/DataTables/GameCheckpoint.txt")
            //    {
            //        Object asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            //        if (asset != null) miniGameChanged = true;
            //    }
            //}
            // 确定更改之后要执行的操作
            //if (miniGameChanged)
            //{
            //    if (miniGameChanged)
            //    {
            //        DataTableGeneratorMenu.TextToBinary("Assets/GameMain/DataTables/GameCheckpoint.txt");
            //    }
            //    AssetDatabase.SaveAssets();
            //}

        }
    }
}
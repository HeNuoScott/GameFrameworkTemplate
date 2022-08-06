// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2021/5/26   14:13:27
// -----------------------------------------------
using GameEntry = GameFrame.Main.GameEntry;
using UnityGameFramework.Runtime;
using GameFramework.DataTable;

using GameFrame.Main;

namespace GameFrame.Hotfix
{
    public static class SceneExtension 
    {
        public static void AddLoadScene(this SceneComponent sceneComponent, int sceneId, object userData = null)
        {
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                return;
            }
            sceneComponent.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, userData);
        }
        public static void AddLoadScene(this SceneComponent sceneComponent, string sceneName, object userData = null)
        {
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow((sceneData) => { return sceneData.AssetName == sceneName ? true : false; });
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneName);
                return;
            }
            sceneComponent.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, userData);
        }

        public static void UnLoadScene(this SceneComponent sceneComponent, string sceneName, object userData = null)
        {
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow((sceneData) => { return sceneData.AssetName == sceneName ? true : false; });
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneName);
                return;
            }
            sceneComponent.UnloadScene(AssetUtility.GetSceneAsset(drScene.AssetName), userData);
        }
        public static void UnLoadScene(this SceneComponent sceneComponent, int sceneId, object userData = null)
        {
            IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
                return;
            }
            sceneComponent.UnloadScene(AssetUtility.GetSceneAsset(drScene.AssetName), userData);
        }
    }
}
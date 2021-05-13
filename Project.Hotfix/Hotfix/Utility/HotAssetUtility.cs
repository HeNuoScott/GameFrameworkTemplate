using Sirius.Runtime;
using GameFramework;

namespace Project.Hotfix
{
    //资源扩展工具
    public static class HotAssetUtility
    {
        public const string UIItemPath = "Assets/GameMain/UI/UIItems";  //UIItem路径
        public const string PerfabsPath = "Assets/GameMain/Perfabs";  //UIItem路径


        //获取UIItems资源内置路径
        public static string GetUIItemsAsset(string assetName)
        {
            return Utility.Text.Format("{0}/{1}.prefab", UIItemPath, assetName);
        }

        public static string GetPerfabsAsset(string assetName)
        {
            return Utility.Text.Format("{0}/{1}.prefab", PerfabsPath, assetName);
        }
    }
}

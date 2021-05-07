using GameFramework.DataTable;
using GameFramework.UI;
using Sirius.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Sirius.Runtime.GameEntry;

namespace Project.Hotfix
{
    //UI扩展
    public static class HotUIExtension
    {
        //是否存在界面
        public static bool HasUIForm(this UIComponent uiComponent, UIFormID uiFormId, string uiGroupName = null)
        {
            return uiComponent.HasUIForm((int)uiFormId, uiGroupName);
        }

        //是否存在界面
        public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            //获取界面配置表数据
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
                return false;

            //获取界面资源路径
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            //界面组名为空则直接检查当前是否存在界面
            if (string.IsNullOrEmpty(uiGroupName))
                return uiComponent.HasUIForm(assetName);

            //获取界面组
            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
                return false;
            //界面组中检查
            return uiGroup.HasUIForm(assetName);
        }

        //获取界面
        public static UGUIForm GetUIForm(this UIComponent uiComponent, UIFormID uiFormId, string uiGroupName = null)
        {
            return uiComponent.GetUIForm((int)uiFormId, uiGroupName);
        }

        //获取界面
        public static UGUIForm GetUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            //获取界面配置表数据
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
                return null;

            //获取界面资源路径
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);

            UIForm uiform = null;
            //界面组名为空则直接获取界面
            if (string.IsNullOrEmpty(uiGroupName))
            {
                uiform = uiComponent.GetUIForm(assetName);
                if (uiform == null)
                    return null;
            }
            else
            {
                IUIGroup group = uiComponent.GetUIGroup(uiGroupName);
                if (group == null)
                    return null;

                uiform = group.GetUIForm(assetName) as UIForm;
            }

            if (uiform == null)
                return null;

            return uiform.Logic as UGUIForm;
        }

        //打开界面
        public static int? OpenUIForm(this UIComponent uiComponent, UIFormID uiFormId, object userData = null)
        {
            return uiComponent.OpenHotUIForm((int)uiFormId, uiFormId.ToString(), userData);
        }

        //关闭UI界面
        public static void CloseUIForm(this UIComponent uiComponent, UGUIForm uiForm)
        {
            uiComponent.CloseUIForm(uiForm.UIForm);
        }
    }
}

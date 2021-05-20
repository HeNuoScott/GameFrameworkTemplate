//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using GameFramework.UI;
using Sirius.Runtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Sirius.Runtime
{
    public static class UIExtension
    {
        public static IEnumerator FadeToAlpha(this CanvasGroup canvasGroup, float alpha, float duration)
        {
            float time = 0f;
            float originalAlpha = canvasGroup.alpha;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
                yield return new WaitForEndOfFrame();
            }

            canvasGroup.alpha = alpha;
        }

        public static IEnumerator SmoothValue(this Slider slider, float value, float duration)
        {
            float time = 0f;
            float originalValue = slider.value;
            while (time < duration)
            {
                time += Time.deltaTime;
                slider.value = Mathf.Lerp(originalValue, value, time / duration);
                yield return new WaitForEndOfFrame();
            }

            slider.value = value;
        }

        public static bool HasUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                return false;
            }

            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (string.IsNullOrEmpty(uiGroupName))
            {
                return uiComponent.HasUIForm(assetName);
            }

            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                return false;
            }

            return uiGroup.HasUIForm(assetName);
        }

        public static UGUIForm GetUIForm(this UIComponent uiComponent, int uiFormId, string uiGroupName = null)
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                return null;
            }

            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            UIForm uiForm = null;
            if (string.IsNullOrEmpty(uiGroupName))
            {
                uiForm = uiComponent.GetUIForm(assetName);
                if (uiForm == null)
                {
                    return null;
                }

                return (UGUIForm)uiForm.Logic;
            }

            IUIGroup uiGroup = uiComponent.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                return null;
            }

            uiForm = (UIForm)uiGroup.GetUIForm(assetName);
            if (uiForm == null)
            {
                return null;
            }

            return (UGUIForm)uiForm.Logic;
        }

        public static void CloseUIForm(this UIComponent uiComponent, UGUIForm uiForm)
        {
            uiComponent.CloseUIForm(uiForm.UIForm);
        }

        public static void OpenDialog(this UIComponent uiComponent, DialogParams dialogParams)
        {
            if ((GameEntry.Procedure.CurrentProcedure as ProcedureBase).UseNativeDialog) OpenNativeDialog(dialogParams);
            else
            {
                ////1	弹出框	DialogForm	Default	TRUE	TRUE
                //string assetName = AssetUtility.GetUIFormAsset("DialogForm");
                //uiComponent.OpenUIForm(assetName, "Default", Constant.AssetPriority.UIFormAsset, true, dialogParams);
                DialogForm dialogForm = Object.Instantiate(GameEntry.BuiltinData.DialogForm);
                dialogForm.Open(dialogParams);
            }
        }

        private static void OpenNativeDialog(DialogParams dialogParams)
        {
            // TODO：这里应该弹出原生对话框，先简化实现为直接按确认按钮
            if (dialogParams.OnClickConfirm != null)
            {
                dialogParams.OnClickConfirm(dialogParams.UserData);
            }
        }

        //打开UI界面
        public static int? OpenRuntimeUIForm(this UIComponent uiComponent, int uiFormId, object userData = null)
        {
            //先获取UI配置表数据
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
                return null;

            //获取资源路径
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (!drUIForm.AllowMultiInstance)   //不允许存在多个界面实例
            {
                if (uiComponent.IsLoadingUIForm(assetName)) //正在加载
                    return null;

                UIForm uiForm = uiComponent.GetUIForm(assetName);   //获取已加载的界面
                if (uiForm != null)
                    return uiForm.SerialId; //TODO:这里返回已打开界面id？还是返回null？
            }

            return uiComponent.OpenUIForm(assetName, drUIForm.UIGroupName, Constant.AssetPriority.UIFormAsset, drUIForm.PauseCoveredUIForm, userData);
        }

        public static int? OpenRuntimeUIForm(this UIComponent uiComponent, int uiFormId, string hotFormTypeName, object userData = null)
        {
            //先获取UI配置表数据
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
                return null;

            //获取资源路径
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (!drUIForm.AllowMultiInstance)   //不允许存在多个界面实例
            {
                if (uiComponent.IsLoadingUIForm(assetName)) //正在加载
                    return null;

                UIForm uiForm = uiComponent.GetUIForm(assetName);   //获取已加载的界面
                if (uiForm != null)
                    return uiForm.SerialId; //TODO:这里返回已打开界面id？还是返回null？
            }

            return uiComponent.OpenUIForm(assetName, drUIForm.UIGroupName, Constant.AssetPriority.UIFormAsset, drUIForm.PauseCoveredUIForm, userData);
        }

        public static int? OpenHotUIForm(this UIComponent uiComponent, int uiFormId, string hotFormTypeName, object userData = null)
        {
            //先获取UI配置表数据
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
                return null;

            //获取资源路径
            string assetName = AssetUtility.GetUIFormAsset(drUIForm.AssetName);
            if (!drUIForm.AllowMultiInstance)   //不允许存在多个界面实例
            {
                if (uiComponent.IsLoadingUIForm(assetName)) //正在加载
                    return null;

                UIForm uiForm = uiComponent.GetUIForm(assetName);   //获取已加载的界面
                if (uiForm != null)
                    return uiForm.SerialId; //TODO:这里返回已打开界面id？还是返回null？
            }

            //UI传递的数据
            //UserUIData uiData = ReferencePool.Acquire<UserUIData>();
            //uiData.Fill(hotFormTypeName, userData);
            UserUIData uiData = new UserUIData(hotFormTypeName, userData);

            return uiComponent.OpenUIForm(assetName, drUIForm.UIGroupName, Constant.AssetPriority.UIFormAsset, drUIForm.PauseCoveredUIForm, uiData);
        }
    }
}

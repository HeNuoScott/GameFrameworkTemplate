//=======================================================
// 作者：
// 描述：组件扩展工具
//=======================================================
using UnityGameFramework.Runtime;
using GameFramework.Resource;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Object = UnityEngine.Object;

namespace Sirius.Runtime
{	
    /// <summary>
    /// 组件扩展工具，因为ILRuntime中最好不要使用Unity工程中的泛型方法，所以组件的操作使用这个类的扩展方法
    /// </summary>
	public static class ComponentExtension
    {

        #region Button

        //给按钮添加点击回调
        public static void ButtonAddClick(this Button button, UnityAction callBack)
        {
            button.onClick.AddListener(callBack);
        }

        //清空按钮回调
        public static void ButtonClearClick(this Button button)
        {
            button.onClick.RemoveAllListeners();
        }

        #endregion

        #region Toggle

        //给Toggle添加点击回调
        public static void ToggleAddChanged(this Toggle toggle, UnityAction<bool> callback)
        {
            toggle.onValueChanged.AddListener(callback);
        }

        //清空Toggle回调
        public static void ToggleClearChanged(this Toggle toggle)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }

        #endregion


        #region Slider

        //给Slider添加拖动回调
        public static void SliderAddChanged(this Slider slider, UnityAction<float> callback)
        {
            slider.onValueChanged.AddListener(callback);
        }

        //给Slider添加拖动回调
        public static void SliderClearChanged(this Slider slider)
        {
            slider.onValueChanged.RemoveAllListeners();
        }

        #endregion

        #region RuntimeComponent

        /// <summary>
        /// 封装的加载资源方法
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="priority"></param>
        /// <param name="loadAssetSuccessCallbacks"></param>
        /// <param name="loadAssetFailureCallbacks"></param>
        /// <param name="userData"></param>
        public static void LoadAsset(this ResourceComponent resource, string assetName, int priority, Action<string, object, float, object> loadAssetSuccessCallbacks, Action<string, string, string, object> loadAssetFailureCallbacks, object userData = null)
        {
            resource.LoadAsset(assetName, priority,
                new LoadAssetCallbacks(
                    (assetName0, asset0, duration0, userData0) => { loadAssetSuccessCallbacks.Invoke(assetName0, asset0, duration0, userData0); },
                    (assetName0, status0, errorMessage0, userData0) => { loadAssetFailureCallbacks.Invoke(assetName0, status0.ToString(), errorMessage0, userData0); }
                    ),
                userData);
        }

        #endregion

    }
}
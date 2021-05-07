using GameFramework;
using ILRuntime.Runtime.Enviorment;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Sirius.Runtime
{	
	//ILRuntime扩展工具包
	public static class ILRuntimeUtility
	{
	    //初始化
	    public static void InitILRuntime(AppDomain appDomain)
	    {
            //注册委托参数
            appDomain.DelegateManager.RegisterMethodDelegate<float>();
	        appDomain.DelegateManager.RegisterMethodDelegate<object, GameFramework.Event.GameEventArgs>();
	        appDomain.DelegateManager.RegisterMethodDelegate<float, float>();
	        appDomain.DelegateManager.RegisterMethodDelegate<bool>();
	        appDomain.DelegateManager.RegisterMethodDelegate<object>();
	        appDomain.DelegateManager.RegisterMethodDelegate<string, string, string, object>(); //加载资源失败的回调
	        appDomain.DelegateManager.RegisterMethodDelegate<string, object, float, object>(); //加载资源成功的回调
	        appDomain.DelegateManager.RegisterMethodDelegate<List<object>>();
	        appDomain.DelegateManager.RegisterMethodDelegate<byte[], int, int>();

            //注册委托转换器
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((action) =>
	        {
	            return new UnityAction(() => ((Action)action).Invoke());
	        });
	        appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<float>>((action) =>
	        {
                return new UnityAction<float>((a) => ((Action<float>)action).Invoke(a));
	        });
            appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<bool>>((action) =>
            {
                return new UnityAction<bool>((a) => ((Action<bool>)action).Invoke(a));
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<GameFrameworkAction<object>>((action) =>
            {
                return new GameFrameworkAction<object>((a) => ((GameFrameworkAction<object>)action).Invoke(a));
            });
            appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<GameFramework.Event.GameEventArgs>>((action) =>
            {
                return new EventHandler<GameFramework.Event.GameEventArgs>((sender, e) =>
                {
                    ((Action<object, GameFramework.Event.GameEventArgs>)action).Invoke(sender, e);
                });
            });

            //注册CLR绑定代码
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);

            //注册跨域继承适配器
            appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());

            //struct 值类型的绑定
            appDomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
            appDomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
            appDomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());

            //注册LitJson
            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appDomain);
	    }
	}
}

using System.Collections.Generic;
using UnityGameFramework.Runtime;
using Sirius.Runtime;
using System.Collections;
using System.Reflection;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;

namespace Sirius.Runtime
{

	//热更新的组件
	public class HotfixComponent : GameFrameworkComponent
	{
        /// <summary>
        /// ILRuntime入口
        /// </summary>
        public AppDomain ILAppDomain { get; private set; }
        System.IO.MemoryStream fdllStreams;
        System.IO.MemoryStream pdbStream;

        //热更新的命名空间
        public const string c_HotfixNamespace = "Project.Hotfix";

        //以下方法在hotfix中注册
        public Action<float, float> OnUpdate = null; //更新回调
	    public Action OnLateUpdate = null;   //延迟更新
	    public Action OnApplication = null;    //应用退出回调
	
        //加载热更新脚本，可动态改变热更新模式：ILRuntime热更新/Mono程序集反射热更新(非IOS)。加载脚本数据的操作放入预加载流程中
        public void LoadHotfixAssembly(byte[] dllBytes, byte[] pdbBytes)
	    {
            Log.Debug($"当前使用的是ILRuntime模式");
            ILAppDomain = new AppDomain();

            fdllStreams = new MemoryStream(dllBytes);
            pdbStream = new MemoryStream(pdbBytes);

            try
            {
                ILAppDomain.LoadAssembly(fdllStreams, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch
            {
                Debug.LogError("加载热更DLL失败，请确保已经通过VS打开HotFix_Project.sln编译过热更DLL");
            }

            //#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
            //            //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
            //            ILAppDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            //#endif
            //做一些ILRuntime的注册，比如重定向函数、注册委托变量、适配器等
            ILRuntimeUtility.InitILRuntime(ILAppDomain);
            string hotfixTypeName = "HotfixEntry";
            string hotfixStartMethodName = "Start";
            StaticMethod start = new ILStaticMethod(hotfixTypeName.HotFixTypeFullName(), hotfixStartMethodName, 1);
            //启动热更新的程序
            start.Run(GameEntry.Instance.gameObject);
            //ILAppDomain.Invoke(hotfixTypeName.HotFixTypeFullName(), hotfixStartMethodName, null, GameEntry.Instance.gameObject);
        }

        //获取类型
        public object GetHotType(string typeName)
        {
            object type = ILAppDomain.LoadedTypes[typeName];
            return type;
        }

        public object GetHotTypeInstance(string typeName)
        {
            ILType type = ILAppDomain.LoadedTypes[typeName] as ILType;
            object instance = new ILTypeInstance(type);
            return instance;
        }

        //创建实例对象
        public object CreateInstance(string typeFullName, params object[] args)
        {
            object instance = ILAppDomain.Instantiate(typeFullName, args);
            return instance;
        }

        //调用一次的方法
        public object InvokeOne(string typeFullName, string methodName, object instance, params object[] args)
        {
            if(instance != null)
            {
                if (ILAppDomain != null) return ILAppDomain.Invoke(typeFullName, methodName, instance, args);
            }
            return null;
        }

        private void Update()
	    {
	        OnUpdate?.Invoke(Time.deltaTime, Time.unscaledDeltaTime);
	    }
	
	    private void LateUpdate()
	    {
	        OnLateUpdate?.Invoke();
	    }

        private void OnDestroy()
        {
            if (fdllStreams != null) fdllStreams.Close();
            if (pdbStream != null) pdbStream.Close();
            fdllStreams = null;
            pdbStream = null;
        }

        private void OnApplicationQuit()
	    {
	        OnApplication?.Invoke();
	    }
	}
}

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using Sirius.Runtime;
using System.IO;
using GameFramework;

namespace Sirius.Editor
{	
	[System.Reflection.Obfuscation(Exclude = true)]
	public class ILRuntimeCLRBinding
	{
        private const string GeneratedPath = "Assets/GameMain/Scripts/Runtime/ILRuntime/Generated";

	    public static void GenerateCLRBindingByAnalysis()
	    {
            if (Directory.Exists(GeneratedPath)) Directory.Delete(GeneratedPath, true);

            GenerateCLRBinding();

            //用新的分析热更dll调用引用来生成绑定代码
            ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
			using (FileStream fs = new FileStream(Path.Combine(AssetUtility.HotfixPath, AssetUtility.HotfixDllName), FileMode.Open, FileAccess.Read))
	        {
	            domain.LoadAssembly(fs);
				//Crossbind Adapter is needed to generate the correct binding code
				ILRuntimeUtility.InitILRuntime(domain);
				ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, GeneratedPath);
			}
            AssetDatabase.Refresh();
	    }

		static void GenerateCLRBinding()
		{
			List<Type> types = new List<Type>();
			types.Add(typeof(int));
			types.Add(typeof(float));
			types.Add(typeof(long));
			types.Add(typeof(object));
			types.Add(typeof(string));
			types.Add(typeof(Array));
			types.Add(typeof(Vector2));
			types.Add(typeof(Vector3));
			types.Add(typeof(Quaternion));
			types.Add(typeof(GameObject));
			types.Add(typeof(UnityEngine.Object));
			types.Add(typeof(Transform));
			types.Add(typeof(RectTransform));
			types.Add(typeof(Time));
			types.Add(typeof(Debug));
			//所有DLL内的类型的真实C#类型都是ILTypeInstance
			types.Add(typeof(List<ILRuntime.Runtime.Intepreter.ILTypeInstance>));

			ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, GeneratedPath);
		}
	}
#endif
}

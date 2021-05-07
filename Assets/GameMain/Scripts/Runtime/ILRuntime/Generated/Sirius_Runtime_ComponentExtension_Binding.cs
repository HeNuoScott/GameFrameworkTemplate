// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/7   16:35:23
// -----------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Sirius_Runtime_ComponentExtension_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Sirius.Runtime.ComponentExtension);
            args = new Type[]{typeof(UnityGameFramework.Runtime.ResourceComponent), typeof(System.String), typeof(System.Int32), typeof(System.Action<System.String, System.Object, System.Single, System.Object>), typeof(System.Action<System.String, System.String, System.String, System.Object>), typeof(System.Object)};
            method = type.GetMethod("LoadAsset", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, LoadAsset_0);


        }


        static StackObject* LoadAsset_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 6);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object @userData = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Action<System.String, System.String, System.String, System.Object> @loadAssetFailureCallbacks = (System.Action<System.String, System.String, System.String, System.Object>)typeof(System.Action<System.String, System.String, System.String, System.Object>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Action<System.String, System.Object, System.Single, System.Object> @loadAssetSuccessCallbacks = (System.Action<System.String, System.Object, System.Single, System.Object>)typeof(System.Action<System.String, System.Object, System.Single, System.Object>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.Int32 @priority = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            System.String @assetName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 6);
            UnityGameFramework.Runtime.ResourceComponent @resource = (UnityGameFramework.Runtime.ResourceComponent)typeof(UnityGameFramework.Runtime.ResourceComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            Sirius.Runtime.ComponentExtension.LoadAsset(@resource, @assetName, @priority, @loadAssetSuccessCallbacks, @loadAssetFailureCallbacks, @userData);

            return __ret;
        }



    }
}

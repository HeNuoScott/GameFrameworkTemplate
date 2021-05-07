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
    unsafe class Sirius_Runtime_SoundExtension_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Sirius.Runtime.SoundExtension);
            args = new Type[]{typeof(UnityGameFramework.Runtime.SoundComponent), typeof(System.Int32), typeof(Sirius.Runtime.Entity), typeof(System.Object)};
            method = type.GetMethod("PlaySound", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, PlaySound_0);
            args = new Type[]{typeof(UnityGameFramework.Runtime.SoundComponent), typeof(System.Int32), typeof(System.Object)};
            method = type.GetMethod("PlayMusic", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, PlayMusic_1);


        }


        static StackObject* PlaySound_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object @userData = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Sirius.Runtime.Entity @bindingEntity = (Sirius.Runtime.Entity)typeof(Sirius.Runtime.Entity).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Int32 @soundId = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            UnityGameFramework.Runtime.SoundComponent @soundComponent = (UnityGameFramework.Runtime.SoundComponent)typeof(UnityGameFramework.Runtime.SoundComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = Sirius.Runtime.SoundExtension.PlaySound(@soundComponent, @soundId, @bindingEntity, @userData);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* PlayMusic_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object @userData = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @musicId = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            UnityGameFramework.Runtime.SoundComponent @soundComponent = (UnityGameFramework.Runtime.SoundComponent)typeof(UnityGameFramework.Runtime.SoundComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = Sirius.Runtime.SoundExtension.PlayMusic(@soundComponent, @musicId, @userData);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}

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
    unsafe class Sirius_Runtime_HotfixComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Sirius.Runtime.HotfixComponent);

            field = type.GetField("OnUpdate", flag);
            app.RegisterCLRFieldGetter(field, get_OnUpdate_0);
            app.RegisterCLRFieldSetter(field, set_OnUpdate_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnUpdate_0, AssignFromStack_OnUpdate_0);
            field = type.GetField("OnLateUpdate", flag);
            app.RegisterCLRFieldGetter(field, get_OnLateUpdate_1);
            app.RegisterCLRFieldSetter(field, set_OnLateUpdate_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnLateUpdate_1, AssignFromStack_OnLateUpdate_1);
            field = type.GetField("OnApplication", flag);
            app.RegisterCLRFieldGetter(field, get_OnApplication_2);
            app.RegisterCLRFieldSetter(field, set_OnApplication_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnApplication_2, AssignFromStack_OnApplication_2);


        }



        static object get_OnUpdate_0(ref object o)
        {
            return ((Sirius.Runtime.HotfixComponent)o).OnUpdate;
        }

        static StackObject* CopyToStack_OnUpdate_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Sirius.Runtime.HotfixComponent)o).OnUpdate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnUpdate_0(ref object o, object v)
        {
            ((Sirius.Runtime.HotfixComponent)o).OnUpdate = (System.Action<System.Single, System.Single>)v;
        }

        static StackObject* AssignFromStack_OnUpdate_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action<System.Single, System.Single> @OnUpdate = (System.Action<System.Single, System.Single>)typeof(System.Action<System.Single, System.Single>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((Sirius.Runtime.HotfixComponent)o).OnUpdate = @OnUpdate;
            return ptr_of_this_method;
        }

        static object get_OnLateUpdate_1(ref object o)
        {
            return ((Sirius.Runtime.HotfixComponent)o).OnLateUpdate;
        }

        static StackObject* CopyToStack_OnLateUpdate_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Sirius.Runtime.HotfixComponent)o).OnLateUpdate;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnLateUpdate_1(ref object o, object v)
        {
            ((Sirius.Runtime.HotfixComponent)o).OnLateUpdate = (System.Action)v;
        }

        static StackObject* AssignFromStack_OnLateUpdate_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @OnLateUpdate = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((Sirius.Runtime.HotfixComponent)o).OnLateUpdate = @OnLateUpdate;
            return ptr_of_this_method;
        }

        static object get_OnApplication_2(ref object o)
        {
            return ((Sirius.Runtime.HotfixComponent)o).OnApplication;
        }

        static StackObject* CopyToStack_OnApplication_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((Sirius.Runtime.HotfixComponent)o).OnApplication;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnApplication_2(ref object o, object v)
        {
            ((Sirius.Runtime.HotfixComponent)o).OnApplication = (System.Action)v;
        }

        static StackObject* AssignFromStack_OnApplication_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @OnApplication = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((Sirius.Runtime.HotfixComponent)o).OnApplication = @OnApplication;
            return ptr_of_this_method;
        }



    }
}

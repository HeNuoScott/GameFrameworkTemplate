// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/14   10:0:13
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
    unsafe class UnityGameFramework_Runtime_DataTableComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            MethodBase method;
            Type[] args;
            Type type = typeof(UnityGameFramework.Runtime.DataTableComponent);
            Dictionary<string, List<MethodInfo>> genericMethods = new Dictionary<string, List<MethodInfo>>();
            List<MethodInfo> lst = null;                    
            foreach(var m in type.GetMethods())
            {
                if(m.IsGenericMethodDefinition)
                {
                    if (!genericMethods.TryGetValue(m.Name, out lst))
                    {
                        lst = new List<MethodInfo>();
                        genericMethods[m.Name] = lst;
                    }
                    lst.Add(m);
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRAircraft)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRAircraft>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_0);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRArmor)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRArmor>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_1);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRAsteroid)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRAsteroid>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_2);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRThruster)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRThruster>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_3);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRWeapon)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRWeapon>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_4);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRScene)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRScene>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_5);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.DRUIForm)};
            if (genericMethods.TryGetValue("GetDataTable", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(GameFramework.DataTable.IDataTable<Sirius.Runtime.DRUIForm>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, GetDataTable_6);

                        break;
                    }
                }
            }


        }


        static StackObject* GetDataTable_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRAircraft>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRArmor>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRAsteroid>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRThruster>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRWeapon>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRScene>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetDataTable_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityGameFramework.Runtime.DataTableComponent instance_of_this_method = (UnityGameFramework.Runtime.DataTableComponent)typeof(UnityGameFramework.Runtime.DataTableComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetDataTable<Sirius.Runtime.DRUIForm>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}

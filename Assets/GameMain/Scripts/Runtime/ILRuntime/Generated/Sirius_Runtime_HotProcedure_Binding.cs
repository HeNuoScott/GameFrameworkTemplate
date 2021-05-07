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
    unsafe class Sirius_Runtime_HotProcedure_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            MethodBase method;
            Type[] args;
            Type type = typeof(Sirius.Runtime.HotProcedure);
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
            args = new Type[]{typeof(Sirius.Runtime.HotProcedureLobby)};
            if (genericMethods.TryGetValue("ChangeProcedure", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ChangeProcedure_0);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.HotProcedureTrainingGroundA)};
            if (genericMethods.TryGetValue("ChangeProcedure", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ChangeProcedure_1);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.HotProcedureTrainingGroundB)};
            if (genericMethods.TryGetValue("ChangeProcedure", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ChangeProcedure_2);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(Sirius.Runtime.HotProcedureChangeScene)};
            if (genericMethods.TryGetValue("ChangeProcedure", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, ChangeProcedure_3);

                        break;
                    }
                }
            }


        }


        static StackObject* ChangeProcedure_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager> @procedureOwner = (GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Sirius.Runtime.HotProcedure instance_of_this_method = (Sirius.Runtime.HotProcedure)typeof(Sirius.Runtime.HotProcedure).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ChangeProcedure<Sirius.Runtime.HotProcedureLobby>(@procedureOwner);

            return __ret;
        }

        static StackObject* ChangeProcedure_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager> @procedureOwner = (GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Sirius.Runtime.HotProcedure instance_of_this_method = (Sirius.Runtime.HotProcedure)typeof(Sirius.Runtime.HotProcedure).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ChangeProcedure<Sirius.Runtime.HotProcedureTrainingGroundA>(@procedureOwner);

            return __ret;
        }

        static StackObject* ChangeProcedure_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager> @procedureOwner = (GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Sirius.Runtime.HotProcedure instance_of_this_method = (Sirius.Runtime.HotProcedure)typeof(Sirius.Runtime.HotProcedure).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ChangeProcedure<Sirius.Runtime.HotProcedureTrainingGroundB>(@procedureOwner);

            return __ret;
        }

        static StackObject* ChangeProcedure_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager> @procedureOwner = (GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>)typeof(GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            Sirius.Runtime.HotProcedure instance_of_this_method = (Sirius.Runtime.HotProcedure)typeof(Sirius.Runtime.HotProcedure).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.ChangeProcedure<Sirius.Runtime.HotProcedureChangeScene>(@procedureOwner);

            return __ret;
        }



    }
}

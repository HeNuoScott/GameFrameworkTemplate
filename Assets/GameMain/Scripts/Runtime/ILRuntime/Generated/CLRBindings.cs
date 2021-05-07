// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/7   16:35:24
// -----------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }

        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion> s_UnityEngine_Quaternion_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2> s_UnityEngine_Vector2_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3> s_UnityEngine_Vector3_Binding_Binder = null;

        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            System_Type_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_String_Binding.Register(app);
            GameFramework_Utility_Binding_Text_Binding.Register(app);
            System_Object_Binding.Register(app);
            UnityEngine_LayerMask_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            Sirius_Runtime_GameEntry_Binding.Register(app);
            UnityGameFramework_Runtime_DataTableComponent_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRAircraft_Binding.Register(app);
            Sirius_Runtime_DRAircraft_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRArmor_Binding.Register(app);
            Sirius_Runtime_DRArmor_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRAsteroid_Binding.Register(app);
            Sirius_Runtime_DRAsteroid_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRThruster_Binding.Register(app);
            Sirius_Runtime_DRThruster_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRWeapon_Binding.Register(app);
            Sirius_Runtime_DRWeapon_Binding.Register(app);
            Sirius_Runtime_HotLog_Binding.Register(app);
            Sirius_Runtime_Entity_Binding.Register(app);
            System_Int32_Binding.Register(app);
            UnityGameFramework_Runtime_EntityLogic_Binding.Register(app);
            Sirius_Runtime_HotEntity_Binding.Register(app);
            Sirius_Runtime_SoundExtension_Binding.Register(app);
            UnityEngine_Random_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityExtension_Binding.Register(app);
            UnityGameFramework_Runtime_EntityComponent_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            UnityEngine_Input_Binding.Register(app);
            UnityEngine_Camera_Binding.Register(app);
            UnityEngine_Rect_Binding.Register(app);
            UnityEngine_Mathf_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            UnityGameFramework_Runtime_Entity_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            Sirius_Runtime_EntityExtension_Binding.Register(app);
            Sirius_Runtime_HotfixComponent_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneSuccessEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_EventComponent_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneFailureEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneUpdateEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_LoadSceneDependencyAssetEventArgs_Binding.Register(app);
            UnityGameFramework_Runtime_SoundComponent_Binding.Register(app);
            UnityGameFramework_Runtime_UIComponent_Binding.Register(app);
            UnityGameFramework_Runtime_SceneComponent_Binding.Register(app);
            UnityGameFramework_Runtime_BaseComponent_Binding.Register(app);
            GameFramework_Fsm_IFsm_1_IProcedureManager_Binding.Register(app);
            GameFramework_Variable_1_Int32_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRScene_Binding.Register(app);
            Sirius_Runtime_DRScene_Binding.Register(app);
            Sirius_Runtime_AssetUtility_Binding.Register(app);
            UnityGameFramework_Runtime_ObjectPoolComponent_Binding.Register(app);
            Sirius_Runtime_HotProcedure_Binding.Register(app);
            System_Single_Binding.Register(app);
            UnityGameFramework_Runtime_ConfigComponent_Binding.Register(app);
            UnityGameFramework_Runtime_VarInt32_Binding.Register(app);
            Sirius_Runtime_UserUIData_Binding.Register(app);
            GameFramework_DataTable_IDataTable_1_DRUIForm_Binding.Register(app);
            Sirius_Runtime_DRUIForm_Binding.Register(app);
            GameFramework_UI_IUIGroup_Binding.Register(app);
            UnityGameFramework_Runtime_UIForm_Binding.Register(app);
            Sirius_Runtime_UIExtension_Binding.Register(app);
            UnityGameFramework_Runtime_UIFormLogic_Binding.Register(app);
            Sirius_Runtime_HotUIForm_Binding.Register(app);
            Sirius_Runtime_ReferenceCollector_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Byte_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_KeyValuePair_2_Byte_Int32_Byte_Array_Binding.Register(app);
            System_Collections_Generic_List_1_Byte_Binding.Register(app);
            System_Enum_Binding.Register(app);
            System_Array_Binding.Register(app);
            Sirius_Runtime_ComponentView_Binding.Register(app);
            UnityEngine_Canvas_Binding.Register(app);
            UnityEngine_UI_CanvasScaler_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            Sirius_Runtime_ComponentExtension_Binding.Register(app);
            UnityEngine_MonoBehaviour_Binding.Register(app);
            UnityEngine_CanvasGroup_Binding.Register(app);
            UnityEngine_UI_Slider_Binding.Register(app);
            UnityEngine_RectTransformUtility_Binding.Register(app);
            UnityEngine_RectTransform_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Type_ILTypeInstance_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Collections_Generic_Queue_1_ILTypeInstance_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_DateTime_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_ILTypeInstance_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_ILTypeInstance_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_ILTypeInstance_ILTypeInstance_Binding.Register(app);
            System_Predicate_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            GameFramework_GameFrameworkMultiDictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Object_LinkedListNode_1_ILTypeInstance_Binding.Register(app);
            GameFramework_GameFrameworkLinkedListRange_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Object_LinkedListNode_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_Object_LinkedListNode_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_LinkedListNode_1_ILTypeInstance_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Quaternion));
            s_UnityEngine_Quaternion_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector2));
            s_UnityEngine_Vector2_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector3));
            s_UnityEngine_Vector3_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3>;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            s_UnityEngine_Quaternion_Binding_Binder = null;
            s_UnityEngine_Vector2_Binding_Binder = null;
            s_UnityEngine_Vector3_Binding_Binder = null;
        }
    }
}

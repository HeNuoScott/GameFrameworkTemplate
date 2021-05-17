// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/17   20:4:26
// -----------------------------------------------
// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/17   19:12:26
// -----------------------------------------------
using Sirius.Runtime;
using GameFramework;
using UnityEngine;

/// <summary>
/// 热更新创建的Mono脚本使用
/// </summary>
[RequireComponent(typeof(ReferenceCollector))]
[DisallowMultipleComponent]
public class HotLoadMonoScript : MonoBehaviour
{
    public ReferenceCollector ReferenceCollector { get; private set; } //组件对象容器
    public object HotLogicInstance { get; private set; }    //热更的逻辑实例
    public string HotLogicScript;  //保存当前的热更脚本

    private InstanceMethod OnInitMethod = null;
    private InstanceMethod OnUpdateMethod = null;
    private InstanceMethod OnLateUpdateMethod = null;
    private InstanceMethod OnTriggerEnterMethod = null;
    private InstanceMethod OnTriggerExitMethod = null;
    private InstanceMethod OnCollisionEnterMethod = null;
    private InstanceMethod OnCollisionExitMethod = null;
    private InstanceMethod OnOnDestroyMethod = null;

    public void InitLogic(string HotLogicTypeName, object HotInstance)
    {
        ReferenceCollector = GetComponent<ReferenceCollector>();
        HotLogicTypeName = HotLogicTypeName.HotFixTypeFullName();
        HotLogicInstance = HotInstance;
        HotLogicScript = HotLogicTypeName;

        //获取方法
        OnInitMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnInit", 1);
        OnUpdateMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnUpdate", 0);
        OnLateUpdateMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnLateUpdate", 0);
        OnTriggerEnterMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnTriggerEnter", 1);
        OnTriggerExitMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnTriggerExit", 1);
        OnCollisionEnterMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnCollisionEnter", 1);
        OnCollisionExitMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnCollisionExit", 1);
        OnOnDestroyMethod = ReferencePool.Acquire<ILInstanceMethod>().Fill(HotLogicInstance, HotLogicTypeName, "OnDestroy", 0);

        if (OnInitMethod.IsAvalible) OnInitMethod.Run(this);
    }
    //释放资源
    private void ReleaseLogicData()
    {
        if (HotLogicInstance != null)
        {

            ReferencePool.Release((IReference)OnInitMethod);
            ReferencePool.Release((IReference)OnUpdateMethod);
            ReferencePool.Release((IReference)OnLateUpdateMethod);
            ReferencePool.Release((IReference)OnTriggerEnterMethod);
            ReferencePool.Release((IReference)OnTriggerExitMethod);
            ReferencePool.Release((IReference)OnCollisionEnterMethod);
            ReferencePool.Release((IReference)OnCollisionExitMethod);
            ReferencePool.Release((IReference)OnOnDestroyMethod);

            ReferenceCollector = null;
            HotLogicInstance = null;
            OnInitMethod = null;
            OnUpdateMethod = null;
            OnLateUpdateMethod = null;
            OnTriggerEnterMethod = null;
            OnTriggerExitMethod = null;
            OnCollisionEnterMethod = null;
            OnCollisionExitMethod = null;
            OnOnDestroyMethod = null;
        }

    }
    private void Update()
    {
        if (OnUpdateMethod.IsAvalible) OnUpdateMethod.Run();
    }
    private void LateUpdate()
    {
        if (OnLateUpdateMethod.IsAvalible) OnLateUpdateMethod.Run();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (OnTriggerEnterMethod.IsAvalible) OnTriggerEnterMethod.Run(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (OnTriggerExitMethod.IsAvalible) OnTriggerExitMethod.Run(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (OnCollisionEnterMethod.IsAvalible) OnCollisionEnterMethod.Run(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (OnCollisionExitMethod.IsAvalible) OnCollisionExitMethod.Run(collision);
    }
    protected virtual void OnDestroy()
    {
        if (OnOnDestroyMethod.IsAvalible) OnOnDestroyMethod.Run();
        ReleaseLogicData();
    }
}

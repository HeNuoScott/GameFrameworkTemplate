using UnityGameFramework.Runtime;
using GameFramework.Fsm;
using UnityEngine;

namespace Sirius
{

    public class FSMTemp
    {
        public IFsm<FSMTemp> m_Fsm = null;

        public FSMTemp()
        {
            FsmComponent fsmComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();

            // 参数传入有限状态机名称、拥有者和所有需要的状态
            m_Fsm = fsmComponent.CreateFsm("ActorFsm", this, new IdleState(), new MoveState());

            m_Fsm.Start<IdleState>();
        }
    }
    /// <summary>
    /// 定义站立状态。
    /// </summary>
    public class IdleState : FsmState<FSMTemp>
    {
        protected override void OnInit(IFsm<FSMTemp> fsm)
        {
            // 创建有限状态机时调用
            base.OnInit(fsm);
            Log.Info("创建站立状态。");
        }

        protected override void OnDestroy(IFsm<FSMTemp> fsm)
        {
            // 销毁有限状态机时调用
            base.OnDestroy(fsm);
            Log.Info("销毁站立状态。");
        }

        protected override void OnEnter(IFsm<FSMTemp> fsm)
        {
            // 进入本状态时调用
            base.OnEnter(fsm);
            Log.Info("进入站立状态。");
        }

        protected override void OnLeave(IFsm<FSMTemp> fsm, bool isShutdown)
        {
            // 离开本状态时调用
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开站立状态。");
        }

        protected override void OnUpdate(IFsm<FSMTemp> fsm, float elapseSeconds, float realElapseSeconds)
        {
            // 本状态被轮询时调用
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询站立状态。");
            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangeState<MoveState>(fsm);
            }
        }
    }
    /// <summary>
    /// 定义移动状态。
    /// </summary>
    public class MoveState : FsmState<FSMTemp>
    {
    }
}
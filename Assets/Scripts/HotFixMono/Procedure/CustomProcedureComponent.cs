using System.Collections;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Fsm;
using UnityEngine;

namespace Sirius
{

    public class CustomProcedureComponent : GameFrameworkComponent
    {
        private FSMProcedure procedureManager = null;

        public void Run()
        {
            procedureManager = new FSMProcedure();
            procedureManager.Start();
        }
    }

    public class FSMProcedure
    {
        public IFsm<FSMProcedure> m_Fsm = null;

        public FSMProcedure()
        {
            FsmComponent fsmComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();

            // 参数传入有限状态机名称、拥有者和所有需要的状态
            m_Fsm = fsmComponent.CreateFsm("FSMProcedure", this, new ProcedureChangeSceneState(), new ProcedureMainState(),new ProcedureMenuState());

            //m_Fsm.Start<IdleState>();
        }

        public void Start()
        {
            m_Fsm.Start<ProcedureMenuState>();
        }
    }
}

using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

namespace Sirius.Runtime
{
    public sealed class HotProcedureEntry : HotProcedure
    {
        public override bool UseNativeDialog { get { return true; } }

        public override string HotProcedureLogicTypeFullName { get { return null; } }

        private bool IsStart = false;

        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {

        }


        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {

        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            if (!IsStart)
            {
                IsStart = true;
                // 首次进入 进入训练大厅
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Lobby"));
                ChangeState<HotProcedureChangeScene>(procedureOwner);
            }
        }


        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
        }


        protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
        {
        }

    }
}

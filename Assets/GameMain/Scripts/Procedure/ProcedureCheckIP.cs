// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/26   14:52:20
// -----------------------------------------------

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine;

namespace Sirius
{
    public class ProcedureCheckIP : ProcedureBase
    {
        private ResetIPForm IPForm = null;

        private bool CheckIPOk = false;

        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            CheckIPOk = false;
            IPForm = GameObject.Instantiate(GameEntry.BuiltinData.ResetIPForm, GameEntry.BuiltinData.transform);
            IPForm.OnClick = ExecuteCheck;
        }

        private void ExecuteCheck(string ip)
        {
            GameEntry.Setting.SetString(ResetIPForm.ConnectIP, ip);
            GameEntry.Setting.Save();
            CheckIPOk = true;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (CheckIPOk)
            {
                CheckIPOk = false;
                ChangeState<ProcedureCheckVersion>(procedureOwner);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if (IPForm != null)
            {
                Object.Destroy(IPForm.gameObject);
                IPForm = null;
            }
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}
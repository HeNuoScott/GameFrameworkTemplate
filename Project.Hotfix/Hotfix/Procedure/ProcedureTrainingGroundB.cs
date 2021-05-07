using GameEntry = Sirius.Runtime.GameEntry;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using Project.Hotfix.Event;
using GameFramework.Fsm;
using Sirius.Runtime;

namespace Project.Hotfix
{
    // 场景训练B
    public class ProcedureTrainingGroundB : ProcedureLogic
    {
		private bool isChangeScene = false;
		private int NextSceneId = 0;

		//初始化流程，框架启动时调用
		public override void OnInit(IFsm<IProcedureManager> procedureOwner, HotProcedure procedureBind)
		{
			base.OnInit(procedureOwner, procedureBind);
		}

		public override void OnEnter(IFsm<IProcedureManager> procedureOwner)
		{
			isChangeScene = false;
			NextSceneId = 0;
			HotfixEntry.Event.Subscribe(ProcedureChangedEventArgs.EventId, OnChangeScene);
		}

		public override void OnUpdate(IFsm<IProcedureManager> procedureOwner)
		{
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Escape))
			{
				HotLog.Debug("切换场景=>大厅");
				HotfixEntry.Event.Fire(this, ProcedureChangedEventArgs.Fill(GameEntry.Config.GetInt("Scene.Lobby")));
			}
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.A))
            {
                HotLog.Debug("切换场景=>训练场景A");
				HotfixEntry.Event.Fire(this, ProcedureChangedEventArgs.Fill(GameEntry.Config.GetInt("Scene.TrainingA")));
            }
			//if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.B))
			//{
			//	HotLog.Debug("切换场景=>训练场景B");
			//	GameEntry.Event.Fire(this, ProcedureChangedEventArgs.Fill(GameEntry.Config.GetInt("Scene.TrainingB")));
			//}
			if (isChangeScene)
			{
				isChangeScene = false;
				procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, NextSceneId);
				RuntimeProcedure.ChangeProcedure<HotProcedureChangeScene>(procedureOwner);
			}
		}

		public override void OnDestroy(IFsm<IProcedureManager> procedureManager)
		{

		}

		public override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
		{
			HotfixEntry.Event.Unsubscribe(ProcedureChangedEventArgs.EventId, OnChangeScene);
		}

		private void OnChangeScene(object sender, GameEventArgs e)
		{
			ProcedureChangedEventArgs eventArgs = e as ProcedureChangedEventArgs;

			isChangeScene = true;
			NextSceneId = eventArgs.SceneId;
		}
	}
}

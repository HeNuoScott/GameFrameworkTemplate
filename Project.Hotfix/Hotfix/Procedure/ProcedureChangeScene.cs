using GameFramework.DataTable;
using GameFramework.Fsm;
using GameFramework.Procedure;
using Sirius.Runtime;
using UnityGameFramework.Runtime;
using GameEntry = Sirius.Runtime.GameEntry;
using GameFramework.Event;

namespace Project.Hotfix
{	
	//切换场景的流程
	public class ProcedureChangeScene : ProcedureLogic
    {
	    private int sceneId = 0;  //菜单场景编号
	
	    private bool m_IsChangeSceneComplete = false;   //切换场景是否完成的标志位
	    private int m_BackgroundMusicId = 0;    //切换场景时的背景音乐
		
	    //流程进入回调
	    public override void OnEnter(IFsm<IProcedureManager> procedureOwner)
	    {	
	        m_IsChangeSceneComplete = false;
	        //订阅事件
	        GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
	        GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
	        GameEntry.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
	        GameEntry.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
	
	        //停止所有声音
	        GameEntry.Sound.StopAllLoadingSounds(); //停止正在加载所有声音
	        GameEntry.Sound.StopAllLoadedSounds();  //停止加载完成的声音
	
	        //隐藏所有实体
	        GameEntry.Entity.HideAllLoadingEntities();  //加载中的实体
	        GameEntry.Entity.HideAllLoadedEntities();   //加载完成的实体

			//关闭所有UI
			GameEntry.UI.CloseAllLoadedUIForms();	//关闭所有已加载的界面。
			GameEntry.UI.CloseAllLoadingUIForms();	//关闭所有正在加载的界面。
			 //卸载所有场景
			string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
	        for (int i = 0; i < loadedSceneAssetNames.Length; i++)
	        {
	            GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
	        }
	
	        //还原游戏进度
	        GameEntry.Base.ResetNormalGameSpeed();
	
	        //获取场景数据
	        sceneId = procedureOwner.GetData<VarInt32>(Constant.ProcedureData.NextSceneId).Value;
	        IDataTable<DRScene> dtScene = GameEntry.DataTable.GetDataTable<DRScene>();
	        DRScene drScene = dtScene.GetDataRow(sceneId);
	        if (drScene == null)
	        {
                HotLog.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
	            return;
	        }
	        //加载场景
	        GameEntry.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, this);
	        m_BackgroundMusicId = drScene.BackgroundMusicId;
	    }
	
	    //流程离开的回调
	    public override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
	    {
	        //取消订阅时间
	        GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
	        GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
	        GameEntry.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
	        GameEntry.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
	    }

		public override void OnUpdate(IFsm<IProcedureManager> procedureOwner)
		{
			if (!m_IsChangeSceneComplete)   //检查是否完成
				return;

			//清空对象池
			GameEntry.ObjectPool.ReleaseAllUnused();

			//切换流程
			switch (sceneId)
			{
				case 1: RuntimeProcedure.ChangeProcedure<HotProcedureLobby>(procedureOwner); break;
				case 2: RuntimeProcedure.ChangeProcedure<HotProcedureTrainingGroundA>(procedureOwner); break;
				case 3: RuntimeProcedure.ChangeProcedure<HotProcedureTrainingGroundB>(procedureOwner); break;
				default: break;
			}
		}

        public override void OnDestroy(IFsm<IProcedureManager> procedureManager)
        {

        }

        //加载场景成功的回调
        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
	    {
	        LoadSceneSuccessEventArgs args = e as LoadSceneSuccessEventArgs;
	        if (args.UserData != this)
	            return;

            HotLog.Info("Load scene '{0}' OK.", args.SceneAssetName);
	        if (m_BackgroundMusicId > 0)
	            GameEntry.Sound.PlayMusic(m_BackgroundMusicId);
	
	        m_IsChangeSceneComplete = true; //加载完成的标志位
	    }
	
	    //加载场景失败的回调
	    private void OnLoadSceneFailure(object sender, GameEventArgs e)
	    {
	        LoadSceneFailureEventArgs args = e as LoadSceneFailureEventArgs;
	        if (args.UserData != this)
	            return;

            HotLog.Error("Load scene '{0}' failure, error message '{1}'.", args.SceneAssetName, args.ErrorMessage);
	    }
	
	    //加载场景更新的回调
	    private void OnLoadSceneUpdate(object sender, GameEventArgs e)
	    {
	        LoadSceneUpdateEventArgs args = e as LoadSceneUpdateEventArgs;
	        if (args.UserData != this)
	            return;

            HotLog.Info("Load scene '{0}' update, progress '{1}'.", args.SceneAssetName, args.Progress.ToString("P2"));
	    }
	
	    //加载场景时加载依赖资源的回调
	    private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
	    {
	        LoadSceneDependencyAssetEventArgs ne = e as LoadSceneDependencyAssetEventArgs;
	        if (ne.UserData != this)
	            return;

            HotLog.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
	    }

    }
}

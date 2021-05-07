using Sirius.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameEntry = Sirius.Runtime.GameEntry;

namespace Project.Hotfix
{	
	/// <summary>
	/// 菜单界面
	/// </summary>
	public class MenuForm : UIFormBase
	{
	    [SerializeField]
	    private GameObject m_QuitButton = null; //退出按钮

		[SerializeField]
		private GameObject m_StartButton = null; //开始按钮

		private ProcedureLobby m_ProcedureMenu = null;   //菜单流程管理
	
	    //点击开始游戏按钮
	    public void OnStartButtonClick()
	    {
            HotLog.Info("On Click Start Button");
			int sceneId = GameEntry.Config.GetInt("Scene.TrainingA");
			HotLog.Debug(sceneId);
			HotfixEntry.Event.Fire(this, ProcedureChangedEventArgs.Fill(sceneId));
			//GameEntry.UI.OpenUIForm(UIFormID.SettingForm);
		}

	    //点击退出按钮
	    public void OnQuitButtonClick()
	    {
            HotLog.Info("On Click Quit Button");
			GameEntry.ApplicationQuit();
		}

		public override void OnInit(object userData)
        {
            base.OnInit(userData);

            ReferenceCollector collector = RuntimeUIForm.ReferenceCollector;
            m_QuitButton = collector.GetGO("Quit");
            m_QuitButton.GetComponent<Button>().onClick.AddListener(OnQuitButtonClick);

			m_StartButton = collector.GetGO("Start");
			m_StartButton.GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
		}

        public override void OnOpen(object userData)
	    {	
	        m_ProcedureMenu = userData as ProcedureLobby;
	        if(m_ProcedureMenu == null)
	        {
                HotLog.Warning("ProcedureMenu is invalid when open MenuForm.");
	            return;
	        }

        }

        public override void OnClose(object userData)
	    {
	        m_ProcedureMenu = null;
	    }
	
	}
}

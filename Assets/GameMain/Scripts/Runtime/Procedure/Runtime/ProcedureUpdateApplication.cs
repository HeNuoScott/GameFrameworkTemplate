// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/20   17:19:10
// -----------------------------------------------
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using System.IO;

namespace Sirius.Runtime
{
    public class ProcedureUpdateApplication : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }
        private int DownloadId = 0;
        private float Progress = 0;
        private bool isDownloading = false;
        private string DownloadPath = "";
        private UpdateResourceForm m_UpdateResourceForm = null;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(DownloadStartEventArgs.EventId, OnDownloadStart);
            GameEntry.Event.Subscribe(DownloadUpdateEventArgs.EventId, OnDownloadChanged);
            GameEntry.Event.Subscribe(DownloadSuccessEventArgs.EventId, OnDownloadSuccess);
            GameEntry.Event.Subscribe(DownloadFailureEventArgs.EventId, OnDownloadFailure);
            Progress = 0;
            isDownloading = false;
            m_UpdateResourceForm = null;

            if (m_UpdateResourceForm == null)
            {
                m_UpdateResourceForm = Object.Instantiate(GameEntry.BuiltinData.UpdateResourceFormTemplate);
            }

            string UpdateApplicationUri = procedureOwner.GetData<VarString>("UpdateApplicationUri");
            procedureOwner.RemoveData("VersionListLength");
            DownloadPath = string.Format("{0}/{1}", Application.persistentDataPath, Path.GetFileName(UpdateApplicationUri));
            DownloadId = GameEntry.Download.AddDownload(DownloadPath, UpdateApplicationUri);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (isDownloading)
            {
                Progress += elapseSeconds * 0.01f;
                if (Progress >= 1) Progress = 0;
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if (m_UpdateResourceForm != null)
            {
                Object.Destroy(m_UpdateResourceForm.gameObject);
                m_UpdateResourceForm = null;
            }

            GameEntry.Event.Unsubscribe(DownloadStartEventArgs.EventId, OnDownloadStart);
            GameEntry.Event.Unsubscribe(DownloadUpdateEventArgs.EventId, OnDownloadChanged);
            GameEntry.Event.Unsubscribe(DownloadSuccessEventArgs.EventId, OnDownloadSuccess);
            GameEntry.Event.Unsubscribe(DownloadFailureEventArgs.EventId, OnDownloadFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }

        private void OnDownloadStart(object sender, GameEventArgs e)
        {
            DownloadStartEventArgs ne = (DownloadStartEventArgs)e;
            if (ne.SerialId != DownloadId) return;
            isDownloading = true;
            Progress = 0;
            RefreshProgress(Progress, "程序开始更新下载!!!");
        }

        private void OnDownloadChanged(object sender, GameEventArgs e)
        {
            DownloadUpdateEventArgs ne = (DownloadUpdateEventArgs)e;
            if (ne.SerialId != DownloadId) return;
            string byteLength = GetByteLengthString(ne.CurrentLength);
            RefreshProgress(Progress, $"已经下载 {byteLength}");
        }

        private void OnDownloadSuccess(object sender, GameEventArgs e)
        {
            DownloadSuccessEventArgs ne = (DownloadSuccessEventArgs)e;
            if (ne.SerialId != DownloadId) return;
            string byteLength = GetByteLengthString(ne.CurrentLength);
            isDownloading = false;
            Progress = 1f;
            RefreshProgress(Progress, $"下载完成 {byteLength}");

            // 需要强制更新游戏应用
            GameEntry.UI.OpenDialog(new DialogParams
            {
                Mode = 2,
                Title = "下载成功提示",
                Message = "是否安装应用程序",
                ConfirmText = "确认安装",
                OnClickConfirm = DownloadSuccess,
                CancelText = "放弃安装",
                OnClickCancel = delegate (object userData) { GameEntry.ApplicationQuit(); },
            });
        }

        private void OnDownloadFailure(object sender, GameEventArgs e)
        {
            DownloadFailureEventArgs ne = (DownloadFailureEventArgs)e;
            if (ne.SerialId != DownloadId) return;
            isDownloading = false;
            Progress = 1f;
            RefreshProgress(Progress, "下载失败");

            // 需要强制更新游戏应用
            GameEntry.UI.OpenDialog(new DialogParams
            {
                Mode = 2,
                Title = "下载失败提示",
                Message = "下载失败请退出程序",
                ConfirmText = "退出",
                OnClickConfirm = DownloadFailure,
                CancelText = "退出",
                OnClickCancel = delegate (object userData) { GameEntry.ApplicationQuit(); },
            });
        }

        private void RefreshProgress(float progress,string description)
        {
            m_UpdateResourceForm.SetProgress(progress, description);
        }

        private string GetByteLengthString(long byteLength)
        {
            if (byteLength < 1024L) // 2 ^ 10
            {
                return Utility.Text.Format("{0} Bytes", byteLength.ToString());
            }

            if (byteLength < 1048576L) // 2 ^ 20
            {
                return Utility.Text.Format("{0} KB", (byteLength / 1024f).ToString("F2"));
            }

            if (byteLength < 1073741824L) // 2 ^ 30
            {
                return Utility.Text.Format("{0} MB", (byteLength / 1048576f).ToString("F2"));
            }

            if (byteLength < 1099511627776L) // 2 ^ 40
            {
                return Utility.Text.Format("{0} GB", (byteLength / 1073741824f).ToString("F2"));
            }

            if (byteLength < 1125899906842624L) // 2 ^ 50
            {
                return Utility.Text.Format("{0} TB", (byteLength / 1099511627776f).ToString("F2"));
            }

            if (byteLength < 1152921504606846976L) // 2 ^ 60
            {
                return Utility.Text.Format("{0} PB", (byteLength / 1125899906842624f).ToString("F2"));
            }

            return Utility.Text.Format("{0} EB", (byteLength / 1152921504606846976f).ToString("F2"));
        }

        private void DownloadSuccess(object userData)
        {
            using (AndroidJavaClass javaClass = new AndroidJavaClass("example.administrator.myapplication.MainActivity"))
            {
                //然后调用android来安装apk
                javaClass.CallStatic<bool>("installAPK", DownloadPath);
            }
        }

        private void DownloadFailure(object userData)
        {
            GameEntry.ApplicationQuit();
        }
    }

}
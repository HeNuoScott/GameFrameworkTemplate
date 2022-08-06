//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityGameFramework.Runtime;
using UnityEngine.UI;
using GameFramework;
using UnityEngine;

namespace GameFrame.Main
{
    public class NativeDialogForm : MonoBehaviour
    {
        [SerializeField]
        private Text m_TitleText = null;

        [SerializeField]
        private Text m_MessageText = null;

        [SerializeField]
        private GameObject[] m_ModeObjects = null;

        [SerializeField]
        private Text[] m_ConfirmTexts = null;

        [SerializeField]
        private Text[] m_CancelTexts = null;

        [SerializeField]
        private Text[] m_OtherTexts = null;

        private int m_DialogMode = 1;
        private bool m_PauseGame = false;
        private object m_UserData = null;
        private GameFrameworkAction<object> m_OnClickConfirm = null;
        private GameFrameworkAction<object> m_OnClickCancel = null;
        private GameFrameworkAction<object> m_OnClickOther = null;

        public void OnConfirmButtonClick()
        {
            m_OnClickConfirm?.Invoke(m_UserData);
            Close();
        }

        public void OnCancelButtonClick()
        {
            m_OnClickCancel?.Invoke(m_UserData);
            Close();
        }

        public void OnOtherButtonClick()
        {
            m_OnClickOther?.Invoke(m_UserData);
            Close();
        }

        public void OnOpen(DialogParams dialogParams)
        {
            if (dialogParams == null)
            {
                Log.Warning("DialogParams is invalid.");
                return;
            }

            m_DialogMode = dialogParams.Mode;
            RefreshDialogMode();

            m_TitleText.text = dialogParams.Title;
            m_MessageText.text = dialogParams.Message;

            m_PauseGame = dialogParams.PauseGame;
            RefreshPauseGame();

            m_UserData = dialogParams.UserData;

            RefreshConfirmText(dialogParams.ConfirmText);
            m_OnClickConfirm = dialogParams.OnClickConfirm;

            RefreshCancelText(dialogParams.CancelText);
            m_OnClickCancel = dialogParams.OnClickCancel;

            RefreshOtherText(dialogParams.OtherText);
            m_OnClickOther = dialogParams.OnClickOther;
        }

        private void Close()
        {
            if (m_PauseGame)
            {
                GameEntry.Base.ResumeGame();
            }

            m_DialogMode = 1;
            m_TitleText.text = string.Empty;
            m_MessageText.text = string.Empty;
            m_PauseGame = false;
            m_UserData = null;

            RefreshConfirmText(string.Empty);
            m_OnClickConfirm = null;

            RefreshCancelText(string.Empty);
            m_OnClickCancel = null;

            RefreshOtherText(string.Empty);
            m_OnClickOther = null;

            Destroy(this.gameObject);
        }

        private void RefreshDialogMode()
        {
            for (int i = 1; i <= m_ModeObjects.Length; i++)
            {
                m_ModeObjects[i - 1].SetActive(i == m_DialogMode);
            }
        }

        private void RefreshPauseGame()
        {
            if (m_PauseGame)
            {
                GameEntry.Base.PauseGame();
            }
        }

        private void RefreshConfirmText(string confirmText)
        {
            for (int i = 0; i < m_ConfirmTexts.Length; i++)
            {
                m_ConfirmTexts[i].text = confirmText;
            }
        }

        private void RefreshCancelText(string cancelText)
        {
            for (int i = 0; i < m_CancelTexts.Length; i++)
            {
                m_CancelTexts[i].text = cancelText;
            }
        }

        private void RefreshOtherText(string otherText)
        {
            for (int i = 0; i < m_OtherTexts.Length; i++)
            {
                m_OtherTexts[i].text = otherText;
            }
        }
    }
}

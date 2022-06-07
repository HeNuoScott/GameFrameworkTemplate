using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain;

namespace GameHotFix
{
    public class UGUIForm : UGuiForm
    {
        public void PlayUISound(int uiSoundId)
        {
            GameEntry.Sound.PlayUISound(uiSoundId);
        }
    }
}
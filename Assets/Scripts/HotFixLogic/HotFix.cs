using UnityEngine;
using GameHotFix;

public class HotFix
{

    public static void GameEntry()
    {
        Debug.Log("HotFixLogic Init");

        HotFixEntry.Start();
    }
}

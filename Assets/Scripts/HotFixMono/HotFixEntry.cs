using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameFramework.Resource;
using System.Collections;
using UnityEngine;

namespace Sirius
{
    public class HotFixEntry : MonoBehaviour
    {
        public const string HPBarName = "HP Bar";

        public static HPBarComponent HPBar
        {
            get;
            private set;
        }

        public static CustomProcedureComponent Procedure
        {
            get;
            private set;
        }

        public static void LoadPreFabs()
        {
            GameEntry.AddComponent<HotFixEntry>();
            GameEntry.AddComponent<CustomProcedureComponent>();
            GameEntry.Huatuo.LoadPreFab(HPBarName);
        }

        public static void InitCustomComponents()
        {
            HPBar = UnityGameFramework.Runtime.GameEntry.GetComponent<HPBarComponent>();
            Procedure = UnityGameFramework.Runtime.GameEntry.GetComponent<CustomProcedureComponent>();

            Procedure.Run();
        }

    }
}
// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/7/1   13:1:53
// -----------------------------------------------
// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2021/5/26   15:44:41
// -----------------------------------------------
using GameFramework.Event;

namespace GameFrame.Hotfix
{

    public class ProcedureChangedEventArgs : GameEventArgs
    {
        public static int EventId = typeof(ProcedureChangedEventArgs).GetHashCode();
        public override int Id { get { return EventId; } }


        public string SceneName
        {
            private set;
            get;
        }

        public override void Clear()
        {
            SceneName = default;
        }

        public static ProcedureChangedEventArgs Fill(string sceneName)
        {
            ProcedureChangedEventArgs tempEventArgs = GameFramework.ReferencePool.Acquire<ProcedureChangedEventArgs>();
            tempEventArgs.SceneName = sceneName;
            return tempEventArgs;
        }
    }
}

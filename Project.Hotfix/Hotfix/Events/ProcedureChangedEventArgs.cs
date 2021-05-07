using Project.Hotfix.Reference;
using Project.Hotfix.Event;

namespace Project.Hotfix
{
    public sealed class ProcedureChangedEventArgs : GameEventArgs
    {
        public static int EventId = typeof(ProcedureChangedEventArgs).GetHashCode();
        public override int Id { get { return EventId; } }

        public int SceneId
        {
            private set;
            get;
        }

        public override void Clear()
        {
            SceneId = default;
        }

        public static ProcedureChangedEventArgs Fill(int sceneId)
        {
            ProcedureChangedEventArgs tempEventArgs = ReferencePool.Acquire<ProcedureChangedEventArgs>();
            tempEventArgs.SceneId = sceneId;
            return tempEventArgs;
        }
    }
}

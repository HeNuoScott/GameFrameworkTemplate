using GameFramework.Event;
using UnityEngine;

namespace Sirius
{
    //1.创建事件参数
    public sealed class TempEventArgs : GameEventArgs
    {
        public static int EventId = typeof(TempEventArgs).GetHashCode();
        public override int Id { get { return EventId; } }

        // 自定义事件参数
        public string EventName
        {
            private set;
            get;
        }

        // 清空事件
        public override void Clear()
        {
            EventName = default(string);
        }

        // 自定义构造函数  填充函数
        public static TempEventArgs Fill(string eventName)
        {
            TempEventArgs tempEventArgs = GameFramework.ReferencePool.Acquire<TempEventArgs>();
            tempEventArgs.EventName = eventName;
            return tempEventArgs;
        }
    }

    public class EventTemp : MonoBehaviour
    {
        private void Start()
        {
            // 注册事件
            GameEntry.Event.Subscribe(TempEventArgs.EventId, OnFireTempEventArgs);
            // 引发事件(安全线程)
            GameEntry.Event.Fire(this, TempEventArgs.Fill("这是一个事件模板,安全线程引发"));
            // 引发事件(安全线程)
            GameEntry.Event.FireNow(this, TempEventArgs.Fill("这是一个事件模板,立即分发"));
            // 注销事件
            GameEntry.Event.Unsubscribe(TempEventArgs.EventId, OnFireTempEventArgs);
        }

        private void OnFireTempEventArgs(object sender, GameEventArgs e)
        {
            TempEventArgs tempEventArgs = (TempEventArgs)e;
            UnityGameFramework.Runtime.Log.Info("接收到事件{0}", tempEventArgs.EventName);
        }
    }
}
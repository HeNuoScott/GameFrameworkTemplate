
namespace Project.Hotfix.Event
{
    //
    // 摘要:
    //     表示当事件提供数据时将处理该事件的方法。
    //
    // 参数:
    //   sender:
    //     事件源。
    //
    //   e:
    //     包含事件数据的对象。
    //
    // 类型参数:
    //   TEventArgs:
    //     事件生成的事件数据的类型。
    public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);

    public class EventArgs
    {
        public static readonly EventArgs Empty;

        public EventArgs() { }
    }
}

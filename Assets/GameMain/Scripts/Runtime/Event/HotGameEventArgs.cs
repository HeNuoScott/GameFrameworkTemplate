// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/6   17:18:27
// -----------------------------------------------
using GameFramework.Event;

public class HotGameEventArgs : GameEventArgs
{
    public override int Id 
    {
        get
        {
           return GetId();
        }
    }

    public virtual int GetId()
    {
        return 0;
    }

    public override void Clear() { }
}

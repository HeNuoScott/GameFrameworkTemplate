// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/9   18:30:34
// -----------------------------------------------

using GameFrame.Main;

namespace GameFrame.Hotfix
{
    /// <summary>
    /// 握手消息包
    /// </summary>
    public class CSHandshake : CSPacketBase
    {
        public override int Id
        {
            get
            {
                return 0;
            }
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}
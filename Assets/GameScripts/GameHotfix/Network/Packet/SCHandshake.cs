// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/9   17:24:6
// -----------------------------------------------

using GameFrame.Main;
using System;

namespace GameFrame.Hotfix
{
    /// <summary>
    /// 握手消息包
    /// </summary>
    public class SCHandshake : SCPacketBase
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
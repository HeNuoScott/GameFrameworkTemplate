//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityGameFramework.Runtime;
using GameFramework.Network;
using GameFrame.Main;

namespace GameFrame.Hotfix
{
    public class SCHeartBeatHandler : PacketHandlerBase
    {
        public override int Id
        {
            get
            {
                return 2;
            }
        }

        public override void Handle(object sender, Packet packet)
        {
            SCHeartBeat packetImpl = (SCHeartBeat)packet;
            Log.Info("Receive packet '{0}'.", packetImpl.Id.ToString());
        }
    }
}

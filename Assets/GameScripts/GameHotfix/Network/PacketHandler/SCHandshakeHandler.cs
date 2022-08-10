// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/9   14:34:50
// -----------------------------------------------

using UnityGameFramework.Runtime;
using GameFramework.Network;
using GameFrame.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Hotfix
{
    // 握手消息助手
    public class SCHandshakeHandler : PacketHandlerBase
    {
        public override int Id
        {
            get
            {
                return 0;
            }
        }

        public override void Handle(object sender, Packet packet)
        {
            SCHandshake handshake = (SCHandshake)packet;
            INetworkChannel m_Channel = sender as INetworkChannel;
            CSHandshake msg = GameFramework.ReferencePool.Acquire<CSHandshake>();

            /* 握手消息协议
             ------------------------------------------------------------
            | len |*****主消息*****|*****子消息*****|*****附加消息*****|
              4b          4b              4b             len-12b
              4b          1              8529      |     0(4b) +  fasle(4b) + token  
             ------------------------------------------------------------
             */
            msg.MainType = 1;
            msg.SubType = 8529;
            //msg.Content = BitConverter.GetBytes(3);
            List<byte> m_Content = new List<byte>();

            m_Content.AddRange(BitConverter.GetBytes(0));
            m_Content.AddRange(BitConverter.GetBytes(false));
            m_Content.AddRange(handshake.Content);
            msg.Content = m_Content.ToArray();
            m_Channel.Send(msg);

            Log.Info("收到握手消息");
        }
    }
}
//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Main
{
    public abstract class SCPacketBase : PacketBase
    {
        #region 附加数据
        private int m_MainType = 0;
        private int m_SubType = 0;
        private List<byte> m_Content;

        public int MainType
        {
            get
            {
                return m_MainType;
            }
            set
            {
                m_MainType = value;
            }
        }
        public int SubType
        {
            get
            {
                return m_SubType;
            }
            set
            {
                m_SubType = value;
            }
        }
        public byte[] Content
        {
            get
            {
                return m_Content.ToArray();
            }
            set
            {
                m_Content = value.ToList();
            }
        }
        #endregion
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ServerToClient;
            }
        }

        public override void Clear()
        {
            m_MainType = 0;
            m_SubType = 0;
            m_Content.Clear();
        }
    }
}

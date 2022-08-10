using GameFrame.Main;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameFrame.Hotfix
{
    public class NetworkTest : MonoBehaviour
    {
        public bool isRuning = false;
        private GameFramework.Network.INetworkChannel m_Channel;
        private NetworkTestChannelHelper m_NetworkChannelHelper;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Init();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isRuning)
                {
                    isRuning = false;
                    m_Channel.Close();
                    Debug.Log("关闭了 <NetworkTest> 网络连接");
                    // 获取框架网络组件
                    NetworkComponent Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent>();
                    Network.DestroyNetworkChannel("NetworkTest");
                }
            }
        }

        private void OnDestroy()
        {
            if (isRuning)
            {
                Debug.Log("关闭了 <NetworkTest> 网络连接");
                m_Channel.Close();
            }
        }

        private void Init()
        {
            if (isRuning) return;

            isRuning = true;
            // 获取框架网络组件
            NetworkComponent Network = UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent>();
            // 创建频道
            m_NetworkChannelHelper = new NetworkTestChannelHelper();
            m_Channel = Network.CreateNetworkChannel("NetworkTest", GameFramework.Network.ServiceType.TcpWithSyncReceive, m_NetworkChannelHelper);
            // 连接服务器
            m_Channel.Connect(IPAddress.Parse("192.168.99.58"), 8001);
        }

        public void Send<T>(T packet) where T : CSPacketBase
        {
            m_Channel.Send(packet);
        }
    }
}
// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/5/26   14:52:33
// -----------------------------------------------

using UnityGameFramework.Runtime;
using GameFramework.Event;
using System.Net.Sockets;
using UnityEngine.UI;
using GameFramework;
using UnityEngine;
using System.Net;
using System;

namespace Sirius
{
    public class ResetIPForm : MonoBehaviour
    {
        public const string ConnectIP = "ConnectIP";
        public class CheckIP { }
        public Action<string> OnClick;
        public InputField inputField = null;
        public Button button_Start = null;
        public Button button_Quit = null;
        public Text text_Tips = null;
        private string stringToEdit = string.Empty;

        private void Start()
        {
            GameEntry.Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
            GameEntry.Event.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
            inputField.onValueChanged.AddListener((str) =>
            {
                stringToEdit = str.Trim();
            });
            button_Start.onClick.AddListener(ExecuteCheck);
            button_Quit.onClick.AddListener(() => UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit));
            SetIPInit();
        }

        public void SetIPInit()
        {
            bool isConnectIP = GameEntry.Setting.HasSetting(ConnectIP);
            if (!isConnectIP)
            {
                inputField.text = stringToEdit = GetLocalIP();
                GameEntry.Setting.SetString(ConnectIP, stringToEdit);
                GameEntry.Setting.Save();
            }
            else
            {
                inputField.text = stringToEdit = GameEntry.Setting.GetString(ConnectIP);
            }
        }

        private void OnDestroy()
        {
            GameEntry.Event.Unsubscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
            GameEntry.Event.Unsubscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
            OnClick = null;
        }

        private void ExecuteCheck()
        {
            if (string.IsNullOrEmpty(stringToEdit)) return;
            
            string Platform = GetPlatformPath();
            string Url = GetCheckVersionUrl(stringToEdit, Platform);
            if (Url == string.Empty)
            {
                text_Tips.gameObject.SetActive(true);
                return;
            }
            button_Start.interactable = false;
            // 向服务器请求版本信息
            GameEntry.WebRequest.AddWebRequest(Utility.Text.Format(Url, Platform), new CheckIP());
        }

        private void OnWebRequestSuccess(object sender, GameEventArgs e)
        {
            WebRequestSuccessEventArgs ne = (WebRequestSuccessEventArgs)e;
            bool isThisRequest = ne.UserData is CheckIP;
            if (!isThisRequest) return;
            OnClick?.Invoke(stringToEdit);
        }

        private void OnWebRequestFailure(object sender, GameEventArgs e)
        {
            WebRequestFailureEventArgs ne = (WebRequestFailureEventArgs)e;
            bool isThisRequest = ne.UserData is CheckIP;
            if (!isThisRequest) return;
            text_Tips.gameObject.SetActive(true);
            button_Start.interactable = true;
            return;
        }

        private string GetPlatformPath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "MacOS";

                case RuntimePlatform.IPhonePlayer:
                    return "IOS";

                case RuntimePlatform.Android:
                    return "Android";

                default:
                    throw new System.NotSupportedException(Utility.Text.Format("Platform '{0}' is not supported.", Application.platform.ToString()));
            }
        }
     
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                Log.Error("获取本机IP出错:" + ex.Message);
                return "";
            }
        }

        public static string GetCheckVersionUrl(string ip, string platform)
        {
            //"CheckVersionUrl": "http://10.21.0.49:8880/ServerAssetbundle/Windows/version.txt",
            string Url = string.Empty;
            if (platform == "Android")
            {
                Url = string.Format("http://{0}:8880/ServerAssetbundle/Android/version.txt", ip);
            }
            else if (platform == "Windows")
            {
                Url = string.Format("http://{0}:8880/ServerAssetbundle/Windows/version.txt", ip);
            }
            return Url;
        }
    }
}
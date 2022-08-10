// -----------------------------------------------
// Copyright © GameFramework. All rights reserved.
// CreateTime: 2022/8/9   14:54:37
// -----------------------------------------------

using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using System.Linq;
using System.Text;
using System;

namespace GameFrame.Main
{
    public class MessagePacketConverter : IReference
    {
        public static readonly int MSG_LEN_MAX = 64 * 1024;// 32*1048576;//4194304;            // 1M
        public static readonly int MSG_LEN_DEFAULT = 1024;
        public static readonly int LEN_LEN = 4;
        public static readonly int Main_LEN = 4;
        public static readonly int Sub_LEN = 4;

        #region 字段
        private int m_len = 0;
        private int m_position = 0;
        private int m_MainType = 0;
        private int m_SubType = 0;
        private List<byte> m_Content = new List<byte>(MSG_LEN_DEFAULT);
        #endregion

        #region 属性
        public int Len
        {
            get
            {
                return m_len;
            }
            set
            {
                m_len = value;
            }
        }
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

        #region 私有方法
        //打包
        public byte[] Encode()
        {
            var tempList = new List<byte>();
            var lenBytes = GetMessageLenBytes();
            var mainBytes = GetMainTypeBytes();//消息类型
            var subBytes = GetSubTypeBytes();//消息类型
            tempList.AddRange(lenBytes);
            tempList.AddRange(mainBytes);
            tempList.AddRange(subBytes);
            tempList.AddRange(Content);

            return tempList.ToArray();
        }

        /// <summary>
        /// 在此函数中消息质保函
        /// | len |*****主消息*****|*****子消息*****|*****附加消息*****|
        ///   4b          4b              4b             len-12b
        /// </summary>
        /// <param name="bytes">|*****主消息*****|*****子消息*****|*****附加消息*****|</param>
        /// <returns></returns>
        public bool Decode(byte[] bytes)
        {
            m_position = 0;
            // 获取长度
            Len = DecodeLen(bytes);
            if (Len > MSG_LEN_MAX)
            {
                Debug.LogError(string.Format("[Error] Message.Decode, Len > {0}", MessagePacketConverter.MSG_LEN_MAX));
                return false;
            }
            m_position += LEN_LEN;
            m_MainType = DecodeMainType(bytes);
            m_position += Main_LEN;
            m_SubType = DecodeSubType(bytes);
            m_position += Sub_LEN;
            Content = DecodeContent(bytes);

            return true;
        }
        public bool Decode(int len, byte[] bytes)
        {
            // 获取长度
            if (m_len > MSG_LEN_MAX)
            {
                Debug.LogError(string.Format("[Error] Message.Decode, Len > {0}", MessagePacketConverter.MSG_LEN_MAX));
                return false;
            }

            m_position = 0;
            Len = len;
            m_MainType = DecodeMainType(bytes);
            m_position += Main_LEN;
            m_SubType = DecodeSubType(bytes);
            m_position += Sub_LEN;
            Content = DecodeContent(bytes);
            //byte[] realLenBytes = BitConverter.GetBytes(len);
            //Content = new byte[LEN_LEN + bytes.Length];
            //List<byte> tempList = new List<byte>();
            //tempList.AddRange(realLenBytes.ToArray());
            //tempList.AddRange(bytes.ToArray());
            //Content = tempList.ToArray();
            return true;
        }

        /// <summary>
        /// 解析包长
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private int DecodeLen(byte[] bytes)
        {
            byte[] lenBytes = new byte[LEN_LEN];
            bytes.ToList().CopyTo(m_position, lenBytes, 0, LEN_LEN);
            int ret = BitConverter.ToInt32(lenBytes, 0);
            return ret;
        }
        /// <summary>
        /// 解析主消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private int DecodeMainType(byte[] bytes)
        {
            byte[] cmdBytes = new byte[Main_LEN];
            bytes.ToList().CopyTo(m_position, cmdBytes, 0, Main_LEN);
            return BitConverter.ToInt32(cmdBytes, 0);
        }
        /// <summary>
        /// 解析子消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private int DecodeSubType(byte[] bytes)
        {
            byte[] cmdBytes = new byte[Main_LEN];
            bytes.ToList().CopyTo(m_position, cmdBytes, 0, Main_LEN);
            return BitConverter.ToInt32(cmdBytes, 0);
        }
        /// <summary>
        /// 解析消息体
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private byte[] DecodeContent(byte[] bytes)
        {
            int msgLen = bytes.Length - m_position;
            byte[] msgBytes = new byte[msgLen];
            bytes.ToList().CopyTo(m_position, msgBytes, 0, msgLen);
            return msgBytes;
        }

        private byte[] GetMessageLenBytes()
        {
            //计算长度
            var len = Convert.ToInt32(Main_LEN + Sub_LEN + Content.Length);
            var realLenBytes = BitConverter.GetBytes(len);
            return realLenBytes;
        }

        private byte[] GetMainTypeBytes()
        {
            var cmdValue = Convert.ToInt32(m_MainType);
            var realCmdBytes = BitConverter.GetBytes(cmdValue);
            return realCmdBytes;
        }

        private byte[] GetSubTypeBytes()
        {
            var cmdValue = Convert.ToInt32(m_SubType);
            var realCmdBytes = BitConverter.GetBytes(cmdValue);
            return realCmdBytes;
        }
        #endregion

        #region 公有方法
        public void AddInt(int param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddLong(long param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddFloat(float param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddShort(short param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddChar(char param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddString(string param)
        {
            var bytes = Encoding.UTF8.GetBytes(param);
            var len = Convert.ToInt16(bytes.Length);
            var strLenBytes = BitConverter.GetBytes(len);

            m_Content.AddRange(strLenBytes);
            m_Content.AddRange(bytes);

            m_len += 2;
            m_len += bytes.Length;
        }

        public void AddBytes(byte[] bytes)
        {
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public void AddBool(bool param)
        {
            var bytes = BitConverter.GetBytes(param);
            m_Content.AddRange(bytes);
            m_len += bytes.Length;
        }

        public int GetInt()
        {
            var intLen = 4;// sizeof(int);
            var bytes = m_Content.GetRange(m_position, intLen).ToArray();
            m_position += intLen;
            return BitConverter.ToInt32(bytes, 0);
        }

        public long GetLong()
        {
            var longLen = 8;// sizeof(long);
            var bytes = m_Content.GetRange(m_position, longLen).ToArray();
            m_position += longLen;
            return BitConverter.ToInt64(bytes, 0);
        }

        public float GetFloat()
        {
            var floatLen = 4;//;sizeof(float);
            var bytes = m_Content.GetRange(m_position, floatLen).ToArray();
            m_position += floatLen;
            return BitConverter.ToSingle(bytes, 0);
        }

        public short GetShort()
        {
            var shortLen = 2;// sizeof(short);
            var bytes = m_Content.GetRange(m_position, shortLen).ToArray();
            m_position += shortLen;
            return BitConverter.ToInt16(bytes, 0);
        }

        public char GetChar()
        {
            var charLen = 1;// sizeof(char);
            var bytes = m_Content.GetRange(m_position, charLen).ToArray();
            m_position += charLen;
            return BitConverter.ToChar(bytes, 0);
        }

        public string GetString()
        {
            //先获取长度
            var strLenBytes = m_Content.GetRange(m_position, 2).ToArray();
            m_position += 2;//sizeof(Int16);
            var strLen = BitConverter.ToInt16(strLenBytes, 0);

            //获取字符串
            var stringBytes = m_Content.GetRange(m_position, strLen).ToArray();
            m_position += strLen;

            return Encoding.UTF8.GetString(stringBytes);
        }

        public bool GetBool()
        {
            var boolLen = 1;// sizeof(bool);
            var bytes = m_Content.GetRange(m_position, boolLen).ToArray();
            m_position += boolLen;
            return BitConverter.ToBoolean(bytes, 0);
        }

        public byte[] GetBytes()
        {
            var bytesLen = m_len - 8;
            var bytes = m_Content.GetRange(m_position, bytesLen).ToArray();
            m_position += bytesLen;
            return bytes;
        }

        public void Clear()
        {
            m_len = 0;
            m_position = 0;
            m_MainType = 0;
            m_SubType = 0;
            m_Content.Clear();
        }
        #endregion

    }
}
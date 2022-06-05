using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Sirius
{
    [StructLayout(LayoutKind.Auto)]
    public struct CampPair
    {
        private readonly CampType m_First;
        private readonly CampType m_Second;

        public CampPair(CampType first, CampType second)
        {
            m_First = first;
            m_Second = second;
        }

        public CampType First
        {
            get
            {
                return m_First;
            }
        }

        public CampType Second
        {
            get
            {
                return m_Second;
            }
        }
    }
}
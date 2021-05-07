// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:35:5
// -----------------------------------------------
using System.Reflection;
using UnityEngine;

namespace Sirius.Runtime
{
    /// <summary>
    /// 单列。
    /// </summary>
    /// 
    public class Singleton<T> where T : Singleton<T>
    {
        private static volatile T instance;
        private static object syncRoot = new Object();
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        // 先获取所有非public的构造方法
                        ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                        // 从ctors中获取无参的构造方法
                        ConstructorInfo ctor = System.Array.Find(ctors, c => c.GetParameters().Length == 0);
                        if (ctor == null)
                            throw new System.Exception("Non-public ctor() not found!");
                        // 调用构造方法
                        instance = ctor.Invoke(null) as T;

                        instance.Init();
                    }
                }
                return instance;
            }
        }

        protected virtual void Init()
        {

        }
    }

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T instance;
        private static object syncRoot = new Object();
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            T[] instances = FindObjectsOfType<T>();
                            if (instances != null)
                            {
                                for (var i = 0; i < instances.Length; i++)
                                {
                                    Destroy(instances[i].gameObject);
                                }
                            }
                            GameObject go = new GameObject();
                            go.name = typeof(T).Name;
                            instance = go.AddComponent<T>();
                            DontDestroyOnLoad(go);
                        }
                    }
                }
                return instance;
            }
        }
    }

}
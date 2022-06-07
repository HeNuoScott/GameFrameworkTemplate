// -----------------------------------------------
// Copyright © GameHotFix. All rights reserved.
// CreateTime: 2021/5/26   13:57:22
// -----------------------------------------------
// -----------------------------------------------
// Copyright © GameHotFix. All rights reserved.
// CreateTime: 2021/4/26   10:34:16
// -----------------------------------------------

using System.Collections;
using System.Reflection;
using UnityEngine;
using GameEntry = GameMain.GameEntry;

public static class SCUtil
{
	/// <summary>
	/// Android 调用类
	/// </summary>
	/// <param name="className"></param>
	/// <param name="doing"></param>
	public static void OnAndroidClassCallStatic(string className, System.Action<AndroidJavaClass> doing) {
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		try {
			using (AndroidJavaClass helperCls = new AndroidJavaClass(className)) {
				doing(helperCls);
			}
		} finally {
		}
	}

	/// <summary>
	/// Android调用类,参数带当前的Unity Activity
	/// </summary>
	/// <param name="className"></param>
	/// <param name="doing"></param>
	public static void OnAndroidClassCallStatic(string className, System.Action<AndroidJavaClass, AndroidJavaObject> doing) {
		if (Application.platform != RuntimePlatform.Android) {
			return;
		}
		try {
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
					using (AndroidJavaClass helperCls = new AndroidJavaClass(className)) {
						doing(helperCls, jo);
					}
				}
			}
		} finally {
		}
	}
}

public abstract class SCScheduleBase
{
    protected MonoBehaviour script;
    protected string updateFunctionName;
    protected float delayTime;

    public abstract void beginSchedule();

    public bool IsUpdate()
    {
        return script.IsInvoking(updateFunctionName);
    }

    public void stopSchedule()
    {
        script.CancelInvoke(updateFunctionName);
    }
}

/// <summary>
/// 辅助间隔更新类
/// </summary>
public class SCScheduleRepeating : SCScheduleBase
{
	float rateTime;

	public SCScheduleRepeating(MonoBehaviour mono, string func, float delay, float updateRate) {
		script = mono;
		updateFunctionName = func;
		delayTime = delay;
		rateTime = updateRate;
	}

	public override void beginSchedule() {
		script.InvokeRepeating(updateFunctionName, delayTime, rateTime);
	}
}

/// <summary>
/// 辅助间隔更新类
/// </summary>
public class SCSchedule : SCScheduleBase
{	

	public SCSchedule(MonoBehaviour mono, string func, float delay)
    {
		script = mono;
		updateFunctionName = func;
		delayTime = delay;
	}

	public override void beginSchedule() {
		script.Invoke(updateFunctionName, delayTime);
	}
}

/// <summary>
/// 延时去做
/// </summary>
public class ScheduleOnce
{
	MonoBehaviour _mono;
	IEnumerator _func = null;

    public static ScheduleOnce CreateActive(float t, System.Action doing)
    {

        return new ScheduleOnce(GameEntry.Base, t, doing);
    }

    public static ScheduleOnce CreateActive (MonoBehaviour mono, float t, System.Action doing)
    {

		return new ScheduleOnce (mono, t, doing);
	}

	ScheduleOnce (MonoBehaviour mono, float t, System.Action doing) {
		_mono = mono;
		_func = WaitSomeTimeToDo (t, doing);
		
		_mono.StartCoroutine (_func);
	}

	public bool IsActive () {
		return (_func != null);
	}

	public void Stop () {
		if (_func != null) {
			_mono.StopCoroutine (_func);
			_func = null;
		}
	}

	IEnumerator WaitSomeTimeToDo (float time, System.Action doing) {
		yield return new WaitForSeconds (time);
		doing ();
		_func = null;
	}
}

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
                    if (ctor == null) throw new System.Exception("Non-public ctor() not found!");
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
// -----------------------------------------------
// Copyright © Sirius. All rights reserved.
// CreateTime: 2021/4/26   10:34:16
// -----------------------------------------------

using UnityEngine;
using System.Collections;
using Sirius.Runtime;

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
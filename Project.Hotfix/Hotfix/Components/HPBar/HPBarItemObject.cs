using Project.Hotfix.ObjectPool;
using Project.Hotfix.Reference;
using UnityEngine;

namespace Project.Hotfix.HPBar
{	
	//血条对象
	public class HPBarItemObject : ObjectBase
	{
		public static HPBarItemObject Create(object target)
		{
			HPBarItemObject hpBarItemObject = ReferencePool.Acquire<HPBarItemObject>();
			hpBarItemObject.Initialize(target);
			return hpBarItemObject;
		}

		//释放
		protected internal override void Release(bool isShutdown)
	    {
	        HPBarItem hpBarItem = Target as HPBarItem;
	        if (hpBarItem == null) return;
	
	        Object.Destroy(hpBarItem.ReferenceCollector.CachedGameObject);
	    }
	
	}
}

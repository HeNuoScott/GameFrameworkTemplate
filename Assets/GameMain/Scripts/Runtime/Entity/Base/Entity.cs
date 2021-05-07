using UnityGameFramework.Runtime;
using GameFramework;
using UnityEngine;

namespace Sirius.Runtime
{	
	/// <summary>
	/// 项目中实体逻辑
	/// </summary>
	public abstract class Entity : EntityLogic
	{
	    /// <summary>
	    /// 实体编号
	    /// </summary>
	    public int Id { get { return Entity.Id; } }
	
	    /// <summary>
	    /// 缓存的动画器
	    /// </summary>
	    public Animation CachedAnimation { get; private set; }
	
	    protected override void OnInit(object userData)
	    {
	        base.OnInit(userData);
	        CachedAnimation = GetComponent<Animation>();
	    }

        /// <summary>
        /// 实体显示
        /// </summary>
        /// <param name="userData">用户自定义数据</param>
        protected override void OnShow(object userData)
	    {
	        base.OnShow(userData);
		
	        Name = Utility.Text.Format("[Entity {0}]", Id.ToString());
	        CachedTransform.localScale = Vector3.one;
	    }
	
	}
}

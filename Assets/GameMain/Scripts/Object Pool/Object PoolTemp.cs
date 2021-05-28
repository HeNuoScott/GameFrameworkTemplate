using GameFramework.ObjectPool;
using UnityGameFramework.Runtime;
using GameFramework;
using UnityEngine;

namespace Sirius
{
    //1.创建对象参数基类
    public class TempItemObject : ObjectBase
    {
        public static TempItemObject Create(object target)
        {
            TempItemObject tempItemObject = ReferencePool.Acquire<TempItemObject>();
            tempItemObject.Initialize(target);
            return tempItemObject;
        }

        protected override void Release(bool isShutdown)
        {
            TempItem tempItem = (TempItem)Target;
            if (tempItem == null) return;
            Object.Destroy(tempItem.gameObject);
        }
    }
    //2.对象自身脚本
    public class TempItem : MonoBehaviour
    {
        public void Reset()
        {
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }    
    //3.创建对象池组件
    public class TempObjectComponent : GameFrameworkComponent
    {
        [SerializeField]
        private TempItem m_TempItem = null;
        public int m_InstancePoolCapacity = 40;

        private IObjectPool<TempItemObject> m_TempItemObject = null;

        private void Start()
        {
            if (m_TempItem == null)
            {
                Log.Error("You must set TempItem first.");
                return;
            }
            m_TempItemObject = GameEntry.ObjectPool.CreateSingleSpawnObjectPool<TempItemObject>("TempItem", m_InstancePoolCapacity);
        }

        public TempItem CreateTemp()
        {
            TempItem objectTemp = null;
            TempItemObject tempObject = m_TempItemObject.Spawn();
            if (tempObject != null)
            {
                objectTemp = (TempItem)tempObject.Target;
            }
            else
            {
                objectTemp = Instantiate(m_TempItem);
                objectTemp.transform.SetParent(this.transform);
                objectTemp.transform.localScale = Vector3.one;
                m_TempItemObject.Register(TempItemObject.Create(objectTemp), true);
            }
            objectTemp.gameObject.SetActive(true);
            return objectTemp;
        }

        public void RecycleTemp(TempItem item)
        {
            item.Reset();
            item.gameObject.SetActive(false);
            m_TempItemObject.Unspawn(item);
        }
    }
    //---------------------------------暂时注释,模板暂时不运行--------------------------------------------------
    ////4.在游戏入口中添加这个对象池组件
    //public partial class GameEntry : MonoBehaviour
    //{
    //    public static TempObjectComponent TempObject
    //    {
    //        get;
    //        private set;
    //    }

    //    private static void InitCustomComponents()
    //    {
    //        TempObject = UnityGameFramework.Runtime.GameEntry.GetComponent<TempObjectComponent>();
    //    }
    //}
    ////5.使用案例
    //public class HowToUseObjectPoolTemp : MonoBehaviour
    //{
    //    public TempItem poolTemp = null;
    //    private void Start()
    //    {
    //        poolTemp = GameEntry.TempObject.CreateTemp();
    //        GameEntry.TempObject.RecycleTemp(poolTemp);
    //    }
    //}
}
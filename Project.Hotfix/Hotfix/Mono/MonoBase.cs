using Sirius.Runtime;
using UnityEngine;

namespace Project.Hotfix
{
    public abstract class MonoBase
    {
        public abstract void OnInit(object callback);
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnTriggerExit(Collider other) { }
        public virtual void OnCollisionEnter(Collision collision) { }
        public virtual void OnCollisionExit(Collision collision) { }
        public virtual void OnDestroy() { }
    }

    public class HotLoadMonoScriptTest : MonoBase
    {
        public HotLoadMonoScript MonoScript { get; set; }

        public HotLoadMonoScriptTest()
        {
            GameObject GameObj = new GameObject("HotLoadMonoScript");
            MonoScript = GameObj.AddComponent<HotLoadMonoScript>();
            GameObj.AddComponent<ComponentView>().Component = this;
            MonoScript.InitLogic(nameof(HotLoadMonoScriptTest), this);
        }

        public override void OnInit(object callback)
        {
            MonoScript = callback as HotLoadMonoScript;
        }
    }

    public class MonoLoadHotScriptTest : MonoBase
    {
        public MonoLoadHotScript MonoScript { get; set; }

        public override void OnInit(object callback)
        {
            MonoScript = callback as MonoLoadHotScript;
        }
    }
}

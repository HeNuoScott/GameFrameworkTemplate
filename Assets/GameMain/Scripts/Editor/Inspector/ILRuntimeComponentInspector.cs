using UnityGameFramework.Editor;
using Sirius.Runtime;
using UnityEditor;

namespace Sirius.Editor
{
    [CustomEditor(typeof(HotfixComponent))]
    public class ILRuntimeComponentInspector : GameFrameworkInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("Available during runtime only.", MessageType.Info);
                return;
            }

            HotfixComponent t = (HotfixComponent)target;

            Repaint();
        }
    }

}
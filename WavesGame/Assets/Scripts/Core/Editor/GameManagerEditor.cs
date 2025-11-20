using UnityEditor;
using UnityEngine.UIElements;

namespace Core.Editor
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _scriptableObjectEditor;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myBehaviour = (GameManager)target;
            var settings = myBehaviour.GetSettings();

            if (settings == null) return;
            EditorGUILayout.Space(15);
            EditorGUILayout.HelpBox("Settings", MessageType.Info);
            CreateCachedEditor(settings, null, ref _scriptableObjectEditor);
            _scriptableObjectEditor.OnInspectorGUI();
        }
    }
}
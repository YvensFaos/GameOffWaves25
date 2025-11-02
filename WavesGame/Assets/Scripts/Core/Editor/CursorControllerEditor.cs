using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UUtils.Editor;

namespace Core.Editor
{
    [CustomEditor(typeof(CursorController))]
    public class CursorControllerEditor : UnityEditor.Editor
    {
        private static readonly Dictionary<CursorState, Color> StateColors = new()
        {
            [CursorState.Roaming] = Color.black,
            [CursorState.SelectGridUnit] = Color.green,
            [CursorState.ShowingOptions] = new Color(0.2f, 0.4f, 1f),
            [CursorState.Targeting] = Color.red
        };

        public override void OnInspectorGUI()
        {
            var cursorController = (CursorController)target;
            var currentState = cursorController.GetState();
            var coloredStyle = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = StateColors[currentState]
                }
            };
            GUILayout.Label($"Current State: {currentState.ToString()}", coloredStyle);
            GUILayout.BeginHorizontal();
            EditorDrawerHelper.DrawActiveBox("Is Active?", cursorController.IsActive());
            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }
}
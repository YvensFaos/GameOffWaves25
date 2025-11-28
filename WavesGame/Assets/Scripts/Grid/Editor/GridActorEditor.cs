/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 *
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

using UnityEditor;
using UnityEngine;

namespace Grid.Editor
{
    [CustomEditor(typeof(GridActor))]
    public class GridActorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var gridActor = (GridActor)target;

            var color = GUI.backgroundColor;
            var health = gridActor.GetCurrentHealth();
            var maxHealth = gridActor.GetMaxHealth();
            var ratio = (float)health / maxHealth;

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.forestGreen;
            GUILayout.Box($"{health}/{maxHealth}",
                GUILayout.Width(EditorGUIUtility.currentViewWidth * ratio), GUILayout.Height(20.0f));
            GUI.backgroundColor = Color.darkRed;
            GUILayout.Box($"[{ratio}]",
                GUILayout.Width(EditorGUIUtility.currentViewWidth * (1 - ratio)), GUILayout.Height(20.0f));
            GUI.backgroundColor = color;
            EditorGUILayout.EndHorizontal();
            
            base.OnInspectorGUI();
        }
    }
}
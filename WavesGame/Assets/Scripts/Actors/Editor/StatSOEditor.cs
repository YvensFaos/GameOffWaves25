using UnityEditor;
using UnityEngine;
using UUtils.Editor;

namespace Actors.Editor
{
    [CustomEditor(typeof(StatSO))]
    public class StatSOEditor : CustomIconEditor<StatSO>
    {
        protected override bool CheckValidSpriteInfo(StatSO scriptableObject)
        {
            return scriptableObject.statIcon != null;
        }

        protected override Sprite GetSprite(StatSO scriptableObject)
        {
            return scriptableObject.statIcon;
        }
    }
}
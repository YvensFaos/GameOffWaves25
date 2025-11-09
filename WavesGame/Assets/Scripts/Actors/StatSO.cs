using NaughtyAttributes;
using UnityEngine;

namespace Actors
{
    [CreateAssetMenu(fileName = "New Stat", menuName = "Waves/Stat", order = 2)]
    public class StatSO : ScriptableObject
    {
        public string statName;
        [ShowAssetPreview]
        public Sprite statIcon;
        [TextArea(3, 10)]
        public string statDescription;
    }
}
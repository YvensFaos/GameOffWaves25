using NaughtyAttributes;
using UnityEngine;
using UUtils;

namespace Actors
{
    [CreateAssetMenu(fileName = "New Naval Ship", menuName = "Waves/Naval Ship", order = 0)]
    public class NavalShipSo : ScriptableObject
    {
        [ShowAssetPreview] public Sprite shipSprite;
        [Header("Stats")] public NavalActorStats stats;
        public string initiativeDie;

        public int RollInitiative()
        {
            return stats.speed.Two + DiceHelper.RollDiceFromString(initiativeDie);
        }
        
        //TODO add a validation for the initiativeDie
    }
}
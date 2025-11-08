using UnityEngine;
using UUtils;

namespace Actors.Cannon
{
    public class BaseCannon : MonoBehaviour
    {
        [SerializeField]
        private CannonSo cannonData;

        public CannonSo GetCannonSo => cannonData;

        public int CalculateDamage()
        {
            return cannonData.damage + DiceHelper.RollDiceFromString(cannonData.damageDie);
        }
    }
}

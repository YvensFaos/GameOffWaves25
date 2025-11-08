using UnityEngine;

namespace Actors
{
    public class NavalTarget : NavalActor
    {
        [SerializeField]
        private NavalActorStats stats;

        public override void TakeDamage(int damage)
        {
            var damageTaken = stats.Sturdiness - damage;
            damageTaken = Mathf.Clamp(damageTaken, 0, int.MaxValue); 
            //TODO replace MaxValue with some more controlled value
            base.TakeDamage(damageTaken);
        }
    }
}
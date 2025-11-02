using Grid;
using UnityEngine;

namespace Actors
{
    public class NavalActor : GridActor
    {
        [SerializeField] private NavalActorType navalType;
        public NavalActorType NavalType => navalType;
    }
}
using System;
using Actors;
using Grid;
using UnityEngine;

namespace Core.Recorder
{
    [Serializable]
    public class WaveActorEntryJson
    {
        [SerializeField] public string name;
        [SerializeField] public string direction;
        [SerializeField] public int areaOfEffect;
        [SerializeField] public int stepAreaDistance;
        [SerializeField] public float damage;

        public WaveActorEntryJson(WaveActor waveActor)
        {
            name = waveActor.name;
            direction = GridMoveTypeExtensions.GridMovementSymbol(waveActor.GetWaveDirection);
            areaOfEffect = waveActor.GetAreaOfEffect();
            stepAreaDistance = waveActor.GetStepAreaDistance();
            damage = waveActor.GetDamage();
        }
    }
}
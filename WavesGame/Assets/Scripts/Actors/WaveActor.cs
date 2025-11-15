using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UUtils;

namespace Actors
{
    [Serializable]
    public class WaveDirectionSprite : Pair<GridMoveType, Sprite>
    {
        public WaveDirectionSprite(GridMoveType one, Sprite two) : base(one, two)
        {
        }
    }

    public class WaveActor : GridActor
    {
        [SerializeField] private GridMoveType waveDirection;
        [SerializeField] private SpriteRenderer waveDirectionSpriteRenderer;
        [SerializeField] private List<WaveDirectionSprite> waveDirectionSprites;
        [SerializeField] private ParticleSystem damageParticles;
        [SerializeField] private int areaOfEffect;

        protected override void Start()
        {
            base.Start();
            var waveDirectionSprite = waveDirectionSprites.Find(pair => pair.One.Equals(waveDirection));
            waveDirectionSpriteRenderer.sprite = waveDirectionSprite.Two;
        }

        public override void TakeDamage(int damage)
        {
            DebugUtils.DebugLogMsg($"Wave actor {name} was attacked with {damage}.", DebugUtils.DebugType.Verbose);
            base.TakeDamage(damage);
            if (damage <= 0) return;
            damageParticles?.Play();
            var attackArea = GridManager.GetSingleton()
                .GetGridUnitsForMoveType(waveDirection, GetUnit().Index(), areaOfEffect);
            attackArea.ForEach(unit =>
            {
                if (unit.ActorsCount() > 0)
                {
                    unit.DamageActors(damage);    
                }
                else
                {
                    var unitTransform = unit.transform;
                    var particles = Instantiate(damageParticles, unitTransform.position, unitTransform.rotation);
                    particles.Play();
                    var totalTime = particles.main.duration;
                    Destroy(particles.gameObject, totalTime);
                }
                
            });
        }
    }
}
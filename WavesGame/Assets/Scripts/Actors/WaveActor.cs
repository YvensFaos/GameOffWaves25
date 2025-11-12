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
        { }
    }
    
    public class WaveActor : GridActor
    {
        [SerializeField] private GridMoveType waveDirection;
        [SerializeField] private SpriteRenderer waveDirectionSpriteRenderer;
        [SerializeField] private List<WaveDirectionSprite> waveDirectionSprites;

        protected override void Start()
        {
            base.Start();
            var waveDirectionSprite = waveDirectionSprites.Find(pair => pair.One.Equals(waveDirection));
            waveDirectionSpriteRenderer.sprite = waveDirectionSprite.Two;
        }
    }
}
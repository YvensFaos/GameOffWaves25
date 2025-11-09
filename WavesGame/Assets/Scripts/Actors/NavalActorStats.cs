using System;

namespace Actors
{
    [Serializable]
    public struct NavalActorStats
    {
        public StatValuePair strength; //Offensive
        public StatValuePair speed; //Movement and Number of Movements per Turn
        public StatValuePair stability; //Constitution against waves
        public StatValuePair sight; //Awareness
        public StatValuePair sturdiness; //Constitution against enemies
        public StatValuePair spirit; //Morale - number of actions per turn

        public void SetStats(int strengthValue, int speedValue, int stabilityValue, int sightValue, int sturdinessValue,
            int spiritValue)
        {
            strength.Two = strengthValue;
            speed.Two = speedValue;
            stability.Two = stabilityValue;
            sight.Two = sightValue;
            sturdiness.Two = sturdinessValue;
            spirit.Two = spiritValue;
        }
    }
}
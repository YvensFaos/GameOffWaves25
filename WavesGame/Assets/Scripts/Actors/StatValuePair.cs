using System;
using UUtils;

namespace Actors
{
    [Serializable]
    public class StatValuePair : Pair<StatSO, int>
    {
        public StatValuePair(StatSO one, int two) : base(one, two)
        { }

        public override string ToString()
        {
            return $"{One.statName}: [{Two}]";
        }
    }
}
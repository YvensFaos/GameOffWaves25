using System;
using System.Collections.Generic;
using System.Linq;
using Actors;
using UnityEngine;
using UUtils;

namespace Core.Recorder
{
    [Serializable]
    public class WavesGameInfoRecordEntryJson : WavesEntryJson
    {
        [SerializeField] public string map;
        [SerializeField] public int randomSeed;
        [SerializeField] public int maxTurns;
        [SerializeField] public List<NavalActorEntryJson> navalActorEntryJsons;
        [SerializeField] public List<WaveActorEntryJson> waveActorEntryJsons;

        public WavesGameInfoRecordEntryJson(string eventType, int turn, long timeStamp, string map, int randomSeed,
            int maxTurns,
            List<NavalActor> navalActors, List<WaveActor> waveActors) : base(eventType, turn, timeStamp)
        {
            this.map = map;
            this.randomSeed = randomSeed;
            this.maxTurns = maxTurns;
            navalActorEntryJsons = navalActors.Select(ship => new NavalActorEntryJson(ship)).ToList();
            waveActorEntryJsons = waveActors.Select(waveActor => new WaveActorEntryJson(waveActor)).ToList();
        }
    }

    public class WavesGameInfoEntry : WavesEntry
    {
        private readonly string _map;
        private readonly int _randomSeed;
        private readonly int _maxTurns;
        private readonly List<NavalActor> _navalActors;
        private readonly List<WaveActor> _waveActors;

        public WavesGameInfoEntry(string map, int randomSeed, int maxTurns, List<NavalActor> navalActors,
            List<WaveActor> waveActors) : base(
            WavesRecordEntryType.Information)
        {
            _map = map;
            _randomSeed = randomSeed;
            _maxTurns = maxTurns;
            _navalActors = navalActors;
            _waveActors = waveActors;
        }

        public override void PerformEntry()
        {
            DebugUtils.DebugLogMsg($"Waves Game Info: {_map}. Random Seed: {_randomSeed}.",
                DebugUtils.DebugType.Temporary);
        }

        protected override string ToJson()
        {
            return JsonUtility.ToJson(new WavesGameInfoRecordEntryJson(
                WavesRecordEntryTypeExtensions.WavesRecordEntryTypeToString(WavesRecordEntryType.Information), turn,
                timeStamp, _map, _randomSeed, _maxTurns, _navalActors, _waveActors));
        }
    }
}
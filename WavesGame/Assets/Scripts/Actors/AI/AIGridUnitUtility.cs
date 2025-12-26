/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 *
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

using System;
using System.Collections.Generic;
using Grid;

namespace Actors.AI
{
    public class AIGridUnitUtility : IComparer<AIGridUnitUtility>, IComparable<AIGridUnitUtility>, IComparable
    {
        private readonly GridUnit _unit;

        public AIGridUnitUtility(GridUnit unit)
        {
            Utility = -1.0f;
            _unit = unit;
        }

        public void CalculateUtility(AINavalShip aiNavalShip, List<GridUnit> attackableUnits)
        {
            var reachableUnits = new HashSet<GridUnit>();
            reachableUnits.UnionWith(attackableUnits);

            var genes = aiNavalShip.GetGenesData();
            var faction = aiNavalShip.GetFaction();

            Utility = CalculateUtilityForGridUnit(aiNavalShip, _unit, genes, faction);
            var enumerator = reachableUnits.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current == null) continue;
                var localUtility = CalculateUtilityForGridUnit(aiNavalShip, current, genes, faction);
                Utility += localUtility;
            }

            enumerator.Dispose();
        }

        public float CalculateUtilityForGridUnit(AINavalShip aiNavalShip, GridUnit unit, AIGenesSO genes,
            AIFaction faction)
        {
            var enemiesHitByWave = 0;
            var utility = 0.0f;
            if (unit.ActorsCount() <= 0) return utility;
            var actorEnumerator = unit.GetActorEnumerator();
            while (actorEnumerator.MoveNext())
            {
                var current = actorEnumerator.Current;
                if (current == null) continue;

                switch (current)
                {
                    case NavalTarget:
                        utility += genes.targetInterest;
                        break;
                    case AINavalShip ally when ally.GetFaction().Equals(faction):
                        utility += genes.friendliness;
                        break;
                    case AINavalShip enemyAI:
                        utility += AttackUtility(enemyAI);
                        break;
                    case NavalShip navalShip:
                        utility += AttackUtility(navalShip);
                        break;
                    case WaveActor waveActor:
                    {
                        var affectedByWave = waveActor.GetUnitsAffectedByWaveAttack();
                        var waveUtility = 0.0f;
                        affectedByWave.ForEach(waveUnit =>
                        {
                            if (waveUnit.ActorsCount() <= 0) return;
                            var waveEnumerator = waveUnit.GetActorEnumerator();
                            while (waveEnumerator.MoveNext())
                            {
                                var waveCurrent = waveEnumerator.Current;
                                if (waveCurrent != null)
                                {
                                    waveUtility += ActorInWaveRangeUtility(waveActor, waveCurrent);
                                }
                            }

                            waveEnumerator.Dispose();
                        });

                        //Reset the wave utility if no enemy at all will be hit by it.
                        if (enemiesHitByWave <= 0)
                        {
                            waveUtility = 0;
                        }

                        utility += waveUtility;
                    }
                        break;
                }
            }

            actorEnumerator.Dispose();
            return utility;

            float AttackUtility(NavalActor navalActor)
            {
                var targetHealthRatio = navalActor.GetHealthRatio();
                var selfHealthRatio = aiNavalShip.GetHealthRatio();
                return genes.aggressiveness + (selfHealthRatio - targetHealthRatio) * genes.selfPreservation;
            }

            float ActorInWaveRangeUtility(WaveActor waveActor, GridActor actor)
            {
                var actorInWaveRangeUtility = 0.0f;
                if (actor.Equals(aiNavalShip)) return -genes.selfPreservation; //Negative utility if attacks itself
                if (actor.Equals(waveActor)) return 0; //No utility for self-wave-attack + prevent infinite recursion
                switch (actor)
                {
                    case NavalTarget:
                        //Utility is equal the likeliness of attacking a target
                        actorInWaveRangeUtility += genes.targetInterest;
                        break;
                    case AINavalShip ally when ally.GetFaction().Equals(faction):
                        //If the AI is more aggressive than friendly, than it generates positive utility for allies
                        actorInWaveRangeUtility = genes.aggressiveness - genes.friendliness;
                        break;
                    case AINavalShip enemyAI:
                        //2.0f instead of 1.0f, so enemies with full health (1.0f) still generate positive utility
                        actorInWaveRangeUtility = genes.aggressiveness * (2.0f - enemyAI.GetHealthRatio());
                        enemiesHitByWave++;
                        break;
                    case NavalShip navalShip:
                        //Same as enemyAI
                        actorInWaveRangeUtility = genes.aggressiveness * (2.0f - navalShip.GetHealthRatio());
                        enemiesHitByWave++;
                        break;
                    case WaveActor anotherWaveActor:
                        //Uses the damage of the wave as utility.
                        //TODO allow recursive wave navigation to assess chain-reaction wave effects
                        actorInWaveRangeUtility = anotherWaveActor.GetDamage();
                        break;
                }

                return actorInWaveRangeUtility;
            }
        }

        public float Utility { get; set; }

        public GridUnit GetUnit() => _unit;

        public int Compare(AIGridUnitUtility x, AIGridUnitUtility y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;
            return y.Utility.CompareTo(x.Utility);
        }

        public int CompareTo(AIGridUnitUtility other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return other is null ? 1 : other.Utility.CompareTo(Utility);
        }

        public int CompareTo(object other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return other switch
            {
                null => 1,
                AIGridUnitUtility otherAI => CompareTo(otherAI),
                _ => -1
            };
        }

        public override string ToString()
        {
            return $"{_unit.Index()} - U = {Utility}";
        }
    }
}
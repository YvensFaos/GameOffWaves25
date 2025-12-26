/*
 * Copyright (c) 2025 Yvens R Serpa [https://github.com/YvensFaos/]
 * 
 * This work is licensed under the Creative Commons Attribution 4.0 International License.
 * To view a copy of this license, visit http://creativecommons.org/licenses/by/4.0/
 * or see the LICENSE file in the root directory of this repository.
 */

using System.Collections.Generic;
using Actors.Cannon;
using Grid;
using UnityEngine;
using UUtils;

namespace Actors.AI
{
    public class AIBrain
    {
        private readonly AINavalShip _aiNavalShip;
        private readonly CannonSo _cannonData;

        public AIBrain(AINavalShip aiNavalShip, CannonSo cannonData)
        {
            DebugUtils.DebugLogMsg("Initializing AIBrain", DebugUtils.DebugType.Temporary);
            _aiNavalShip = aiNavalShip;
            _cannonData = cannonData;
        }

        public bool CalculateMovement(Vector2Int position, int stepsAvailable, out AIGridUnitUtility chosenAction)
        {
            var walkableUnits = GridManager.GetSingleton().GetGridUnitsInRadiusManhattan(position, stepsAvailable);
            var utilities = new List<AIGridUnitUtility>();

            foreach (var unit in walkableUnits)
            {
                var gridUnitUtility = new AIGridUnitUtility(unit);
                var attackableFromUnit = GridManager.GetSingleton().GetGridUnitsForMoveType(_cannonData.targetAreaType,
                    unit.Index(), _cannonData.area, _cannonData.deadZone);
                gridUnitUtility.CalculateUtility(_aiNavalShip, attackableFromUnit);
                utilities.Add(gridUnitUtility);
            }

            chosenAction = null;
            if (utilities.Count == 0) return false;
            utilities.Sort();
            var possibleActionsCount = Mathf.Min(utilities.Count, _aiNavalShip.GetGenesData().possibleActionsCount);
            var possibleActions = utilities.GetRange(0, possibleActionsCount);
            chosenAction = RandomHelper<AIGridUnitUtility>.GetRandomFromList(possibleActions);
            return true;
        }

        public bool CalculateAction(Vector2Int position, out AIGridUnitUtility chosenAction)
        {
            var attackableFromUnit = GridManager.GetSingleton().GetGridUnitsForMoveType(_cannonData.targetAreaType,
                position, _cannonData.area, _cannonData.deadZone);
            chosenAction = null;
            
            if(attackableFromUnit == null || attackableFromUnit.Count == 0) return false;

            var genes = _aiNavalShip.GetGenesData();
            var faction = _aiNavalShip.GetFaction();
            var utilities = new List<AIGridUnitUtility>();
            foreach (var unit in attackableFromUnit)
            {
                var gridUnitUtility = new AIGridUnitUtility(unit);
                var utility = gridUnitUtility.CalculateUtilityForGridUnit(_aiNavalShip, unit, genes, faction);
                gridUnitUtility.Utility = utility;
                utilities.Add(gridUnitUtility);
            }
            if (utilities.Count == 0) return false;
            utilities.Sort();
            var possibleActionsCount = Mathf.Min(utilities.Count, _aiNavalShip.GetGenesData().possibleActionsCount);
            var possibleActions = utilities.GetRange(0, possibleActionsCount);
            chosenAction = RandomHelper<AIGridUnitUtility>.GetRandomFromList(possibleActions);
            return true;
        }

        public static GridUnit GenerateRandomMovement(Vector2Int position, int stepsAvailable)
        {
            var walkableUnits = GridManager.GetSingleton().GetGridUnitsInRadiusManhattan(position, stepsAvailable);
            return RandomHelper<GridUnit>.GetRandomFromList(walkableUnits);
        }
    }
}
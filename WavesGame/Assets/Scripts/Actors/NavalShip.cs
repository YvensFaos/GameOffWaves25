using System;
using DG.Tweening;
using Grid;
using UnityEngine;
using UUtils;

namespace Actors
{
    public class NavalShip : NavalActor
    {
        [SerializeField] private NavalShipSo shipData;

        private void Start()
        {
            SetUnit(GridManager.GetSingleton().GetGridPosition(transform));
            //Adjust position to match the grid precisely
            var gridUnit = GetUnit();
            transform.position = gridUnit.transform.position;
            gridUnit.AddActor(this);
        }

        public override void MoveTo(GridUnit unit, Action onFinishMoving, bool animate = false, float time = 0.5f)
        {
            if (animate)
            {
                var steps = GridManager.GetSingleton()
                    .GetManhattanPathFromTo(GetUnit().Index(), unit.Index(), shipData.movementStepsPerTurn, true);
                if (steps.Count <= 0) return;
                var movementSequence = DOTween.Sequence();
                steps.ForEach(step =>
                {
                    DebugUtils.DebugLogMsg($"Path [{step.Index()}]", DebugUtils.DebugType.Temporary);
                    movementSequence.Append(transform.DOMove(step.transform.position, time));
                });
                movementSequence.OnComplete(() =>
                {
                    //Moves to the final step in the sequence.
                    UpdateUnitOnMovement(steps[^1]);
                    onFinishMoving?.Invoke();
                });
                movementSequence.Play();
            }
            else
            {
                transform.position = unit.transform.position;
                UpdateUnitOnMovement(unit);
                onFinishMoving?.Invoke();
            }
        }

        public NavalShipSo ShipData => shipData;
    }
}
using System;
using System.Collections.Generic;
using Actors;
using DG.Tweening;
using Grid;
using NaughtyAttributes;
using UI;
using UnityEngine;
using UUtils;

namespace Core
{
    public class CursorController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField, ReadOnly] private Vector2Int index;
        [SerializeField] private Vector2Int initialIndex;
        
        [Header("References")]
        [SerializeField] private Animator cursorAnimator;
        [SerializeField] private PlayerNavalShipOptionsPanel navalShipOptionsPanel;

        private CursorStateMachine _stateMachine;
        private List<GridUnit> _walkableUnits;
        private NavalActor _selectedActor;
        private bool _movingAnimation;
        private bool _active = true;

        private static readonly int Select = Animator.StringToHash("Select");
        
        
        private void Awake()
        {
            AssessUtils.CheckRequirement(ref cursorAnimator, this);
        }

        #region Action-Related

        private void OnEnable()
        {
            PlayerController.GetSingleton().onMoveAction += Move;
            PlayerController.GetSingleton().onInteract += Interact;
        }

        private void OnDisable()
        {
            PlayerController.GetSingleton().onMoveAction -= Move;
            PlayerController.GetSingleton().onInteract -= Interact;
        }
        #endregion

        private void Start()
        {
            _stateMachine = new CursorStateMachine(this);
            MoveToIndex(initialIndex);
        }

        private void Interact()
        {
            if (!_active) return;
            var validPosition = GridManager.GetSingleton().CheckGridPosition(index, out var gridUnit);
            if (!validPosition)
            {
                InvalidPosition();
                return;
            }

            _stateMachine.UpdateState(gridUnit);
        }

        private void Move(Vector2 direction)
        {
            if (!_active) return;
            var newIndex = new Vector2Int(index.x + (int)direction.x, index.y + (int)direction.y);
            MoveToIndex(newIndex);
        }

        private void MoveToIndex(Vector2Int newIndex)
        {
            if (_movingAnimation) return;
            if (newIndex.x == index.x && newIndex.y == index.y) return;
            var validPosition = GridManager.GetSingleton().CheckGridPosition(newIndex, out var gridUnit);
            if (!validPosition) InvalidPosition();
            _movingAnimation = true;
            transform.DOMove(gridUnit.transform.position, 0.2f).OnComplete(() =>
            {
                _movingAnimation = false;
                index = gridUnit.Index();
            });
        }

        private void InvalidPosition()
        {
            DebugUtils.DebugLogWarningMsg($"{name} tried to move to an invalid position.");
        }

        public void SetSelectedActor(NavalActor navalActor)
        {
            cursorAnimator.SetBool(Select, true);
            _selectedActor = navalActor;
            //Show the options; for now, just show the valid positions
            //Grid Manager show the valid positions for this naval actor
            var type = _selectedActor.NavalType;
            switch (type)
            {
                case NavalActorType.Player:
                    if (_selectedActor is NavalShip navalShip)
                    {
                        var data = navalShip.ShipData;
                        _walkableUnits = GridManager.GetSingleton().GetGridUnitsInRadius(index, data.movementRadius);
                        _walkableUnits.ForEach(unit => { unit.DisplayWalkingVisuals(); });
                    }
                    navalShipOptionsPanel.gameObject.SetActive(true);
                    break;
                case NavalActorType.Enemy:
                    break;
                case NavalActorType.Collectable:
                    break;
                case NavalActorType.Obstacle:
                    break;
                case NavalActorType.Wave:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void CancelSelectedActor()
        {
            if (_selectedActor == null) return;
            _selectedActor = null;
            _walkableUnits.ForEach(unit => { unit.HideWalkingVisuals(); });
            _walkableUnits = null;
            navalShipOptionsPanel.gameObject.SetActive(false);
            cursorAnimator.SetBool(Select, false);
            _stateMachine.ResetToRoaming();
        }

        public void ToggleActive(bool toggle)
        {
            _active = toggle;
        }
        
        public bool IsActive() => _active;
        public CursorState GetState() => _stateMachine?.CurrentState ?? CursorState.Roaming;
    }
}
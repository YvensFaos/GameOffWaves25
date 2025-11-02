using System;
using Actors;
using Grid;
using UUtils;

namespace Core
{
    public enum CursorState
    {
        Roaming,
        SelectGridUnit,
        ShowingOptions,
        Targeting
    }

    public class CursorStateMachine
    {
        private CursorController _cursorController;
        private CursorState _currentState;
        private GridUnit _currentGridUnit;

        public CursorStateMachine(CursorController cursorController)
        {
            _cursorController = cursorController;
            _currentState = CursorState.Roaming;
        }

        public void UpdateState(GridUnit unit)
        {
            switch (_currentState)
            {
                case CursorState.Roaming:
                    _currentGridUnit = unit;
                    ChangeStateTo(CursorState.SelectGridUnit);
                    break;
                case CursorState.SelectGridUnit:
                    break;
                case CursorState.ShowingOptions:
                    break;
                case CursorState.Targeting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ResetToRoaming()
        {
            ChangeStateTo(CursorState.Roaming);
        }

        private void ChangeStateTo(CursorState newState)
        {
            DebugUtils.DebugLogMsg($"Change state from {_currentState} to {newState}");
            switch (_currentState)
            {
                case CursorState.Roaming:
                    _cursorController.ToggleActive(false);
                    break;
                case CursorState.SelectGridUnit:
                    break;
                case CursorState.ShowingOptions:
                    break;
                case CursorState.Targeting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _currentState = newState;
            DebugUtils.DebugLogMsg($"Stated changed to {_currentState}");
            
            switch (_currentState)
            {
                case CursorState.Roaming:
                    _cursorController.ToggleActive(true);
                    break;
                case CursorState.SelectGridUnit:
                    if (_currentGridUnit.ActorsCount() > 0)
                    {
                        var getTopActor = _currentGridUnit.GetActor();
                        if (getTopActor is NavalActor navalActor)
                        {
                            _currentState = CursorState.ShowingOptions;
                            _cursorController.SetSelectedActor(navalActor);
                        }
                        else
                        {
                            //TODO something wrong
                        }
                    }
                    else
                    {
                        // ReSharper disable once TailRecursiveCall
                        ChangeStateTo(CursorState.Roaming);
                        // If select a grid unit with no actor, then just return back to roaming
                    }

                    break;
                case CursorState.ShowingOptions:
                    break;
                case CursorState.Targeting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public CursorState CurrentState => _currentState;
    }
}
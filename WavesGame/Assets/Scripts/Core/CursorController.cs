using DG.Tweening;
using Grid;
using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Vector2Int index;
        [SerializeField] private Vector2Int initialIndex;

        private bool _moving;
        private bool _active = true;

        #region Action-Related

        private void OnEnable()
        {
            PlayerController.GetSingleton().onNavigateAction += Navigate;
        }

        private void OnDisable()
        {
            PlayerController.GetSingleton().onNavigateAction -= Navigate;
        }

        #endregion

        private void Start()
        {
            MoveToIndex(initialIndex);
        }

        private void Navigate(Vector2 direction)
        {
            if (!_active) return;
            var newIndex = new Vector2Int(index.x + (int)direction.x, index.y + (int)direction.y);
            MoveToIndex(newIndex);
        }

        private void MoveToIndex(Vector2Int newIndex)
        {
            if (_moving) return;
            if (newIndex.x == index.x && newIndex.y == index.y) return;
            var validPosition = GridManager.GetSingleton().CheckGridPosition(newIndex, out var gridUnit);
            //TODO if invalidPosition, play cancel sound
            _moving = true;
            transform.DOMove(gridUnit.transform.position, 0.2f).OnComplete(() =>
            {
                _moving = false;
                index = gridUnit.Index();
            });
        }

        public void ToggleActive(bool toggle)
        {
            _active = toggle;
        }
    }
}
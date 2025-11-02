using System.Collections.Generic;
using UnityEngine;
using UUtils;

namespace Grid
{
    public class GridManager : WeakSingleton<GridManager>
    {
        [SerializeField] private List<GridUnit> gridUnits;
        [SerializeField] private TilemapInfo tilemapInfo;
        private GridUnit[,] _grid;

        protected override void Awake()
        {
            base.Awake();
            AssessUtils.CheckRequirement(ref tilemapInfo, this);
        }

        private void Start()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            var dimensions = tilemapInfo.GetDimensions();
            var bounds = tilemapInfo.GetTileMapBounds();
            _grid = new GridUnit[dimensions.x, dimensions.y];
            gridUnits.ForEach(unit =>
            {
                var index = GetUnitPosition(unit, dimensions, bounds);
                _grid[index.x, index.y] = unit;
                unit.SetIndex(index);
            });
        }

        private static Vector2Int GetUnitPosition(GridUnit unit, Vector2Int dimensions, Bounds tileBounds)
        {
            var cellWidth = tileBounds.size.x / dimensions.x;
            var cellHeight = tileBounds.size.y / dimensions.y;
            var localOffset = unit.transform.position - tileBounds.min;
            var gridX = Mathf.FloorToInt(localOffset.x / cellWidth);
            var gridY = Mathf.FloorToInt(localOffset.y / cellHeight);
            return new Vector2Int(gridX, gridY);
        }

        public void AddGridUnit(GridUnit unit)
        {
            gridUnits.Add(unit);
        }

        public bool CheckGridPosition(Vector2Int position, out GridUnit unit)
        {
            if (position.x < 0 || position.x > _grid.GetLength(0) || position.y < 0 || position.y > _grid.GetLength(1))
            {
                var validPosition = Vector2Int.zero;
                validPosition.x = position.x < 0 ? 0 :
                    position.x > _grid.GetLength(0) ? _grid.GetLength(0) - 1 : position.x;
                validPosition.y = position.y < 0 ? 0 :
                    position.y > _grid.GetLength(0) ? _grid.GetLength(0) - 1 : position.y;
                unit = _grid[validPosition.x, validPosition.y];
                return false;
            }

            unit = _grid[position.x, position.y];
            return true;
        }
    }
}
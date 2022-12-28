using UnityEngine;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TetrisBlast.Grid.Abstract;
using UnityEditor;

namespace TetrisBlast.Grid
{
    [ExecuteInEditMode]
    public class GridManager : MonoBehaviour
    {
        public static GridManager GlobalAccess { get; private set; } = null;

        [field: SerializeField] public GridData gridData { get; private set; } = new GridData();

        private GameObject currentGrid;

        public void Awake()
        {
            GlobalAccess = this;
        }

        public void ReBuild()
        {
            if (currentGrid != null)
                DestroyImmediate(currentGrid);

            CreateToGrid(gridData);
        }

        public void CreateToGrid(IGridData data)
        {
            var size = data.GetGridSize();
            var coreData = data.GetCoreData();
            var offset = data.gridOffset;
            var coreSize = coreData.size;
            var grid = new GameObject();
            grid.name = "Grid";
            grid.transform.position = Vector2.zero;

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var pos = offset + new Vector2(coreSize.x * x, coreSize.y * y);
                    var inst = Instantiate(coreData.getGridCorePrefab, pos, Quaternion.identity);
                    inst.transform.SetParent(grid.transform);
                    inst.name = $"Grid {y} {x}";
                }
            }

            currentGrid = grid;
        }

        [Serializable]
        public class GridData : IGridData
        {
            [field: SerializeField] public Vector2 gridSize { get; private set; } = new Vector2(9, 9);
            [field: SerializeField] public Vector2 gridOffset { get; set; } = new Vector2();

            [field: SerializeField]
            public Dictionary<int, List<GridCore>> storage { get; private set; } =
                new Dictionary<int, List<GridCore>>();


            [field: SerializeField] private GridCoreData _core_data = new GridCoreData();

            public IGridCoreData GetCoreData() => _core_data;

            public Vector2 GetGridSize() => gridSize;

            [Serializable]
            public class GridCoreData : IGridCoreData
            {
                [field: SerializeField] public Vector2 size { get; set; } = new Vector2();
                [field: SerializeField] [CanBeNull] private GameObject _grid_core = null;
                public GameObject getGridCorePrefab => _grid_core;
            }
        }
    }
}
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TetrisBlast.Grid.Abstract;
using UnityEditor;

namespace TetrisBlast.Grid
{
    [ExecuteInEditMode]
    public class GridManager : MonoBehaviour
    {
        public static GridManager GlobalAccess { get; private set; } = null;

        [field: SerializeField] public GridData _gridData = new GridData();

        [field: SerializeField]
        public GridData gridData
        {
            get => _gridData;
            set => _gridData = value;
        }

        private GameObject currentGrid;

        public void Awake()
        {
            GlobalAccess = this;
            ReBuild();
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
                var listgrids = new List<GridCore>();

                for (int x = 0; x < size.x; x++)
                {
                    var pos = offset + new Vector2(coreSize.x * x, coreSize.y * y);
                    var inst = Instantiate(coreData.getGridCorePrefab, pos, Quaternion.identity);
                    inst.transform.SetParent(grid.transform);
                    inst.name = $"Grid {y} {x}";
                    var gridInfo = inst.GetComponent<GridCore>();
                    gridInfo.info = new CordinantInfo(y, x);

                    listgrids.Add(gridInfo);
                }


                gridData.storage.Add(y, listgrids);
            }

            currentGrid = grid;
        }

        public void FindsCompletedGridCores(params CordinantInfo[] cordinants)
        {
            var yCordinants = new List<int>();
            var xCordinants = new List<int>();


            foreach (var cor in cordinants)
            {
                if (!yCordinants.Contains(cor.key)) yCordinants.Add(cor.key);
                if (!xCordinants.Contains(cor.orderX)) xCordinants.Add(cor.orderX);
            }

            CheckGridCoresWithY(yCordinants);
            CheckGridCoresWithX(xCordinants);
        }

        public void CheckGridCoresWithY(List<int> yCordinants)
        {
            for (int key = 0; key < gridData.storage.Count; key++)
            {
                if (yCordinants.Contains(key))
                {
                    var isThisLiceCompleted = false;

                    for (int order = 0; order < gridData.storage[key].Count; order++)
                    {
                        isThisLiceCompleted = true;

                        if (!gridData.storage[key][order].isFull)
                        {
                            isThisLiceCompleted = false;
                            break;
                        }
                    }

                    if (isThisLiceCompleted)
                    {
                        Debug.Log("Burada");
                        LineExplosion(key);
                    }
                }
            }
        }

        public void CheckGridCoresWithX(List<int> xCordinants)
        {
            var completedList = new Dictionary<int, List<GridCore>>();

            foreach (var coreId in xCordinants)
            {
                if (!completedList.ContainsKey(coreId))
                    completedList.Add(coreId, new List<GridCore>());

                foreach (var line in gridData.storage.Values)
                {
                    if (line.Count > coreId)
                    {
                        completedList[coreId].Add(line[coreId]);
                    }
                }
            }

            if (completedList.Count > 0)
            {
                foreach (var currentKey in xCordinants)
                {
                    var list = completedList[currentKey];
                    foreach (var gridCore in list)
                    {
                        if (!gridCore.isFull)
                        {
                            completedList.Remove(currentKey);
                            break;
                        }
                    }
                }
            }

            RowExplosion(completedList);
        }

        void LineExplosion(int key)
        {
            Debug.Log("Explosion " + key);
        }

        void RowExplosion(Dictionary<int, List<GridCore>> grids)
        {
            Debug.Log("Explosion " + grids.Count);
        }

        [Serializable]
        public class GridData : IGridData
        {
            [field: SerializeField] public Vector2 gridSize { get; private set; } = new Vector2(9, 9);
            [field: SerializeField] public Vector2 gridOffset { get; set; } = new Vector2();

            [field: SerializeField]
            public Dictionary<int, List<GridCore>> _storage = new Dictionary<int, List<GridCore>>();

            public Dictionary<int, List<GridCore>> storage
            {
                get => _storage;
                set => _storage = value;
            }

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
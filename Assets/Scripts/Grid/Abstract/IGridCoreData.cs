using JetBrains.Annotations;
using UnityEngine;

namespace TetrisBlast.Grid.Abstract
{
    public interface IGridCoreData
    {
        public Vector2 size { get; }
        [CanBeNull] public GameObject getGridCorePrefab { get; }
    }
}
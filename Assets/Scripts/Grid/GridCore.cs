using JetBrains.Annotations;
using TetrisBlast.Shapes;
using TetrisBlast.Shapes.Cores;
using UnityEngine;

namespace TetrisBlast.Grid
{
    public class GridCore : MonoBehaviour
    {
        [field: SerializeField] public int Id { get; set; } = -1;
        [field: SerializeField] public bool isFull { get; set; } = false;
        [field: SerializeField] [CanBeNull] public Core shapeCore { get; set; } = null;
        public CordinantInfo info;
        
        public void AddCore([CanBeNull] Core core)
        {
            var p = core != null;
            isFull = p;
            shapeCore = core;
            core.gridCoreInfo = info;
        }
    }
}
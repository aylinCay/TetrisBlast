﻿using JetBrains.Annotations;
using UnityEngine;

namespace TetrisBlast.Grid.Abstract
{
    public interface IGridData
    {
       
        public Vector2 gridOffset { get; }
        public IGridCoreData GetCoreData();
        public Vector2 GetGridSize();
    }
}
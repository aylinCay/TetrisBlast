using System;
using System.Collections.Generic;
using TetrisBlast.Grid;
using TetrisBlast.Shapes.Cores;


namespace TetrisBlast.Shapes
{
    using UnityEngine;

    public class Shape : MonoBehaviour
    {
        public List<Core> cores = new List<Core>();
        public List<Core> successCore = new List<Core>();
        public List<Core> failCore = new List<Core>();

        public bool isSelected;
        public Vector3 moveOffset;
        public GridCore mainGrid = null;
        private Vector3 selectedPosition;
        public bool isLocated;

        public List<CordinantInfo> _cordinant_ınfos = new List<CordinantInfo>();

        public void Start()
        {
        }

        public void NotifyCore(Core core, bool isSuccess = false)
        {
            var isExitsSuccess = successCore.Contains(core);
            var isExitsFail = failCore.Contains(core);

            if (isSuccess)
            {
                if (core.isPivotCore) mainGrid = core.currentGridCore;
                if (!isExitsSuccess) successCore.Add(core);
                if (isExitsFail) failCore.Remove(core);
                return;
            }

            if (core.isPivotCore) mainGrid = null;
            if (isExitsSuccess) successCore.Remove(core);
            if (!isExitsFail) failCore.Add(core);
        }


        private void Update()
        {
            if (isSelected)
            {
                var inputPosition = Input.mousePosition;
                inputPosition.z = Camera.main.farClipPlane;
                var pos = Camera.main.ScreenToWorldPoint(inputPosition);
                pos.z = 0f;

                transform.position = Vector3.Lerp(transform.position, pos + moveOffset, .1f);

                if (Input.GetButtonUp("Fire1"))
                {
                    isSelected = false;
                    if (mainGrid != null)
                    {
                        transform.position = mainGrid.transform.position;
                        isLocated = true;
                        Complete();

                        Debug.Log(_cordinant_ınfos.Count);
                        GridManager.GlobalAccess.FindsCompletedGridCores(_cordinant_ınfos.ToArray());

                        return;
                    }

                    transform.position = selectedPosition;
                }
            }
        }

        public void Complete()
        {
            foreach (var VARIABLE in cores)
            {
                VARIABLE.SetToGrid();
            }
        }

        public void CoresReset()
        {
            isLocated = false;
            foreach (var VARIABLE in cores)
            {
                VARIABLE.Reset();
            }
        }

        public void OnSelect(bool isSelect, Core core)
        {
            isSelected = isSelect;
            selectedPosition = transform.position;
            moveOffset = (selectedPosition - core.transform.position);

            moveOffset.x = 0;

            moveOffset.y = moveOffset.y == 0 ? 1f : Mathf.Abs(moveOffset.y);
            Debug.Log("move" + moveOffset);
        }
    }
}
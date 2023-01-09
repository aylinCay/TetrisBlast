using System;
using System.Linq;
using TetrisBlast.Grid;
using UnityEngine;

namespace TetrisBlast.Shapes.Cores
{
    public class Core : MonoBehaviour
    {
        public bool isPivotCore;
        public Shape parent;

        public GridCore currentGridCore;
        RaycastHit2D hit;
        private LayerMask mask => ShapeManager.GloballAccess.coreLayerMask;
        public CordinantInfo gridCoreInfo;
        private void Start()
        {
            if (parent != null)
            {
                if (!parent.cores.Contains(this))
                    parent.cores.Add(this);
            }
        }

        private void Update()
        {
            if (parent.isSelected)
            {
                GridCheck();
            }
        }

        public void GridCheck()
        {
            hit = Physics2D.Raycast(transform.position, Vector3.back, 10f, mask);

            if (hit.collider != null)
            {
                if (CheckToGrid())
                {
                    Success();
                    return;
                }

                Fail();
                return;
            }

            Fail();
        }

        public bool CheckToGrid()
        {
            return hit.transform.TryGetComponent<GridCore>(out currentGridCore) && !currentGridCore.isFull;
        }

        public void Success()
        {
            parent.NotifyCore(this, true);
        }

        public void Fail()
        {
            parent.NotifyCore(this, false);
        }

        public void SetToGrid()
        {
            if (parent.isLocated)
            {
                if (!currentGridCore.isFull)
                {
                    currentGridCore.AddCore(this);
                    parent._cordinant_ınfos.Add(gridCoreInfo);
                }
            }
        }

        public void Reset()
        {
            if (currentGridCore != null)
            {
                if (currentGridCore.shapeCore == this)
                {
                    currentGridCore.AddCore(null);

                    if (parent._cordinant_ınfos.Contains(gridCoreInfo))
                    {
                        parent._cordinant_ınfos.Remove(gridCoreInfo);
                    }
                }
            }
        }

        public void Select()
        {
            if (parent != null)
            {
                parent.CoresReset();


                parent.OnSelect(true, this);
            }
        }
    }
}
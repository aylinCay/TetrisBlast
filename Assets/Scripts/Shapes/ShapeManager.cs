using System;
using System.Reflection;
using EasyButtons;
using TetrisBlast.Shapes.Cores;
using UnityEngine;


namespace TetrisBlast.Shapes
{
    [UnityEngine.ExecuteInEditMode]
    public class ShapeManager : MonoBehaviour
    {
        public static ShapeManager GloballAccess { get; private set; } = null;
        public LayerMask coreLayerMask;
        public LayerMask mask;


        private void Awake()
        {
            GloballAccess = this;
        }


        void MyFunction()
        {
            Debug.Log("MyFunction çalıştı!");
        }

        [Button]
        public void MakeButton()
        {
            MyFunction();
        }

        public void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
                var pos = Camera.main.ScreenToWorldPoint(mousePosition);
                var hit = Physics2D.Raycast(pos, Vector3.back, 50f, mask);

                if (hit.collider != null)
                {
                    var shapeCore = hit.transform.GetComponent<Core>();
                    shapeCore.Select();
                }
            }
        }
    }
}
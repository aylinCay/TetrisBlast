using System.Security.Policy;
using UnityEditor;
using UnityEngine;

namespace TetrisBlast.Grid.Editor
{
    [CustomEditor(typeof(GridManager))]
    [CanEditMultipleObjects]
    public class GridManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var manager = target as GridManager;
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Build"))
            {
                if (manager != null) manager.ReBuild();
            }

            EditorGUILayout.EndVertical();
        }
    }
}
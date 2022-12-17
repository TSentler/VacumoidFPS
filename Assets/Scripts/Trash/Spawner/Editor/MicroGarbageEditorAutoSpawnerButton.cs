using UnityEditor;
using UnityEngine;

namespace Trash
{
    [CustomEditor(typeof(MicroGarbageEditorAutoSpawner))]
    class MicroGarbageEditorAutoSpawnerButton : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var _spawner = (MicroGarbageEditorAutoSpawner)target;
            if (GUILayout.Button("Spawn"))
            {
                _spawner.SpawnButton();
            }
            if (GUILayout.Button("Clear"))
            {
                _spawner.ClearButton();
            }
        }
    }
}
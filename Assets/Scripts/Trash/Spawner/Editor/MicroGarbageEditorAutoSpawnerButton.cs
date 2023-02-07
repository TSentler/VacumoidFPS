using UnityEditor;
using UnityEditor.SceneManagement;
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
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            if (GUILayout.Button("Clear"))
            {
                _spawner.ClearButton();
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
}
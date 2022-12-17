#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class DefaultSceneLoader
{
    static DefaultSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode) 
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
        }

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            var current = EditorSceneManager.GetActiveScene().buildIndex;
            EditorSceneManager.LoadScene (0);
            EditorSceneManager.LoadScene (current);
        }
    }
}
#endif

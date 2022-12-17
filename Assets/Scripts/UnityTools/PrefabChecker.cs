#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace UnityTools
{
    public static class PrefabChecker
    {
        public static bool InPrefabFileOrStage(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            return InPrefabFile(checkedGameObject) || InPrefabStage();
#else
            return false;
#endif
        }
        
        public static bool InPrefabFile(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            return PrefabUtility.GetPrefabAssetType(checkedGameObject) !=
                    PrefabAssetType.NotAPrefab
                    && IsConnectedAtScenePrefab(checkedGameObject) == false;
#else
            return false;
#endif
        }
        
        public static bool InPrefabStage()
        {
#if UNITY_EDITOR
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();

            return isValidPrefabStage;
#else
            return false;
#endif
        }
        
        public static bool IsConnectedAtScenePrefab(GameObject checkedGameObject)
        {
#if UNITY_EDITOR
            var instanceStatus =
                PrefabUtility.GetPrefabInstanceStatus(checkedGameObject);
            bool prefabConnected = instanceStatus == PrefabInstanceStatus.Connected;

            return prefabConnected;
#else
            return true;
#endif
        }

    }
}

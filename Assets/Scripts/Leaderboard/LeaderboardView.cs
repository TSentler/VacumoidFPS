using System.Collections.Generic;
using UnityEngine;
using YaVk;

namespace Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _parentObject;
        [SerializeField] private GameObject _leaderboardElementPrefab;

        private List<GameObject> _spawnedElements = new ();

        public void ConstructLeaderboard(List<PlayerInfoLeaderboard> leaderList)
        {
            ClearLeaderboard();

            for (int i = 0; i < leaderList.Count; i++)
            {
                var info = leaderList[i];
                GameObject leaderboardElementInstance = 
                    Instantiate(_leaderboardElementPrefab, _parentObject);

                var leaderboardElement = leaderboardElementInstance.
                    GetComponent<LeaderboardElement>();
                leaderboardElement.Initialize(i + 1, 
                    info.Name, info.Score, false);

                _spawnedElements.Add(leaderboardElementInstance);
            }
        }

        private void ClearLeaderboard()
        {
            foreach (var element in _spawnedElements)
                Destroy(element);

            _spawnedElements = new List<GameObject>();
        }
    }
}

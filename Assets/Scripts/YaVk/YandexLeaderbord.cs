using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YaVk
{
    public class YandexLeaderboard 
    {
        private readonly string _leaderboardName = "TrashLeaderboard";
        private readonly int _topCount = 5;

        private List<PlayerInfoLeaderboard> LeaderboardToList(
            LeaderboardGetEntriesResponse result)
        {
            var topPlayers = new List<PlayerInfoLeaderboard>();
            Debug.Log($"My rank = {result.userRank}");

            int resultsAmount = result.entries.Length;

            resultsAmount = Mathf.Clamp(resultsAmount, 1, _topCount);

            for (int i = 0; i < resultsAmount; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = "Anonymos";

                int score = result.entries[i].score;

                topPlayers.Add(new PlayerInfoLeaderboard(name, score));
            }

            return topPlayers;
        }
        
        public void GetTopPlayers(
            UnityAction<List<PlayerInfoLeaderboard>> successCallback,
            bool test = false)
        {
            if(test)
            {
                var topPlayers = new List<PlayerInfoLeaderboard>();
                
                for (int i = 0; i < _topCount; i++)
                {
                    topPlayers.Add(new PlayerInfoLeaderboard("name", i));
                }

                successCallback?.Invoke(topPlayers);
                return;
            }

            Leaderboard.GetEntries(_leaderboardName,
                result =>
                {
                    successCallback?.Invoke(LeaderboardToList(result));
                });
        }
        
        public void GetLeaderboardPlayerEntry(
            UnityAction<LeaderboardEntryResponse> successCallback)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, 
                result => 
                    successCallback?.Invoke(result));
        }

        public void AddPlayerToLeaderboard(int score,
            UnityAction successCallback = null)
        {
            if (!PlayerAccount.IsAuthorized)
                return;

            Leaderboard.SetScore(_leaderboardName, score, 
                () => successCallback?.Invoke());
        }
    }
}

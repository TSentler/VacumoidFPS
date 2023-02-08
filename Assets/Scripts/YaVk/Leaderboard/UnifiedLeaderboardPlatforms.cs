using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine.Events;
using VkLeaderboard = Agava.VKGames.Leaderboard;

namespace YaVk
{
    public class UnifiedLeaderboardPlatforms
    {
        public UnifiedLeaderboardPlatforms()
        {
            _yaLeaderboard = new YandexLeaderboard();
            if (Defines.IsYandexGames)
            {
                YandexGamesSdk.CallbackLogging = true;
            }
        }
        
        private YandexLeaderboard _yaLeaderboard;
        
        public void GetLeaderboardPlayerEntry(
            UnityAction<LeaderboardEntryResponse> successCallback)
        {
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                successCallback?.Invoke(null);
            }
            else if (Defines.IsVkGames)
            {
                successCallback?.Invoke(null);
            }
            else if (Defines.IsYandexGames)
            {
                _yaLeaderboard.GetLeaderboardPlayerEntry(successCallback);
            }
        }

        public bool IsLeaderboardAccess()
        {
            if (Defines.IsVkMobileGames 
                || Defines.IsYandexGames)
            {
                return true;
            }
            
            return false;
        }
        
        public bool IsAutoLeaderboard()
        {
            if (Defines.IsVkGames)
            {
                return true;
            }
            
            return false;
        }
        
        public void GetLeaderboard(int score, 
            UnityAction<List<PlayerInfoLeaderboard>> successCallback)
        {
            if (IsLeaderboardAccess() == false)
                return;

            if (Defines.IsUnityWebGl == false 
                || Defines.IsUnityEditor)
            {
                _yaLeaderboard.GetTopPlayers(leaderList =>
                {
                    successCallback?.Invoke(leaderList);
                }, true);
            }
            else if (Defines.IsVkGames)
            {
                successCallback?.Invoke(new ());
                VkLeaderboard.ShowLeaderboard(score);
            }
            else if (Defines.IsYandexGames)
            {
                _yaLeaderboard.AddPlayerToLeaderboard(score, () =>
                {
                   _yaLeaderboard.GetTopPlayers(leaderList =>
                    {
                        successCallback?.Invoke(leaderList);
                    });     
                });
            }
        }
        
        public void AddPlayerToLeaderboard(int score)
        {
            if (Defines.IsUnityEditor == false
                && Defines.IsYandexGames)
            {
                _yaLeaderboard.AddPlayerToLeaderboard(score);
            }
        }
        
    }
}
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
#if YANDEX_GAMES
            YandexGamesSdk.CallbackLogging = true;
#endif
        }
        
        private YandexLeaderboard _yaLeaderboard;
        
        public void GetLeaderboardPlayerEntry(
            UnityAction<LeaderboardEntryResponse> successCallback)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            successCallback?.Invoke(null);
#elif VK_GAMES
            successCallback?.Invoke(null);
#elif YANDEX_GAMES
            _yaLeaderboard.GetLeaderboardPlayerEntry(successCallback);
#endif
        }

        public bool IsLeaderboardAccess()
        {
#if VK_GAMES_MOBILE || YANDEX_GAMES 
            return true;
#endif
            return false;
        }
        
        public bool IsAutoLeaderboard()
        {
#if VK_GAMES
            return true;
#endif
            return false;
        }
        
        public void GetLeaderboard(int score, 
            UnityAction<List<PlayerInfoLeaderboard>> successCallback)
        {
            if (IsLeaderboardAccess() == false)
                return;
            
#if !UNITY_WEBGL || UNITY_EDITOR
            _yaLeaderboard.GetTopPlayers(leaderList =>
            {
                successCallback?.Invoke(leaderList);
            }, true);
#elif VK_GAMES
            successCallback?.Invoke(new ());
            VkLeaderboard.ShowLeaderboard(score);
#elif YANDEX_GAMES
            _yaLeaderboard.AddPlayerToLeaderboard(score, () =>
            {
               _yaLeaderboard.GetTopPlayers(leaderList =>
                {
                    successCallback?.Invoke(leaderList);
                });     
            });
#endif
        }
        
        public void AddPlayerToLeaderboard(int score)
        {
#if YANDEX_GAMES && !UNITY_EDITOR
            _yaLeaderboard.AddPlayerToLeaderboard(score);
#endif
        }
        
    }
}
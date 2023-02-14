namespace SocialNetwork
{
    public static class Defines
    {
        
        public static bool IsUnityEditor =>
#if UNITY_EDITOR
            true;
#else
            false;
#endif

        public static bool IsUnityWebGl =>
#if UNITY_WEBGL
            true;
#else
            false;
#endif

        public static bool IsYandexGames =>
#if YANDEX_GAMES
            true;
#else
            false;
#endif

        public static bool IsVkMobileGames =>
#if VK_GAMES_MOBILE
            true;
#else
            false;
#endif
        
        public static bool IsVkGames =>
#if VK_GAMES
            true;
#else
            false;
#endif

        public static bool IsItchIoGames =>
#if ITCHIO_GAMES 
            true;
#else
            false;
#endif
    }
}
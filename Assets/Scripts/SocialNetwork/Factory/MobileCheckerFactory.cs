using SocialNetwork.Interfaces;
using SocialNetwork.VK;
using SocialNetwork.Yandex;

namespace SocialNetwork.Factory
{
    public class MobileCheckerFactory
    {
        public IMobileChecker Get()
        {
            IMobileChecker provider;
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                provider = new PlugProvider();
            }
            else if (Defines.IsYandexGames)
            {
                provider = new YandexProvider();
            }
            else if (Defines.IsVkGames)
            {
                provider = new VkProvider();
            }
            else
            {
                provider = new PlugProvider();
            }

            return provider;
        }
    }
}
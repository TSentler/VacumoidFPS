using SocialNetwork.Interfaces;
using SocialNetwork.VK;
using SocialNetwork.Yandex;

namespace SocialNetwork.Factory
{
    public class InitializerFactory
    {
        public IInitialize Get()
        {
            IInitialize platform;
            if (Defines.IsUnityWebGl == false
                || Defines.IsUnityEditor)
            {
                platform = new PlugProvider();
            }
            else if (Defines.IsYandexGames)
            {
                platform = new YandexProvider();
            }
            else if (Defines.IsVkGames)
            {
                platform = new VkProvider();
            }
            else
            {
                platform = new PlugProvider();
            }

            return platform;
        }
    }
}
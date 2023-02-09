using YaVk.Interfaces;
using YaVk.VK;
using YaVk.Yandex;

namespace YaVk.Factory
{
    public class InitializerFactory
    {
        public Initializer Get()
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

            return new Initializer(platform);
        }
    }
}
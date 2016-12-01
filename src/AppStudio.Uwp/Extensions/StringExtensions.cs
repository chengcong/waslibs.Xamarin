#if UWP
using Windows.ApplicationModel.Resources;
#else
using System.Reflection;
using System.Resources;
using Xamarin.Forms;

#endif

namespace AppStudio.Uwp
{
    public static class StringExtensions
    {
        public static string StringResource(this string self)
        {
#if UWP
            return Singleton<ResourceLoader>.Instance.GetString(self);
#else
            var resourceManager = new ResourceManager("AppStudio.Uwp.Strings.Resources", typeof(StringExtensions).GetTypeInfo().Assembly);
             return resourceManager.GetString(self);
#endif
        }
    }
}

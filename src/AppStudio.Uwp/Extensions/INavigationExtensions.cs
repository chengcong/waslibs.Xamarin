using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;
using AppStudio.Xamarin.Navigation;

namespace AppStudio.Xamarin
{
    public static class INavigationExtensions
    {
        public static async Task NavigateToPage(this INavigation navigation, string page, object parameter = null)
        {
            Assembly ass = Application.Current.GetType().GetTypeInfo().Assembly;
            var targetPage = ass.DefinedTypes.FirstOrDefault(t => t.Name == page);
            if (targetPage != null)
            {
                await navigation.NavigateToPage(targetPage.AsType(), parameter);
            }

        }

        public static async Task NavigateToPage(this INavigation navigation, Type page, object parameter = null)
        {
            if (page == null) return;
            Page targetPage = (Page)Activator.CreateInstance(page);
            IPageWithNavParameter targetPageWithNavParameter = targetPage as IPageWithNavParameter;
            if (targetPageWithNavParameter != null)
            {
                targetPageWithNavParameter.NavParameter = parameter;
            }
            if (targetPage != null)
            {
                await navigation.PushAsync(targetPage);
            }
        }
    }
}

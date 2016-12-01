using System;

#if UWP
namespace AppStudio.Uwp.Navigation
#else
namespace AppStudio.Xamarin.Navigation
#endif
{
    [Obsolete("Implement your custom navigation logic")]
    public interface INavigable
    {
        NavigationInfo NavigationInfo { get; set; }
    }
}

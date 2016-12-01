using System;
#if UWP
using Windows.Networking.Connectivity;
namespace AppStudio.Uwp.Services
#else
namespace AppStudio.Xamarin.Services
#endif
{
    public static class InternetConnection
    {
        public static bool IsInternetAvailable()
        {
            try
            {
#if UWP
                ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
                return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
#else
                return false;
#endif
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }
    }
}

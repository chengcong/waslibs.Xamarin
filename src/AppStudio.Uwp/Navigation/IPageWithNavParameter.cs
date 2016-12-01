

#if UWP
namespace AppStudio.Uwp.Navigation
#else
namespace AppStudio.Xamarin.Navigation
#endif
{
    public interface IPageWithNavParameter
    {
        object NavParameter { get; set; }
    }
}

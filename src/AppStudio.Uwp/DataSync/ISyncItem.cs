#if UWP
namespace AppStudio.Uwp.DataSync
#else
namespace AppStudio.Xamarin.DataSync
#endif
{
    public interface ISyncItem<T>
    {
        void Sync(T other);
        bool NeedSync(T other);
    }
}

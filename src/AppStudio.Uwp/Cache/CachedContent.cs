using System;
using System.Collections.Generic;

#if UWP
namespace AppStudio.Uwp.Cache
#else
namespace AppStudio.Xamarin.Cache
#endif
{
    public class CachedContent<T>
    {
        public DateTime Timestamp { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

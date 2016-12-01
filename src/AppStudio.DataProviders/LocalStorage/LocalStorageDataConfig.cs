using System.Reflection;

namespace AppStudio.DataProviders.LocalStorage
{
    public class LocalStorageDataConfig
    {
        public string FilePath { get; set; }
#if !UWP
        public Assembly AssemblyForEmbeddedResource { get; set; }
#endif

        public string OrderBy { get; set; }

        public SortDirection OrderDirection { get; set; }    
    }
}

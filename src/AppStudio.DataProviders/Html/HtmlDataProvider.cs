using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppStudio.DataProviders.Exceptions;
using AppStudio.DataProviders.LocalStorage;

#if UWP
using Windows.Storage;
using Windows.Storage.Streams;
#else
using PCLStorage;
#endif

namespace AppStudio.DataProviders.Html
{
    public class HtmlDataProvider : DataProviderBase<LocalStorageDataConfig, HtmlSchema>
    {
        public override bool HasMoreItems
        {
            get
            {
                return false;
            }
        }

        protected override async Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(LocalStorageDataConfig config, int pageSize, IParser<TSchema> parser)
        {
#if UWP
            var uri = new Uri(string.Format("ms-appx://{0}", config.FilePath));

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            IRandomAccessStreamWithContentType randomStream = await file.OpenReadAsync();

            using (StreamReader r = new StreamReader(randomStream.AsStreamForRead()))
            {
                return await parser.ParseAsync(await r.ReadToEndAsync());
            }
#else
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync(config.FilePath);
            return await parser.ParseAsync(await file.ReadAllTextAsync());
#endif

        }

        protected override IParser<HtmlSchema> GetDefaultParserInternal(LocalStorageDataConfig config)
        {
            return new HtmlParser();
        }

        protected override Task<IEnumerable<TSchema>> GetMoreDataAsync<TSchema>(LocalStorageDataConfig config, int pageSize, IParser<TSchema> parser)
        {
            throw new NotSupportedException();
        }

        protected override void ValidateConfig(LocalStorageDataConfig config)
        {
            if (config == null)
            {
                throw new ConfigNullException();
            }
            if (config.FilePath == null)
            {
                throw new ConfigParameterNullException("FilePath");
            }
        }
    }
}

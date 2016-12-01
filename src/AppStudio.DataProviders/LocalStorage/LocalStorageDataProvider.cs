﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Exceptions;

#if UWP
using Windows.Storage;
using Windows.Storage.Streams;
#else
using PCLStorage;
#endif

using System.Linq;

namespace AppStudio.DataProviders.LocalStorage
{
    public class LocalStorageDataProvider<T> : DataProviderBase<LocalStorageDataConfig, T> where T : SchemaBase
    {
        object _totalItems;


        bool _hasMoreItems = false;
        public override bool HasMoreItems
        {
            get
            {
                return _hasMoreItems;
            }
        }

        protected override async Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(LocalStorageDataConfig config, int pageSize, IParser<TSchema> parser)
        {
            ContinuationToken = "1";
#if UWP
            IRandomAccessStreamWithContentType randomStream = await GetRandomStream(config);

            using (StreamReader r = new StreamReader(randomStream.AsStreamForRead()))
            {
                var items = await parser.ParseAsync(await r.ReadToEndAsync());
                if (items != null && items.Any())
                {
                    _totalItems = items.ToList();

                    var totalAsTSchema = (_totalItems as IEnumerable<TSchema>);
                   
                    _hasMoreItems = totalAsTSchema.Count() > pageSize;
                    ContinuationToken = GetContinuationToken(ContinuationToken);

                    var resultToReturn = totalAsTSchema.AsQueryable().OrderBy(config.OrderBy, config.OrderDirection).Take(pageSize).ToList();
                    return resultToReturn;
                }
                _hasMoreItems = false;
                return new TSchema[0];
            }
#else
            IEnumerable<TSchema> items = null;
            if (config.AssemblyForEmbeddedResource == null)
            {
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.GetFileAsync(config.FilePath);
                items = await parser.ParseAsync(await file.ReadAllTextAsync());
            }
            else
            {
                string filename = config.FilePath.Replace('/', '.');
                filename = config.AssemblyForEmbeddedResource.GetName().Name + (filename.StartsWith(".") ? "" : ".") +
                           filename;

                Stream stream = config.AssemblyForEmbeddedResource.GetManifestResourceStream(filename);
                using (var reader = new System.IO.StreamReader(stream))
                {
                    items = await parser.ParseAsync(await reader.ReadToEndAsync());
                }
            }
            if (items != null && items.Any())
                {
                    _totalItems = items.ToList();

                    var totalAsTSchema = (_totalItems as IEnumerable<TSchema>);

                    _hasMoreItems = totalAsTSchema.Count() > pageSize;
                    ContinuationToken = GetContinuationToken(ContinuationToken);
                
                var resultToReturn =
                        totalAsTSchema.AsQueryable()
                            .OrderBy(config.OrderBy, config.OrderDirection)
                            .Take(pageSize)
                            .ToList();
                    return resultToReturn;
                }
            
            _hasMoreItems = false;
            return new TSchema[0];
#endif
        }

        protected override async Task<IEnumerable<TSchema>> GetMoreDataAsync<TSchema>(LocalStorageDataConfig config, int pageSize, IParser<TSchema> parser)
        {
            int page = Convert.ToInt32(ContinuationToken);
            var items = await Task.Run(() => { return GetMoreData<TSchema>(pageSize, page); });

            _hasMoreItems = items.Any();
            ContinuationToken = GetContinuationToken(ContinuationToken);

            return items;
        }

        public async Task<IEnumerable<TSchema>> GetDataByIdsAsync<TSchema>(LocalStorageDataConfig config, IEnumerable<string> ids) where TSchema : SchemaBase
        {
            return await GetDataByIdsAsync(config, ids, new JsonParser<TSchema>());
        }

        public async Task<IEnumerable<TSchema>> GetDataByIdsAsync<TSchema>(LocalStorageDataConfig config, IEnumerable<string> ids, IParser<TSchema> parser) where TSchema : SchemaBase
        {
#if UWP
            IRandomAccessStreamWithContentType randomStream = await GetRandomStream(config);

            using (StreamReader r = new StreamReader(randomStream.AsStreamForRead()))
            {
                var items = await parser.ParseAsync(await r.ReadToEndAsync());
                if (items != null && items.Any())
                {
                    var totalAsTSchema = (items.ToList() as IEnumerable<TSchema>);

                    var resultToReturn = totalAsTSchema.AsQueryable().Where(x => ids.Contains(x._id)).ToList();
                    return resultToReturn;
                }
                return new TSchema[0];
            }
#else
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync(config.FilePath);
            var items = await parser.ParseAsync(await file.ReadAllTextAsync());
            if (items != null && items.Any())
            {
                var totalAsTSchema = (items.ToList() as IEnumerable<TSchema>);

                var resultToReturn = totalAsTSchema.AsQueryable().Where(x => ids.Contains(x._id)).ToList();
                return resultToReturn;
            }
            return new TSchema[0];
#endif
        }

        protected override IParser<T> GetDefaultParserInternal(LocalStorageDataConfig config)
        {
            return new JsonParser<T>();
        }

        protected override void ValidateConfig(LocalStorageDataConfig config)
        {
            if (config == null)
            {
                throw new ConfigNullException();
            }
            if (config.FilePath == null)
            {
                throw new ConfigParameterNullException(nameof(config.FilePath));
            }
        }

        private static string GetContinuationToken(string currentToken)
        {
            var token = (Convert.ToInt32(currentToken) + 1).ToString();
            return token;
        }
#if UWP
        private async Task<IRandomAccessStreamWithContentType> GetRandomStream(LocalStorageDataConfig config)
        {
            var uri = new Uri(string.Format("ms-appx://{0}", config.FilePath));
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await file.OpenReadAsync();
        }
#endif

        private IEnumerable<TSchema> GetMoreData<TSchema>(int pageSize, int page)
        {
            if (_totalItems == null)
            {
                throw new InvalidOperationException("LoadMoreDataAsync can not be called. You must call the LoadDataAsync method prior to calling this method");
            }
            var total = (_totalItems as IEnumerable<TSchema>);
            var resultToReturn = total.AsQueryable().OrderBy(Config.OrderBy, Config.OrderDirection).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            return resultToReturn;
        }

    }
}

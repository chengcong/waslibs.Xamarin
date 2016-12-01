﻿using System;
using System.Collections.Generic;
#if UWP
using Windows.Web.Http;
#else
using System.Net;
#endif
using System.Threading.Tasks;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Exceptions;

namespace AppStudio.DataProviders.Instagram
{
    public class InstagramDataProvider : DataProviderBase<InstagramDataConfig, InstagramSchema>
    {
        private const string BaseUrl = "https://api.instagram.com/v1";

        private InstagramOAuthTokens _tokens;

        public InstagramDataProvider(InstagramOAuthTokens tokens)
        {
            _tokens = tokens;
        }

        public override bool HasMoreItems
        {
            get
            {
                return false;
            }
        }

        protected override async Task<IEnumerable<TSchema>> GetDataAsync<TSchema>(InstagramDataConfig config, int pageSize, IParser<TSchema> parser)
        {
            var settings = new HttpRequestSettings
            {
                RequestedUri = this.GetApiUrl(config, pageSize)
            };

            HttpRequestResult result = await HttpRequest.DownloadAsync(settings);
            if (result.Success)
            {
                return await parser.ParseAsync(result.Result);
            }

            if (result.StatusCode == HttpStatusCode.BadRequest && !string.IsNullOrEmpty(result.Result) && (result.Result.Contains("OAuthParameterException") || result.Result.Contains("OAuthAccessTokenException")))
            {
                throw new OAuthKeysRevokedException();
            }

            throw new RequestFailedException(result.StatusCode, result.Result);
        }

        protected override IParser<InstagramSchema> GetDefaultParserInternal(InstagramDataConfig config)
        {
            return new InstagramParser();
        }

        protected override Task<IEnumerable<TSchema>> GetMoreDataAsync<TSchema>(InstagramDataConfig config, int pageSize, IParser<TSchema> parser)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateConfig(InstagramDataConfig config)
        {
            if (config == null)
            {
                throw new ConfigNullException();
            }
            if (config.Query == null)
            {
                throw new ConfigParameterNullException("Query");
            }
            if (_tokens == null)
            {
                throw new ConfigParameterNullException("Tokens");
            }
            if (string.IsNullOrEmpty(_tokens.ClientId))
            {
                throw new OAuthKeysNotPresentException("ClientId");
            }
        }

        private Uri GetApiUrl(InstagramDataConfig config, int maxRecords)
        {
            if (config.QueryType == InstagramQueryType.Tag)
            {
                return new Uri($"{BaseUrl}/tags/{config.Query}/media/recent?client_id={_tokens.ClientId}&count={maxRecords}");
            }
            else
            {
                return new Uri($"{BaseUrl}/users/{config.Query}/media/recent/?client_id={_tokens.ClientId}&count={maxRecords}");
            }
        }
    }
}

using System.Collections.Generic;
#if UWP
using Windows.Web.Http;
#else
using System.Net;
#endif

namespace AppStudio.DataProviders.Core
{
    public class HttpRequestResult
    {
        public HttpRequestResult()
        {
#if UWP
            this.StatusCode = HttpStatusCode.Ok;
#else
            this.StatusCode = HttpStatusCode.OK;
#endif
            this.Result = string.Empty;
        }
        internal HttpStatusCode StatusCode { get; set; }

        public string Result { get; set; }

        public bool Success { get { return (
#if UWP
            this.StatusCode == HttpStatusCode.Ok
#else
            this.StatusCode == HttpStatusCode.OK
#endif
            && !string.IsNullOrEmpty(this.Result)); } }
            }

    public class HttpRequestResult<TSchema> : HttpRequestResult where TSchema : SchemaBase
    {
        internal HttpRequestResult(HttpRequestResult result)
        {
            StatusCode = result.StatusCode;
            Result = result.Result;
        }

        public HttpRequestResult():base()
        {
           
        }

        public IEnumerable<TSchema> Items { get; set; } = new TSchema[0];

        public int HttpStatusCode
        {
            get
            {
                return (int)StatusCode;
            }
        }
    }
}

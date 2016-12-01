using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UWP
namespace AppStudio.Uwp.Html
#else
namespace AppStudio.Xamarin.Html
#endif
{
    public sealed class HtmlNode : HtmlFragment
    {
        public Dictionary<string, string> Attributes { get; }

        internal HtmlNode(HtmlTag openTag)
        {
            Name = openTag.Name.ToLowerInvariant();
            Attributes = openTag.Attributes;
        }
    }
}

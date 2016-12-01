﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStudio.Uwp.Html;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.Xaml.Media;
using Windows.UI;
using AppStudio.Uwp.Navigation;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;
using System.IO;

namespace AppStudio.Uwp.Controls.Html.Writers
{
    class Channel9Writer : IFrameVideoWriter
    {
        public override string[] TargetTags
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Match(HtmlFragment fragment)
        {
            var src = GetIframeSrc(fragment);
            return fragment.Name == "iframe" && !string.IsNullOrWhiteSpace(src) && src.ToLowerInvariant().Contains("channel9.msdn.com");
        }

        protected override ImageStyle GetStyle(DocumentStyle style)
        {
            return style.Channel9;
        }

        protected override void SetScreenshot(ImageEx img, HtmlNode node)
        {
            img.Source = GetEmbebedImage("AppStudio.Uwp.Controls.HtmlBlock.channel9-screen.png");
            img.Background = new SolidColorBrush(Color.FromArgb(255, 249, 203, 66));
        }
    }
}

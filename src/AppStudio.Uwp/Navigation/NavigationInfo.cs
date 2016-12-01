﻿using System;

#if UWP
namespace AppStudio.Uwp.Navigation
#else
namespace AppStudio.Xamarin.Navigation
#endif
{
    [Obsolete("Implement your custom navigation logic")]
    public class NavigationInfo
    {
        public string TargetPage { get; set; }
        
        public Uri TargetUri { get; set; }

        public NavigationType NavigationType { get; set; }

        public bool IncludeState { get; set; }
        
        public static NavigationInfo FromPage(string pageFullType)
        {
            return FromPage(pageFullType, false);
        }

        public static NavigationInfo FromPage(string pageFullType, bool includeState)
        {
            return new NavigationInfo
            {
                NavigationType = NavigationType.Page,
                TargetPage = pageFullType,
                IncludeState = includeState
            };
        }
    }

    [Obsolete("Implement your custom navigation logic")]
    public enum NavigationType
    {
        Page,
        DeepLink
    }
}

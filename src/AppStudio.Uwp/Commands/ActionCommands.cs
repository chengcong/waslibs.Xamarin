// ***********************************************************************
// <copyright file="ActionCommands.cs" company="Microsoft">
//     Copyright (c) 2015 Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows.Input;

#if UWP
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Appointments;
using Windows.UI.Xaml;
using Windows.System;

namespace AppStudio.Uwp.Commands
#else
using AppStudio.Xamarin.Navigation;
using Xamarin.Forms;
using System.Net;

namespace AppStudio.Xamarin.Commands
#endif
{
    /// <summary>
    /// This class defines commands used to implement the actions.
    /// </summary>
    public sealed class ActionCommands
    {
        /// <summary>
        /// Gets the command used to send an email.
        /// </summary>
        public static ICommand Mailto
        {
            get
            {
#if UWP
                return new RelayCommand<string>(async mail =>
                {
                    if (!string.IsNullOrEmpty(mail))
                    {
                        await Launcher.LaunchUriAsync(new Uri(string.Format("mailto:{0}", mail)));
            }
        }, (mail => !string.IsNullOrEmpty(mail)));
#else
                return new RelayCommand<string>(mail =>
                {
                    if (!string.IsNullOrEmpty(mail))
                    {
                        Device.OpenUri(new Uri(string.Format("mailto:{0}", mail)));
                    }
                }, (mail => !string.IsNullOrEmpty(mail)));
#endif

            }

        }

        /// <summary>
        /// Gets the command used to call to a telephone.
        /// </summary>
        public static ICommand CallToPhone
        {
            get
            {
#if UWP
                return new RelayCommand<string>(async phone =>
                {
                    if (!string.IsNullOrEmpty(phone))
                    {
                        await Launcher.LaunchUriAsync(new Uri(string.Format("tel:{0}", phone)));
                    }
                }, (phone => !string.IsNullOrEmpty(phone)));
#else
                return new RelayCommand<string>(phone =>
                {
                    if (!string.IsNullOrEmpty(phone))
                    {
                        Device.OpenUri(new Uri(string.Format("tel:{0}", phone)));
                    }
                }, (phone => !string.IsNullOrEmpty(phone)));

#endif
            }
        }

        /// <summary>
        /// Gets the command used to navigate to an Url.
        /// </summary>
        public static ICommand NavigateToUrl
        {
            get
            {
#if UWP
                return new RelayCommand<string>(async url =>
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        await Launcher.LaunchUriAsync(new Uri(url));
                    }
                }, (url => !string.IsNullOrEmpty(url)));
#else
                return new RelayCommand<string>(url =>
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        Device.OpenUri(new Uri(url));
                    }
                }, (url => !string.IsNullOrEmpty(url)));
#endif
            }
        }

        public static ICommand MapsPosition
        {
            get
            {
#if UWP
                return new RelayCommand<string>(async coordinates =>
                {
                    if (!string.IsNullOrEmpty(coordinates))
                    {
                        await Launcher.LaunchUriAsync(new Uri("bingmaps:" + coordinates, UriKind.Absolute));
                    }
                }, (coordinates => !string.IsNullOrEmpty(coordinates)));
#else
                return new RelayCommand<string>(coordinates =>
                {
                    if (!string.IsNullOrEmpty(coordinates))
                    {
                        switch (Device.OS)
                        {
//coordinates dans quel format ???
                            case TargetPlatform.iOS:
                                Device.OpenUri(
                                  new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(coordinates))));
                                break;
                            case TargetPlatform.Android:
                                Device.OpenUri(
                                  new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(coordinates))));
                                break;
                            case TargetPlatform.Windows:
                            case TargetPlatform.WinPhone:
                                Device.OpenUri(
                                  new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(coordinates))));
                                break;
                        }
                    }
                }, (coordinates => !string.IsNullOrEmpty(coordinates)));
#endif
            }
        }

        public static ICommand MapsHowToGet
        {
            get
            {
#if UWP
                return new RelayCommand<string>(async address =>
                {
                    if (!string.IsNullOrEmpty(address))
                    {
                        await Launcher.LaunchUriAsync(new Uri("bingmaps:?rtp=~adr." + address, UriKind.Absolute));
                    }
                }, (address => !string.IsNullOrEmpty(address)));
#else
                return new RelayCommand<string>(address =>
                {
                    if (!string.IsNullOrEmpty(address))
                    {
                        switch (Device.OS)
                        {
                            //coordinates dans quel format ???
                            case TargetPlatform.iOS:
                                Device.OpenUri(
                                  new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                                break;
                            case TargetPlatform.Android:
                                Device.OpenUri(
                                  new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                                break;
                            case TargetPlatform.Windows:
                            case TargetPlatform.WinPhone:
                                Device.OpenUri(
                                  new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                                break;
                        }
                    }
                }, (coordinates => !string.IsNullOrEmpty(coordinates)));
#endif

            }
        }

        public static ICommand Share
        {
            get
            {
                return new RelayCommand(() =>
                {
#if UWP
                    DataTransferManager.ShowShareUI();
#endif
                });
            }
        }

        /// <summary>
        /// Gets the command used to add Appointment to Calendar
        /// </summary>
        public static ICommand AddToCalendar
        {
            get
            {
#if UWP
                return new RelayCommand<Appointment>(async appointment =>
                {
                    if (appointment != null)
                    {
                        await AppointmentManager.ShowEditNewAppointmentAsync(appointment);
                    }
                }, (appointment => appointment != null));
#else
                return new RelayCommand(() =>
                {
                });
#endif
            }
        }
    }
}

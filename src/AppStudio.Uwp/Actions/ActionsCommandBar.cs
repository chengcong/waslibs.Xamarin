using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if UWP
using Windows.ApplicationModel.Resources;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
#else
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Xaml;
using Caliburn.Micro.Xamarin.Forms;
using Caliburn;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin;

using System.Resources;

using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
#endif

namespace AppStudio.Uwp.Actions
{
#if UWP
    public class ActionsCommandBar : CommandBar
#else
    public class ActionsCommandBar : ContentView
#endif
    {
#if UWP
        public static readonly DependencyProperty ActionsSourceProperty =
            DependencyProperty.Register("ActionsSource", typeof(List<ActionInfo>), typeof(ActionsCommandBar), new PropertyMetadata(null, ActionsSourcePropertyChanged));
        public static readonly DependencyProperty HideOnLandscapeProperty =
            DependencyProperty.Register("HideOnLandscape", typeof(bool), typeof(ActionsCommandBar), new PropertyMetadata(false));
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(ActionsCommandBar), new PropertyMetadata(true, OnIsVisiblePropertyChanged));
#else
        public static readonly DependencyProperty ActionsSourceProperty =
     DependencyPropertyHelper.Register("ActionsSource", typeof(List<ActionInfo>), typeof(ActionsCommandBar), null, ActionsSourcePropertyChanged);
        public static readonly DependencyProperty HideOnLandscapeProperty =
            DependencyPropertyHelper.Register("HideOnLandscape", typeof(bool), typeof(ActionsCommandBar), false);
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyPropertyHelper.Register("IsVisible", typeof(bool), typeof(ActionsCommandBar), true, OnIsVisiblePropertyChanged);

#endif

        private static void OnIsVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = d as ActionsCommandBar;
#if UWP
            var isVisible = (bool)e.NewValue;
            if (isVisible)
            {
                self.Visibility = Visibility.Visible;
            }
            else
            {
                self.Visibility = Visibility.Collapsed;
            }
#else
            self.IsVisible = (bool)e.NewValue;
#endif
        }

        public ActionsCommandBar()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DisplayInformation.GetForCurrentView().OrientationChanged += ((sender, args) =>
                {
                    UpdateVisibility(sender.CurrentOrientation);
                });
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Setter needed for binding.")]
        public List<ActionInfo> ActionsSource
        {
            get { return (List<ActionInfo>)GetValue(ActionsSourceProperty); }
            set { SetValue(ActionsSourceProperty, value); }
        }

        public bool HideOnLandscape
        {
            get { return (bool)GetValue(HideOnLandscapeProperty); }
            set
            {
                SetValue(HideOnLandscapeProperty, value);
            }
        }
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }        
        private static void ActionsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ActionsCommandBar;

            if (control != null)
            {
                foreach (var action in control.ActionsSource)
                {
                    var label = GetText(action, ActionTextProperties.Label);
                    var automationPropertiesName = GetText(action, ActionTextProperties.AutomationPropertiesName);
                    var button = FindButton(label, control);                    
                    if (button == null)
                    {
                        button = new AppBarButton();

                        if (action.ActionType == ActionType.Primary)
                        {
                            control.PrimaryCommands.Add(button);
                        }
                        else if (action.ActionType == ActionType.Secondary)
                        {
                            control.SecondaryCommands.Add(button);
                        }
                    }                    
                    button.Command = action.Command;
                    button.CommandParameter = action.CommandParameter;
                    button.Label = label;
                    AutomationProperties.SetName(button, automationPropertiesName);
                    if (!string.IsNullOrEmpty(label))
                    {
                        ToolTipService.SetToolTip(button, GetTooltip(label));
                        ToolTipService.SetPlacement(button, PlacementMode.Mouse);
                    }
                    if (Application.Current.Resources.ContainsKey(action.Style))
                    {
                        button.Style = Application.Current.Resources[action.Style] as Style;
                    }
                    if (button.Command?.CanExecute(button?.CommandParameter) == true)
                    {
                        button.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        button.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private static TextBlock GetTooltip(string label)
        {
            return new TextBlock()
            {
                Text = label,
                Foreground = new SolidColorBrush((Color)Application.Current.Resources["SystemBaseHighColor"])
            };
        }

        private enum ActionTextProperties { Label, AutomationPropertiesName };
        private static string GetText(ActionInfo action, ActionTextProperties property)
        {
#if UWP
            var resourceLoader = new ResourceLoader();
#else
            var resourceLoader = new ResourceManager("AppStudio.Uwp.Strings.Resources", typeof(ActionsCommandBar).GetTypeInfo().Assembly);

#endif
            string text = null;

            if (!string.IsNullOrEmpty(action.Name))
            {
                if (property == ActionTextProperties.Label)
                    text = resourceLoader.GetString(string.Format("{0}/Label", action.Name));
                else if (property == ActionTextProperties.AutomationPropertiesName)
                    text = resourceLoader.GetString(string.Format("{0}/AutomationPropertiesName", action.Name));
            }
            if (string.IsNullOrEmpty(text))
            {
                if (property == ActionTextProperties.Label)
                {
                    text = action.Text;
                }
                else if (property == ActionTextProperties.AutomationPropertiesName)
                {
                    text = GetText(action, ActionTextProperties.Label);
                }
            }
            return text;
        }

        private static AppBarButton FindButton(string label, ActionsCommandBar bar)
        {
            return bar.PrimaryCommands
                            .OfType<AppBarButton>()
                            .Union(bar.SecondaryCommands.OfType<AppBarButton>())
                            .FirstOrDefault(b => b.Label == label);
        }

        private void UpdateVisibility(DisplayOrientations orientation)
        {
            if (HideOnLandscape)
            {
                if (orientation == DisplayOrientations.Landscape ||
                   orientation == DisplayOrientations.LandscapeFlipped)
                {
                    this.Visibility = Visibility.Collapsed;
                }
                else if (orientation == DisplayOrientations.Portrait ||
                        orientation == DisplayOrientations.PortraitFlipped)
                {
                    this.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

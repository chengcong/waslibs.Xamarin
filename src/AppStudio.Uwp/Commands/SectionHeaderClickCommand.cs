using System.Windows.Input;
#if UWP
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
#else
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Caliburn.Micro;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
#endif

namespace AppStudio.Uwp.Commands
{
    public static class SectionHeaderClickCommand
    {
#if UWP
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", 
            typeof(ICommand),
            typeof(SectionHeaderClickCommand), 
            new PropertyMetadata(null, OnCommandPropertyChanged));
#else
        public static readonly DependencyProperty CommandProperty = DependencyPropertyHelper.RegisterAttached(
    "Command",
    typeof(ICommand),
    typeof(SectionHeaderClickCommand),
    null, OnCommandPropertyChanged);
#endif

        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
        {
            if (dependencyObject != null)
            {
                dependencyObject.SetValue(CommandProperty, value);
            }
        }

        public static ICommand GetCommand(DependencyObject dependencyObject)
        {
            return dependencyObject == null ? null : (ICommand)dependencyObject.GetValue(CommandProperty);
        }

        private static void OnCommandPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
#if UWP
            var control = dependencyObject as Hub;
            if (control != null)
            {
                control.SectionHeaderClick += OnSectionHeaderClick;
            }
#endif
        }
#if UWP

        private static void OnSectionHeaderClick(object sender, HubSectionHeaderClickEventArgs args)
        {
            var control = sender as Hub;
            var command = GetCommand(control);

            if (command != null && command.CanExecute(args.Section.DataContext))
            {
                command.Execute(args.Section.DataContext);
            }
        }
#endif
    }
}

// ***********************************************************************
// <copyright file="ItemClickCommand.cs" company="Microsoft">
//     Copyright (c) 2015 Microsoft. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Windows.Input;
#if UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Uwp.Commands
#else
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppStudio.Xamarin.Services;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;

namespace AppStudio.Xamarin.Commands
#endif
{
    /// <summary>
    /// This class defines an attached property that can be used to attach a Command to a 
    /// ListView, allowing to bind any ICommand when clicking any ListView. 
    /// </summary>
    public static class ItemClickCommand
    {
        /// <summary>
        /// Definition for the attached property.
        /// </summary>
#if UWP
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", 
            typeof(ICommand),
            typeof(ItemClickCommand), 
            new PropertyMetadata(null, OnCommandPropertyChanged));
#else
        public static readonly DependencyProperty CommandProperty = DependencyPropertyHelper.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ItemClickCommand),
            null, OnCommandPropertyChanged);

#endif

        /// <summary>
        /// Sets a command into the attached property.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to assign the command.</param>
        /// <param name="value">The command to attach.</param>
        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
        {
            if (dependencyObject != null)
            {
                dependencyObject.SetValue(CommandProperty, value);
            }
        }

        /// <summary>
        /// Gets the command from the attached property.
        /// </summary>
        /// <param name="dependencyObject">The dependency object to query.</param>
        /// <returns>The ICommand attached.</returns>
        public static ICommand GetCommand(DependencyObject dependencyObject)
        {
            return dependencyObject == null ? null : (ICommand)dependencyObject.GetValue(CommandProperty);
        }

        /// <summary>
        /// Handles the <see cref="E:CommandPropertyChanged" /> event.
        /// </summary>
        /// <param name="dependencyObject">The dependency object changed.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnCommandPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
#if UWP
            var control = dependencyObject as ListViewBase;
            if (control != null)
            {
                control.ItemClick += OnItemClick;
            }
#else
            var control = dependencyObject as ListView;
            if (control != null)
            {
                control.ItemTapped += OnItemTapped;
            }
#endif
        }
#if UWP
        /// <summary>
        /// Handles the <see cref="E:ItemClick" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var control = sender as ListViewBase;
            var command = GetCommand(control);

            if (command != null && command.CanExecute(e.ClickedItem))
            {
                command.Execute(e.ClickedItem);
            }
        }
#else
        private static void OnItemTapped(object sender, ItemTappedEventArgs e)
        {

            var control = sender as ListView;
            var command = GetCommand(control);

            if (command != null && command.CanExecute(e.Item))
            {
                command.Execute(e.Item);
            }
        }
#endif
    }
}

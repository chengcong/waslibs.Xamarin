using System;
#if UWP
using Windows.UI.Xaml;

namespace AppStudio.Uwp.Services
#else
using DependencyProperty = Xamarin.Forms.BindableProperty;

namespace AppStudio.Xamarin.Services
#endif
{
    /// <summary>
    /// Class that abstracts the differences in creating a DepedencyProperty / BindableProperty on the different platforms.
    /// </summary>
    public static class DependencyPropertyHelper
    {
        /// <summary>
        /// Register an attached dependency / bindable property
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="propertyType">The property type</param>
        /// <param name="ownerType">The owner type</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="propertyChangedCallback">Callback to executed on property changed</param>
        /// <returns>The registred attached dependecy property</returns>
        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, object defaultValue = null, PropertyChangedCallback propertyChangedCallback = null)
        {
#if UWP
                        return DependencyProperty.RegisterAttached(name, propertyType, ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback));
#else
            return DependencyProperty.CreateAttached(name, propertyType, ownerType, defaultValue, propertyChanged: (obj, oldValue, newValue) => {
                if (propertyChangedCallback != null)
                    propertyChangedCallback(obj, new DependencyPropertyChangedEventArgs(newValue, oldValue, null));
            });
#endif
        }

        /// <summary>
        /// Register a dependency / bindable property
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="propertyType">The property type</param>
        /// <param name="ownerType">The owner type</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="propertyChangedCallback">Callback to executed on property changed</param>
        /// <returns>The registred dependecy property</returns>
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType, object defaultValue = null, PropertyChangedCallback propertyChangedCallback = null)
        {
#if UWP
                        return DependencyProperty.Register(name, propertyType, ownerType, new PropertyMetadata(defaultValue, propertyChangedCallback));

#else
            return DependencyProperty.Create(name, propertyType, ownerType, defaultValue, propertyChanged: (obj, oldValue, newValue) =>
            {
                if (propertyChangedCallback != null)
                    propertyChangedCallback(obj, new DependencyPropertyChangedEventArgs(newValue, oldValue, null));
            });
#endif
        }

    }
}

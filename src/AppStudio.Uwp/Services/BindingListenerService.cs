using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AppStudio.Uwp.Services
#else
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Xaml;
using FrameworkElement = Xamarin.Forms.VisualElement;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;

namespace AppStudio.Xamarin.Services
#endif
{
    public class BindingListenerService
    {
        private static int index;
        private readonly DependencyProperty property;
        private FrameworkElement target;
        public event EventHandler<BindingChangedEventArgs> Changed;

        public BindingListenerService()
        {
#if UWP
            property = DependencyProperty.RegisterAttached(
                "DependencyPropertyListener" + index++,
                typeof(object),
                typeof(BindingListenerService),
                new PropertyMetadata(null, (d, e) => { OnChanged(new BindingChangedEventArgs(e)); }));
#else
            property = DependencyPropertyHelper.RegisterAttached(
                "DependencyPropertyListener" + index++,
                typeof(object),
                typeof(BindingListenerService),
                null, (d, e) => { OnChanged(new BindingChangedEventArgs(e)); });

#endif
        }

        public void OnChanged(BindingChangedEventArgs e)
        {
            Changed?.Invoke(target, e);
        }

        public void Attach(FrameworkElement element, Binding binding)
        {
            if(element == null)
            {
                throw new ArgumentNullException("element");
            }
            if (target != null)
            {
                throw new Exception("Cannot attach an already attached listener");
            }
            target = element;
            target.SetBinding(property, binding);
        }

        public void Detach()
        {
            target.ClearValue(property);
            target = null;
        }
    }

    public class BindingChangedEventArgs : EventArgs
    {
        public BindingChangedEventArgs(DependencyPropertyChangedEventArgs e)
        {
            EventArgs = e;
        }

        public DependencyPropertyChangedEventArgs EventArgs { get; private set; }
    }
}

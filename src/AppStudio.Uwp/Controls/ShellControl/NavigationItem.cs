using System;
using System.Collections.Generic;

#if UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Uwp.Controls
#else
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using Xamarin.Forms.Xaml;
using AppStudio.Xamarin.Services;
using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;

namespace AppStudio.Xamarin.Controls
#endif
{
    public class NavigationItem : DependencyObject
    {
        public static readonly NavigationItem Separator = new NavigationItem { IsSeparator = true };

        protected NavigationItem()
        {
            this.IsSeparator = false;
        }
        public NavigationItem(string id)
        {
            this.ID = id;
            this.IsSeparator = false;
        }
        public NavigationItem(string id, string caption) : this(id)
        {
            this.Caption = caption;
        }
#if UWP
        public NavigationItem(string id, Symbol symbol, string caption, Brush color = null) : this(id, caption)
        {
            this.Icon = CreateIcon(symbol, color);
        }
        public NavigationItem(string id, string glyph, string caption, Brush color = null) : this(id, caption)
        {
            this.Icon = CreateIcon(glyph, color);
        }
        public NavigationItem(string id, Uri uriSource, string caption, Brush color = null) : this(id, caption)
        {
            this.Icon = CreateIcon(uriSource, color);
        }

        public NavigationItem(string id, string caption, Control control) : this(id, caption)
        {
            this.Control = control;
        }
        public NavigationItem(string id, string caption, Action<NavigationItem> onClick) : this(id, caption)
        {
            this.OnClick = onClick;
        }

        public NavigationItem(string id, string glyph, string caption, Control control) : this(id, glyph, caption)
        {
            this.Control = control;
        }
        public NavigationItem(string id, string glyph, string caption, Action<NavigationItem> onClick, Brush background = null) : this(id, glyph, caption)
        {
            this.OnClick = onClick;
            if (background != null)
            {
                this.Background = background;
            }
        }
        public NavigationItem(string id, string glyph, string caption, IEnumerable<NavigationItem> subItems, Brush background = null) : this(id, glyph, caption)
        {
            this.SubItems = subItems;
            if (background != null)
            {
                this.Background = background;
            }
        }

        public NavigationItem(string id, Symbol symbol, string caption, Control control, Brush color = null) : this(id, symbol, caption, color)
        {
            this.Control = control;
        }
        public NavigationItem(string id, Symbol symbol, string caption, Action<NavigationItem> onClick = null, Brush color = null, Brush background = null) : this(id, symbol, caption, color)
        {
            this.OnClick = onClick;
            if (background != null)
            {
                this.Background = background;
            }
        }
        public NavigationItem(string id, Symbol symbol, string caption, IEnumerable<NavigationItem> subItems, Brush color = null, Brush background = null) : this(id, symbol, caption, color)
        {
            this.SubItems = subItems;
            if (background != null)
            {
                this.Background = background;
            }
        }

        public NavigationItem(string id, Uri uriSource, string caption, Control control, Brush color = null) : this(id, uriSource, caption, color)
        {
            this.Control = control;
        }
        public NavigationItem(string id, Uri uriSource, string caption, Action<NavigationItem> onClick = null, Brush color = null, Brush background = null) : this(id, uriSource, caption, color)
        {
            this.OnClick = onClick;
            if (background != null)
            {
                this.Background = background;
            }
        }
        public NavigationItem(string id, Uri uriSource, string caption, IEnumerable<NavigationItem> subItems, Brush color = null, Brush background = null) : this(id, uriSource, caption, color)
        {
            this.SubItems = subItems;
            if (background != null)
            {
                this.Background = background;
            }
        }
#endif
        public bool IsSeparator { get; set; }

#region ID
        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
#if UWP
        public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(string), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public static readonly DependencyProperty IDProperty = DependencyPropertyHelper.Register("ID", typeof(string), typeof(NavigationItem), null);
#endif
        #endregion

        #region Icon
#if UWP

        public IconElement Icon
        {
            get { return (IconElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconElement), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyPropertyHelper.Register("Icon", typeof(string), typeof(NavigationItem), null);
#endif

        #endregion

        #region Image
        public Image Image
        {
            get { return (Image)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
#if UWP
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(Image), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public static readonly DependencyProperty ImageProperty = DependencyPropertyHelper.Register("Image", typeof(Image), typeof(NavigationItem), null);
#endif
        #endregion

        #region Caption
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

#if UWP
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public static readonly DependencyProperty CaptionProperty = DependencyPropertyHelper.Register("Caption", typeof(string), typeof(NavigationItem), null);
#endif
#endregion

#region Background
#if UWP
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(NavigationItem), new PropertyMetadata(null));
#endif
#endregion

#region Control
#if UWP
        public Control Control
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        public static readonly DependencyProperty ControlProperty = DependencyProperty.Register("Control", typeof(Control), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public VisualElement Control
        {
            get { return (VisualElement)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }
        public static readonly DependencyProperty ControlProperty = DependencyPropertyHelper.Register("Control", typeof(VisualElement), typeof(NavigationItem), null);
#endif
#endregion

#region SubItems
        public IEnumerable<NavigationItem> SubItems
        {
            get { return (IEnumerable<NavigationItem>)GetValue(SubItemsProperty); }
            set { SetValue(SubItemsProperty, value); }
        }
#if UWP
        public static readonly DependencyProperty SubItemsProperty = DependencyProperty.Register("SubItems", typeof(IEnumerable<NavigationItem>), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public static readonly DependencyProperty SubItemsProperty = DependencyPropertyHelper.Register("SubItems", typeof(IEnumerable<NavigationItem>), typeof(NavigationItem), null);
#endif
#endregion

#region ClearSelection
        public bool ClearSelection
        {
            get { return (bool)GetValue(ClearSelectionProperty); }
            set { SetValue(ClearSelectionProperty, value); }
        }

#if UWP
        public static readonly DependencyProperty ClearSelectionProperty = DependencyProperty.Register("ClearSelection", typeof(bool), typeof(NavigationItem), new PropertyMetadata(false));
#else
        public static readonly DependencyProperty ClearSelectionProperty = DependencyPropertyHelper.Register("ClearSelection", typeof(bool), typeof(NavigationItem), false);
#endif
#endregion

#region OnClick
        public Action<NavigationItem> OnClick
        {
            get { return (Action<NavigationItem>)GetValue(OnClickProperty); }
            set { SetValue(OnClickProperty, value); }
        }

#if UWP
        public static readonly DependencyProperty OnClickProperty = DependencyProperty.Register("OnClick", typeof(Action<NavigationItem>), typeof(NavigationItem), new PropertyMetadata(null));
#else
        public static readonly DependencyProperty OnClickProperty = DependencyPropertyHelper.Register("OnClick", typeof(Action<NavigationItem>), typeof(NavigationItem), null);
#endif
#endregion
#if UWP
        public static FontIcon CreateIcon(string glyph, Brush color = null)
        {
            if(glyph == null)
            {
                throw new ArgumentNullException("glyph");
            }
            string[] parts = glyph.Split(':');
            if (parts.Length == 1)
            {
                if (color == null)
                {
                    return new FontIcon { Glyph = System.Net.WebUtility.HtmlDecode(glyph) };
                }
                else
                {
                    return new FontIcon { Glyph = System.Net.WebUtility.HtmlDecode(glyph), Foreground = color };
                }
            }
            if (color == null)
            {
                return new FontIcon
                {
                    Glyph = System.Net.WebUtility.HtmlDecode(parts[1]),
                    FontFamily = new FontFamily(parts[0])
                };
            }
            else
            {
                return new FontIcon
                {
                    Glyph = System.Net.WebUtility.HtmlDecode(parts[1]),
                    FontFamily = new FontFamily(parts[0]),
                    Foreground = color
                };
            }

        }
        public static BitmapIcon CreateIcon(Uri uriSource, Brush color = null)
        {
            if (color == null)
            {
                return new BitmapIcon { UriSource = uriSource, Width = 20, Height = 20 };
            }
            else
            {
                return new BitmapIcon { UriSource = uriSource, Width = 20, Height = 20, Foreground = color };
            }
        }
        public static SymbolIcon CreateIcon(Symbol symbol, Brush color = null)
        {
            if (color == null)
            {
                return new SymbolIcon(symbol);
            }
            else
            {
                return new SymbolIcon(symbol) { Foreground = color };
            }
        }
#endif
    }
}

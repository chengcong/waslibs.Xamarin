using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if UWP
namespace AppStudio.Uwp.EventArguments
#else
namespace AppStudio.Xamarin.EventArguments
#endif
{
    public class BoolEventArgs : EventArgs
    {
        public bool Value { get; private set; }
        public BoolEventArgs(bool value)
        {
            this.Value = value;
        }
    }
}

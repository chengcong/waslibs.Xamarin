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
    public  class ExceptionEventArgs : EventArgs
    {
        public Exception Exception{ get; private set; }
        public ExceptionEventArgs(Exception ex)
        {
            this.Exception = ex;
        }
    }
}

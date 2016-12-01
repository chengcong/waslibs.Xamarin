using System;
using System.Diagnostics.CodeAnalysis;
#if! UWP
using MvvmHelpers;
#endif
using Newtonsoft.Json;

namespace AppStudio.DataProviders
{
#if UWP
    public abstract class SchemaBase
#else
    public abstract class SchemaBase : ObservableObject
#endif
    {
        [CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1709", Justification = "This property is reserved.")]
        [SuppressMessage("Microsoft.Naming", "CA1707", Justification = "This property is reserved.")]
        public string _id { get; set; }
#if !UWP
        public bool Equals(SchemaBase other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;

            return this._id == other._id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SchemaBase);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this._id))
            {
                return 0;
            }
            return this._id.GetHashCode();
        }
#endif
    }
}

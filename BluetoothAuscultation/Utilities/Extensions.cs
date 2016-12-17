using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace BluetoothAuscultation.Utilities
{
    

    public static class Extensions
    {
        public static T GuardNotNull<T>(this T item, [Optional, DefaultParameterValue(null)] string name, [Optional, DefaultParameterValue(null)] string message)
        {
            if (object.ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(name ?? string.Empty, message ?? string.Empty);
            }
            return item;
        }

        public static object IfNull(this object item, object result)
        {
            if (!item.IsNull())
            {
                return item;
            }
            return result;
        }

        public static T IfNull<T>(this T item, T result)
        {
            if (!item.IsNull())
            {
                return item;
            }
            return result;
        }

        public static bool IsNotNull(this object item)
        {
            return !object.ReferenceEquals(item, null);
        }

        public static bool IsNull(this object item)
        {
            return object.ReferenceEquals(item, null);
        }
    }
}


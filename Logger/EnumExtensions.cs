using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Molytho.Logger
{
    public static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetName<T>(this T enumObject) where T : System.Enum
        {
            //Null return value is not an issue when the caller doesn't mess with casts
            return Enum.GetName(typeof(T), enumObject)!;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMaxNameLength<T>() where T : System.Enum
        {
            return Enum.GetNames(typeof(T)).Max(t => t.Length);
        }
    }
}

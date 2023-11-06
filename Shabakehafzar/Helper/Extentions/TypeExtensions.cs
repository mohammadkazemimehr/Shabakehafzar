﻿using System.ComponentModel;

namespace Shabakehafzar.API.Helper.Extentions
{
    public static class TypeExtensions
    {
        public static T ToType<T>(this object src)
        {
            if (src is T value)
                return value;

            try
            {
                try
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(src);
                }
                catch
                {
                    return (T)Convert.ChangeType(src, typeof(T));
                }
            }
            catch
            {
                return default;
            }
        }

        public static int? ToNullableInt(this string src)
        {
            if (!int.TryParse(src, out var val))
                return null;

            return val;
        }
    }
}

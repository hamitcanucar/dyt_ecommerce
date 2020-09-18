using System.Collections.Generic;
using System;

namespace dyt_ecommerce.Util
{
    public static class StringSplitExtensions
    {
        private const char SEPERATOR = ';';

        delegate bool StringConverterDelegate<T>(string str, out T data);

        public static IEnumerable<Guid> SplitToGuid(this string data)
        {
            return Split(data, (string str, out Guid x) => Guid.TryParse(str, out x));
        }

        public static IEnumerable<int> SplitToInt(this string data)
        {
            return Split(data, (string str, out int x) => int.TryParse(str, out x));
        }

        public static IEnumerable<TEnum> SplitToEnum<TEnum>(this string data) where TEnum : struct
        {
            return Split(data, (string str, out TEnum x) => Enum.TryParse(str, true, out x));
        }

        private static IEnumerable<T> Split<T>(string data, StringConverterDelegate<T> converter)
        {
            if (string.IsNullOrWhiteSpace(data)) yield break;
            var temp = data.Split(SEPERATOR);

            foreach (var item in temp)
            {
                if (converter(item, out var result)) yield return result;
            }
        }
    }
}
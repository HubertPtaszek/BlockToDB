using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BlockToDB.Utils
{
    public static class AnyTypeConverter
    {
        public static T Get<T>(string val, string delimiter = ";")
        {
            if (string.IsNullOrEmpty(val))
            {
                return default(T);
            }

            string source = val;

            if (typeof(T) != typeof(string))
            {
                source = source
                    .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                    .First();
            }

            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(source);
            }
            catch
            {
                return default(T);
            }
        }

        public static IEnumerable<T> GetMultiple<T>(string source, string delimiter = ";")
        {
            return (source ?? string.Empty)
                .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => Get<T>(part, delimiter))
                .ToList();
        }
    }
}
